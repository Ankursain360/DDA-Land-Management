using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface IDemandLetterService : IEntityService<Demandletters>
    {
        Task<PagedResult<Demandletters>> GetPagedDemandletter(DemandletterSearchDto model);
        Task<List<Demandletters>> GetAllDemandletter();
        Task<PagedResult<Demandletter>> GetDefaultListingReportData(DefaulterListingReportSearchDto defaulterListingReportSearchDto);

        Task<bool> Update(int id, Demandletters demandletter);
        Task<bool> Create(Demandletters demandletter);
        Task <Demandletters> FetchSingleResult(int id);
        Task<List<Demandletters>> BindFileNoList();
        Task<PagedResult<Demandletters>> GetPagedReliefReport(ReliefReportSearchDto model);
        Task<List<Locality>> BindLoclityList();
    }
}
