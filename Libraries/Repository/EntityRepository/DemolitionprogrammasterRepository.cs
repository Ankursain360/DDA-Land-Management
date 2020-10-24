using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace Repository.EntityRepository
{
    public class DemolitionprogrammasterRepository : GenericRepository<Demolitionprogrammaster>, IDemolitionprogrammasterRepository
    {
        public DemolitionprogrammasterRepository(DataContext dbcontext) : base(dbcontext)
        { }



        public async Task<List<Demolitionprogrammaster>> GetDemolitionprogrammaster()
        {
            return await _dbContext.Demolitionprogrammaster.Where(x => x.IsActive == 1).ToListAsync();
        }

        public async Task<PagedResult<Demolitionprogrammaster>> GetPagedDemolitionprogrammaster(DemolitionprogrammasterSearchDto model)
        {
            return await _dbContext.Demolitionprogrammaster.Where(x => x.IsActive == 1).GetPaged<Demolitionprogrammaster>(model.PageNumber, model.PageSize);
        }



    }
}
