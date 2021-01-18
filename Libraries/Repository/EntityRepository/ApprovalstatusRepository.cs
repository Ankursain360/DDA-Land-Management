using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Repository.Common;

namespace Libraries.Repository.EntityRepository
{
   public class ApprovalstatusRepository : GenericRepository<Approvalstatus>, IApprovalstatusRepository
    {
        public ApprovalstatusRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Approvalstatus>> GetPagedApprovalStatus(ApprovalstatusSearchDto model)
        {
            var data = await _dbContext.Approvalstatus
                       
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))
                            .OrderByDescending(s => s.IsActive)
                            .ThenBy(s => s.Name)
                           
                        .GetPaged<Approvalstatus>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data.Results = data.Results.OrderBy(x => x.Name).ToList();
                        break;
                    case ("STATUS"):
                        data.Results = data.Results
                                       .OrderBy(x => x.IsActive == 0).ToList();
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data.Results = data.Results.OrderByDescending(x => x.Name).ToList();
                        break;
                    case ("STATUS"):
                        data.Results = data.Results
                                       .OrderByDescending(x => x.IsActive == 0).ToList();
                        break;
                }
            }

            return data;
        }
        public async Task<List<Approvalstatus>> GetAllApprovedstatus()
        {
            var data = await _dbContext.Approvalstatus.OrderBy(x => x.Id).ToListAsync();
            return data;
        }

    }
}
