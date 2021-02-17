using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;

namespace Libraries.Service.IApplicationService
{
   public interface IAwardmasterdetailsService : IEntityService<Awardmasterdetail>
    {
        Task<List<Awardmasterdetail>> Getawardmasterdetails();
        Task<PagedResult<Awardmasterdetail>> GetPagedawardmasterdetails(AwardMasterDetailsSearchDto model);
        Task<bool> Update(int id, Awardmasterdetail awardmasterdetail);
        Task<bool> Create(Awardmasterdetail awardmasterdetail);
        Task<Awardmasterdetail> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<bool> CheckUniqueName(int id, string name);
        Task<List<Acquiredlandvillage>> Getvillage();
        Task<List<Proposaldetails>> GetPurposal();
        Task<List<Undersection17>> Getundersection17();
        Task<List<Undersection6>> Getundersection6();
        Task<List<Undersection4>> Getundersection4();

    }

}
