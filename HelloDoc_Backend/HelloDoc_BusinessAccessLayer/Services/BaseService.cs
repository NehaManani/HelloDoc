using System.Linq.Expressions;
using HelloDoc_BusinessAccessLayer.IServices;
using HelloDoc_DataAccessLayer.IRepositories;
using HelloDoc_Entities.DTOs.Common;

namespace HelloDoc_BusinessAccessLayer.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly IBaseRepository<T> _repository;
        public BaseService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual async Task AddAsync(T entity) => await _repository.AddAsync(entity);

        public virtual async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter) => await _repository.GetFirstOrDefaultAsync(filter);

        public virtual async Task UpdateAsync(T entity) => await _repository.UpdateAsync(entity);

        public virtual async Task AddRangeAsync(IEnumerable<T> models) => await _repository.AddRangeAsync(models);

        public void UpdateRange(IEnumerable<T> models)
            => _repository.UpdateRange(models);

        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> filter) => await _repository.AnyAsync(filter);

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, Expression<Func<T, T>> select) => await _repository.GetAllAsync(filter, select);

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter) => await _repository.GetAllAsync(filter);

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, IEnumerable<Expression<Func<T, object>>> includes) => await _repository.GetAllAsync(filter, includes);

        public async Task<PageListResponseDTO<T>> GetAllAsync(PageListRequestEntity<T> pageListRequest)
        => await _repository.GetAllAsync(pageListRequest);
    }
}