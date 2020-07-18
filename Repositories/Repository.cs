using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GenericCrud.Databases;
using GenericCrud.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace GenericCrud.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly IApplicationDbContext _context;

        public Repository(IApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<TEntity> FindByIdAsync(Guid id)
            => await _context.Set<TEntity>().FindAsync(id);

        public async Task<TEntity> FindByAsync(Expression<Func<TEntity, bool>> predicate)
            => await _context.Set<TEntity>().SingleOrDefaultAsync(predicate);

        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate)
            => await _context.Set<TEntity>().Where(predicate).ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAllAsync()
            => await _context.Set<TEntity>().ToListAsync();
        public async Task<TEntity> FindByWithIncludesAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes)
        {
            IQueryable<TEntity> queryable = _context.Set<TEntity>();

            if (includes != null)
            {
                queryable = includes(queryable);
            }

            return await queryable.SingleOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetAllWithIncludesAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes)
        {
            IQueryable<TEntity> queryable = _context.Set<TEntity>();

            if (includes != null)
            {
                queryable = includes(queryable);
            }

            return await queryable.ToListAsync();
        }

        public async Task AddAsync(TEntity entity)
            =>  await _context.Set<TEntity>().AddAsync(entity);

        public async Task AddRangeAsync(IEnumerable<TEntity> entity)
            => await _context.Set<TEntity>().AddRangeAsync(entity);

        public void Edit(TEntity entity)
            => _context.Set<TEntity>().Update(entity);

        public void EditRange(IEnumerable<TEntity> entity)
            => _context.Set<TEntity>().UpdateRange(entity);

        public void Delete(TEntity entity)
            => _context.Set<TEntity>().Remove(entity);

        public void DeleteRange(IEnumerable<TEntity> entity)
            => _context.Set<TEntity>().RemoveRange(entity);
    }
}