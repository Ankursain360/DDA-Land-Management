using Libraries.Model.Entity;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Repository.Common;

namespace Libraries.Service.IApplicationService
{
    public interface INewlandSchemeService : IEntityService<Newlandscheme>
    {

        Task<List<Newlandscheme>> GetAllScheme();

        Task<bool> Update(int id, Newlandscheme Scheme);
        Task<bool> Create(Newlandscheme scheme);
        Task<Newlandscheme> FetchSingleResult(int id);
        Task<List<Newlandscheme>> GetSchemeUsingRepo();
        Task<bool> Delete(int id);
        Task<bool> CheckUniqueName(int id, string name);
        Task<PagedResult<Newlandscheme>> GetPagedScheme(NewlandschemeSearchDto model);

    }
}
