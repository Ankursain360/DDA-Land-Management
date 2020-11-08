using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{

    public interface IProposalplotdetailsRepository : IGenericRepository<Proposalplotdetails>
    {
        Task<List<Proposalplotdetails>> GetProposalplotdetails();
        Task<List<Proposaldetails>> GetAllProposaldetails();
        Task<List<Locality>> GetAllLocality();
        Task<List<Khasra>> GetAllKhasra();

        //Task<bool> Any(int id, string name);
        Task<List<Proposalplotdetails>> GetAllProposalplotdetails();
        Task<PagedResult<Proposalplotdetails>> GetPagedProposalplotdetails(ProposalplotdetailSearchDto model);
    }
}
