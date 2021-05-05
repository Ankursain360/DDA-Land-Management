using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IPropertyTypeRepository : IGenericRepository<PropertyType>
    {
        Task<PagedResult<PropertyType>> GetPagedPropertyType(PropertyTypeSearchDto model);
        Task<List<PropertyType>> GetAllPropertyType();
        Task<List<PropertyType>> GetAllPropertyTypeList();
    }
}
