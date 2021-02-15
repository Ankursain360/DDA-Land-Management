using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IJaraidetailRepository : IGenericRepository<Jaraidetails>
    {
        Task<PagedResult<Jaraidetails>> GetPagedJaraidetail(JaraiDetailsSearchDto model);
        Task<List<Jaraidetails>> GetAllJaraidetail();
        Task<List<Acquiredlandvillage>> GetAllVillage();
        Task<List<Khasra>> GetAllKhasra(int? villageId);

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
