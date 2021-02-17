
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;


namespace Libraries.Repository.IEntityRepository
{
    public interface IUndersection17plotdetailRepository : IGenericRepository<Undersection17plotdetail>
    {
        Task<PagedResult<Undersection17plotdetail>> GetPagedUndersection17plotdetail(Undersection17plotdetailSearchDto model);
        Task<List<Undersection17plotdetail>> GetAllUndersection17plotdetail();

        Task<List<Acquiredlandvillage>> GetAllVillageList();
        Task<List<Khasra>> GetAllKhasraList(int? villageId);
        Task<List<Undersection17>> GetAllUndersection17List();

        Task<Khasra> FetchSingleKhasraResult(int? khasraId);
    }
}
