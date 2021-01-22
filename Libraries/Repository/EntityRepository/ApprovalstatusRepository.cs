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
                            .GetPaged<Approvalstatus>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Approvalstatus
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))
                            .OrderBy(x => x.Name)
                            .GetPaged<Approvalstatus>(model.PageNumber, model.PageSize);
                        break;

                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Approvalstatus
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))
                            .OrderByDescending(x => x.IsActive)
                            .GetPaged<Approvalstatus>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Approvalstatus
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))
                            .OrderByDescending(x => x.Name)
                            .GetPaged<Approvalstatus>(model.PageNumber, model.PageSize);
                        break;

                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Approvalstatus
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))
                            .OrderBy(x => x.IsActive)
                            .GetPaged<Approvalstatus>(model.PageNumber, model.PageSize);
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
