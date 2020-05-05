using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GenericCrud.Databases;
using GenericCrud.Models;
using Microsoft.EntityFrameworkCore;

namespace GenericCrud.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IBaseEntity
    {
        private readonly IApplicationDbContext _context;

        public Repository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
            => await _context.Set<TEntity>().ToListAsync();

        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> expression)
            => await _context.Set<TEntity>().Where(expression).ToListAsync();

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> expression)
            => await _context.Set<TEntity>().SingleOrDefaultAsync(expression);

        public async Task<TEntity> GetByIdAsync(Guid id)
            => await _context.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == id);

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.Instance.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.Instance.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.Instance.SaveChangesAsync();
        }
    }
}