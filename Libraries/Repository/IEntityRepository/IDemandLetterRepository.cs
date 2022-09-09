using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using System;

namespace Libraries.Repository.IEntityRepository
{
    public interface IDemandLetterRepository : IGenericRepository<Demandletters>
    {
        Task<PagedResult<Demandletters>> GetPagedDemandletter(DemandletterSearchDto model);
        Task<PagedResult<Demandletters>> GetPagedDuplicateDemandletter(DuplicateDemandLetterSearchDto model);
        Task<List<Demandletters>> GetAllDemandletter();
        Task<PagedResult<Demandletters>> GetDefaultListingReportData(DefaulterListingReportSearchDto defaulterListingReportSearchDto);
        Task<List<Demandletters>> GetDemandLetterReportList(DownloadDemandLetterReportDto report);
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
        Task<PagedResult<Demandletters>> GetPagedDemandCollectionLedgerReport(DemandCollectionLedgerSearchDto model);
        Task<List<DemandCollectionLedgerListDataDto>> GetPagedDemandCollectionLedgerReport1(DemandCollectionLedgerSearchDto model);
        Task<List<DuesVsPaidAmountDto>> GetDuesVsPaidAmountListDto(DuesVsPaidAmountSearchDto model);


        Task<List<Damagepayeeregister>> GetFileAutoCompleteDetails(string prefix);
        Task<Damagepayeeregister> GetFileDetails(int fileid);

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
        Task<List<Comratelisttypeb>> ComRateListTypeBSpecific(DateTime dateTimeSpecific, DateTime date1, string localityId, int[] subEncroachersId);
        Task<List<Comratelisttypea>> ComRateListTypeASpecific(DateTime specificDateTime, DateTime date1, string localityId, int[] subEncroachersId);
    }
}
