﻿using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface IAnnexureAApprovalService : IEntityService<Fixingdemolition>
    {
        Task<PagedResult<Fixingdemolition>> GetPagedAnnexureA(AnnexureAApprovalSearchDto model, int userId, int zoneId);
        Task<Fixingdemolition> FetchSingleResult(int id);
        Task<List<Fixingdemolition>> GetAllFixingdemolition(AnnexureAApprovalSearchDto model, int userId, int zoneId);
        Task<bool> IsApplicationPendingAtUserEnd(int id, int userId);
    }
}
