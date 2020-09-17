using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
  
     public class Undersection22Repository : GenericRepository<Undersection22>, IUndersection22Repository
    {
        public Undersection22Repository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Undersection22>> GetUndersection22()
        {
            return await _dbContext.Undersection22.ToListAsync();
        }
        //public async Task<bool> Any(int id, string name)
        //{
        //    return await _dbContext.Module.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        //}
     }
}
