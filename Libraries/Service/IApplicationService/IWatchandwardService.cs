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

    public interface IWatchandwardService : IEntityService<Watchandward>
    {
        Task<List<Watchandward>> GetAllWatchandward();
        Task<List<Watchandward>> GetWatchandwardUsingRepo();
        Task<List<Khasra>> GetAllKhasra();
       
        Task<List<Locality>> GetAllLocality();
        Task<bool> Update(int id, Watchandward watchandward);

        Task<bool> Create(Watchandward watchandward);

        Task<Watchandward> FetchSingleResult(int id);

        Task<bool> Delete(int id);


        Task<PagedResult<Watchandward>> GetWatchandwardReportData(WatchandwardSearchDto watchandwardSearchDto);
       


        Task<PagedResult<Watchandward>> GetPagedWatchandward(WatchandwardSearchDto model);



        //*****multiple files*********Added by ishu


        Task<Watchandwardphotofiledetails> GetWatchandwardphotofiledetails(int watchandwardId);
        Task<Watchandwardreportfiledetails> GetWatchandwardreportfiledetails(int watchandwardId);
        Task<bool> SaveWatchandwardphotofiledetails(Watchandwardphotofiledetails watchandwardphotofiledetails);
        Task<bool> SaveWatchandwardreportfiledetails(Watchandwardreportfiledetails watchandwardreportfiledetails);
        Task<bool> DeleteWatchandwardphotofiledetails(int Id);
        Task<bool> DeleteWatchandwardreportfiledetails(int Id);
        Task<List<Propertyregistration>> GetAllPrimaryList();
        Task<Propertyregistration> FetchSingleResultOnPrimaryList(int v);
    }
}
