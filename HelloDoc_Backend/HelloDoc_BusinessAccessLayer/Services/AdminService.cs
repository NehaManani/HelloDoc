using System.Linq.Expressions;
using HelloDoc_BusinessAccessLayer.IServices;
using HelloDoc_Common.Constants;
using HelloDoc_DataAccessLayer.IRepositories;
using HelloDoc_Entities.DataModels;
using HelloDoc_Entities.DTOs.Common;
using HelloDoc_Entities.DTOs.Request;
using HelloDoc_Entities.ExtensionMethods;

namespace HelloDoc_BusinessAccessLayer.Services
{
    public class AdminService : IAdminService
    {
        public readonly IUnitOfWork _unitOfWork;

        public AdminService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PageListResponseDTO<UserRequest>> GetPatientRequestList(PageListRequestDTO patientRequest)
        {
            PageListRequestEntity<User> pageListRequestEntity = new()
            {
                PageIndex = patientRequest.PageIndex,
                PageSize = patientRequest.PageSize,
                SortColumn = !string.IsNullOrEmpty(patientRequest.SortColumn) ? patientRequest.SortColumn : null!,
                SortOrder = patientRequest.SortOrder,
                IncludeExpressions = [x => x.UserRoles, x => x.Genders],
                Predicate = user =>
                        user.Role == 2 &&
                        (string.IsNullOrEmpty(patientRequest.SearchQuery) ||
                        user.FirstName.Trim().ToLower().Contains(patientRequest.SearchQuery.Trim().ToLower()) ||
                        user.LastName.Trim().ToLower().Contains(patientRequest.SearchQuery.Trim().ToLower()) ||
                        user.Email.Trim().ToLower().Contains(patientRequest.SearchQuery.Trim().ToLower())) &&
                        (string.IsNullOrEmpty(patientRequest.Status) || user.UserStatuses.Status == patientRequest.Status)
            };

            PageListResponseDTO<User> pageListResponse = await _unitOfWork.UserRepository.GetAllAsync(pageListRequestEntity);

            List<UserRequest> patientRequestListResponseDTOs = UserMappingProfile.ToPatientRequestListResponseDTO(pageListResponse.Records);

            return new PageListResponseDTO<UserRequest>(pageListResponse.PageIndex, pageListResponse.PageSize, pageListResponse.TotalRecords, patientRequestListResponseDTOs);
        }

        public async Task<StatusCountResponse> StatusCountRequest()
        {
            IEnumerable<User> users = await _unitOfWork.UserRepository.GetAllAsync(
                user => user.Role == UserRoleConstants.Patient,
                new List<Expression<Func<User, object>>>
                {
                    user => user.UserStatuses
                });

            StatusCountResponse statusCountResponse = UserMappingProfile.ToStatusCountResponse(users);

            return statusCountResponse;
        }
    }
}