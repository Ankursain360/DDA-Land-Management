using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    
    public interface IDisposallandRepository : IGenericRepository<Disposalland>
    {
        //Task<List<Disposalland>> GetDisposalland();
        Task<List<Disposalland>> GetAllDisposalland();

        Task<List<Utilizationtype>> GetAllUtilizationtype();
        Task<List<Acquiredlandvillage>> GetAllVillage();
        Task<List<Khasra>> GetAllKhasra(int? villageId);

        Task<PagedResult<Disposalland>> GetPagedDisposalLand(DisposalLandSearchDto model);


    }
}
