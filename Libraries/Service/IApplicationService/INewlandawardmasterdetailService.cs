using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;

namespace Libraries.Service.IApplicationService
{
    public interface INewlandawardmasterdetailService : IEntityService<Newlandawardmasterdetail>
    {
        Task<List<Newlandawardmasterdetail>> Getawardmasterdetails();
        Task<PagedResult<Newlandawardmasterdetail>> GetPagedawardmasterdetails(NewlandawardmasterSearchDto model);
        Task<bool> Update(int id, Newlandawardmasterdetail newlandawardmasterdetail);
        Task<bool> Create(Newlandawardmasterdetail newlandawardmasterdetail);
        Task<Newlandawardmasterdetail> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<bool> CheckUniqueName(int id, string name);
        Task<List<Newlandvillage>> Getvillage();
        Task<List<Proposaldetails>> GetPurposal();
        Task<List<Undersection17>> Getundersection17();
        Task<List<Undersection6>> Getundersection6();
        Task<List<Undersection4>> Getundersection4();

    }

}
