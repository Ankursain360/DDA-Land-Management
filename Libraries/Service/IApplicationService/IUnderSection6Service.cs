using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Repository.Common;

namespace Libraries.Service.IApplicationService
{
    public interface IUnderSection6Service
    {

        Task<List<Undersection4>> GetAllundersection4();

        Task<List<Undersection6>> GetAllUndersection6();
        Task<List<Undersection6>> GetUndersection6UsingRepo();
        Task<bool> Update(int id, Undersection6 undersection6);
        Task<bool> Create(Undersection6 undersection6);
        Task<Undersection6> FetchSingleResult(int id);
        Task<bool> Delete(int id);

        Task<PagedResult<Undersection6>> GetPagedUndersection6details(Undersection6SearchDto model);



    }
}
