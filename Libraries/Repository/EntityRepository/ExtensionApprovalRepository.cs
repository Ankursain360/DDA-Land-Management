
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
    public class ExtensionApprovalRepository : GenericRepository<Extension>, IExtensionApprovalRepository
    {

        public ExtensionApprovalRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<Extension> FetchSingleResult(int id)
        {
            return await _dbContext.Extension
                                   .Include(x => x.Allotment)
                                    .Include(x => x.Allotment.Application)
                                    .Where(x => x.Id == id)
                                    .FirstOrDefaultAsync();
        }

        public async Task<PagedResult<Extension>> GetPagedExtensionDetails(ExtensionApprovalSearchDto model, int userId)
        {
            var data = await _dbContext.Extension
                                       .Include(x => x.Allotment)
                                       .Include(x => x.Allotment.Application)
                                        .Where(x => x.IsActive == 1 && x.ApprovedStatus == model.StatusId
                                        && (model.StatusId == 0 ? x.PendingAt == Convert.ToString(userId) : x.PendingAt == "0")
                                        )
                                        .GetPaged<Extension>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("REFNO"):
                        data = null;
                        data = await _dbContext.Extension
                                       .Include(x => x.Allotment)
                                       .Include(x => x.Allotment.Application)
                                        .Where(x => x.IsActive == 1 && x.ApprovedStatus == model.StatusId
                                        // && (model.StatusId == 0 ? x.ConvertPendingAt == userId : x.PendingAt == 0)
                                        && (model.StatusId == 0 ? x.PendingAt == Convert.ToString(userId) : x.PendingAt == "0")
                                       // && (string.IsNullOrEmpty(model.name) || x.Allotment.Application.RefNo.Contains(model.name))
                                        )
                                        .OrderBy(x => x.Allotment.Application.RefNo)
                                        .GetPaged<Extension>(model.PageNumber, model.PageSize);
                        

                        break;
                   

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("REFNO"):
                        data = null;
                        data = await _dbContext.Extension
                                      .Include(x => x.Allotment)
                                      .Include(x => x.Allotment.Application)
                                       .Where(x => x.IsActive == 1 && x.ApprovedStatus == model.StatusId
                                       // && (model.StatusId == 0 ? x.ConvertPendingAt == userId : x.PendingAt == 0)
                                       && (model.StatusId == 0 ? x.PendingAt == Convert.ToString(userId) : x.PendingAt == "0")
                                      // && (string.IsNullOrEmpty(model.name) || x.Allotment.Application.RefNo.Contains(model.name))
                                       )
                                       .OrderByDescending(x => x.Allotment.Application.RefNo)
                                       .GetPaged<Extension>(model.PageNumber, model.PageSize);
                      

                        break;
                   
                }
            }
            return data;

        }
    }


}
