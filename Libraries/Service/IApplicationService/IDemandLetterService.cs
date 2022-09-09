using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using System;

namespace Libraries.Service.IApplicationService
{
    public interface IDemandLetterService : IEntityService<Demandletters>
    {
        Task<PagedResult<Demandletters>> GetPagedDemandletter(DemandletterSearchDto model);
        Task<PagedResult<Demandletters>> GetPagedDuplicateDemandletter(DuplicateDemandLetterSearchDto model);
        Task<List<Demandletters>> GetAllDemandletter();
        Task<PagedResult<Demandletters>> GetDefaultListingReportData(DefaulterListingReportSearchDto defaulterListingReportSearchDto);
        Task<List<Demandletters>> GetDemandLetterReportList(DownloadDemandLetterReportDto report);
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
        Task<List<PropertyType>> GetPropertyType();
        Task<List<Demandletters>> GetFileNoList();
        Task<PagedResult<Demandletters>> GetPagedPenaltyImpositionReport(PenaltyImpositionReportSearchDto model);
        Task<PagedResult<Demandletters>> GetPagedImpositionReportOfCharges(ImpositionOfChargesSearchDto model);

        /*-----------------Demand Collection Ledger Report Start------------------*/
        Task<PagedResult<Demandletters>> GetPagedDemandCollectionLedgerReport(DemandCollectionLedgerSearchDto model);
        Task<List<DemandCollectionLedgerListDataDto>> GetPagedDemandCollectionLedgerReport1(DemandCollectionLedgerSearchDto model);
        /*-----------------Demand Collection Ledger Report Start------------------*/

        Task<List<DuesVsPaidAmountDto>> GetDuesVsPaidAmountListDto(DuesVsPaidAmountSearchDto model);


        Task<List<FileNODto>> GetFileAutoCompleteDetails(string prefix);
        Task<DemandAutoFillDto> GetFileNODetail(int fileid);
        Task<Encrochmenttype> FetchResultEncroachmentType(DateTime date1);
        Task<List<Resratelisttypea>> RateListTypeA(DateTime date1, string localityId, int[] subEncroachersId);
        Task<List<Resratelisttypeb>> RateListTypeB(DateTime date1, string localityId, int[] subEncroachersId);
        Task<List<Resratelisttypec>> RateListTypeC(DateTime date1, string localityId, int[] subEncroachersId);
        Task<List<Resratelisttypeb>> RateListTypeBSpecific(DateTime dateTimeSpecific, DateTime date1, string localityId, int[] subEncroachersId);
        Task<List<Resratelisttypea>> RateListTypeASpecific(DateTime specificDateTime, DateTime date1, string localityId, int[] subEncroachersId);
        Task<Comencrochmenttype> FetchResultCOMEncroachmentType(DateTime date1);
        Task<List<Comratelisttypea>> ComRateListTypeA(DateTime date1, string localityId, int[] subEncroachersId);
        Task<List<Comratelisttypeb>> ComRateListTypeB(DateTime date1, string localityId, int[] subEncroachersId);
        Task<List<Comratelisttypec>> ComRateListTypeC(DateTime date1, string localityId, int[] subEncroachersId);
        Task<List<Comratelisttypea>> ComRateListTypeASpecific(DateTime specificDateTime, DateTime dateTime, string localityId, int[] subEncroachersId);
        Task<List<Comratelisttypeb>> ComRateListTypeBSpecific(DateTime dateTimeSpecific, DateTime date1, string localityId, int[] subEncroachersId);
    }
}
