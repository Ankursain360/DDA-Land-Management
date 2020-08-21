using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Model.Common;

namespace Libraries.Service.Common
{
    public interface IEntityService<T> : IService
        where T : BaseEntity
    {
		void Create(T entity);
        void Delete(T entity);
        Task<List<T>> GetAll();
        void Update(T entity);
    }
}