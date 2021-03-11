using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{

    public interface ILicenceFeesRepository : IGenericRepository<Licencefees>
    {
        Task<List<Licencefees>> GetAllLicencefees();
        Task<List<PropertyType>> GetAllPropertyType();
        Task<PagedResult<Licencefees>> GetPagedLicencefees(LicencefeesSearchDto model);
    }
}
