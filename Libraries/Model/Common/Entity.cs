namespace Libraries.Model.Common
{
    public abstract class BaseEntity
    {

    }
    public class Entity<T>: BaseEntity, IEntity<T>
    {
         public virtual T Id { get; set; }
    }
}