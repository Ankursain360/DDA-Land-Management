using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface ILandAcquisitionAwardsService : IEntityService<LandAcquisitionAwards>
    {
        //Task<List<Locality>> GetLocalityList(); 
        Task<PagedResult<LandAcquisitionAwards>> GetPagedLandAcquisitionAwards(LandAcquisitionAwardsDto model);
        Task<LandAcquisitionAwards> FetchDocumentDetails(int id);
        Task<List<LandAcquisitionAwards>> GetLandAcquisitionAwardsList(LandAcquisitionAwardsSearchDto model);
        Task<List<LandAcquisitionAwards>> GetAllAcquisitionAwards();
    }
}
