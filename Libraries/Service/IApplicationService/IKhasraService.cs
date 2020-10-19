using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Repository.Common;

namespace Libraries.Service.IApplicationService
{
    public interface IKhasraService
    {
        
        Task<List<LandCategory>> GetAllLandCategory();
        Task<List<Locality>> GetAllLocalityList();
        Task<List<Khasra>> GetKhasraUsingRepo();
        Task<List<Khasra>> GetAllKhasra();

        Task<bool> Update(int id, Khasra khasra);
        Task<bool> Create(Khasra khasra);
        Task<Khasra> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<PagedResult<Khasra>> GetPagedKhasra(KhasraMasterSearchDto model);





    }
}