﻿using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IWatchAndWardApprovalRepository:IGenericRepository<Watchandward>
    {
        Task<List<Watchandward>> GetWatchandward();
        Task<List<Watchandward>> GetAllWatchandward();
        Task<List<Locality>> GetAllLocality();
        Task<List<Khasra>> GetAllKhasra();
        Task<PagedResult<Watchandward>> GetPagedWatchandward(WatchandwardApprovalSearchDto model, int userId, int zoneId,int deprtId);
        Task<Watchandward> FetchSingleResult(int id);
        Task<bool> IsApplicationPendingAtUserEnd(int id, int userId);
    }
}
