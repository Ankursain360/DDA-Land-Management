using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Repository.Common;
namespace Libraries.Service.IApplicationService
{
   
   public interface INewlandkhasraService
    {

        Task<List<LandCategory>> GetAllLandCategory();
        Task<List<Newlandvillage>> GetAllVillageList();
        Task<List<Newlandkhasra>> GetKhasraUsingRepo();
        Task<List<Newlandkhasra>> GetAllKhasra();

        Task<bool> Update(int id, Newlandkhasra khasra);
        Task<bool> Create(Newlandkhasra khasra);
        Task<Newlandkhasra> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<PagedResult<Newlandkhasra>> GetPagedKhasra(NewlandkhasraSearchDto model);

        Task<PagedResult<Newlandkhasra>> GetPagednewlandVillageKhasraReport(NewlandVillageDetailsKhasraWiseReportSearchDto model);
        Task<List<Newlandkhasra>> GetAllKhasraList(int? villageId);
    }
}
