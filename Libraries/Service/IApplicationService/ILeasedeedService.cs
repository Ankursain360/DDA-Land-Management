
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{

    public interface ILeasedeedService : IEntityService<Leasedeed>
    {

        Task<List<Allotmententry>> GetAllApplications();
        Task<Allotmententry> FetchSingleDetails(int? Id);
        Task<List<Leasedeed>> GetAllLeasedeed();
        Task<PagedResult<Leasedeed>> GetPagedLeasedeed(LeasedeedSearchDto model);

        Task<bool> Update(int id, Leasedeed deed);
        Task<bool> Create(Leasedeed deed);
        Task<Leasedeed> FetchSingleResult(int id);
        Task<bool> Delete(int id);
      
    }
}
