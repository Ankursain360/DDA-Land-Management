
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
            var AllDataList = await _dbContext.Extension.ToListAsync();
            var UserWiseDataList = AllDataList.Where(x => x.PendingAt.Split(',').Contains(userId.ToString()));
            List<int> myIdList = new List<int>();
            foreach (Extension myLine in UserWiseDataList)
                myIdList.Add(myLine.Id);
            int[] myIdArray = myIdList.ToArray();

            var data = await _dbContext.Extension
                                       .Include(x => x.Allotment)
                                       .Include(x => x.Allotment.Application)
                                       .Include(x => x.ApprovedStatusNavigation)
                                       .Where(x => x.IsActive == 1
                                        && (model.StatusId == 0 ? x.PendingAt != "0" : x.PendingAt == "0")
                                        && (model.StatusId == 0 ? (myIdArray).Contains(x.Id) : x.PendingAt == "0")
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
                                        .Include(x => x.ApprovedStatusNavigation)
                                        .Where(x => x.IsActive == 1
                                         && (model.StatusId == 0 ? x.PendingAt != "0" : x.PendingAt == "0")
                                         && (model.StatusId == 0 ? (myIdArray).Contains(x.Id) : x.PendingAt == "0")
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
                                       .Include(x => x.ApprovedStatusNavigation)
                                       .Where(x => x.IsActive == 1
                                        && (model.StatusId == 0 ? x.PendingAt != "0" : x.PendingAt == "0")
                                        && (model.StatusId == 0 ? (myIdArray).Contains(x.Id) : x.PendingAt == "0")
                                        )
                                       .OrderByDescending(x => x.Allotment.Application.RefNo)
                                       .GetPaged<Extension>(model.PageNumber, model.PageSize);
                      

                        break;
                   
                }
            }
            return data;

        }

        public async Task<bool> IsApplicationPendingAtUserEnd(int id, int userId)
        {
            var result = false;
            var AllDataList = await _dbContext.Extension
                                                .Where(x => x.IsActive == 1 && x.Id == id)
                                                .ToListAsync();
            var UserWiseDataList = AllDataList.Where(x => x.PendingAt.Split(',').Contains(userId.ToString())).ToList();

            if (UserWiseDataList.Count == 0)
                result = false;
            else
                result = true;

            return result;
        }
    }


}
