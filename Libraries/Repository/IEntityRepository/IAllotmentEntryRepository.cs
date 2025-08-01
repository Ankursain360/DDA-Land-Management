﻿using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;


namespace Libraries.Repository.IEntityRepository
{
    public interface IAllotmentEntryRepository : IGenericRepository<Allotmententry>
    {
        Task<PagedResult<Allotmententry>> GetPagedAllotmententry(AllotmentEntrySearchDto model);
        Task<List<Allotmententry>> GetAllAllotmententry();

        Task<List<Leaseapplication>> GetAllLeaseapplication(int approved);
        Task<List<Leaseapplication>> GetAllLeaseapplicationforview(int approved);
        Task<List<Leasetype>> GetAllLeasetype();
        Task<List<Leasepurpose>> GetAllLeasepurpose();
        Task<List<Leasesubpurpose>> GetAllLeaseSubpurpose(int purposeId);
        Task<Leaseapplication> FetchSingleLeaseapplicationResult(int? applicationId);

        //Task<Allotmententry> FetchSingleCalculationDetails(int? LeasesTypeId);
        Task<Documentcharges> FetchSingledocumentResult(int? leasePurposeId, int? leaseSubPurposeId, string allotmentDate);

        Task<Premiumrate> FetchSinglerateResult(int? leasePurposeId, int? leaseSubPurposeId, string allotmentDate);
        Task<Groundrent> FetchSinglegroundrentResult(int? leasePurposeId, int? leaseSubPurposeId, string allotmentDate);
        Task<Licencefees> FetchSinglefeeResult(int? leasePurposeId, int? leaseSubPurposeId, string allotmentDate);
        Task<Leaseapplication> FetchLeaseApplicationmailDetails(int id);

        Task<List<DemandletterdatalistDto>> Getdemandletteralldata(DemandletterDateSearchDto model);
        Task<bool> CreatePaymentPremiumDr(Payment model);
        Task<List<PayemntDescriptionListDto>> GetPagedPaymentReport(PaymentdetailssearchDto model);
    }
}
