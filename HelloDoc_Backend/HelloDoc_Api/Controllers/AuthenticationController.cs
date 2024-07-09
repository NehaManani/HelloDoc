using HelloDoc_BusinessAccessLayer.IServices;
using HelloDoc_Common.Exceptions;
using HelloDoc_DataAccessLayer.Helpers;
using HelloDoc_Entities.DTOs.Request;
using HelloDoc_Entities.DTOs.Response;
using Microsoft.AspNetCore.Mvc;
using static HelloDoc_Common.Constants.MessageConstants;

namespace HelloDoc_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
                throw new ModelStateException(ModelState);

            string token = await _authenticationService.Login(loginRequest);

            return ResponseHelper.CreatedResponse(token, SuccessMessage.OTP_SENT, true);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] EmailRequest emailRequest)
        {
            await _authenticationService.ForgotPassword(emailRequest.Email);

            return ResponseHelper.CreatedResponse(string.Empty, SuccessMessage.FORGET_PASSWORD_MAIL_SENT, true);
        }

        [HttpPost("resend-otp")]
        public async Task<IActionResult> ResendOTP([FromBody] EmailRequest emailRequest)
        {
            if (!ModelState.IsValid)
                throw new ModelStateException(ModelState);

            await _authenticationService.SendOtp(emailRequest.Email);

            return ResponseHelper.CreatedResponse(string.Empty, SuccessMessage.OTP_SENT, true);
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOTP(VerifyOtpResponse verifyOtpResponse)
        {
            if (!ModelState.IsValid)
                throw new ModelStateException(ModelState);

            string? response = await _authenticationService.VerifyOtp(verifyOtpResponse);
            return ResponseHelper.CreatedResponse(response, SuccessMessage.Login_SUCCESS, true);
        }

        [HttpPost]
        [Route("register-patient-request")]
        public async Task<IActionResult> RegisterPatientRequest([FromForm] RegisterPatientRequest registerPatientRequest)
        {
            if (!ModelState.IsValid)
                throw new ModelStateException(ModelState);

            await _authenticationService.RegisterPatientRequest(registerPatientRequest);

            return ResponseHelper.CreatedResponse(string.Empty, SuccessMessage.REGISTER_REQUEST_SENT, true);
        }

        [HttpPost]
        [Route("register-provider-request")]
        public async Task<IActionResult> RegisterProviderRequest([FromForm] RegisterProviderRequest registerProviderRequest)
        {
            if (!ModelState.IsValid)
                throw new ModelStateException(ModelState);

            await _authenticationService.RegisterProviderRequest(registerProviderRequest);

            return ResponseHelper.CreatedResponse(string.Empty, SuccessMessage.REGISTER_REQUEST_SENT, true);
        }
    }
}