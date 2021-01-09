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
   public class DemandLetterRepository : GenericRepository<Demandletters>, IDemandLetterRepository
    {
        public DemandLetterRepository(DataContext dbContext) : base(dbContext)
        {

        }


        public async Task<List<Demandletters>> GetAllDemandletter()
        {
            return await _dbContext.Demandletters.Where(x => x.IsActive == 1).ToListAsync();
        }
        public async Task<PagedResult<Demandletters>> GetPagedDemandletter(DemandletterSearchDto model)
        {
            return await _dbContext.Demandletters.OrderByDescending(x => x.Id).GetPaged<Demandletters>(model.PageNumber, model.PageSize);
        }
        public async Task<PagedResult<Demandletter>> GetDefaultListingReportData(DefaulterListingReportSearchDto defaulterListingReportSearchDto)
        {
            var data = await _dbContext.Demandletter
                    .Where(x => x.CreatedDate >= defaulterListingReportSearchDto.fromDate
                    && x.CreatedDate <= defaulterListingReportSearchDto.toDate)
                    .OrderByDescending(x => x.Id)

                    .GetPaged(defaulterListingReportSearchDto.PageNumber, defaulterListingReportSearchDto.PageSize);

            return data;
        }





    }
}
