using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;


namespace Libraries.Repository.IEntityRepository
{
    public interface INewLandEnhanceCompensationRepository : IGenericRepository<Newlandenhancecompensation>
    {
        Task<PagedResult<Newlandenhancecompensation>> GetPagedNewlandenhancecompensation(NewlandenhancecompensationSearchDto model);
        Task<List<Newlandenhancecompensation>> GetAllNewlandenhancecompensation();

        Task<List<Newlandvillage>> GetAllVillageList();
        Task<List<Newlandkhasra>> GetAllKhasraList(int? villageId);
        
        Task<Newlandkhasra> FetchSingleKhasraResult(int? khasraId);
    }
}
