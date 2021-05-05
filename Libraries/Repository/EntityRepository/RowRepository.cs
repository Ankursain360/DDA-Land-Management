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
            var data = await _dbContext.Row
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.RowNo.Contains(model.name)))
                  .GetPaged<Row>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Row
                           .Where(x => (string.IsNullOrEmpty(model.name) || x.RowNo.Contains(model.name)))
                           .OrderBy(s => s.Id)
                           .GetPaged<Row>(model.PageNumber, model.PageSize);

                        break;
                    case ("ISACTIVE"):
                        data = null;
                        data = await _dbContext.Row
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.RowNo.Contains(model.name)))
                           .OrderBy(s => s.Id)
                            .GetPaged<Row>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Row
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.RowNo.Contains(model.name)))
                           .OrderBy(s => s.Id)
                            .GetPaged<Row>(model.PageNumber, model.PageSize);
                        break;


                    case ("ISACTIVE"):
                        data = null;
                        data = await _dbContext.Row
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.RowNo.Contains(model.name)))
                           .OrderBy(s => s.Id)
                            .GetPaged<Row>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            return data;
        }


      

        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Row.AnyAsync(t => t.Id != id && t.RowNo.ToLower() == name.ToLower());
        }


    }
}
