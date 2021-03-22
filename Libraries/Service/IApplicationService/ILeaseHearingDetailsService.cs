using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Libraries.Service.IApplicationService
{
    public interface ILeaseHearingDetailsService : IEntityService<Requestforproceeding>
    {
        Task<PagedResult<Requestforproceeding>> GetPagedRequestLetterDetails(LeaseHearingDetailsSearchDto model, int userId);
        Task<Requestforproceeding> FetchRequestforproceedingData(int id);
        Task<List<Approvalstatus>> BindDropdownApprovalStatus();
    }
}
