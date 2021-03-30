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
        Task<List<Allotteeevidenceupload>> GetAllotteeEvidenceHistoryDetails(int id);
        Task<Allotteeevidenceupload> FetchAllotteeEvidenceUploadDetails(int id);
        Task<bool> Create(Allotteeevidenceupload allotteeevidenceupload);
        Task<bool> Update(int id, Allotteeevidenceupload allotteeevidenceupload);
    }
}
