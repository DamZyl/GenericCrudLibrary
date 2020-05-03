using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GenericCrud.Models;

namespace GenericCrud.Repositories
{
    public interface IRepository<TEntity> where TEntity : IBaseEntity
    {
        // Read
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> expression);  
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> expression);  
        Task<TEntity> GetByIdAsync(Guid id);
        
        // Crud
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}