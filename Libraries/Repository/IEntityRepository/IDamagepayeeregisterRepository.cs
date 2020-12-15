using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    
    public interface IDamagepayeeregisterRepository : IGenericRepository<Damagepayeeregister>
    {

        //Task<bool> Any(int id, string name);
        Task<List<Locality>> GetLocalityList();
        Task<List<District>> GetDistrictList();
        Task<List<Damagepayeeregister>> GetAllDamagepayeeregister();
        Task<PagedResult<Damagepayeeregister>> GetPagedDamagepayeeregister(DamagepayeeregisterSearchDto model);
    }
}
