using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericCrud.Databases;
using GenericCrud.Models;

namespace GenericCrud.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IApplicationDbContext _context;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();
        
        public UnitOfWork(IApplicationDbContext context)
        {
            _context = context;
        }
        
        public IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories.Keys.Contains(typeof(TEntity)))
            {
                return _repositories[typeof(TEntity)] as IRepository<TEntity>;
            }

            IRepository<TEntity> repository = new Repository<TEntity>(_context);
            _repositories.Add(typeof(TEntity), repository);
            return repository;
        }

        public async Task<int> Commit()
            => await _context.Instance.SaveChangesAsync();

        public void Rollback()
            => _context.Instance.ChangeTracker
                .Entries()
                .ToList()
                .ForEach(x => x.Reload());
    }
}