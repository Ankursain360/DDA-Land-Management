using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface INewlandus22plotRepository : IGenericRepository<Newlandus22plot>
    {
        Task<PagedResult<Newlandus22plot>> GetPagedUS22Plot(Newlandus22plotSearchDto model);
        Task<List<Newlandus22plot>> GetAllUS22Plot();
        Task<List<Newlandus4plot>> GetAllUS4Plot();
        Task<List<Newlandus6plot>> GetAllUS6Plot();
        Task<List<Newlandus17plot>> GetAllUS17Plot();
        Task<Newlandus6plot> FetchUS6Plot(int? notificationId);
        Task<List<LandNotification>> GetAllNotification();
        Task<List<Newlandvillage>> GetAllVillage();
        Task<List<Newlandkhasra>> GetAllKhasra(int? villageId);
        Task<Newlandkhasra> FetchSingleKhasraResult(int? khasraId);
    }
}
