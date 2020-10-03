using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
 
     public interface ILdolandRepository : IGenericRepository<Ldoland>
    {
        Task<List<Ldoland>> GetLdoland();

        Task<List<Ldoland>> GetAllLdoland();
        Task<List<Serialnumber>> GetAllSerialnumber();
        Task<List<LandNotification>> GetAllLandNotification();
        Task<PagedResult<Ldoland>> GetPagedLdoland(LdolandSearchDto model);

    }
}
