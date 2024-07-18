using System.Linq.Expressions;
using HelloDoc_DataAccessLayer.Configuration;
using HelloDoc_Entities.DTOs.Common;

namespace HelloDoc_DataAccessLayer.IRepositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task AddAsync(T entity, CancellationToken cancellationToken = default);

        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);

        IQueryable<T> GetAll();

        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

        Task AddRangeAsync(IEnumerable<T> models);

        void UpdateRange(IEnumerable<T> models);

        Task<(int count, IEnumerable<T> data)> GetPaginationWithExpressions(FilterCriteria<T> criteria);

        Task<bool> AnyAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> GetAllAsyncSelect(Expression<Func<T, bool>> filter, Expression<Func<T, T>> select, CancellationToken cancellationToken = default);
        Task<int> CountAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, IEnumerable<Expression<Func<T, object>>> includes);
        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> expression);

        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, IEnumerable<Expression<Func<T, object>>> includes);

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, Expression<Func<T, T>> select);

        Task<T> GetIncludeAsync(Expression<Func<T, object>> include, Expression<Func<T, bool>> filter);

        void DeleteRange(IEnumerable<T> moduleAccessPermissions);

        Task<int> CountAsync(Expression<Func<T, bool>>? filter = null);

        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, IEnumerable<Expression<Func<T, object>>> includes, string[]? thenIncludeExpressions);

        Task<PageListResponseDTO<T>> GetAllAsync(PageListRequestEntity<T> pageListRequest);
    }
}
