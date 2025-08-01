﻿using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Repository.Common;
using Libraries.Service.Common;

namespace Libraries.Service.IApplicationService
{
    public interface IUndersection17plotdetailService : IEntityService<Undersection17plotdetail>
    {


       
        Task<List<Acquiredlandvillage>> GetAllVillageList();
        Task<List<Khasra>> BindKhasra(int? villageId);
        Task<Khasra> FetchSingleKhasraResult(int? khasraId);
        Task<List<Undersection17>> GetAllUndersection17List();

        Task<List<Undersection17plotdetail>> GetUndersection17plotdetailUsingRepo();
        Task<List<Undersection17plotdetail>> GetAllUndersection17plotdetail();
        Task<List<Undersection17plotdetail>> GetAllUndersection17plotdetailList(Undersection17plotdetailSearchDto model);
        Task<bool> Update(int id, Undersection17plotdetail undersection17plotdetail);
        Task<bool> Create(Undersection17plotdetail undersection17plotdetail);
        Task<Undersection17plotdetail> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<PagedResult<Undersection17plotdetail>> GetPagedUndersection17plotdetail(Undersection17plotdetailSearchDto model);

       
        Task<List<Unotification17detailsListDto>> GetPagednotification17detailsList(Unotification17detailsSearchDto model);
        Task<PagedResult<Undersection17plotdetail>> GetAllNotificationList(NotificationList17SearchDto model);


    }
}
