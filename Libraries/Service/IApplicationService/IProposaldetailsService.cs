using Libraries.Model.Entity;
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
        //Task<List<Module>> GetAllModule(); // To Get all data added by ishu
        Task<bool> Update(int id, Proposaldetails proposaldetails);

        Task<bool> Create(Proposaldetails proposaldetails);

        Task<Proposaldetails> FetchSingleResult(int id);

        Task<bool> Delete(int id);

        Task<bool> CheckUniqueName(int id, string Proposaldetails);
    }
}
