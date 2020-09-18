using Libraries.Model.Entity;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{

     public interface IDisposallandService : IEntityService<Disposalland>
    {
        Task<List<Disposalland>> GetAllDisposalland();
        Task<List<Disposalland>> GetDisposallandUsingRepo();
        Task<List<Utilizationtype>> GetAllUtilizationtype();
        Task<List<Village>> GetAllVillage();
        Task<List<Khasra>> GetAllKhasra();
        Task<bool> Update(int id, Disposalland disposalland);

        Task<bool> Create(Disposalland disposalland);

        Task<Disposalland> FetchSingleResult(int id);

        Task<bool> Delete(int id);

      
    }
}
