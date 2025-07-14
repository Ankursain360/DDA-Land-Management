using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class FeedbackRepository : GenericRepository<tblfeedback>, IFeedbackRepository
    {
        public FeedbackRepository(DataContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Tblfeedbacks.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        } 

        public async Task<PagedResult<tblfeedback>> GetPagedResult(FeedbackSearchDto model)
        {
            var data = await _dbContext.Tblfeedbacks
                   .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))
                  .GetPaged<tblfeedback>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Tblfeedbacks
                           .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))
                           .OrderBy(s => s.Name)
                           .GetPaged<tblfeedback>(model.PageNumber, model.PageSize);

                        break;
                    case ("ISACTIVE"):
                        data = null;
                        data = await _dbContext.Tblfeedbacks
                           .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))
                           .OrderByDescending(x => x.IsActive)
                           .GetPaged<tblfeedback>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Tblfeedbacks
                           .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))
                           .OrderByDescending(s => s.Name)
                           .GetPaged<tblfeedback>(model.PageNumber, model.PageSize);
                        break;

                    case ("ISACTIVE"):
                        data = null;
                        data = await _dbContext.Tblfeedbacks
                           .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))
                           .OrderBy(x => x.IsActive)
                           .GetPaged<tblfeedback>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            return data;
        }

        public async Task<List<tblfeedback>> GetTblfeedbacks()
        {
            return await _dbContext.Tblfeedbacks.Where(x => x.IsActive == 1).ToListAsync();
        }
    }
}
