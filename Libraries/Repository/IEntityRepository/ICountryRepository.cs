using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Repository.Common;

namespace Libraries.Repository.IEntityRepository
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        Task<List<Country>> GetCountry();
    }
}