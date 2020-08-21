using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Model.Common;
using Libraries.Repository.Common;

namespace Libraries.Service.Common
{
    public class EntityService<T> : IEntityService<T> where T : BaseEntity
    {
        private readonly IGenericRepository<T> _repository;
        public EntityService(IUnitOfWork unitOfWork, IGenericRepository<T> repository)
        {
            _repository=repository;
        }

        public void Create(T entity)
        {
            if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			_repository.Add(entity);
        }

        public void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
			_repository.Delete(entity);
        }

        public async Task<List<T>> GetAll()
        {
            return await _repository.GetAll();
        }

        public void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
			_repository.Edit(entity);
        }
    }
}