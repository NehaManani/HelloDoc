using System.Linq.Expressions;
using HelloDoc_BusinessAccessLayer.IServices;
using HelloDoc_Common.Constants;
using HelloDoc_Common.Exceptions;
using HelloDoc_DataAccessLayer.IRepositories;
using HelloDoc_Entities.DataModels;
using HelloDoc_Entities.DTOs.Common;
using HelloDoc_Entities.DTOs.Request;
using HelloDoc_Entities.ExtensionMethods;
using Microsoft.AspNetCore.Http;
using static HelloDoc_Common.Constants.MessageConstants;

namespace HelloDoc_BusinessAccessLayer.Services
{
    public class AdminService : IAdminService
    {
        public readonly IUnitOfWork _unitOfWork;

        public AdminService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PageListResponseDTO<UserRequest>> GetPatientProviderRequestList(PageListRequestDTO patientRequest, int userType)
        {
            PageListRequestEntity<User> pageListRequestEntity = new()
            {
                PageIndex = patientRequest.PageIndex,
                PageSize = patientRequest.PageSize,
                SortColumn = !string.IsNullOrEmpty(patientRequest.SortColumn) ? patientRequest.SortColumn : null!,
                SortOrder = patientRequest.SortOrder,
                IncludeExpressions = [x => x.UserRoles, x => x.Genders],
                Predicate = user =>
                        user.Role == userType &&
                        user.Status != UserStatusConstants.Block &&
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

        public async Task<StatusCountResponse> StatusCountRequest(int userType)
        {
            IEnumerable<User> users = await _unitOfWork.UserRepository.GetAllAsync(
                user => user.Role == userType,
                new List<Expression<Func<User, object>>>
                {
                    user => user.UserStatuses
                });

            StatusCountResponse statusCountResponse = UserMappingProfile.ToStatusCountResponse(users);

            return statusCountResponse;
        }

        public async Task<RegisterPatientRequest> GetPatientDetails(int userId)
        {
            User? user = await _unitOfWork.UserRepository.GetFirstOrDefaultAsync(u => u.Id == userId,
                new List<Expression<Func<User, object>>> { u => u.UserRoles, u => u.UserStatuses, u => u.Genders });

            if (user == null)
                throw new CustomException(StatusCodes.Status404NotFound, ErrorMessage.USER_NOT_FOUND);

            PatientDetails? patientDetails = await _unitOfWork.PatientDetailsRepository.GetFirstOrDefaultAsync(
                    pd => pd.UserId == userId);

            RegisterPatientRequest? registerPatientRequest = PatientDetailsMappingProfile.ToGetPatientDetails(user, patientDetails);

            return registerPatientRequest;
        }

        public async Task BlockUserRequest(BlockCaseRequest blockCaseRequest)
        {
            User? user = await _unitOfWork.UserRepository.GetFirstOrDefaultAsync(u => u.Id == blockCaseRequest.UserId);

            if (user == null)
                throw new CustomException(StatusCodes.Status404NotFound, ErrorMessage.USER_NOT_FOUND);

            UserMappingProfile.ToSetReasonForBlock(blockCaseRequest, user);

            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.SaveAsync();
        }
    }
}