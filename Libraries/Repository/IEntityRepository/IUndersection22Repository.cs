using Dto.Search;
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
        Task<List<Undersection22>> GetAllUndersection22List(Undersection22SearchDto model);
        Task<PagedResult<Undersection22>> GetPagedUndersection22(Undersection22SearchDto model);
    }
}
