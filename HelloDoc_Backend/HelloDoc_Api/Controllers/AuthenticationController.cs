using HelloDoc_BusinessAccessLayer.IServices;
using HelloDoc_Common.Exceptions;
using HelloDoc_DataAccessLayer.Helpers;
using HelloDoc_Entities.DTOs.Request;
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

            return ResponseHelper.CreatedResponse(token, SuccessMessage.Login_SUCCESS, true);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            await _authenticationService.ForgotPassword(request.Email);

            return ResponseHelper.CreatedResponse(string.Empty, SuccessMessage.FORGET_PASSWORD_MAIL_SENT, true);
        }

        [HttpPost]
        [Route("submit-register-patient-request")]
        public async Task<IActionResult> SubmitRegisterPatientRequest([FromForm] SubmitRegisterPatientRequest submitRegisterPatientRequest)
        {
            if (!ModelState.IsValid)
                throw new ModelStateException(ModelState);

            await _authenticationService.SubmitRegisterPatientRequest(submitRegisterPatientRequest);

            return ResponseHelper.CreatedResponse(string.Empty, SuccessMessage.REGISTER_REQUEST_SENT, true);
        }
    }
}