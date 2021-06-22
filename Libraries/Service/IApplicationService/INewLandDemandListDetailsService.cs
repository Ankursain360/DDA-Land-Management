//using System;
//using System.Collections.Generic;
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
    public interface INewLandDemandListDetailsService : IEntityService<Newlanddemandlistdetails>
    {
        Task<List<Newlandvillage>> GetVillageList();
        Task<List<Newlandkhasra>> GetKhasraList(int id);
        Task<PagedResult<Newlanddemandlistdetails>> GetPagedDMSFileUploadList(NewLandDemandListDetailsSearchDto model);
        Task<bool> Create(Newlanddemandlistdetails newlanddemandlistdetails);
        Task<Newlanddemandlistdetails> FetchSingleResult(int id);
        Task<bool> Update(int id, Newlanddemandlistdetails newlanddemandlistdetails);
        Task<bool> Delete(int id, int userId);
        int GetLocalityByName(string name);
        int GetKhasraByName(string name);
        Task<bool> CheckUniqueName(int id, string fileNo);
        Task<List<Newlanddemandlistdetails>> GetAllDemandlistdetails();
    }
}

