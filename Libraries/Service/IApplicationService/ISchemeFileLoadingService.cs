using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.Common;
using Dto.Search;
using Libraries.Repository.Common;

namespace Libraries.Service.IApplicationService
{
    public interface ISchemeFileLoadingService : IEntityService<Schemefileloading>
    {
        Task<List<Schemefileloading>> GetAllSchemeFileLoading();
        Task<List<Schemefileloading>> GetSchemeFileLoadingUsingRepo();

        Task<bool> Update(int id, Schemefileloading schemefileloading);
        Task<bool> Create(Schemefileloading schemefileloading);
        Task<Schemefileloading> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<bool> CheckUniqueName(int id, string scheme);

        Task<PagedResult<Schemefileloading>> GetPagedSchemeFileLoading(SchemeFileLoadingSearchDto model);

       

    }
}

