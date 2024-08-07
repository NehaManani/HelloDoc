using System.Linq.Expressions;
using HelloDoc_Entities.DTOs.Common;

namespace HelloDoc_BusinessAccessLayer.IServices
{
    public interface IBaseService<T> where T : class
    {
        Task AddAsync(T entity);

        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter);

        Task UpdateAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> models);

        void UpdateRange(IEnumerable<T> models);

        Task<bool> AnyAsync(Expression<Func<T, bool>> filter);

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, Expression<Func<T, T>> select);

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter);

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, IEnumerable<Expression<Func<T, object>>> includes);

        Task<PageListResponseDTO<T>> GetAllAsync(PageListRequestEntity<T> pageListRequest);
    }
}