using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
        public interface IUndersection22plotdetailsRepository : IGenericRepository<Undersection22plotdetails>
        {
        Task<List<Undersection22plotdetails>> GetAllUS22PlotDetails();
        Task<List<Acquiredlandvillage>> GetAllAcquiredlandvillage();
        Task<List<Khasra>> GetAllKhasra(int? villageId);
        Task<List<Undersection4>> GetAllUndersection4();
        Task<List<Undersection6>> GetAllUndersection6();
        Task<List<Undersection17>> GetAllUndersection17();
        Task<List<Undersection22>> GetAllUndersection22();
        Task<Khasra> FetchSingleKhasraResult(int? khasraId);
        Task<PagedResult<Undersection22plotdetails>> GetPagedUndersection22plotdetails(Undersection22plotdetailsSearchDto model);
        Task<List<Unotification22detailsListDto>> GetPagednotification22detailsList(Unotification22detailsSearchDto model);
        Task<PagedResult<Undersection22plotdetails>> GetAllNotificationList(NotificationList22SearchDto model);
    }
}
