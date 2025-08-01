﻿using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;

namespace Libraries.Repository.IEntityRepository
{
    public interface IKhasraRepository : IGenericRepository<Khasra>
    {
        Task<PagedResult<Khasra>> GetPagedKhasra(KhasraMasterSearchDto model);
        Task<List<Khasra>> GetAllKhasra();
       
        Task<List<LandCategory>> GetAllLandCategory();
        Task<List<Acquiredlandvillage>> GetAllVillageList();
        Task<List<Khasra>> GetAllKhasraList(int? villageId);
        Task<PagedResult<Khasra>> GetPagedVillageKhasraReport(VillageDetailsKhasraWiseReportSearchDto model);
        Task<List<Khasra>> getAllVillageDetailsKhasraWise(VillageDetailsKhasraWiseReportSearchDto model);


    }
}
