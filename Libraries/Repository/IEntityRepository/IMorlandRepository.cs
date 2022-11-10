using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;

namespace Libraries.Repository.IEntityRepository
{
    public interface IMorlandRepository : IGenericRepository<Morland>
    {
        Task<PagedResult<Morland>> GetPagedMorland(MorLandsSearchDto model);
        Task<List<Morland>> GetAllMorland();
        Task<List<Morland>> GetAllMorlandList(MorLandsSearchDto model);
        Task<List<Otherlandnotification>> GetAllLandNotification();
      // Task<List<Serialnumber>> GetAllSerialnumber();
        Task<bool> Any(int id, string Name);

    }
}
