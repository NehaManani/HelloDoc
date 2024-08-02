using HelloDoc_Entities.DTOs.Common;
using HelloDoc_Entities.DTOs.Request;

namespace HelloDoc_BusinessAccessLayer.IServices
{
    public interface IAdminService
    {
        Task<PageListResponseDTO<UserRequest>> GetPatientProviderRequestList(PageListRequestDTO admitRequestList, int userType);
        Task<StatusCountResponse> StatusCountRequest(int userType);
        Task<RegisterPatientRequest> GetPatientDetails(int userId);
        Task BlockUserRequest(BlockCaseRequest blockCaseRequest);
    }
}