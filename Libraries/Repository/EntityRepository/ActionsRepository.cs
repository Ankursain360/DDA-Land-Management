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
                             .GetPaged<Actions>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Actions
                             .Where(x => (string.IsNullOrEmpty(model.Name) || x.Name.Contains(model.Name))
                             && (string.IsNullOrEmpty(model.Icon) || x.Icon.Contains(model.Icon))
                             && (string.IsNullOrEmpty(model.Color) || x.Color.Contains(model.Color)))
                             .OrderBy(s => s.Name)
                             .GetPaged<Actions>(model.PageNumber, model.PageSize);
                        break;
                    case ("ICON"):
                        data = null;
                        data = await _dbContext.Actions
                             .Where(x => (string.IsNullOrEmpty(model.Name) || x.Name.Contains(model.Name))
                             && (string.IsNullOrEmpty(model.Icon) || x.Icon.Contains(model.Icon))
                             && (string.IsNullOrEmpty(model.Color) || x.Color.Contains(model.Color)))
                             .OrderBy(s => s.Icon)
                             .GetPaged<Actions>(model.PageNumber, model.PageSize);
                        break;
                    case ("COLOR"):
                        data = null;
                        data = await _dbContext.Actions
                             .Where(x => (string.IsNullOrEmpty(model.Name) || x.Name.Contains(model.Name))
                             && (string.IsNullOrEmpty(model.Icon) || x.Icon.Contains(model.Icon))
                             && (string.IsNullOrEmpty(model.Color) || x.Color.Contains(model.Color)))
                             .OrderBy(s => s.Color)
                             .GetPaged<Actions>(model.PageNumber, model.PageSize);
                        break;
                   
                    case ("ISACTIVE"):
                        data = null;
                        data = await _dbContext.Actions
                             .Where(x => (string.IsNullOrEmpty(model.Name) || x.Name.Contains(model.Name))
                             && (string.IsNullOrEmpty(model.Icon) || x.Icon.Contains(model.Icon))
                             && (string.IsNullOrEmpty(model.Color) || x.Color.Contains(model.Color)))
                             .OrderByDescending(s => s.IsActive)
                             .GetPaged<Actions>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Actions
                             .Where(x => (string.IsNullOrEmpty(model.Name) || x.Name.Contains(model.Name))
                             && (string.IsNullOrEmpty(model.Icon) || x.Icon.Contains(model.Icon))
                             && (string.IsNullOrEmpty(model.Color) || x.Color.Contains(model.Color)))
                             .OrderByDescending(s => s.Name)
                             .GetPaged<Actions>(model.PageNumber, model.PageSize);
                        break;
                    case ("ICON"):
                        data = null;
                        data = await _dbContext.Actions
                             .Where(x => (string.IsNullOrEmpty(model.Name) || x.Name.Contains(model.Name))
                             && (string.IsNullOrEmpty(model.Icon) || x.Icon.Contains(model.Icon))
                             && (string.IsNullOrEmpty(model.Color) || x.Color.Contains(model.Color)))
                             .OrderByDescending(s => s.Icon)
                             .GetPaged<Actions>(model.PageNumber, model.PageSize);
                        break;
                    case ("COLOR"):
                        data = null;
                        data = await _dbContext.Actions
                             .Where(x => (string.IsNullOrEmpty(model.Name) || x.Name.Contains(model.Name))
                             && (string.IsNullOrEmpty(model.Icon) || x.Icon.Contains(model.Icon))
                             && (string.IsNullOrEmpty(model.Color) || x.Color.Contains(model.Color)))
                             .OrderByDescending(s => s.Color)
                             .GetPaged<Actions>(model.PageNumber, model.PageSize);
                        break;

                    case ("ISACTIVE"):
                        data = null;
                        data = await _dbContext.Actions
                             .Where(x => (string.IsNullOrEmpty(model.Name) || x.Name.Contains(model.Name))
                             && (string.IsNullOrEmpty(model.Icon) || x.Icon.Contains(model.Icon))
                             && (string.IsNullOrEmpty(model.Color) || x.Color.Contains(model.Color)))
                             .OrderBy(s => s.IsActive)
                             .GetPaged<Actions>(model.PageNumber, model.PageSize);
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
