using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Libraries.Service.IApplicationService
{
    public interface INoticeGenerationService : IEntityService<Leasenoticegeneration>
    {
        Task<PagedResult<Requestforproceeding>> GetPagedRequestLetterDetails(LeaseHearingDetailsSearchDto model);
        Task<Leasenoticegeneration> FetchNoticeGenerationDetails(int id);
        Task<List<Leasenoticegeneration>> GetNoticeHistoryDetails(int id);
        Task<bool> Create(Leasenoticegeneration leasenoticegeneration);
        Task<bool> Update(int id, Leasenoticegeneration leasenoticegeneration);
    }
}
