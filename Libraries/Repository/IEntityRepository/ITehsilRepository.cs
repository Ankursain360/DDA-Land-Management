using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
  
    public interface ITehsilRepository : IGenericRepository<Tehsil>
    {
        Task<List<Tehsil>> GetTehsil();
        //Task<List<Scheme>> GetAllScheme();
        Task<bool> Any(int id, string name);
        Task<List<Tehsil>> GetAllTehsil();
        Task<PagedResult<Tehsil>> GetPagedTehsil(TehsilSearchDto model);
    }
}
