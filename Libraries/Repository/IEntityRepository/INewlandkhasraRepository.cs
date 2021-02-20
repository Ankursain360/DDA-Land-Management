using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;


namespace Libraries.Repository.IEntityRepository
{
    public interface INewlandkhasraRepository : IGenericRepository<Newlandkhasra>
    {
        Task<PagedResult<Newlandkhasra>> GetPagedKhasra(NewlandkhasraSearchDto model);
        Task<List<Newlandkhasra>> GetAllKhasra();
        Task<List<LandCategory>> GetAllLandCategory();
        Task<List<Newlandvillage>> GetAllVillageList();
        Task<List<Newlandkhasra>> GetAllKhasraList(int? villageId);
      

    }
}
