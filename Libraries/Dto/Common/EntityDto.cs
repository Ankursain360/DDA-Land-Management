
namespace Dto.Common
{
    public abstract class BaseEntityDto
    {

    }
    public class EntityDto<T>: BaseEntityDto, IEntityDto<T>
    {
        public virtual T Id { get; set; }
    }
}
