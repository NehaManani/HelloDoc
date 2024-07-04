using HelloDoc_DataAccessLayer.Data;
using HelloDoc_DataAccessLayer.IRepositories;

namespace HelloDoc_DataAccessLayer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private IUserRepository _userRepository;
        private IPatientDetailsRepository _patientDetailsRepository;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IBaseRepository<T> GetRepository<T>() where T : class
        {
            return new BaseRepository<T>(_dbContext);
        }

        public int Save() => _dbContext.SaveChanges();

        public Task<int> SaveAsync() => _dbContext.SaveChangesAsync();

        public IUserRepository UserRepository
        {
            get
            {
                return _userRepository ??= new UserRepository(_dbContext);
            }
        }

        public IPatientDetailsRepository PatientDetailsRepository
        {
            get
            {
                return _patientDetailsRepository ??= new PatientDetailsRepository(_dbContext);
            }
        }
    }
}