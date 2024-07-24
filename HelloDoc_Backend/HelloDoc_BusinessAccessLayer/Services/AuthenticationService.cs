using System.Net;
using AutoMapper;
using HelloDoc_Common.Utils;
using Microsoft.AspNetCore.Http;
using HelloDoc_Common.Constants;
using HelloDoc_Common.Exceptions;
using Microsoft.AspNetCore.Hosting;
using HelloDoc_Entities.DataModels;
using HelloDoc_Entities.DTOs.Common;
using HelloDoc_Entities.DTOs.Request;
using HelloDoc_BusinessAccessLayer.Helpers;
using HelloDoc_BusinessAccessLayer.IServices;
using HelloDoc_DataAccessLayer.IRepositories;
using static HelloDoc_Common.Constants.MessageConstants;
using HelloDoc_Entities.ExtensionMethods;

namespace HelloDoc_BusinessAccessLayer.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMailService _mailService;
        public readonly IUnitOfWork _unitOfWork;
        private readonly IHostingEnvironment _environment;
        private readonly JwtTokenHelper _jwtTokenHelper;
        private readonly IMapper _mapper;

        public AuthenticationService(IMailService mailService, IUnitOfWork unitOfWork, IHostingEnvironment environment, JwtTokenHelper jwtTokenHelper, IMapper mapper)
        {
            _mailService = mailService;
            _unitOfWork = unitOfWork;
            _environment = environment;
            _jwtTokenHelper = jwtTokenHelper;
            _mapper = mapper;
        }

        public async Task<string> Login(LoginRequest loginRequest)
        {
            User? user = await _unitOfWork.UserRepository.GetFirstOrDefaultAsync(u => u.Email == loginRequest.Email);

            if (user == null || !PasswordUtil.VerifyPassword(loginRequest.Password, user.Password))
                throw new CustomException((int)HttpStatusCode.NotFound, ErrorMessage.VALID_CREDENTIALS);

            await SendOtp(user.Email);

            return user.FirstName + " " + user.LastName;
        }

        public async Task ForgotPassword(string email)
        {
            await SendOtp(email);
        }

        public async Task SendOtp(string email)
        {
            User? user = await GetUserByEmailAsync(email) ?? throw new CustomException(StatusCodes.Status404NotFound, ErrorMessage.USER_NOT_FOUND);

            if (user != null)
            {
                //generate a random number of six digit OTP
                await GenerateOtp(user);

                //sent otp in mail
                MailDTO mailDto = new()
                {
                    ToEmail = user.Email,
                    Subject = EmailConstants.OTP_SUBJECT,
                    Body = MailBodyUtil.SendOtpForAuthenticationBody(user.OTP, _environment.WebRootPath)
                };
                await _mailService.SendMailAsync(mailDto);
            }
        }

        public async Task<string> VerifyOtp(VerifyOtpRequest verifyOtpRequest)
        {
            User user = await GetUserByEmailAsync(verifyOtpRequest.Email) ?? throw new CustomException(StatusCodes.Status404NotFound, ErrorMessage.USER_NOT_FOUND);

            if (user.OTP != verifyOtpRequest.Otp) throw new CustomException(StatusCodes.Status400BadRequest, ErrorMessage.INVALID_OTP);

            else if (user.OtpExpiryTime < DateTime.UtcNow) throw new CustomException(StatusCodes.Status400BadRequest, ErrorMessage.OTP_EXPIRED);

            UserMappingProfile.ToVerifyOtp(user);

            string? token = _jwtTokenHelper.GenerateJwtToken(user) ?? throw new Exception(ErrorMessage.INVALID_ATTEMPT);

            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.SaveAsync();

            return token;
        }

        public async Task ResetPassword(ResetPasswordRequest resetPasswordRequest)
        {
            User? user = await GetUserByEmailAsync(resetPasswordRequest.Email) ?? throw new CustomException(StatusCodes.Status404NotFound, ErrorMessage.USER_NOT_FOUND);

            if (user != null)
            {
                string? hashedPassword = PasswordUtil.HashPassword(resetPasswordRequest.Password);
                UserMappingProfile.ToSetPassword(user, hashedPassword);
                await _unitOfWork.UserRepository.UpdateAsync(user);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task RegisterPatientRequest(RegisterPatientRequest registerPatientRequest)
        {
            User? user = UserMappingProfile.ToRegisterPatientUserRequest(registerPatientRequest);
            if (await _unitOfWork.UserRepository.AnyAsync(u => u.Email == user.Email))
                throw new CustomException(StatusCodes.Status409Conflict, ErrorMessage.EMAIL_ALREADY_EXIST);

            if (user != null)
            {
                string? hashedPassword = PasswordUtil.HashPassword(registerPatientRequest.Password);
                UserMappingProfile.ToSetPatientStatusAndRole(user, hashedPassword);
                await _unitOfWork.UserRepository.AddAsync(user);
                await _unitOfWork.SaveAsync();
            }

            PatientDetails patientDetails = PatientDetailsMappingProfile.ToRegisterPatientDetailsRequest(registerPatientRequest);

            PatientDetailsMappingProfile.ToSetUserId(patientDetails, user);

            string ConfirmationNumber = await GenerateConfirmationNumber(user);

            PatientDetailsMappingProfile.ToConfirmationNumber(patientDetails, ConfirmationNumber);

            await _unitOfWork.PatientDetailsRepository.AddAsync(patientDetails);
            await _unitOfWork.SaveAsync();
        }

        public async Task RegisterProviderRequest(RegisterProviderRequest registerProviderRequest)
        {
            User? user = UserMappingProfile.ToRegisterProviderUserRequest(registerProviderRequest);

            if (await _unitOfWork.UserRepository.AnyAsync(u => u.Email == user.Email))
                throw new CustomException(StatusCodes.Status409Conflict, ErrorMessage.EMAIL_ALREADY_EXIST);

            if (user != null)
            {
                string? hashedPassword = PasswordUtil.HashPassword(registerProviderRequest.Password);
                UserMappingProfile.ToSetProviderStatusAndRole(user, hashedPassword);
                await _unitOfWork.UserRepository.AddAsync(user);
                await _unitOfWork.SaveAsync();
            }

            ProviderDetails providerDetails = ProviderDetailsMappingProfile.ToRegisterProviderDetailsRequest(registerProviderRequest);

            ProviderDetailsMappingProfile.ToSetUserId(providerDetails, user);

            string ConfirmationNumber = await GenerateConfirmationNumber(user);

            ProviderDetailsMappingProfile.ToConfirmationNumber(providerDetails, ConfirmationNumber);

            await _unitOfWork.ProviderDetailsRepository.AddAsync(providerDetails);
            await _unitOfWork.SaveAsync();
        }

        #region Helper Methods

        private async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _unitOfWork.UserRepository.GetFirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<string> GenerateOtp(User user)
        {
            Random generator = new();
            string otp = generator.Next(100000, 999999).ToString();
            DateTime otpExpiryTime = DateTime.UtcNow.AddMinutes(10);

            UserMappingProfile.ToGenerateOtp(user, otp, otpExpiryTime);
            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.SaveAsync();

            return otp;
        }


        private async Task<string> GenerateConfirmationNumber(User user)
        {
            string firstNamePart = user.FirstName.Substring(0, 2).ToUpper();
            string lastNamePart = user.LastName.Substring(0, 2).ToUpper();
            string datePart = DateTime.UtcNow.ToString("MMdd");

            string uniqueId = Guid.NewGuid().ToString("N").Substring(0, 4).ToUpper();

            return $"{firstNamePart}{lastNamePart}{datePart}{uniqueId}";
        }


        #endregion
    }
}