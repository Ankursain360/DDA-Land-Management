﻿using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Dto.Search;
using Libraries.Repository.Common;

namespace Libraries.Service.IApplicationService
{
    public interface IUndersection6plotService
    {

        Task<List<Undersection6plot>> GetAllUndersection6Plot();
        Task<List<Undersection6plot>> GetAllNoUndersection6plotList(NotificationUndersection6plotDto model);
        Task<List<Khasra>> BindKhasra(int? villageId);
        Task<List<Undersection6>> GetAllNotificationNo();
        Task<List<Acquiredlandvillage>> GetAllVillage();
        Task<List<Undersection6plot>> GetUndersection6PlotUsingRepo();
        Task<bool> Update(int id, Undersection6plot undersection6plot);
        Task<bool> Create(Undersection6plot undersection6plot);
        Task<Undersection6plot> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<Khasra> FetchSingleKhasraResult(int? khasraId);
        Task<PagedResult<Undersection6plot>> GetPagedNoUndersection6plot(NotificationUndersection6plotDto model);
        Task<List<Unotification6detailsListDto>> GetPagednotification6detailsList(Unotification6detailsSearchDto model);

        Task<PagedResult<Undersection6plot>> GetAllNotificationList(NotificationList6SearchDto model);

    }
}
