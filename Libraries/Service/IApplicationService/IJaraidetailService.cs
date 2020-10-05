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
        Task<PagedResult<Jaraidetail>> GetPagedJaraidetail(JaraiDetailsSearchDto model);
        Task<List<Jaraidetail>> GetJaraidetail();
        Task<List<Khewat>> GetAllKhewat();
        Task<List<Acquiredlandvillage>> GetAllVillage();
        Task<List<Khasra>> BindKhasra();
        Task<List<Taraf>> GetAllTaraf();
        Task<List<Khatauni>> GetAllKhatauni();
        Task<List<Jaraidetail>> GetJaraidetailUsingRepo();
        Task<bool> Update(int id, Jaraidetail jaraidetail);
        Task<bool> Create(Jaraidetail jaraidetail);
        Task<Jaraidetail> FetchSingleResult(int id);
        Task<bool> Delete(int id);

    }
}
