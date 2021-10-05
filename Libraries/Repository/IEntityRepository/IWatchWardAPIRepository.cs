using System;
using System.Collections.Generic;

using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Model.Entity;

namespace Libraries.Repository.IEntityRepository
{
    public interface IWatchWardAPIRepository : IGenericRepository<Watchandward>
    {
        Task<List<APIGetPrimaryListNoListDto>> GetPrimaryListNoList();
        Task<bool> SaveWatchandwardphotofiledetails(Watchandwardphotofiledetails item);
        Task<bool> SaveWatchandwardreportfiledetails(Watchandwardreportfiledetails item);

        Task<List<ApiSaveWatchandwardDto>> GetAllWatchandward(ApiWatchWardParmsDto dto);
        Task<Userprofile> GetUserOngivenUserId(int userId);
    }
}