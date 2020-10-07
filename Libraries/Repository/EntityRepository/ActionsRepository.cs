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
            return await _dbContext.Actions.GetPaged<Actions>(model.PageNumber, model.PageSize);
        }

        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Actions.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        }
    }
}
