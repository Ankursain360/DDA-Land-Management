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
   
    public interface IProposalplotdetailsService : IEntityService<Proposalplotdetails>
    {
        Task<List<Proposalplotdetails>> GetAllProposalplotdetails();
        Task<List<Proposalplotdetails>> GetProposalplotdetailsUsingRepo();
        Task<List<Proposaldetails>> GetAllProposaldetails();
        Task<List<Locality>> GetAllLocality();
        Task<List<Khasra>> GetAllKhasra();
        Task<bool> Update(int id, Proposalplotdetails proposalplotdetails);

        Task<bool> Create(Proposalplotdetails proposalplotdetails);

        Task<Proposalplotdetails> FetchSingleResult(int id);

        Task<bool> Delete(int id);

        Task<PagedResult<Proposalplotdetails>> GetPagedProposalplotdetails(ProposalplotdetailSearchDto model);

        //Task<bool> CheckUniqueName(int id, string proposaldetails);
    }
}
