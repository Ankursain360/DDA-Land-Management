using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;

namespace Libraries.Repository.EntityRepository
{
    public class SchemeRepository : GenericRepository<Scheme>, ISchemeRepository
    {
        public SchemeRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<List<Scheme>> GetAllScheme()
        {
            return await _dbContext.Scheme.OrderByDescending(x => x.Id).ToListAsync();
        }
       
        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Scheme.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        }

        public async Task<PagedResult<Scheme>> GetPagedScheme(SchemeSearchDto model)
        {
            return await _dbContext.Scheme.GetPaged<Scheme>(model.PageNumber, model.PageSize);
        }

    }
}
