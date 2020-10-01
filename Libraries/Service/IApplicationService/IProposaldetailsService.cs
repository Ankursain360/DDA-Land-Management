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
   
    public interface IProposaldetailsService : IEntityService<Proposaldetails>
    {
        Task<List<Proposaldetails>> GetAllProposaldetails();
        Task<List<Proposaldetails>> GetProposaldetailsUsingRepo();
        Task<List<Scheme>> GetAllScheme(); // To Get all data added by ishu
        Task<bool> Update(int id, Proposaldetails proposaldetails);

        Task<bool> Create(Proposaldetails proposaldetails);

        Task<Proposaldetails> FetchSingleResult(int id);

        Task<bool> Delete(int id);

        Task<bool> CheckUniqueName(int id, string proposaldetails);
        Task<PagedResult<Proposaldetails>> GetPagedProposaldetails(ProposaldetailsSearchDto model);
        
    }
}
