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
            var data = await _dbContext.Request.
             
               Where(x => x.IsActive == 1 && x.ApprovedStatus == model.StatusId 
               //&& (model.StatusId == 0 ? x.PendingAt == userId : x.PendingAt == 0)
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











    }
}
