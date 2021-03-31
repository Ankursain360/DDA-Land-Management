

using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;

namespace Libraries.Service.IApplicationService
{
    public interface IJudgementService
    {

        public Task<PagedResult<Requestforproceeding>> GetPagedRequestForProceeding(RequestForProceedingSearchDto model);
        Task<List<UserBindDropdownDto>> BindUsernameNameList();
        Task<List<Allotmententry>> GetAllAllotment();
        Task<List<Honble>> GetAllHonble();
        Task<Requestforproceeding> FetchSingleReqDetails(int? RequestId);

        Task<List<Leasenoticegeneration>> FetchNoticeGenerationDetails(int? RequestId);
        Task<Leasenoticegeneration> FetchSingleNotice(int? id);
        
        Task<List<Allotteeevidenceupload>> FetchAllotteeEvidenceDetails(int? RequestId);
        Task<Allotteeevidenceupload> FetchSingleEvidence(int? id);
        Task<List<Hearingdetails>> FetchHearingDetails(int? RequestId);
        Task<Actiontakenbydda> FetchActionTakenByDDADetails(int? RequestId);
        Task<bool> Update(int id, Judgement judge);
        Task<bool> Create(Judgement judge);
        Task<Judgement> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<List<Judgementstatus>> GetJudgementStatusList();
    }
}
