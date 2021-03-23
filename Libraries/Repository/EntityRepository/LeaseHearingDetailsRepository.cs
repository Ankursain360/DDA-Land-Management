using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository.Common;

namespace Libraries.Repository.EntityRepository
{
    public class LeaseHearingDetailsRepository : GenericRepository<Requestforproceeding>, ILeaseHearingDetailsRepository
    {

        public LeaseHearingDetailsRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Approvalstatus>> BindDropdownApprovalStatus()
        {
            var badCodes = new[] { 1, 2 };
            List<Approvalstatus> ApprovalstatusList = await _dbContext.Approvalstatus
                                                                        .Where(x => x.IsActive == 1 && badCodes.Contains(x.Id))
                                                                        .ToListAsync();
            return ApprovalstatusList;
        }

        public async Task<Requestforproceeding> FetchRequestforproceedingData(int id)
        {
            return await _dbContext.Requestforproceeding
                                        .Include(x => x.Allotment)
                                        .Include(x => x.Allotment.Application)
                                        .Where(x => x.IsActive == 1 && x.Id == id)
                                        .FirstOrDefaultAsync();
        }

        public async Task<PagedResult<Requestforproceeding>> GetPagedRequestLetterDetails(LeaseHearingDetailsSearchDto model, int userId)
        {
            var data = await _dbContext.Requestforproceeding
                                        .Include(x => x.Allotment)
                                        .Include(x => x.Allotment.Application)
                                        .Where(x => x.IsActive == 1
                                        && (x.Allotment.Application.RefNo != null ? x.Allotment.Application.RefNo.Contains(model.refno == "" ? x.Allotment.Application.RefNo : model.refno) : true)
                                        && (x.Allotment.Application.Name != null ? x.Allotment.Application.Name.Contains(model.name == "" ? x.Allotment.Application.Name : model.name) : true)
                                        && (x.PendingAt == userId)
                                        )
                                        .GetPaged<Requestforproceeding>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                data = null;
                if (model.SortBy.ToUpper() == "ISACTIVE")
                {
                    data = await _dbContext.Requestforproceeding
                                            .Include(x => x.Allotment)
                                            .Include(x => x.Allotment.Application)
                                            .Where(x => x.IsActive == 1
                                            && (x.Allotment.Application.RefNo != null ? x.Allotment.Application.RefNo.Contains(model.refno == "" ? x.Allotment.Application.RefNo : model.refno) : true)
                                            && (x.Allotment.Application.Name != null ? x.Allotment.Application.Name.Contains(model.name == "" ? x.Allotment.Application.Name : model.name) : true)
                                            && (x.PendingAt == userId)
                                            )
                                   .OrderByDescending(s => s.IsActive)
                                   .GetPaged<Requestforproceeding>(model.PageNumber, model.PageSize);
                }
                else
                {
                    data = null;
                    data = await _dbContext.Requestforproceeding
                                            .Include(x => x.Allotment)
                                            .Include(x => x.Allotment.Application)
                                            .Where(x => x.IsActive == 1
                                            && (x.Allotment.Application.RefNo != null ? x.Allotment.Application.RefNo.Contains(model.refno == "" ? x.Allotment.Application.RefNo : model.refno) : true)
                                            && (x.Allotment.Application.Name != null ? x.Allotment.Application.Name.Contains(model.name == "" ? x.Allotment.Application.Name : model.name) : true)
                                            && (x.PendingAt == userId)
                                            )
                                           .OrderBy(s =>
                                           (model.SortBy.ToUpper() == "REFNO" ? (s.Allotment == null ? null : s.Allotment.Application == null ? null : s.Allotment.Application.RefNo)
                                           : model.SortBy.ToUpper() == "SOCIETYNAME" ? (s.Allotment == null ? null : s.Allotment.Application == null ? null : s.Allotment.Application.Name)
                                           : (s.Allotment == null ? null : s.Allotment.Application == null ? null : s.Allotment.Application.RefNo))
                                           )
                                            .GetPaged<Requestforproceeding>(model.PageNumber, model.PageSize);
                }

            }
            else if (SortOrder == 2)
            {
                data = null;
                if (model.SortBy.ToUpper() == "ISACTIVE")
                {
                    data = await _dbContext.Requestforproceeding
                                            .Include(x => x.Allotment)
                                            .Include(x => x.Allotment.Application)
                                            .Where(x => x.IsActive == 1
                                            && (x.Allotment.Application.RefNo != null ? x.Allotment.Application.RefNo.Contains(model.refno == "" ? x.Allotment.Application.RefNo : model.refno) : true)
                                            && (x.Allotment.Application.Name != null ? x.Allotment.Application.Name.Contains(model.name == "" ? x.Allotment.Application.Name : model.name) : true)
                                            && (x.PendingAt == userId)
                                            )
                                   .OrderBy(s => s.IsActive)
                                   .GetPaged<Requestforproceeding>(model.PageNumber, model.PageSize);
                }
                else
                {
                    data = null;
                    data = await _dbContext.Requestforproceeding
                                            .Include(x => x.Allotment)
                                            .Include(x => x.Allotment.Application)
                                            .Where(x => x.IsActive == 1
                                            && (x.Allotment.Application.RefNo != null ? x.Allotment.Application.RefNo.Contains(model.refno == "" ? x.Allotment.Application.RefNo : model.refno) : true)
                                            && (x.Allotment.Application.Name != null ? x.Allotment.Application.Name.Contains(model.name == "" ? x.Allotment.Application.Name : model.name) : true)
                                            && (x.PendingAt == userId)
                                            )
                                           .OrderByDescending(s =>
                                           (model.SortBy.ToUpper() == "REFNO" ? (s.Allotment == null ? null : s.Allotment.Application == null ? null : s.Allotment.Application.RefNo)
                                           : model.SortBy.ToUpper() == "SOCIETYNAME" ? (s.Allotment == null ? null : s.Allotment.Application == null ? null : s.Allotment.Application.Name)
                                           : (s.Allotment == null ? null : s.Allotment.Application == null ? null : s.Allotment.Application.RefNo))
                                           )
                                            .GetPaged<Requestforproceeding>(model.PageNumber, model.PageSize);
                }
            }
            return data;
        }
    }
}
