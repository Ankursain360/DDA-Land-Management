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
      
        Task<bool> Update(int id, Saknidetails sakni);
        Task<bool> Create(Saknidetails sakni);
        Task<Saknidetails> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<PagedResult<Saknidetails>> GetPagedSaknidetail(SakaniDetailsSearchDto model);
        Task<List<Saknidetails>> GetAllSaknidetail();
        Task<List<Acquiredlandvillage>> GetAllVillage();
        Task<List<Khasra>> GetAllKhasra(int? villageId);

        //********* rpt ! Owner Details **********

        Task<bool> SaveOwner(Sakniowner owner);
        Task<List<Sakniowner>> GetAllOwner(int id);
        Task<bool> DeleteOwner(int Id);

        //********* rpt ! Lessee Details **********

        Task<bool> Savelessee(Saknilessee lessee);
        Task<List<Saknilessee>> GetAllSaknilessee(int id);
        Task<bool> Deletelessee(int Id);

        //********* rpt ! Tenant Details **********

        Task<bool> SaveTenant(Saknitenant tenant);
        Task<List<Saknitenant>> GetAllTenant(int id);
        Task<bool> DeleteTenant(int Id);

        //*********  sakni Khasra Details **********

        Task<bool> SaveSaknikhasra(Saknikhasra sakniKhasra);
        Task<List<Saknikhasra>> GetAllSaknikhasra(int id);
        Task<bool> DeleteSaknikhasra(int Id);
        Task<Saknikhasra> FetchSingleSaknikhasra(int id);

        Task<bool> UpdateKhasra(int id, Saknikhasra skh);
    }
}
