using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GenericCrud.Models;
using Microsoft.EntityFrameworkCore.Query;

namespace GenericCrud.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        // Read
        Task<TEntity> FindByIdAsync(Guid id);
        Task<TEntity> FindByAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> FindByWithIncludesAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes);
        Task<IEnumerable<TEntity>> GetAllWithIncludesAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes);
        
        // Write
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entity);
        void Edit(TEntity entity);
        void EditRange(IEnumerable<TEntity> entity);
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entity);
    }
}