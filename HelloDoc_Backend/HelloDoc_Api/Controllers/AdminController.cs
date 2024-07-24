using HelloDoc_BusinessAccessLayer.IServices;
using HelloDoc_Common.Exceptions;
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
            if (!ModelState.IsValid) throw new ModelStateException(ModelState);

            PageListResponseDTO<UserRequest>? response = await _adminService.GetPatientRequestList(pageListRequest);

            return ResponseHelper.CreatedResponse(response, null, success: true);
        }

        [HttpGet]
        [Route("status-count-list")]
        public async Task<IActionResult> StatusCountList()
        {
            StatusCountResponse? statusCountResponse = await _adminService.StatusCountRequest();

            return ResponseHelper.CreatedResponse(statusCountResponse, null, true);
        }

        [HttpGet]
        [Route("get-patient-details")]
        public async Task<IActionResult> GetPatientDetailsById(int userId)
        {
            RegisterPatientRequest? registerPatientRequest = await _adminService.GetPatientDetails(userId);

            return ResponseHelper.CreatedResponse(registerPatientRequest, null, true);
        }
    }
}