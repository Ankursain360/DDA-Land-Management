using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Repository.Common;


namespace Libraries.Repository.IEntityRepository
{
    public interface IRebateRepository : IGenericRepository<Rebate>
    {

        
        Task<List<PropertyType>> GetPropertyTypeList();

        object GetFromDateData(int propertyId);
        Task<List<Rebate>> GetAllDetails();
    }
}