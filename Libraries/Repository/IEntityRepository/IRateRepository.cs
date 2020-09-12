using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Repository.Common;


namespace Libraries.Repository.IEntityRepository
{
    public interface IRateRepository : IGenericRepository<Rate>
    {

        Task<List<Rate>> GetAllDetails();
        Task<List<PropertyType>> GetPropertyTypeList();

        object GetFromDateData(int propertyId);
    }
}