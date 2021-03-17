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
        //Task<List<Khasra>> BindKhasra(int? villageId);
        Task<Leaseapplication> FetchSingleLeaseapplicationResult(int? applicationId);
       


       

    }
}
