using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{

    public interface IWatchandwardRepository : IGenericRepository<Watchandward>
    {
        Task<List<Watchandward>> GetWatchandward();
        Task<List<Watchandward>> GetAllWatchandward();
        Task<List<Locality>> GetAllLocality();

        Task<List<Khasra>> GetAllKhasra();
        Task<Watchandward> FetchSingleResult(int id); //added by ishu
        Task<PagedResult<Watchandward>> GetWatchandwardReportData(WatchAndWardPeriodReportSearchDto watchAndWardPeriodReportSearchDto);
        //Task<PagedResult<Page>> GetPagedPage(PageSearchDto model);

        Task<PagedResult<Watchandward>> GetPagedWatchandward(WatchandwardSearchDto model, int zoneId);


        //*****multiple files*********


        Task<Watchandwardphotofiledetails> GetWatchandwardphotofiledetails(int watchandwardId);
        Task<Watchandwardreportfiledetails> GetWatchandwardreportfiledetails(int watchandwardId);
        Task<bool> SaveWatchandwardphotofiledetails(Watchandwardphotofiledetails watchandwardphotofiledetails);
        Task<bool> SaveWatchandwardreportfiledetails(Watchandwardreportfiledetails watchandwardreportfiledetails);
        Task<bool> DeleteWatchandwardphotofiledetails(int Id);
        Task<bool> DeleteWatchandwardreportfiledetails(int Id);
        Task<List<Propertyregistration>> GetAllPrimaryList();
        Task<Propertyregistration> FetchSingleResultOnPrimaryList(int propertyId);
        Task<bool> RollBackEntryPhoto(int id);
        Task<bool> RollBackEntryReport(int id);
    }
}
