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
    public interface IAllotteeEvidenceUploadService : IEntityService<Allotteeevidenceupload>
    {
        Task<PagedResult<Requestforproceeding>> GetPagedRequestLetterDetails(AllotteeEvidenceSearchDto model);
        //Task<Leasenoticegeneration> FetchNoticeGenerationDetails(int id);
        Task<List<Allotteeevidenceupload>> GetAllotteeEvidenceHistoryDetails(int id);
        //Task<bool> Create(Leasenoticegeneration leasenoticegeneration);
        //Task<bool> Update(int id, Leasenoticegeneration leasenoticegeneration);
    }
}
