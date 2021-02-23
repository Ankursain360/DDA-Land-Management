using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Repository.Common;
using Libraries.Service.Common;

namespace Libraries.Service.IApplicationService
{
    public interface IUndersection17plotdetailService : IEntityService<Undersection17plotdetail>
    {

       
        Task<List<Acquiredlandvillage>> GetAllVillageList();
        Task<List<Khasra>> GetAllKhasraList(int? villageId);
        Task<List<Undersection17>> GetAllUndersection17List();

        Task<List<Undersection17plotdetail>> GetUndersection17plotdetailUsingRepo();
        Task<List<Undersection17plotdetail>> GetAllUndersection17plotdetail();

        Task<bool> Update(int id, Undersection17plotdetail undersection17plotdetail);
        Task<bool> Create(Undersection17plotdetail undersection17plotdetail);
        Task<Undersection17plotdetail> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<PagedResult<Undersection17plotdetail>> GetPagedUndersection17plotdetail(Undersection17plotdetailSearchDto model);

        Task<Khasra> FetchSingleKhasraResult(int? khasraId);



    }
}
