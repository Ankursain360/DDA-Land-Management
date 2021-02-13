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
    public interface IDemandListDetailsService : IEntityService<Demandlistdetails>
    {
        Task<List<Acquiredlandvillage>> GetVillageList();
        Task<List<Khasra>> GetKhasraList(int id);
        Task<PagedResult<Demandlistdetails>> GetPagedDMSFileUploadList(DemandListDetailsSearchDto model);
        Task<bool> Create(Demandlistdetails demandlistdetails);
        Task<Demandlistdetails> FetchSingleResult(int id);
        Task<bool> Update(int id, Demandlistdetails demandlistdetails);
        Task<bool> Delete(int id, int userId);
        int GetLocalityByName(string name);
        int GetKhasraByName(string name);
        Task<bool> CheckUniqueName(int id, string fileNo);
    }
}
