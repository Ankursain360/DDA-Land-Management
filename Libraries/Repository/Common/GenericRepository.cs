using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Libraries.Model;
using Libraries.Model.Common;
using Microsoft.EntityFrameworkCore;
using Dto.Master;

namespace Libraries.Repository.Common
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly DataContext _dbContext;
        protected readonly DbSet<T> _dbset;
        public GenericRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
            _dbset = dbContext.Set<T>();
        }

        public T Add(T entity)
        {
            return _dbset.Add(entity).Entity;
        }

        public virtual void Edit(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
        public T Delete(T entity)
        {
            return _dbset.Remove(entity).Entity;
        }

        public async Task<List<T>> FindBy(Expression<Func<T, bool>> predicate)
        {
            List<T> query = await _dbset.Where(predicate).ToListAsync();
            return query;
        }

        public virtual async Task<List<T>> GetAll()
        {
            return await _dbset.ToListAsync();
        }

        public virtual async Task AddRange(List<T> entities)
        {
            await _dbset.AddRangeAsync(entities);
        }

        public virtual async Task<int> Save()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public Task<List<T>> ExecuteQuery(string procedureName, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(List<T> entities)
        {
            _dbset.RemoveRange(entities);
        }
    }
}