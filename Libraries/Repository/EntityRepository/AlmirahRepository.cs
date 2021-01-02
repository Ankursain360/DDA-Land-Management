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
    public class AlmirahRespository : GenericRepository<Almirah>, IAlmirahRepository
    {
        public AlmirahRespository(DataContext dbcontext) : base(dbcontext)
        { }

        public async Task<List<Almirah>> GetAlmirah()
        {
            return await _dbContext.Almirah.Where(x => x.IsActive == 1).ToListAsync();
        }

        public async Task<PagedResult<Almirah>> GetPagedAlmirah(AlmirahSearchDto model)
        {
            return await _dbContext.Almirah.Where(x => x.IsActive == 1).GetPaged<Almirah>(model.PageNumber, model.PageSize);
        }
        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Almirah.AnyAsync(t => t.Id != id && t.AlmirahNo.ToLower() == name.ToLower());
        }
    }
}
