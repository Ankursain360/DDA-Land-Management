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
                    .Where(x => x.DueDate >= defaulterListingReportSearchDto.fromDate
                    && x.DueDate <= defaulterListingReportSearchDto.toDate)
                    .OrderByDescending(x => x.Id)

                    .GetPaged(defaulterListingReportSearchDto.PageNumber, defaulterListingReportSearchDto.PageSize);

            return data;
        }

        public Task<PagedResult<Demandletter>> GetPagedReliefReport()
        {
            throw new System.NotImplementedException();
        }


        //*******   Penalty Imposition Report**********
        public async Task<List<Locality>> GetLocalityList()
        {
            var localityList = await _dbContext.Locality.Where(x => x.IsActive == 1).ToListAsync();
            return localityList;
        }
        public async Task<List<Demandletters>> GetFileNoList()
        {
            var fileNoList = await _dbContext.Demandletters.Where(x => x.IsActive == 1).ToListAsync();
            return fileNoList;
        }
        public async Task<PagedResult<Demandletters>> GetPagedPenaltyImpositionReport(PenaltyImpositionReportSearchDto model)
        {
            var data = await _dbContext.Demandletters 
                    .Include(x => x.Locality)
                    .Where(x => (x.Id == (model.fileNo == 0 ? x.Id : model.fileNo))
                   && (x.LocalityId == (model.locality == 0 ? x.LocalityId : model.locality)))
                    .GetPaged(model.PageNumber, model.PageSize);
            return data;

        }

    }
}
