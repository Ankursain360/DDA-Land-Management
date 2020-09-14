using Libraries.Model.Entity;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface ISchemeService : IEntityService<Scheme>
    {
        Task<List<Scheme>> GetAllScheme();
      
        Task<bool> Update(int id, Scheme Scheme);
        Task<bool> Create(Scheme scheme);
        Task<Scheme> FetchSingleResult(int id);
        Task<List<Scheme>> GetSchemeUsingRepo();
        Task<bool> Delete(int id);
        Task<bool> CheckUniqueName(int id, string name);
    }
}
