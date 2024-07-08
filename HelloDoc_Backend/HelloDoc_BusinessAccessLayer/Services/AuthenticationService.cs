using Microsoft.AspNetCore.Hosting;
using AutoMapper;
using HelloDoc_BusinessAccessLayer.IServices;
using HelloDoc_DataAccessLayer.IRepositories;
using HelloDoc_BusinessAccessLayer.Helpers;
using HelloDoc_Entities.DataModels;
using HelloDoc_Common.Utils;
using HelloDoc_Entities.DTOs.Request;
using HelloDoc_DataAccessLayer.Helpers;
using static HelloDoc_Common.Constants.MessageConstants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Features;
using HelloDoc_Common.Exceptions;
using System.Net;
using HelloDoc_Entities.DTOs.Common;
using HelloDoc_Common.Constants;
using Microsoft.AspNetCore.Http;
using HelloDoc_Entities.DTOs.Response;

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

            string? token = _jwtTokenHelper.GenerateJwtToken(user) ?? throw new Exception(ErrorMessage.INVALID_ATTEMPT); ;
            return token;
        }

        public async Task ForgotPassword(string email)
        {
            await SendOtp(email);
        }

        public async Task SendOtp(string email)
        {
            User? user = await GetUserByEmailAsync(email);
            if (user != null)
            {
                //generate a random number of six digit OTP
                Random generator = new Random();
                user.OTP = generator.Next(100000, 999999).ToString();
                user.OtpExpiryTime = DateTime.UtcNow.AddMinutes(10); // Ensure the time is in UTC
                await _unitOfWork.UserRepository.UpdateAsync(user);
                await _unitOfWork.SaveAsync();

                //sent otp in mail
                MailDTO mailDto = new()
                {
                    ToEmail = user.Email,
                    Subject = EmailConstants.OTP_SUBJECT,
                    Body = MailBodyUtil.SendOtpForAuthenticationBody(user.OTP, _environment.WebRootPath)
                };
                await _mailService.SendMailAsync(mailDto);
            }
            else
            {
                throw new CustomException(StatusCodes.Status404NotFound, ErrorMessage.USER_NOT_FOUND);
            }
        }

        public async Task SubmitRegisterPatientRequest(SubmitRegisterPatientRequest submitRegisterPatientRequest)
        {
            User? user = submitRegisterPatientRequest.ReturnPatientRequestUser(submitRegisterPatientRequest);

            if (user != null)
            {
                string? hashedPassword = PasswordUtil.HashPassword(submitRegisterPatientRequest.Password);
                user.Password = hashedPassword;
                user.Status = 1;
                user.Role = 2;
            }

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveAsync();

            PatientDetails patientDetails = submitRegisterPatientRequest.ReturnPatientDetailsRequest(submitRegisterPatientRequest);

            if (submitRegisterPatientRequest.Document != null)
            {
                patientDetails.Document = submitRegisterPatientRequest.Document;
            }

            patientDetails.UserId = user.Id;
            await _unitOfWork.PatientDetailsRepository.AddAsync(patientDetails);
            await _unitOfWork.SaveAsync();
        }

        public async Task<string> VerifyOtp(VerifyOtpResponse otpData)
        {
            User user = await GetUserByEmailAsync(otpData.Email) ?? throw new CustomException(StatusCodes.Status404NotFound, ErrorMessage.USER_NOT_FOUND);

            if (user.OTP != otpData.Otp) throw new CustomException(StatusCodes.Status400BadRequest, ErrorMessage.INVALID_OTP);

            else if (user.OtpExpiryTime < DateTime.Now) throw new CustomException(StatusCodes.Status400BadRequest, ErrorMessage.OTP_EXPIRED);

            user.OTP = null;
            user.OtpExpiryTime = null;

            string? token = EncodingMailToken(otpData.Email);

            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.SaveAsync();

            return token;
        }

        #region Helper Methods

        private async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _unitOfWork.UserRepository.GetFirstOrDefaultAsync(user => user.Email == email);
        }

        public static string EncodingMailToken(string email) => System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(email + "&" + DateTime.UtcNow.AddMinutes(SystemConstants.TOKEN_EXPIRE_MINUTES)));

        public static string DecodingMailToken(string token) => System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(token));

        #endregion
    }
}