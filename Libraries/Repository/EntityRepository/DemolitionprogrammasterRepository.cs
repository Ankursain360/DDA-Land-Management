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
    public class DemolitionprogrammasterRepository : GenericRepository<Demolitionprogram>, IDemolitionprogrammasterRepository
    {
        public DemolitionprogrammasterRepository(DataContext dbcontext) : base(dbcontext)
        { }



        public async Task<List<Demolitionprogram>> GetDemolitionprogrammaster()
        {
            return await _dbContext.Demolitionprogrammaster.Where(x => x.IsActive == 1).ToListAsync();
        }

        public async Task<PagedResult<Demolitionprogram>> GetPagedDemolitionprogrammaster(DemolitionprogrammasterSearchDto model)
        {
            return await _dbContext.Demolitionprogrammaster.Where(x => x.IsActive == 1).GetPaged<Demolitionprogram>(model.PageNumber, model.PageSize);
        }



    }
}
