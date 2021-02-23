using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.Common;
using Dto.Search;
using Libraries.Repository.Common;

namespace Libraries.Service.IApplicationService
{
    public interface INewLandEnhanceCompensationService : IEntityService<Newlandenhancecompensation>
    {


        Task<List<Newlandvillage>> GetAllVillageList();
        Task<List<Newlandkhasra>> GetAllKhasraList(int? villageId);
        

        Task<List<Newlandenhancecompensation>> GetNewlandenhancecompensationUsingRepo();
        Task<List<Newlandenhancecompensation>> GetAllNewlandenhancecompensation();

        Task<bool> Update(int id, Newlandenhancecompensation newlandenhancecompensation);
        Task<bool> Create(Newlandenhancecompensation newlandenhancecompensation);
        Task<Newlandenhancecompensation> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<PagedResult<Newlandenhancecompensation>> GetPagedNewlandenhancecompensation(NewlandenhancecompensationSearchDto model);

        Task<Newlandkhasra> FetchSingleKhasraResult(int? khasraId);



    }
}
