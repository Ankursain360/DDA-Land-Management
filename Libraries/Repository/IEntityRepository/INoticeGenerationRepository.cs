using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Libraries.Repository.IEntityRepository
{
    public interface INoticeGenerationRepository : IGenericRepository<Leasenoticegeneration>
    {
        Task<PagedResult<Requestforproceeding>> GetPagedRequestLetterDetails(LeaseHearingDetailsSearchDto model);
        Task<Leasenoticegeneration> FetchNoticeGenerationDetails(int id);
        Task<List<Leasenoticegeneration>> GetNoticeHistoryDetails(int id);
        Task<List<Requestforproceeding>> GetNoticeDetails();
    }
}