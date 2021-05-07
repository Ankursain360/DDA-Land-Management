
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IJudgementRepository : IGenericRepository<Judgement>
    {
        public Task<PagedResult<Requestforproceeding>> GetPagedRequestForProceeding(RequestForProceedingSearchDto model);

        Task<List<Allotmententry>> GetAllAllotment();
        Task<List<Honble>> GetAllHonble();
        Task<List<UserBindDropdownDto>> BindUsernameNameList();
        Task<Requestforproceeding> FetchSingleReqDetails(int? RequestId);
        Task<List<Leasenoticegeneration>> FetchNoticeGenerationDetails(int? RequestId);
        Task<Leasenoticegeneration> FetchSingleNotice(int? id);

        Task<List<Allotteeevidenceupload>> FetchAllotteeEvidenceDetails(int? RequestId); 
        Task<Allotteeevidenceupload> FetchSingleEvidence(int? id);
        Task<List<Hearingdetails>> FetchHearingDetails(int? RequestId);
        Task<Actiontakenbydda> FetchActionTakenByDDADetails(int? RequestId);

        //****  For Judgement page  ********

        Task<List<Judgement>> GetAllJudgement();
       
        Task<Judgement> FetchSingleResult(int id);
        Task<List<Judgementstatus>> GetJudgementStatusList();
        Task<List<Requestforproceeding>> GetAllJudgementIndex();
    }
}
