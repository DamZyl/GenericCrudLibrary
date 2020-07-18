using GenericCrud.Models;

namespace GenericCrud.Mappers
{
    public abstract class AbstractMapperWithParams <TEntity, TViewModel> : IMapperWithParams<TEntity, TViewModel> where TEntity : BaseEntity where TViewModel : ViewModel
    {
        public abstract TViewModel MapObject(TEntity entity, params object[] parameters);
    }
}