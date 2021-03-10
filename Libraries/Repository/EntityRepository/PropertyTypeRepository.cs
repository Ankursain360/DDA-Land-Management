using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{

    public class PropertyTypeRepository : GenericRepository<PropertyType>, IPropertyTypeRepository
    {
        public PropertyTypeRepository(DataContext dbContext) : base(dbContext)
        {

        }
        
        public async Task<PagedResult<PropertyType>> GetPagedPropertyType(PropertyTypeSearchDto model)
        {
            var data = await _dbContext.PropertyType
                       .GetPaged<PropertyType>(model.PageNumber, model.PageSize);

            if (model.SortBy == null)
            {
                model.SortBy = "Type";
            }
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("TYPE"):
                        data = null;
                        data = await _dbContext.PropertyType
                                                   .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)) )
                                               .OrderBy(x => x.Name)
                                               .GetPaged<PropertyType>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.PropertyType
                                                   .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))
                                               .OrderByDescending(x => x.IsActive)
                                               .GetPaged<PropertyType>(model.PageNumber, model.PageSize);
                        break;


                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("TYPE"):
                        data = null;
                        data = await _dbContext.PropertyType
                                                   .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))
                                               .OrderByDescending(x => x.Name)
                                               .GetPaged<PropertyType>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.PropertyType
                                                   .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))
                                               .OrderBy(x => x.IsActive)
                                               .GetPaged<PropertyType>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            return data;

        }
        public async Task<List<PropertyType>> GetAllPropertyType()
        {
            return await _dbContext.PropertyType.Where(x => x.IsActive == 1).ToListAsync();
        }

    }
}
