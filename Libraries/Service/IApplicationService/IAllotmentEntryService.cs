using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Repository.Common;
using Libraries.Service.Common;

namespace Libraries.Service.IApplicationService
{
    public interface IAllotmentEntryService : IEntityService<Allotmententry>
    {



        Task<List<Leaseapplication>> GetAllLeaseapplication();
        Task<List<Leasetype>> GetAllLeasetype();
        Task<List<Leasepurpose>> GetAllLeasepurpose();
        Task<List<Leasesubpurpose>> GetAllLeaseSubpurpose(int purposeUseId);

        Task<Leaseapplication> FetchSingleLeaseapplicationResult(int? applicationId);
        //Task<Allotmententry> FetchSingleCalculationDetails(int? LeasesTypeId);
        Task<Documentcharges> FetchSingledocumentResult(int? leasesTypeId);
        Task<Premiumrate> FetchSinglerateResult(int? leasePurposeId, int? leaseSubPurposeId, string allotmentDate);
        Task<Groundrent> FetchSinglegroundrentResult(int? leasePurposeId, int? leaseSubPurposeId, string allotmentDate);
        Task<Licencefees> FetchSinglefeeResult(int? leasePurposeId, int? leaseSubPurposeId, string allotmentDate);
        Task<List<Allotmententry>> GetAllotmententryUsingRepo();
        Task<List<Allotmententry>> GetAllAllotmententry();

        Task<bool> Update(int id, Allotmententry allotmententry);
        Task<bool> Create(Allotmententry allotmententry);
        Task<Allotmententry> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<PagedResult<Allotmententry>> GetPagedAllotmententry(AllotmentEntrySearchDto model);


        


    }
}
