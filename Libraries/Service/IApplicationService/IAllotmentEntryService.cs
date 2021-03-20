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
        Task<List<Leasesubpurpose>> GetAllLeasesubpurpose();

        Task<Leaseapplication> FetchSingleLeaseapplicationResult(int? applicationId);
        //Task<List<Undersection17>> GetAllUndersection17List();

        Task<List<Allotmententry>> GetAllotmententryUsingRepo();
        Task<List<Allotmententry>> GetAllAllotmententry();

        Task<bool> Update(int id, Allotmententry allotmententry);
        Task<bool> Create(Allotmententry allotmententry);
        Task<Allotmententry> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<PagedResult<Allotmententry>> GetPagedAllotmententry(AllotmentEntrySearchDto model);


        


    }
}
