using System.Threading.Tasks;
using GenericCrud.Models;

namespace GenericCrud.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        Task<int> Commit();
        void Rollback();
    }
}