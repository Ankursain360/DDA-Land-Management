using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Dto.Master;

namespace Libraries.Repository.IEntityRepository
{
    public interface INewlandpossesiondetailsRepository : IGenericRepository<Newlandpossessiondetails>
    {

        Task<List<Newlandpossessiondetails>> GetAllPossessiondetails();
        Task<List<Newlandpossessiondetails>> GetAllPossessiondetailsList(NewlandpossesiondetailsSearchDto model);
        Task<List<Newlandvillage>> GetAllVillage();
        Task<List<Newlandkhasra>> GetAllPossKhasra();
        Task<List<Undersection17>> GetAllus17();
        Task<List<Undersection6>> GetAllus6();
        Task<List<Undersection4>> GetAllus4();
        Task<List<Newlandkhasra>> BindKhasra(int? villageId);
        Task<Newlandkhasra> FetchSingleKhasraResult(int? khasraId);
        Task<PagedResult<Newlandpossessiondetails>> GetPagedNoPossessiondetails(NewlandpossesiondetailsSearchDto model);
            }
}
