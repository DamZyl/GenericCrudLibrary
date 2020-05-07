using GenericCrud.Models;

namespace GenericCrud.Mappers
{
    public abstract class AbstractMapper<TEntity, TDto> : IMapper<TEntity, TDto> where TEntity : IBaseEntity where TDto : IDto
    {
        public abstract TDto MapObject(TEntity entity);
    }
}