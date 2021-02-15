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
        Task<List<Jaraidetails>> GetAllJaraidetail();
        Task<List<Acquiredlandvillage>> GetAllVillage();
        Task<List<Khasra>> GetAllKhasra(int? villageId);
        Task<List<Jaraidetails>> GetJaraidetailUsingRepo();
        Task<bool> Update(int id, Jaraidetails jarai);
        Task<bool> Create(Jaraidetails jarai);
        Task<Jaraidetails> FetchSingleResult(int id);
        Task<bool> Delete(int id);

        //********* rpt ! Owner Details **********

        Task<bool> SaveOwner(Jaraiowner Jaraiowner);
        Task<List<Jaraiowner>> GetAllOwner(int id);
        Task<bool> DeleteOwner(int Id);

        //********* rpt ! Lessee Details **********

        Task<bool> SaveJarailessee(Jarailessee Jarailessee);
        Task<List<Jarailessee>> GetAllJarailessee(int id);
        Task<bool> DeleteJarailessee(int Id);

        //********* rpt ! Farmer Details **********

        Task<bool> Savefarmer(Jaraifarmer farmer);
        Task<List<Jaraifarmer>> GetAllFarmer(int id);
        Task<bool> DeleteFarmer(int Id);

    }
}
