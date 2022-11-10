using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;

namespace Libraries.Repository.IEntityRepository
{
  public interface IAwardmasterdetailsRepository : IGenericRepository<Awardmasterdetail>
    {
        Task<PagedResult<Awardmasterdetail>> GetPagedawardmasterdetails(AwardMasterDetailsSearchDto model);
        Task<List<Awardmasterdetail>> Getawardmasterdetails();
        Task<List<Awardmasterdetail>> GetAllawardmasterdetailsList(AwardMasterDetailsSearchDto model);
        Task<bool> Any(int id, string name);
        Task<List<Acquiredlandvillage>> Getvillage();
        Task<List<Proposaldetails>> GetPurposal();
        Task<List<Undersection17>> Getundersection17();
        Task<List<Undersection6>> Getundersection6();
        Task<List<Undersection4>> Getundersection4();
    }
}
