using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.Common;

namespace Libraries.Service.IApplicationService
{
    public interface ICountryService : IEntityService<Country>
    {
        Task<List<Country>> GetAllCountry();
        Task<List<Country>> GetCountryUsingRepo();
    }
}