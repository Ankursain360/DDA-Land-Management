using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Repository.Common;
using Dto.Master;

namespace Libraries.Service.IApplicationService
{
    public interface INewlandpossessiondetailsService
    {
        Task<List<Newlandkhasra>> BindKhasra(int? villageId);
        Task<List<Newlandvillage>> GetAllVillage();
        Task<List<Newlandkhasra>> GetAllPossKhasra();
        Task<List<Undersection17>> GetAllus17();
        Task<List<Undersection6>> GetAllus6();
        Task<List<Undersection4>> GetAllus4();
        Task<List<Newlandpossessiondetails>> GetAllPossessiondetails();
        Task<List<Newlandpossessiondetails>> GetAllPossessiondetailsList(NewlandpossesiondetailsSearchDto model);
        Task<List<Newlandpossessiondetails>> GetPossessiondetailsUsingRepo();
        Task<bool> Update(int id, Newlandpossessiondetails possessiondetails);
        Task<bool> Create(Newlandpossessiondetails newlandpossessiondetails);
        Task<Newlandpossessiondetails> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<PagedResult<Newlandpossessiondetails>> GetPagedNoPossessiondetails(NewlandpossesiondetailsSearchDto model);


        Task<Newlandkhasra> FetchSingleKhasraResult(int? khasraId);
        
    }
}
