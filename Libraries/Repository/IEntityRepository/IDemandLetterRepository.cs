using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IDemandLetterRepository : IGenericRepository<Demandletter>
    {
        Task<PagedResult<Demandletter>> GetPagedDemandletter(DemandletterSearchDto model);
        Task<List<Demandletter>> GetAllDemandletter();
        Task<PagedResult<Demandletter>> GetDefaultListingReportData(DefaulterListingReportSearchDto defaulterListingReportSearchDto);

    }
}
