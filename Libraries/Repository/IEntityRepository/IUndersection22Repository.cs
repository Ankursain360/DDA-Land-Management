using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
  
      public interface IUndersection22Repository : IGenericRepository<Undersection22>
    {
        Task<List<Undersection22>> GetUndersection22();
        //Task<bool> Any(int id, string name);
    }
}
