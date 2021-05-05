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
    public class ColumnRespository : GenericRepository<Column>,IColumnRepository  
    {
        public ColumnRespository(DataContext dbcontext) : base(dbcontext)
        { }

        public async Task<List<Column>> GetColumn()
        {
            return await _dbContext.Column.Where(x => x.IsActive == 1).ToListAsync();
        }

       
        public async Task<PagedResult<Column>> GetPagedColumn(ColumnSearchDto model)
        {
            var data = await _dbContext.Column
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.ColumnNo.Contains(model.name)))
                  .GetPaged<Column>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Column
                           .Where(x => (string.IsNullOrEmpty(model.name) || x.ColumnNo.Contains(model.name)))
                           .OrderBy(s => s.Id)
                           .GetPaged<Column>(model.PageNumber, model.PageSize);

                        break;
                    case ("ISACTIVE"):
                        data = null;
                        data = await _dbContext.Column
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.ColumnNo.Contains(model.name)))
                            .OrderBy(s => s.Id)
                            .GetPaged<Column>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Column
                           .Where(x => (string.IsNullOrEmpty(model.name) || x.ColumnNo.Contains(model.name)))
                           .OrderByDescending(s => s.ColumnNo)
                           .GetPaged<Column>(model.PageNumber, model.PageSize);
                        break;


                    case ("ISACTIVE"):
                        data = null;
                        data = await _dbContext.Column
                           .Where(x => (string.IsNullOrEmpty(model.name) || x.ColumnNo.Contains(model.name)))
                           .OrderByDescending(s => s.ColumnNo)
                           .GetPaged<Column>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            return data;
        }
        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Column.AnyAsync(t => t.Id != id && t.ColumnNo.ToLower() == name.ToLower());
        }
    }
}
