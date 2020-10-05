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
        Task<PagedResult<Sakanidetail>> GetPagedSakanidetail(SakaniDetailsSearchDto model);
        Task<List<Sakanidetail>> GetSakanidetail();
        Task<List<Khewat>> GetAllKhewat();
        Task<List<Acquiredlandvillage>> GetAllVillage();
        Task<List<Khasra>> BindKhasra();
        Task<List<Sakanidetail>> GetSakanidetailUsingRepo();
        Task<bool> Update(int id, Sakanidetail sakanidetail);
        Task<bool> Create(Sakanidetail sakanidetail);
        Task<Sakanidetail> FetchSingleResult(int id);
        Task<bool> Delete(int id);


    }
}
