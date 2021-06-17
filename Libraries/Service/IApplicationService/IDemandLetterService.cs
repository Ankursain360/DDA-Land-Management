using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;

namespace Libraries.Service.IApplicationService
{
    public interface IDemandLetterService : IEntityService<Demandletters>
    {
        Task<PagedResult<Demandletters>> GetPagedDemandletter(DemandletterSearchDto model);
        Task<PagedResult<Demandletters>> GetPagedDuplicateDemandletter(DuplicateDemandLetterSearchDto model);
        Task<List<Demandletters>> GetAllDemandletter();
        Task<PagedResult<Demandletters>> GetDefaultListingReportData(DefaulterListingReportSearchDto defaulterListingReportSearchDto);

        Task<bool> Update(int id, Demandletters demandletter);
        Task<bool> Create(Demandletters demandletter);
        Task <Demandletters> FetchSingleResult(int id);

        Task<List<Demandletters>> BindPropertyNoList();
        Task<PagedResult<Demandletters>> GetPagedDemandletterReport(DemandletterreportSearchDto model);


        /*-----------------Relief Report Start------------------*/
        Task<PagedResult<Demandletters>> GetPagedReliefReport(ReliefReportSearchDto model);
        Task<List<Demandletters>> BindFileNoList();
        Task<List<Locality>> BindLoclityList();

        /*-----------------Relief Report End------------------*/
        //*******   Penalty Imposition Report**********
        Task<List<Locality>> GetLocalityList();
        Task<List<Demandletters>> GetFileNoList();
        Task<PagedResult<Demandletters>> GetPagedPenaltyImpositionReport(PenaltyImpositionReportSearchDto model);
        Task<PagedResult<Demandletters>> GetPagedImpositionReportOfCharges(ImpositionOfChargesSearchDto model);

        /*-----------------Demand Collection Ledger Report Start------------------*/
        Task<PagedResult<Demandletters>> GetPagedDemandCollectionLedgerReport(DemandCollectionLedgerSearchDto model);
        Task<List<DemandCollectionLedgerListDataDto>> GetPagedDemandCollectionLedgerReport1(DemandCollectionLedgerSearchDto model);
        /*-----------------Demand Collection Ledger Report Start------------------*/

        Task<List<DuesVsPaidAmountDto>> GetDuesVsPaidAmountListDto(DuesVsPaidAmountSearchDto model);
    }
}
