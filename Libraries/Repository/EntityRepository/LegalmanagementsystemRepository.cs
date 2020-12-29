using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
   public class LegalmanagementsystemRepository : GenericRepository<Legalmanagementsystem>, ILegalmanagementsystemRepository
    {
        public LegalmanagementsystemRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<List<Locality>> GetLocalityList()
        {
            var localityList = await _dbContext.Locality.Where(x => x.IsActive == 1).ToListAsync();
            return localityList;
        }
    }
}
