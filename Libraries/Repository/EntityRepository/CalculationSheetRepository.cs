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
    
    public class CalculationSheetRepository : GenericRepository<Allotmententry>, ICalculationSheetRepository
    {
        public CalculationSheetRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<List<Allotmententry>> GetAllApplications()
        {
            List<Allotmententry> list = await _dbContext.Allotmententry.Include(x => x.Application).ToListAsync();
            return list;
        }
       

        public async Task<Allotmententry> FetchSingleAppAreaDetails(int? ApplicationId)
        {
            return await _dbContext.Allotmententry.Where(x => x.ApplicationId == ApplicationId).SingleOrDefaultAsync();
        }
    }
}
