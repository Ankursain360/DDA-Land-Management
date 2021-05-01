using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{

    public interface IDemandListDetailsRepository : IGenericRepository<Demandlistdetails>
    {
        int GetLocalityByName(string name);
        int GetKhasraByName(string name);
        Task<bool> Any(int id, string fileNo);
        Task<PagedResult<Demandlistdetails>> GetPagedDMSFileUploadList(DemandListDetailsSearchDto model);
        Task<Demandlistdetails> FetchSingleResult(int id);
        Task<List<Acquiredlandvillage>> GetVillageList();
        Task<List<Khasra>> GetKhasraList(int id);
        Task<List<Demandlistdetails>> GetAllDemandlistdetails();
    }
}
