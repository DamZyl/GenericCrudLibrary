using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GenericCrud.Databases
{
    public interface IApplicationDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync(bool confirmAllTransactions, CancellationToken cancellationToken);
        int SaveChanges();
        int SaveChanges(bool confirmAllTransactions);
        void Dispose();
    }
}