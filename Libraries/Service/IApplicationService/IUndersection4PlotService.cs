using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Dto.Search;
using Libraries.Repository.Common;

namespace Libraries.Service.IApplicationService
{
    public interface IUndersection4PlotService
    {

        Task<List<Undersection4plot>> GetAllUndersection4Plot();
       Task<List<Khasra>> BindKhasra(int? villageId);
        Task<List<Undersection4>> GetAllNotificationNo();
        Task<List<Acquiredlandvillage>> GetAllVillage();
        Task<List<Undersection4plot>> GetUndersection4PlotUsingRepo();
        Task<bool> Update(int id, Undersection4plot undersection4plot);
        Task<bool> Create(Undersection4plot undersection4plot);
        Task<Undersection4plot> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<Khasra> FetchSingleKhasraResult(int? khasraId);
        Task<PagedResult<Undersection4plot>> GetPagedNoUndersection4plot(NotificationUndersection4plotDto model);
        Task<List<Unotification4detailsListDto>> GetPagednotification4detailsList(Unotification4detailsSearchDto model);
        //Task<List<Undersection4plot>> GetAllNotificationList(int? NotificationId);
        Task<PagedResult<Undersection4plot>> GetAllNotificationList(NotificationList4SearchDto model);

    }
}
