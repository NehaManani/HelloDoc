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

            IActionResult? result = await _authenticationService.Login(loginRequest);

            return result;
        }
    }
}