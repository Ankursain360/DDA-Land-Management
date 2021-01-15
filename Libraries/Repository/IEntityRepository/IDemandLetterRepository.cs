using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IDemandLetterRepository : IGenericRepository<Demandletters>
    {
        Task<PagedResult<Demandletters>> GetPagedDemandletter(DemandletterSearchDto model);
        Task<List<Demandletters>> GetAllDemandletter();
        Task<PagedResult<Demandletter>> GetDefaultListingReportData(DefaulterListingReportSearchDto defaulterListingReportSearchDto);
        /*-----------------Relief Report Start------------------*/
        Task<PagedResult<Demandletters>> GetPagedReliefReport(ReliefReportSearchDto model);
        Task<List<Demandletters>> BindFileNoList();
        Task<List<Locality>> BindLoclityList();
        /*-----------------Relief Report End------------------*/

        //*******   Penalty Imposition Report**********
        Task<List<Locality>> GetLocalityList();
        Task<List<Demandletters>> GetFileNoList();
        Task<PagedResult<Demandletters>> GetPagedPenaltyImpositionReport(PenaltyImpositionReportSearchDto model);
        Task<PagedResult<Demandletters>> GetPagedDemandCollectionLedgerReport(DemandCollectionLedgerSearchDto model);
        Task<List<DemandCollectionLedgerListDataDto>> GetPagedDemandCollectionLedgerReport1(DemandCollectionLedgerSearchDto model);
        Task<List<Demandletters>> BindPropertyNoList();
    }
}
