using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
   public class DemandLetterRepository : GenericRepository<Demandletter>, IDemandLetterRepository
    {
        public DemandLetterRepository(DataContext dbContext) : base(dbContext)
        {

        }


        public async Task<List<Demandletter>> GetAllDemandletter()
        {
            return await _dbContext.Demandletter.Where(x => x.IsActive == 1).ToListAsync();
        }
        public async Task<PagedResult<Demandletter>> GetPagedDemandletter(DemandletterSearchDto model)
        {
            return await _dbContext.Demandletter.OrderByDescending(x => x.Id).GetPaged<Demandletter>(model.PageNumber, model.PageSize);
        }





    }
}
