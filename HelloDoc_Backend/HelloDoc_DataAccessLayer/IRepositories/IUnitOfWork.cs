using HelloDoc_DataAccessLayer.IRepositories;

namespace HelloDoc_DataAccessLayer.IRepositories
{
    public interface IUnitOfWork
    {
        IBaseRepository<T> GetRepository<T>() where T : class;

        int Save();

        Task<int> SaveAsync();

        public IUserRepository UserRepository { get; }

        public IPatientDetailsRepository PatientDetailsRepository { get; }

        public IProviderDetailsRepository ProviderDetailsRepository { get; }
    }
}