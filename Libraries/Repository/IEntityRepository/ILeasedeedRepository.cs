using System;
using System.Collections.Generic;
using System.Text;

using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface ILeasedeedRepository : IGenericRepository<Leasedeed>

    {
        Task<List<Allotmententry>> GetAllApplications();
        
        Task<Allotmententry> FetchSingleDetails(int? Id);

        Task<List<Leasedeed>> GetAllLeasedeed();
        Task<PagedResult<Leasedeed>> GetPagedLeasedeed(LeasedeedSearchDto model);
    }
}
