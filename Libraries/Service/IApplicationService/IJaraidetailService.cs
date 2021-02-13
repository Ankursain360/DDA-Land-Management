using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;

namespace Libraries.Service.IApplicationService
{
    public interface IJaraidetailService
    {
        Task<PagedResult<Jaraidetails>> GetPagedJaraidetail(JaraiDetailsSearchDto model);
        Task<List<Jaraidetails>> GetJaraidetail();
        Task<List<Khewat>> GetAllKhewat();
        Task<List<Acquiredlandvillage>> GetAllVillage();
        Task<List<Khasra>> BindKhasra();
        Task<List<Taraf>> GetAllTaraf();
        Task<List<Khatauni>> GetAllKhatauni();
        Task<List<Jaraidetails>> GetJaraidetailUsingRepo();
        Task<bool> Update(int id, Jaraidetails jaraidetail);
        Task<bool> Create(Jaraidetails jaraidetail);
        Task<Jaraidetails> FetchSingleResult(int id);
        Task<bool> Delete(int id);

    }
}
