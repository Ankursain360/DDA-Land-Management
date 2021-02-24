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
    public interface INewlandus17plotService : IEntityService<Newlandus17plot>
    {
    Task<bool> Update(int id, Newlandus17plot us6);
    Task<bool> Create(Newlandus17plot us6);
    Task<Newlandus17plot> FetchSingleResult(int id);
    Task<bool> Delete(int id);
    Task<PagedResult<Newlandus17plot>> GetPagedUS17Plot(Newlandus17plotSearchDto model);
    Task<List<Newlandus17plot>> GetAllUS17Plot();
    Task<List<LandNotification>> GetAllNotification();
    Task<List<Newlandvillage>> GetAllVillage();
    Task<List<Newlandkhasra>> GetAllKhasra(int? villageId);
    Task<Newlandkhasra> FetchSingleKhasraResult(int? khasraId);
    }
}
