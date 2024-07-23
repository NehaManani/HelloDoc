using HelloDoc_Entities.DTOs.Common;
using HelloDoc_Entities.DTOs.Request;

namespace HelloDoc_BusinessAccessLayer.IServices
{
    public interface IAdminService
    {
        Task<PageListResponseDTO<UserRequest>> GetPatientRequestList(PageListRequestDTO admitRequestList);
        Task<StatusCountResponse> StatusCountRequest();
    }
}