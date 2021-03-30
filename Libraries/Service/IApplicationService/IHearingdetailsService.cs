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
        Task<bool> Delete(int id);
        Task<Hearingdetails> FetchSingleResult(int id);
        Task<bool> Update(int id, Hearingdetails hearingdetails);
        Task<bool> Create(Hearingdetails hearingdetails);
        Task<List<Leasenoticegeneration>> GetAllLeasenoticegeneration(int? AppId);
        Task<List<Requestforproceeding>> GetAllRequestforproceeding();
        Task<PagedResult<Hearingdetails>> GetPagedHearingDetails(HearingdetailsSeachDto model);
        Task<bool> SaveHearingphotofiledetails(Hearingdetailsphotofiledetails hearingdetailsphotofiledetails);
        Task<Hearingdetailsphotofiledetails> GetHphotofiledetails(int hid);
        Task<bool> DeleteHphotofiledetails(int Id);
        Task<Requestforproceeding> FetchSingleResultReq(int id);
    }
}
