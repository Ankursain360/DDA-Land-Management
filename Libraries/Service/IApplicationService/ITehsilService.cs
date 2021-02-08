using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
   
    public interface ITehsilService : IEntityService<Tehsil>
    {
        Task<List<Tehsil>> GetAllTehsil();
        Task<List<Tehsil>> GetTehsilUsingRepo();
        //Task<List<Scheme>> GetAllScheme(); // To Get all data added by ishu
        Task<bool> Update(int id, Tehsil tehsil);

        Task<bool> Create(Tehsil tehsil);

        Task<Tehsil> FetchSingleResult(int id);

        Task<bool> Delete(int id);

        Task<bool> CheckUniqueName(int id, string tehsil);
        Task<PagedResult<Tehsil>> GetPagedTehsil(TehsilSearchDto model);
        
    }
}
