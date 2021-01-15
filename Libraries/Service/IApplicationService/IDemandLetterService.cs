using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface IDemandLetterService : IEntityService<Demandletters>
    {
        Task<PagedResult<Demandletters>> GetPagedDemandletter(DemandletterSearchDto model);
        Task<List<Demandletters>> GetAllDemandletter();
        Task<PagedResult<Demandletter>> GetDefaultListingReportData(DefaulterListingReportSearchDto defaulterListingReportSearchDto);

        Task<bool> Update(int id, Demandletters demandletter);
        Task<bool> Create(Demandletters demandletter);
        Task <Demandletters> FetchSingleResult(int id);

        /*-----------------Relief Report Start------------------*/
        Task<PagedResult<Demandletters>> GetPagedReliefReport(ReliefReportSearchDto model);
        Task<List<Demandletters>> BindFileNoList();
        Task<List<Locality>> BindLoclityList();
        Task<List<Demandletters>> BindPropertyNoList();

        /*-----------------Relief Report End------------------*/
        //*******   Penalty Imposition Report**********
        Task<List<Locality>> GetLocalityList();
        Task<List<Demandletters>> GetFileNoList();
        Task<PagedResult<Demandletters>> GetPagedPenaltyImpositionReport(PenaltyImpositionReportSearchDto model);

        /*-----------------Demand Collection Ledger Report Start------------------*/
        Task<PagedResult<Demandletters>> GetPagedDemandCollectionLedgerReport(DemandCollectionLedgerSearchDto model);
        Task<List<DemandCollectionLedgerListDataDto>> GetPagedDemandCollectionLedgerReport1(DemandCollectionLedgerSearchDto model);
        /*-----------------Demand Collection Ledger Report Start------------------*/
    }
}
