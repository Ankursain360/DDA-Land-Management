using Libraries.Model.Entity;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Dto.Master;
using Libraries.Repository.Common;

namespace Libraries.Service.IApplicationService
{
    public interface ICancellationEntryService : IEntityService<Cancellationentry>
    {
        Task<List<Allotmententry>> GetAllAllotment();
        Task<List<Honble>> GetAllHonble();

        Task<bool> Create(Cancellationentry cancellationentry);
        Task<bool> Update(int id, Cancellationentry scheme);
        Task<List<Cancellationentry>> GetAllRequestForProceeding();
        Task<Cancellationentry> FetchSingleResult(int id);
        Task<List<Cancellationentry>> GetRequestUsingRepo();
        Task<bool> Delete(int id);
        Task<PagedResult<Cancellationentry>> GetPagedCancellationEntry(CancellationEntrySearchDto model);
        Task<List<UserBindDropdownDto>> BindUsernameNameList();

    }
}
