using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;

namespace Libraries.Service.IApplicationService
{
    public interface IAlmirahService : IEntityService<Almirah>
    {
        Task<List<Almirah>> GetAllAlmirah();
        Task<List<Almirah>> GetAlmirahUsingReport();

        Task<bool> Update(int id, Almirah almirah);
        Task<bool> Create(Almirah almirah);
        Task<Almirah> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<bool> CheckUniqueName(int id, string almirah);

        Task<PagedResult<Almirah>> GetPagedAlmirah(AlmirahSearchDto model);
    }
}
