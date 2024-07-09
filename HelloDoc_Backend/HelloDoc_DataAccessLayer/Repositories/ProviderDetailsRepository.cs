using HelloDoc_DataAccessLayer.Data;
using HelloDoc_DataAccessLayer.IRepositories;
using HelloDoc_Entities.DataModels;

namespace HelloDoc_DataAccessLayer.Repositories
{
    public class ProviderDetailsRepository : BaseRepository<ProviderDetails>, IProviderDetailsRepository
    {
        public new readonly AppDbContext _dbContext;
        public ProviderDetailsRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}