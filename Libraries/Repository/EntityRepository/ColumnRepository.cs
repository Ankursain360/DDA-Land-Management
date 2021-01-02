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
            return await _dbContext.Column.Where(x => x.IsActive == 1).GetPaged<Column>(model.PageNumber, model.PageSize);
        }
        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Column.AnyAsync(t => t.Id != id && t.ColumnNo.ToLower() == name.ToLower());
        }
    }
}
