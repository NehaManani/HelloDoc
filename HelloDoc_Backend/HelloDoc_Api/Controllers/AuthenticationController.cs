using Microsoft.AspNetCore.Mvc;

namespace HelloDoc_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        [HttpGet("test")]
        public async Task<IActionResult> TestApi()
        {
            return Ok();
        }
    }
}