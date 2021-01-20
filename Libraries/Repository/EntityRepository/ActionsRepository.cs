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
using Microsoft.EntityFrameworkCore;


namespace Libraries.Repository.EntityRepository
{
    public class ActionsRepository : GenericRepository<Actions>, IActionsRepository
    {

        public ActionsRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<PagedResult<Actions>> GetPagedActions(ActionsSearchDto model)
        {
            //return await _dbContext.Actions.GetPaged<Actions>(model.PageNumber, model.PageSize);
            var data = await _dbContext.Actions
                             .Where(x => (string.IsNullOrEmpty(model.Name) || x.Name.Contains(model.Name))
                            && (string.IsNullOrEmpty(model.Icon) || x.Icon.Contains(model.Icon))
                             && (string.IsNullOrEmpty(model.Color) || x.Color.Contains(model.Color)))
                               //  .OrderByDescending(x => x.IsActive)
                                 .GetPaged<Actions>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data.Results = data.Results.OrderBy(x => x.Name).ToList();
                        break;
                    case ("ICON"):
                        data.Results = data.Results.OrderBy(x => x.Icon).ToList();
                        break;
                    case ("COLOR"):
                        data.Results = data.Results.OrderBy(x => x.Color).ToList();
                        break;
                   
                    case ("ISACTIVE"):
                        data.Results = data.Results.OrderBy(x => x.IsActive==0).ToList();
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
                    case ("ICON"):
                        data.Results = data.Results.OrderByDescending(x => x.Icon).ToList();
                        break;
                    case ("COLOR"):
                        data.Results = data.Results.OrderByDescending(x => x.Color).ToList();
                        break;
                   
                    case ("ISACTIVE"):
                        data.Results = data.Results.OrderByDescending(x => x.IsActive==0).ToList();
                        break;
                }
            }
            return data;
        }

        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Actions.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        }
    }
}
