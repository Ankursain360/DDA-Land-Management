
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{

    public interface INewlandProposaldetailsRepository : IGenericRepository<Newlandacquistionproposaldetails>
    {
        Task<List<Newlandacquistionproposaldetails>> GetProposaldetails();
        Task<List<Newlandscheme>> GetAllScheme();
        Task<bool> Any(int id, string name);
        Task<List<Newlandacquistionproposaldetails>> GetAllProposaldetails();
        Task<PagedResult<Newlandacquistionproposaldetails>> GetPagedProposaldetails(NewlandacquistionproposaldetailsSearchDto model);
    }
}

