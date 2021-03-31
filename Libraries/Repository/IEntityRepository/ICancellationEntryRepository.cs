using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Dto.Master;

namespace Libraries.Repository.IEntityRepository
{
    public interface ICancellationEntryRepository : IGenericRepository<Cancellationentry>
    {
        Task<List<Allotmententry>> GetAllAllotment();
        Task<List<Honble>> GetAllHonble();
        Task<List<Cancellationentry>> GetAllRequestForProceeding();
        Task<PagedResult<Cancellationentry>> GetPagedCancellationEntry(CancellationEntrySearchDto model);
        Task<List<UserBindDropdownDto>> BindUsernameNameList();
        Task<Allotmententry> FetchAllottmentDetails(int allottmentId);
    }
}
