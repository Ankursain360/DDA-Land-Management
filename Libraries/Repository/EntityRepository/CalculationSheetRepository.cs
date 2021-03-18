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
            var result = await _dbContext.Allotmententry.Where(x => x.Id == ApplicationId).SingleOrDefaultAsync();
            var masterPremiumAmount = await _dbContext.Premiumrate.Where(x => x.FromDate <= result.AllotmentDate && x.ToDate >= result.AllotmentDate).FirstOrDefaultAsync();
            result.PremiumRate = masterPremiumAmount.PremiumRate;
            result.TotalPremiumAmount = Convert.ToDecimal(0.00024711)*masterPremiumAmount.PremiumRate * result.AllotedArea ?? 0;
            return result;
        }
    }
}
