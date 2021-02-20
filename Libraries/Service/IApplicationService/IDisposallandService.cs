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

     public interface IDisposallandService : IEntityService<Disposalland>
    {
        Task<List<Disposalland>> GetAllDisposalland();
        Task<List<Disposalland>> GetDisposallandUsingRepo();

        Task<Disposalland> FetchSingleResult(int id);
        Task<bool> Update(int id, Disposalland disposalland);
        Task<bool> Create(Disposalland disposalland);
        Task<List<Utilizationtype>> GetAllUtilizationtype();
        Task<List<Acquiredlandvillage>> GetAllVillage();
        Task<List<Khasra>> GetAllKhasra(int? villageId);
        Task<bool> Delete(int id);
        Task<PagedResult<Disposalland>> GetPagedDisposalLand(DisposalLandSearchDto model);
    }
}
