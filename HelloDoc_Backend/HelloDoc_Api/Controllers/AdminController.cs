using HelloDoc_Api.Helpers;
using HelloDoc_BusinessAccessLayer.IServices;
using HelloDoc_Common.Exceptions;
using HelloDoc_DataAccessLayer.Helpers;
using HelloDoc_Entities.DTOs.Common;
using HelloDoc_Entities.DTOs.Request;
using Microsoft.AspNetCore.Mvc;
using static HelloDoc_Common.Constants.MessageConstants;

namespace HelloDoc_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AdminPolicy]
    public class AdminController : ControllerBase
    {

        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("patient-provider-list")]
        public async Task<IActionResult> GetPatientProviderRequestList(PageListRequestDTO pageListRequest, int userType)
        {
            if (!ModelState.IsValid) throw new ModelStateException(ModelState);

            PageListResponseDTO<UserRequest>? response = await _adminService.GetPatientProviderRequestList(pageListRequest, userType);

            return ResponseHelper.CreatedResponse(response, null, success: true);
        }

        [HttpGet]
        [Route("status-count-list")]
        public async Task<IActionResult> StatusCountList(int userType)
        {
            StatusCountResponse? statusCountResponse = await _adminService.StatusCountRequest(userType);

            return ResponseHelper.CreatedResponse(statusCountResponse, null, true);
        }

        [HttpGet]
        [Route("get-patient-details")]
        public async Task<IActionResult> GetPatientDetailsById(int userId)
        {
            RegisterPatientRequest? registerPatientRequest = await _adminService.GetPatientDetails(userId);

            return ResponseHelper.CreatedResponse(registerPatientRequest, null, true);
        }

        [HttpPost]
        [Route("block-user-case")]
        public async Task<IActionResult> BlockUserCase(BlockCaseRequest blockCaseRequest)
        {
            await _adminService.BlockUserRequest(blockCaseRequest);

            return ResponseHelper.CreatedResponse(string.Empty, SuccessMessage.BLOCK_USER, true);
        }
    }
}