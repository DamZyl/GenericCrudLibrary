using GenericCrud.Models;

namespace GenericCrud.Mappers
{
    public abstract class AbstractMapper<TEntity, TViewModel> : IMapper<TEntity, TViewModel> where TEntity : BaseEntity where TViewModel : ViewModel
    {
        public abstract TViewModel MapObject(TEntity entity);
    }
}