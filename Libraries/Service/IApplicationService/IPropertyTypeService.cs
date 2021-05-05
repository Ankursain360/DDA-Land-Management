using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{

    public interface IPropertyTypeService : IEntityService<PropertyType>
    {
        Task<List<PropertyType>> GetAllPropertyType();
        Task<List<PropertyType>> GetAllPropertyTypeList();
        
        Task<bool> Update(int id, PropertyType rent);
        Task<bool> Create(PropertyType rate);
        Task<PropertyType> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<PagedResult<PropertyType>> GetPagedPropertyType(PropertyTypeSearchDto model);

    }
}
