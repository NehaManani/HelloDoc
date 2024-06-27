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

        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            User? user = await _unitOfWork.UserRepository.GetFirstOrDefaultAsync(u => u.Email == loginRequest.Email);

            if (user == null || !PasswordUtil.VerifyPassword(loginRequest.Password, user.Password))
                return ResponseHelper.CreatedResponse(false, ErrorMessage.VALID_CREDENTIALS, (object?)null);

            string? token = _jwtTokenHelper.GenerateJwtToken(user) ?? throw new Exception(ErrorMessage.INVALID_ATTEMPT);

            return ResponseHelper.CreatedResponse(true, "Login successful", token);
        }


        #region Helper Methods

        #endregion
    }
}