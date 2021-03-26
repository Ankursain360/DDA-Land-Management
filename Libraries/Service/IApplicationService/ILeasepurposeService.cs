using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.Common;
using Dto.Search;
using Libraries.Repository.Common;

namespace Libraries.Service.IApplicationService
{
    public interface ILeasepurposeService : IEntityService<Leasepurpose>
    {
        Task<List<Leasepurpose>> GetLeasepurposes();
        Task<List<Leasepurpose>> GetLeasepurposeUsingRepo();

        Task<bool> Update(int id, Leasepurpose Leasepurpose);
        Task<bool> Create(Leasepurpose Leasepurpose);
        Task<Leasepurpose> FetchSingleResult(int id);
        Task<bool> Delete(int id);


        Task<PagedResult<Leasepurpose>> GetpagedLeasepurpose(LeasepurposeSearchDto model);



    }
}

