using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{

    public interface INewLandDemandListDetailsRepository : IGenericRepository<Newlanddemandlistdetails>
    {
        int GetLocalityByName(string name);
        int GetKhasraByName(string name);
        Task<bool> Any(int id, string fileNo);
        Task<PagedResult<Newlanddemandlistdetails>> GetPagedDMSFileUploadList(NewLandDemandListDetailsSearchDto model);
        Task<Newlanddemandlistdetails> FetchSingleResult(int id);
        Task<List<Newlandvillage>> GetVillageList();
        Task<List<Newlandkhasra>> GetKhasraList(int id);
        Task<List<Newlanddemandlistdetails>> GetAllDemandlistdetails();
    }
}
