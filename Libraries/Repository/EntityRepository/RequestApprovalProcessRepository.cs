using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class RequestApprovalProcessRepository : GenericRepository<Request>, IRequestApprovalProcessRepository
    {

        public RequestApprovalProcessRepository(DataContext dbContext) : base(dbContext)
        {

        }



        public async Task<PagedResult<Request>> GetPagedProcessRequest(RequestApprovalSearchDto model, int userId)
        {
            var AllDataList = await _dbContext.Request.ToListAsync();
            var UserWiseDataList = AllDataList.Where(x => x.PendingAt.Split(',').Contains(userId.ToString()));
            List<int> myIdList = new List<int>();
            foreach (Request myLine in UserWiseDataList)
                myIdList.Add(myLine.Id);
            int[] myIdArray = myIdList.ToArray();

            var data = await _dbContext.Request
                                        .Include(x => x.ApprovedStatusNavigation)
                                        .Where(x => x.IsActive == 1
                                        && (model.StatusId == 0 ? x.PendingAt != "0" : x.PendingAt == "0")
                                        && (model.StatusId == 0 ? (myIdArray).Contains(x.Id) : x.PendingAt == "0")
                                        && (model.approvalstatusId == 0 ? (x.ApprovedStatus == x.ApprovedStatus) : (x.ApprovedStatus == model.approvalstatusId))
                                        )
                                        .GetPaged<Request>(model.PageNumber, model.PageSize);



            int SortOrder = (int)model.orderby;
            if (SortOrder == 1)
            {
                switch (model.colname.ToUpper())
                {
                    case ("NAME"):
                        data.Results = data.Results.OrderBy(x => x.PproposalName).ToList();
                        break;
                    case ("AREA"):
                        data.Results = data.Results.OrderBy(x => x.AreaLocality).ToList();
                        break;
                    case ("FILENO"):
                        data.Results = data.Results.OrderBy(x => x.PfileNo).ToList();
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.colname.ToUpper())
                {
                    case ("NAME"):
                        data.Results = data.Results.OrderByDescending(x => x.PproposalName).ToList();
                        break;
                    case ("AREA"):
                        data.Results = data.Results.OrderByDescending(x => x.AreaLocality).ToList();
                        break;
                    case ("FILENO"):
                        data.Results = data.Results.OrderByDescending(x => x.PfileNo).ToList();
                        break;


                }
            }
            return data;
        }

        public async Task<Request> FetchSingleResult(int id)
        {
            return await _dbContext.Request
                                    .Where(x => x.Id == id)
                                    .FirstOrDefaultAsync();
        }
        public async Task<bool> IsApplicationPendingAtUserEnd(int id, int userId)
        {
            var result = false;
            var AllDataList = await _dbContext.Request
                                                .Where(x => x.IsActive == 1 && x.Id == id)
                                                .ToListAsync();
            var UserWiseDataList = AllDataList.Where(x => x.PendingAt.Split(',').Contains(userId.ToString())).ToList();

            if (UserWiseDataList.Count == 0)
                result = false;
            else
                result = true;

            return result;
        }
        public async Task<List<Request>> GetAllRequest()
        {
            return await _dbContext.Request
                                    .Include(x => x.ApprovedStatusNavigation)
                                    .Where(x =>x.IsActive == 1)
                                    .OrderByDescending(x => x.Id)
                                    .ToListAsync();
        }

    }
}
