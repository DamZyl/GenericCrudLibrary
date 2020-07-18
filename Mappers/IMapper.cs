using GenericCrud.Models;

namespace GenericCrud.Mappers
{
    public interface IMapper<in TEntity, out TViewModel> where TEntity : BaseEntity where TViewModel : ViewModel
    {
        TViewModel MapObject(TEntity entity);
    }
}