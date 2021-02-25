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

    public interface INewLandProposalPlotDetailsService : IEntityService<Newlandacquistionproposalplotdetails>
    {
        Task<List<Newlandacquistionproposalplotdetails>> GetAllProposalplotdetails();
        Task<List<Newlandacquistionproposalplotdetails>> GetProposalplotdetailsUsingRepo();
        Task<List<Newlandacquistionproposaldetails>> GetAllProposaldetails();
       
        Task<List<Newlandvillage>> GetAllVillageList();
        Task<List<Newlandkhasra>> GetAllKhasraList(int? villageId);
        Task<Newlandkhasra> FetchSingleKhasraResult(int? khasraId);
        Task<bool> Update(int id, Newlandacquistionproposalplotdetails newlandacquistionproposalplotdetails);

        Task<bool> Create(Newlandacquistionproposalplotdetails newlandacquistionproposalplotdetails);

        Task<Newlandacquistionproposalplotdetails> FetchSingleResult(int id);

        Task<bool> Delete(int id);

        Task<PagedResult<Newlandacquistionproposalplotdetails>> GetPagedProposalplotdetails(NewLandProposalplotdetailSearchDto model);
        

    }
}
