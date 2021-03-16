using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Libraries.Repository.EntityRepository
{
    public class LeaseApplicationFormApprovalRepository : GenericRepository<Leaseapplication>, ILeaseApplicationFormApprovalRepository
    {

        public LeaseApplicationFormApprovalRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<Leaseapplication> FetchSingleResult(int id)
        {
            return await _dbContext.Leaseapplication
                                    .Include(x => x.Leaseapplicationdocuments)
                                    .Where(x => x.Id == id)
                                    .FirstOrDefaultAsync();
        }

        public async Task<PagedResult<Leaseapplication>> GetPagedLeaseApplicationFormDetails(LeaseApplicationFormApprovalSearchDto model, int userId)
        {
            var data = await _dbContext.Leaseapplication
                                        .Include(x => x.Leaseapplicationdocuments)
                                        .Where(x => x.IsActive == 1 && x.ApprovedStatus == model.StatusId
                                        && (model.StatusId == 0 ? x.PendingAt == userId : x.PendingAt == 0)
                                        )
                                        .GetPaged<Leaseapplication>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                data = null;
                data = await _dbContext.Leaseapplication
                                        .Include(x => x.Leaseapplicationdocuments)
                                        .Where(x => x.IsActive == 1 && x.ApprovedStatus == model.StatusId
                                        && (model.StatusId == 0 ? x.PendingAt == userId : x.PendingAt == 0)
                                        )
                                       .OrderBy(s =>
                                       (model.SortBy.ToUpper() == "REFNO" ? s.RefNo
                                       : model.SortBy.ToUpper() == "REGISTRATIONNO" ? s.RegistrationNo
                                       : s.RefNo)
                                       )
                                       .GetPaged<Leaseapplication>(model.PageNumber, model.PageSize);

            }
            else if (SortOrder == 2)
            {
                data = null;
                data = await _dbContext.Leaseapplication
                                        .Include(x => x.Leaseapplicationdocuments)
                                        .Where(x => x.IsActive == 1 && x.ApprovedStatus == model.StatusId
                                        && (model.StatusId == 0 ? x.PendingAt == userId : x.PendingAt == 0)
                                        )
                                       .OrderByDescending(s =>
                                       (model.SortBy.ToUpper() == "REFNO" ? s.RefNo
                                       : model.SortBy.ToUpper() == "REGISTRATIONNO" ? s.RegistrationNo
                                       : s.RefNo)
                                       )
                                       .GetPaged<Leaseapplication>(model.PageNumber, model.PageSize);
            }
            return data;
        }
    }


}
