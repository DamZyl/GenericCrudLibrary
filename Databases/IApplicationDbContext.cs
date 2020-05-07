using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GenericCrud.Databases
{
    public interface IApplicationDbContext : IDisposable
    {
        DbContext Instance { get; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}