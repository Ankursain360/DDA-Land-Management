using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    
    public interface IWatchandwardRepository : IGenericRepository<Watchandward>
    {
        Task<List<Watchandward>> GetWatchandward();
        Task<List<Watchandward>> GetAllWatchandward();
        Task<List<Village>> GetAllVillage();
        Task<List<Khasra>> GetAllKhasra();
        //Task<PagedResult<Page>> GetPagedPage(PageSearchDto model);
    }
}
