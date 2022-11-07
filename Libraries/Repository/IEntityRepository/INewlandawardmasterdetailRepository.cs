using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;

namespace Libraries.Repository.IEntityRepository
{
    public interface INewlandawardmasterdetailRepository : IGenericRepository<Newlandawardmasterdetail>
    {
        Task<PagedResult<Newlandawardmasterdetail>> GetPagedawardmasterdetails(NewlandawardmasterSearchDto model);
        Task<List<Newlandawardmasterdetail>> Getawardmasterdetails();
        Task<List<Newlandawardmasterdetail>> GetAllawardmasterdetailsList(NewlandawardmasterSearchDto model);
        Task<bool> Any(int id, string name);
        Task<List<Newlandvillage>> Getvillage();
        Task<List<Proposaldetails>> GetPurposal();
        Task<List<Undersection17>> Getundersection17();
        Task<List<Undersection6>> Getundersection6();
        Task<List<Undersection4>> Getundersection4();
    }
}
