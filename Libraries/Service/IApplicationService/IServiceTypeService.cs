using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.Common;
using Dto.Search;
using Libraries.Repository.Common;

namespace Libraries.Service.IApplicationService
{
    public interface IServiceTypeService : IEntityService<Servicetype>
    {
        Task<List<Servicetype>> GetAllServicetype();
        Task<List<Servicetype>> GetServicetypeUsingRepo();

        Task<bool> Update(int id, Servicetype servicetype);
        Task<bool> Create(Servicetype servicetype);
        Task<Servicetype> FetchSingleResult(int id);
        Task<bool> Delete(int id);


        Task<PagedResult<Servicetype>> GetPagedServicetype(ServiceTypeSearchDto model);



    }
}


