using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
        public interface INewlandus4plotRepository : IGenericRepository<Newlandus4plot>
        {
            Task<PagedResult<Newlandus4plot>> GetPagedUS4Plot(Newlandus4plotSearchDto model);
            Task<List<Newlandus4plot>> GetAllUS4Plot();
        
            Task<List<LandNotification>> GetAllNotification();
            Task<List<Newlandvillage>> GetAllVillage();
            Task<List<Newlandkhasra>> GetAllKhasra(int? villageId);
            Task<Newlandkhasra> FetchSingleKhasraResult(int? khasraId);
        Task<Newlandkhasra> FetchSingleKhasra1Result(int? khasraId); 
    }
}
