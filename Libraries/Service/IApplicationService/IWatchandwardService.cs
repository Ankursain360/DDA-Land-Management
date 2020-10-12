using Libraries.Model.Entity;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
   
    public interface IWatchandwardService : IEntityService<Watchandward>
    {
        Task<List<Watchandward>> GetAllWatchandward();
        Task<List<Watchandward>> GetWatchandwardUsingRepo();
        Task<List<Khasra>> GetAllKhasra();
        Task<List<Village>> GetAllVillage();
        Task<bool> Update(int id, Watchandward watchandward);

        Task<bool> Create(Watchandward watchandward);

        Task<Watchandward> FetchSingleResult(int id);

        Task<bool> Delete(int id);

        Task<List<Watchandward>> GetWatchandwardReportData(int village, DateTime fromdate, DateTime todate);
        //Task<PagedResult<Page>> GetPagedPage(PageSearchDto model);
    }
}
