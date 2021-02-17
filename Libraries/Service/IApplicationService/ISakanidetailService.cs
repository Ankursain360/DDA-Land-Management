using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
namespace Libraries.Service.IApplicationService
{
    public interface ISakanidetailService
    {
        Task<PagedResult<Saknidetails>> GetPagedSakanidetail(SakaniDetailsSearchDto model);
        Task<List<Saknidetails>> GetSakanidetail();
        Task<List<Khewat>> GetAllKhewat();
        Task<List<Acquiredlandvillage>> GetAllVillage();
        Task<List<Khasra>> BindKhasra();
        Task<List<Saknidetails>> GetSakanidetailUsingRepo();
        Task<bool> Update(int id, Saknidetails sakanidetail);
        Task<bool> Create(Saknidetails sakanidetail);
        Task<Saknidetails> FetchSingleResult(int id);
        Task<bool> Delete(int id);


    }
}
