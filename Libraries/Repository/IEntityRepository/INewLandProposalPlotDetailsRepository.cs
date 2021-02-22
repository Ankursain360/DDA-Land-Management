using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{

    public interface INewLandProposalPlotDetailsRepository : IGenericRepository<Newlandacquistionproposalplotdetails>
    {
        Task<List<Newlandacquistionproposalplotdetails>> GetProposalplotdetails();
        Task<List<Newlandacquistionproposaldetails>> GetAllProposaldetails();
        Task<List<Newlandvillage>> GetAllVillageList();
        Task<List<Newlandkhasra>> GetAllKhasraList(int? villageId);
        Task<Newlandkhasra> FetchSingleKhasraResult(int? khasraId);


        //Task<bool> Any(int id, string name);
        Task<List<Newlandacquistionproposalplotdetails>> GetAllProposalplotdetails();
        Task<PagedResult<Newlandacquistionproposalplotdetails>> GetPagedProposalplotdetails(NewLandProposalplotdetailSearchDto model);
    }
}
