using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;

namespace Libraries.Repository.IEntityRepository
{
    public interface INewlandAppealdetailRepository : IGenericRepository<Newlandappealdetail>
    {
        Task<List<Newlandappealdetail>> GetNewlandappealdetails();

        Task<PagedResult<Newlandappealdetail>> GetPagedNewlandAppealdetails(NewlandAppealdetailSearchDto model);
    }
}

