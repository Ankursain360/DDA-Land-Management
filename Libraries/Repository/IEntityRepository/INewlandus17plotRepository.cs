using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface INewlandus17plotRepository : IGenericRepository<Newlandus17plot>
    {
        Task<PagedResult<Newlandus17plot>> GetPagedUS17Plot(Newlandus17plotSearchDto model);
        Task<List<Newlandus17plot>> GetAllUS17Plot();
        Task<List<LandNotification>> GetAllNotification();
        Task<List<Newlandvillage>> GetAllVillage();
        Task<List<Newlandkhasra>> GetAllKhasra(int? villageId);
        Task<Newlandkhasra> FetchSingleKhasraResult(int? khasraId);
    }
}