using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface ISakanidetailRepository : IGenericRepository<Saknidetails>
    {
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
        Task <Saknikhasra> FetchSingleSaknikhasra(int id);

        Task<bool> UpdateKhasra(int id, Saknikhasra skh);

    }
}
