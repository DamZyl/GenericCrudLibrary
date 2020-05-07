using GenericCrud.Models;

namespace GenericCrud.Mappers
{
    public interface IMapper<in TEntity, out TDto> where TEntity : IBaseEntity where TDto : IDto
    {
        TDto MapObject(TEntity entity);
    }
}