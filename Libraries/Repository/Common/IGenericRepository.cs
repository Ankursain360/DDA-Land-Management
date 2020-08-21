using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Libraries.Model.Common;

namespace Libraries.Repository.Common
{
    public interface IGenericRepository <T> where T : BaseEntity
    {
        Task<List<T>> GetAll();
		Task<List<T>> FindBy(Expression<Func<T, bool>> predicate);
		T Add(T entity);
		T Delete(T entity);
		void Edit(T entity);
		Task AddRange(List<T> entities);
		Task<int> Save();
		Task<List<T>> ExecuteQuery(string procedureName, params object[] parameters);
    }
}