using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface IUndersection22Service : IEntityService<Undersection22>
    {
        Task<List<Undersection22>> GetAllUndersection22();
        Task<List<Undersection22>> GetUndersection22UsingRepo();

        Task<bool> Update(int id, Undersection22 undersection22); 

        Task<bool> Create(Undersection22 undersection22);

        Task<Undersection22> FetchSingleResult(int id); 

        Task<bool> Delete(int id);
        Task<PagedResult<Undersection22>> GetPagedUndersection22(Undersection22SearchDto model);


        //Task<bool> CheckUniqueName(int id, string Module);   // To check Unique Value  for designation
    }
}
