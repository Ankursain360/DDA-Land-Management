using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;

namespace Libraries.Repository.EntityRepository
{
    public class RowRepository : GenericRepository<Row>, IRowRepository 
    {
        public RowRepository(DataContext dbcontext) : base(dbcontext)
        { }

        public async Task<List<Row>> GetRow()
        {
            return await _dbContext.Row.Where(x => x.IsActive == 1).ToListAsync();
        }

        public async Task<PagedResult<Row>> GetPagedRow(RowSearchDto model)
        {
            return await _dbContext.Row.Where(x => x.IsActive == 1).GetPaged<Row>(model.PageNumber, model.PageSize);
        }
        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Row.AnyAsync(t => t.Id != id && t.RowNo.ToLower() == name.ToLower());
        }


    }
}
