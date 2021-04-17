using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;

namespace Libraries.Repository.IEntityRepository
{
    public interface IUnderSection4PlotRepository : IGenericRepository<Undersection4plot>
    {
        Task<List<Undersection4plot>> GetAllUndersection4Plot();
        Task<List<Undersection4>> GetAllNotificationNo();
        Task<List<Acquiredlandvillage>> GetAllVillage();
        Task<List<Khasra>> BindKhasra(int? villageId);
        Task<Khasra> FetchSingleKhasraResult(int? khasraId);
        Task<PagedResult<Undersection4plot>> GetPagedNoUndersection4plot(NotificationUndersection4plotDto model);

        Task<List<Unotification4detailsListDto>> GetPagednotification4detailsList(Unotification4detailsSearchDto model);
        Task<PagedResult<Undersection4plot>> GetAllNotificationList(NotificationList4SearchDto model);
    }
}
