using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface IDemandLetterService : IEntityService<Demandletter>
    {
        Task<PagedResult<Demandletter>> GetPagedDemandletter(DemandletterSearchDto model);
        Task<List<Demandletter>> GetAllDemandletter();
        Task<PagedResult<Demandletter>> GetDefaultListingReportData(DefaulterListingReportSearchDto defaulterListingReportSearchDto);

        Task<bool> Update(int id, Demandletter demandletter);
        Task<bool> Create(Demandletter demandletter);
        Task <Demandletter> FetchSingleResult(int id);

    }
}
