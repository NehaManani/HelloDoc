using System.Linq.Expressions;
using HelloDoc_DataAccessLayer.Configuration;
using HelloDoc_DataAccessLayer.Data;
using HelloDoc_DataAccessLayer.IRepositories;
using HelloDoc_DataAccessLayer.QueryExtension;
using Microsoft.EntityFrameworkCore;

namespace HelloDoc_DataAccessLayer.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly AppDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public virtual async Task AddAsync(T entity, CancellationToken cancellationToken = default) =>
            await _dbSet.AddAsync(entity, cancellationToken);

        public virtual async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default) => await _dbSet.FirstOrDefaultAsync(filter, cancellationToken);

        public virtual IQueryable<T> GetAll()
            => _dbSet.AsNoTracking().AsQueryable();

        public Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> models)
           => await _dbSet.AddRangeAsync(models);

        public virtual void UpdateRange(IEnumerable<T> models)
            => _dbSet.UpdateRange(models);

        public async Task<(int count, IEnumerable<T> data)> GetPaginationWithExpressions(FilterCriteria<T> criteria)
        {
            var result = await GetAll().AsNoTracking().EvaluatePageQuery(criteria);

            return result;
        }

        public Task<bool> AnyAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default) => _dbSet.AnyAsync(filter, cancellationToken);
        public async Task<IEnumerable<T>> GetAllAsync()
           => await _dbSet.AsNoTracking().ToListAsync();
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
            => await _dbSet
                .AsNoTracking()
                .Where(filter)
                .ToListAsync(cancellationToken);

        public async Task<IEnumerable<T>> GetAllAsyncSelect(Expression<Func<T, bool>> filter, Expression<Func<T, T>> select, CancellationToken cancellationToken = default)
        => await _dbSet
            .AsNoTracking()
            .Where(filter)
            .Select(select)
            .ToListAsync(cancellationToken);

        public async Task<T> GetIncludeAsync(Expression<Func<T, object>> include, Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = _dbSet;
            query = query.Include(include);
            return await query.SingleOrDefaultAsync(filter);
        }

        public Task<int> CountAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default) => _dbSet.CountAsync(filter, cancellationToken);

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, IEnumerable<Expression<Func<T, object>>> includes)
        {
            IQueryable<T> query = GetAll();

            query = query.Where(filter);

            if (includes is not null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.Where(expression).AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, IEnumerable<Expression<Func<T, object>>> includes)
        {
            IQueryable<T> query = GetAll();

            query = query.Where(filter);

            if (includes is not null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));

            return await query.FirstOrDefaultAsync();
        }

        public void DeleteRange(IEnumerable<T> moduleAccessPermissions)
        {
            _dbSet.RemoveRange(moduleAccessPermissions);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? filter = null)
       => filter is null ?
           await _dbSet.CountAsync()
           : await _dbSet.CountAsync(filter);

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, Expression<Func<T, T>> select)
        {
            IQueryable<T> query = GetAll().Where(filter).Select(select);
            return await query.ToListAsync();
        }

        public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, IEnumerable<Expression<Func<T, object>>> includes, string[]? thenIncludeExpressions)
        {
            IQueryable<T> query = GetAll();

            query = query.Where(filter);

            if (includes is not null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (thenIncludeExpressions != null)
            {
                query = thenIncludeExpressions.Aggregate(query, (current, thenInclude) =>
                {
                    return current.Include(thenInclude);
                });
            }
            return await query.FirstOrDefaultAsync();
        }
    }
}