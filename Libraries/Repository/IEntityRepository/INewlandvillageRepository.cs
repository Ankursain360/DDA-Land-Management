using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;

namespace Libraries.Repository.IEntityRepository
{
    

    public interface INewlandvillageRepository : IGenericRepository<Newlandvillage>
    {

        Task<List<Newlandvillage>> GetNewlandvillage();
        Task<List<District>> GetAllDistrict();
        Task<List<Tehsil>> GetAllTehsil();
        Task<List<Zone>> GetAllZone();

        Task<List<Newlandvillage>> GetAllVillageList();
        Task<PagedResult<Newlandvillage>> GetPagedNewlandvillage(NewlandvillageSearchDto model);

        Task<PagedResult<Newlandvillage>> GetPagedNewLandVillageReport(NewlandVillageReportSearchDto model);

        Task<PagedResult<Newlandvillage>> GetPagedNewlandAcquiredVillageReport(NewlandAcquiredVillageReportSearchDto model);



    }
}
