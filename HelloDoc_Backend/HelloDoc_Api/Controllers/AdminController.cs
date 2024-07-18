using HelloDoc_BusinessAccessLayer.IServices;
using HelloDoc_DataAccessLayer.Helpers;
using HelloDoc_Entities.DTOs.Common;
using HelloDoc_Entities.DTOs.Request;
using Microsoft.AspNetCore.Mvc;

namespace HelloDoc_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {

        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("patient-request-list")]
        public async Task<IActionResult> GetPatientRequestList(PageListRequestDTO pageListRequest)
        {
            PageListResponseDTO<UserRequest>? response = await _adminService.GetPatientRequestList(pageListRequest);
            return ResponseHelper.CreatedResponse(response, null, success: true);
        }
    }
}