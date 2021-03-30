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
    }
}
