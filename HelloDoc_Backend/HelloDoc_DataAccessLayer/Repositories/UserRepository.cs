using System.Linq.Expressions;
using HelloDoc_DataAccessLayer.Data;
using HelloDoc_DataAccessLayer.IRepositories;
using HelloDoc_Entities.DataModels;

namespace HelloDoc_DataAccessLayer.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public new readonly AppDbContext _dbContext;
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> IsDuplicateEmail(string email, long? userId)
        => userId is null ? await AnyAsync(EmailFilter(email)) : await AnyAsync(EmailFilter(email, userId));


        #region helper method

        private static Expression<Func<User, bool>> EmailFilter(string email,
        long? userId = null)
        => userId is null ? user => user.Email == email
                            : user => user.Email == email && user.Id != userId;

        #endregion
    }
}