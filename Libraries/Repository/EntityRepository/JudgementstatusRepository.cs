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

    public class JudgementstatusRepository : GenericRepository<Judgementstatus>, IJudgementstatusRepository
    {
        public JudgementstatusRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<PagedResult<Judgementstatus>> GetPagedJudgementstatus(JudgementstatusSearchDto model)
        {
            var data = await _dbContext.Judgementstatus
                       .GetPaged<Judgementstatus>(model.PageNumber, model.PageSize);

            if (model.SortBy == null)
            {
                model.SortBy = "Type";
            }
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("Status"):
                        data = null;
                        data = await _dbContext.Judgementstatus
                                                   .Where(x => (string.IsNullOrEmpty(model.status) || x.Status.Contains(model.status)))
                                               .OrderBy(x => x.Status)
                                               .GetPaged<Judgementstatus>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Judgementstatus
                                                   .Where(x => (string.IsNullOrEmpty(model.status) || x.Status.Contains(model.status)))
                                               .OrderByDescending(x => x.IsActive)
                                               .GetPaged<Judgementstatus>(model.PageNumber, model.PageSize);
                        break;


                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("Status"):
                        data = null;
                        data = await _dbContext.Judgementstatus
                                                   .Where(x => (string.IsNullOrEmpty(model.status) || x.Status.Contains(model.status)))
                                               .OrderByDescending(x => x.Status)
                                               .GetPaged<Judgementstatus>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Judgementstatus
                                                   .Where(x => (string.IsNullOrEmpty(model.status) || x.Status.Contains(model.status)))
                                               .OrderBy(x => x.IsActive)
                                               .GetPaged<Judgementstatus>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            return data;

        }
        public async Task<List<Judgementstatus>> GetAllJudgementstatus()
        {
            return await _dbContext.Judgementstatus.Where(x => x.IsActive == 1).ToListAsync();
        }

    }
}
