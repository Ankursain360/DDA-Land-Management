﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Libraries.Repository.IEntityRepository
{
    public interface ILeaseHearingDetailsRepository : IGenericRepository<Requestforproceeding>
    {
        Task<PagedResult<Requestforproceeding>> GetPagedRequestLetterDetails(LeaseHearingDetailsSearchDto model, int userId);
        Task<List<Approvalstatus>> BindDropdownApprovalStatus();
        Task<Requestforproceeding> FetchRequestforproceedingData(int id);
    }
}