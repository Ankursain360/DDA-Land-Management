﻿

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
    public interface IExtensionApprovalService : IEntityService<Extension>
    {
        Task<PagedResult<Extension>> GetPagedExtensionDetails(ExtensionApprovalSearchDto model, int userId);
        Task<Extension> FetchSingleResult(int id);
        Task<bool> IsApplicationPendingAtUserEnd(int id, int userId);
    }
}
