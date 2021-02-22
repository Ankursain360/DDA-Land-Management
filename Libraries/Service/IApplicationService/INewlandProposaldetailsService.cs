//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Service.IApplicationService
//{
//    class INewlandProposaldetailsService
//    {
//    }
//}
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

    public interface INewlandProposaldetailsService : IEntityService<Newlandacquistionproposaldetails>
    {
        Task<List<Newlandacquistionproposaldetails>> GetAllProposaldetails();
        Task<List<Newlandacquistionproposaldetails>> GetProposaldetailsUsingRepo();
        Task<List<Newlandscheme>> GetAllScheme(); 
        Task<bool> Update(int id, Newlandacquistionproposaldetails newlandacquistionproposaldetails);

        Task<bool> Create(Newlandacquistionproposaldetails newlandacquistionproposaldetails);

        Task<Newlandacquistionproposaldetails> FetchSingleResult(int id);

        Task<bool> Delete(int id);

        Task<bool> CheckUniqueName(int id, string proposaldetails);
        Task<PagedResult<Newlandacquistionproposaldetails>> GetPagedProposaldetails(NewlandacquistionproposaldetailsSearchDto model);

    }
}

