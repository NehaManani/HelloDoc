using System.Linq.Expressions;
using HelloDoc_BusinessAccessLayer.IServices;
using HelloDoc_DataAccessLayer.IRepositories;
using HelloDoc_Entities.DataModels;
using HelloDoc_Entities.DTOs.Common;
using HelloDoc_Entities.DTOs.Request;
using Org.BouncyCastle.Math.EC.Rfc7748;

namespace HelloDoc_BusinessAccessLayer.Services
{
    public class AdminService : IAdminService
    {
        public readonly IUnitOfWork _unitOfWork;

        public AdminService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PageListResponseDTO<UserRequest>> GetPatientRequestList(PageListRequestDTO admitRequestList)
        {
            PageListRequestEntity<User> pageListRequestEntity = new()
            {
                PageIndex = admitRequestList.PageIndex,
                PageSize = admitRequestList.PageSize,
                SortColumn = !string.IsNullOrEmpty(admitRequestList.SortColumn) ? admitRequestList.SortColumn : null!,
                SortOrder = admitRequestList.SortOrder,
                IncludeExpressions = [x => x.UserRoles, x => x.Genders],
                Predicate = user =>
                user.Role == 2 &&
                (string.IsNullOrEmpty(admitRequestList.SearchQuery) ||
                user.FirstName.Trim().ToLower().Contains(admitRequestList.SearchQuery.Trim().ToLower()) ||
                user.LastName.Trim().ToLower().Contains(admitRequestList.SearchQuery.Trim().ToLower()) ||
                user.Email.Trim().ToLower().Contains(admitRequestList.SearchQuery.Trim().ToLower()))
            };

            PageListResponseDTO<User> pageListResponse = await _unitOfWork.UserRepository.GetAllAsync(pageListRequestEntity);

            List<UserRequest> admitRequestListResponseDTOs = pageListResponse.Records.Select(admitRequest => new UserRequest
            {
                FirstName = admitRequest.FirstName,
                LastName = admitRequest.LastName,
                Email = admitRequest.Email,
                PhoneNumber = admitRequest.PhoneNumber,
                Gender = (int)admitRequest.Gender,
                Role = admitRequest.UserRoles.Role,
                City = admitRequest.City,
                Zip = admitRequest.Zip,
                Address = admitRequest.Address
            }).ToList();

            return new PageListResponseDTO<UserRequest>(pageListResponse.PageIndex, pageListResponse.PageSize, pageListResponse.TotalRecords, admitRequestListResponseDTOs);
        }
    }
}