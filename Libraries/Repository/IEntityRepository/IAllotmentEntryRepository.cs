using Libraries.Model.Entity;
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

        Task<List<Leaseapplication>> GetAllLeaseapplication();
        Task<List<Leasetype>> GetAllLeasetype();
        Task<List<Leasepurpose>> GetAllLeasepurpose();
        Task<List<Leasesubpurpose>> GetAllLeaseSubpurpose(int purposeId);
        Task<Leaseapplication> FetchSingleLeaseapplicationResult(int? applicationId);

        //Task<Allotmententry> FetchSingleCalculationDetails(int? LeasesTypeId);
        Task<Documentcharges> FetchSingledocumentResult(int? leasesTypeId);

        Task<Premiumrate> FetchSinglerateResult(int? leasePurposeId, int? leaseSubPurposeId, string allotmentDate);




    }
}
