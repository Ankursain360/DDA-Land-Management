using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;

namespace Libraries.Service.IApplicationService
{
    public interface IHearingdetailsService : IEntityService<Hearingdetails>
    {
        
        string GetDownload(int id);
        Task<bool> Delete(int id);
        Task<Hearingdetails> FetchSingleResult(int id);
        Task<bool> Update(int id, Hearingdetails hearingdetails);
        Task<bool> Create(Hearingdetails hearingdetails);
        Task<List<Leasenoticegeneration>> GetAllLeasenoticegeneration(int? AppId);
        Task<List<Requestforproceeding>> GetAllRequestforproceeding();
        Task<PagedResult<Hearingdetails>> GetPagedHearingDetails(HearingdetailsSeachDto model);
        Task<bool> SaveHearingphotofiledetails(Hearingdetailsphotofiledetails hearingdetailsphotofiledetails);
        Task<Hearingdetailsphotofiledetails> GetHphotofiledetails(int hid);
        Task<Hearingdetails> FetchSingleHearingdetailswithReqProc(int? RequestId);
      //  Task<bool> DeleteHphotofiledetails(int Id);
        Task<Requestforproceeding> FetchSingleResultReq(int id);
       Task<PagedResult<Requestforproceeding>> GetPagedRequestForProceeding(HearingdetailsSeachDto model);

        Task<List<Allotmententry>> GetAllAllotment();
        Task<List<Honble>> GetAllHonble();

        Task<Requestforproceeding> FetchSingleReqDetails(int? RequestId);
        Task<List<Leasenoticegeneration>> FetchNoticeGenerationDetails(int? RequestId);
        Task<Leasenoticegeneration> FetchSingleNotice(int? id);

        Task<List<Allotteeevidenceupload>> FetchAllotteeEvidenceDetails(int? RequestId);
        Task<Allotteeevidenceupload> FetchSingleEvidence(int? id);
        Task<List<Requestforproceeding>> GetHearingDetails();
    }
}
