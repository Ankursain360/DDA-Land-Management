using Libraries.Model.Entity;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;

using Libraries.Repository.Common;


namespace Libraries.Service.IApplicationService
{
   
    public interface IDisposallandtypeService : IEntityService<Disposallandtype>
    {
        Task<List<Disposallandtype>> GetAllDisposallandtype();
        Task<List<Disposallandtype>> GetDisposallandtypeUsingRepo();

        Task<bool> Update(int id, Disposallandtype disposallandtype); 

        Task<bool> Create(Disposallandtype disposallandtype);

        Task<Disposallandtype> FetchSingleResult(int id);  

        Task<bool> Delete(int id);    

        Task<bool> CheckUniqueName(int id, string Module);
        Task<PagedResult<Disposallandtype>> GetPagedDisposalLandType(DisposalLandTypeSearchDto model);

    }
}
