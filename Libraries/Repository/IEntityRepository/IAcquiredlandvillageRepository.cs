using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;

namespace Libraries.Repository.IEntityRepository
{
    public interface IAcquiredlandvillageRepository : IGenericRepository<Acquiredlandvillage>
    {

        Task<List<Acquiredlandvillage>> GetAcquiredlandvillage();
        Task<List<District>> GetAllDistrict();
        Task<List<Tehsil>> GetAllTehsil();
        Task<List<Zone>> GetAllZone();
        Task<PagedResult<Acquiredlandvillage>> GetPagedAcquiredlandvillage(AcquiredLandVillageSearchDto model);
    }
}
