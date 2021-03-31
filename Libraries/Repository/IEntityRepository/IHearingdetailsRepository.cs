using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;

namespace Libraries.Repository.IEntityRepository
{
    public interface IHearingdetailsRepository : IGenericRepository<Hearingdetails>
    {
        Task<bool> DeleteHphotofiledetails(int Id);
        Task<Hearingdetailsphotofiledetails> GetHphotofiledetails(int hid);
        Task<bool> SaveHearingphotofiledetails(Hearingdetailsphotofiledetails hearingdetailsphotofiledetails);
        Task<List<Requestforproceeding>> GetAllRequestforproceeding();
        Task<List<Leasenoticegeneration>> GetAllLeasenoticegeneration(int? AppId);
        Task<PagedResult<Hearingdetails>> GetPagedHearingDetails(HearingdetailsSeachDto model);
        public Task<PagedResult<Requestforproceeding>> GetPagedRequestForProceeding(RequestForProceedingSearchDto model);

        Task<List<Allotmententry>> GetAllAllotment();
        Task<List<Honble>> GetAllHonble();
        
        Task<Requestforproceeding> FetchSingleReqDetails(int? RequestId);
        Task<List<Leasenoticegeneration>> FetchNoticeGenerationDetails(int? RequestId);
        Task<Leasenoticegeneration> FetchSingleNotice(int? id);

        Task<List<Allotteeevidenceupload>> FetchAllotteeEvidenceDetails(int? RequestId);
        Task<Allotteeevidenceupload> FetchSingleEvidence(int? id);
    }
}
