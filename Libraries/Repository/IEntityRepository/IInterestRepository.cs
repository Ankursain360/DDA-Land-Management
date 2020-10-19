using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;


namespace Libraries.Repository.IEntityRepository
{
    public interface IInterestRepository : IGenericRepository<Interest>
    {

        Task<List<Interest>> GetAllDetails();
        Task<List<PropertyType>> GetPropertyTypeList();
        object GetFromDateData(int propertyId);
        Task<PagedResult<Interest>> GetPagedInterest(InterestSearchDto model);
        int IsRecordExist(int propertyId);
    }
}