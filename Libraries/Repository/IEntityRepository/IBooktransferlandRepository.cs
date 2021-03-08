using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    
     public interface IBooktransferlandRepository : IGenericRepository<Booktransferland>
    {
        Task<List<Booktransferland>> GetBooktransferland();
        Task<List<Booktransferland>> GetAllBooktransferland();

        Task<List<LandNotification>> GetAllLandNotification();
       
        Task<List<Acquiredlandvillage>> GetAllLocality();
        Task<List<Khasra>> GetAllKhasra();
        Task<List<Khasra>> BindKhasra(int? villageId);
        Task<PagedResult<Booktransferland>> GetPagedBooktransferland(BooktransferlandSearchDto model);

    }
}
