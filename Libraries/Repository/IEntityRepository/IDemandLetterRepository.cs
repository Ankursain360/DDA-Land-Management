using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IDemandLetterRepository : IGenericRepository<Demandletters>
    {
        Task<PagedResult<Demandletters>> GetPagedDemandletter(DemandletterSearchDto model);
        Task<List<Demandletters>> GetAllDemandletter();
        Task<PagedResult<Demandletter>> GetDefaultListingReportData(DefaulterListingReportSearchDto defaulterListingReportSearchDto);

    }
}
