using HelloDoc_Entities.DataModels;

namespace HelloDoc_DataAccessLayer.IRepositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<bool> IsDuplicateEmail(string email, long? userId);
    }
}