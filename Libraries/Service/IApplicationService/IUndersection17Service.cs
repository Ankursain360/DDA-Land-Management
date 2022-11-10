using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface IUndersection17Service
    {

        Task<List<Undersection6>> GetAllUndersection6List();
        Task<List<Undersection17>> GetUndersection17UsingRepo();
        Task<List<Undersection17>> GetAllUndersection17();
        Task<List<Undersection17>> GetAllUndersection17List(UnderSection17SearchDto model);
        Task<PagedResult<Undersection17>> GetPagedUndersection17(UnderSection17SearchDto model);
        Task<bool> Update(int id, Undersection17 undersection17);
        Task<bool> Create(Undersection17 undersection17);
        Task<Undersection17> FetchSingleResult(int id);
        Task<bool> Delete(int id);






    }
}