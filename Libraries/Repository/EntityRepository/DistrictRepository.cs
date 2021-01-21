using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
namespace Libraries.Repository.EntityRepository
{
  public  class DistrictRepository:GenericRepository<District>, IDistrictRepository
    {
        public DistrictRepository(DataContext dbcontext):base(dbcontext)
        { }

        public async Task<List<District>> GetDistricts()
        {
            return await _dbContext.District.Where(x => x.IsActive == 1).ToListAsync();
        }

        public async Task<PagedResult<District>> GetPagedDistrict(DistrictSearchDto model)
        {
            var data = await _dbContext.District
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                  && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code)))
                 .GetPaged<District>(model.PageNumber, model.PageSize); 
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.District
                           .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                            && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code)))
                           .OrderBy(s => s.Name)
                           .GetPaged<District>(model.PageNumber, model.PageSize);

                        break;
                    case ("CODE"):
                        data = null;
                        data = await _dbContext.District
                           .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                            && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code)))
                           .OrderBy(s => s.Code)
                           .GetPaged<District>(model.PageNumber, model.PageSize);
                        break;
                    case ("ISACTIVE"):
                        data = null;
                        data = await _dbContext.District
                           .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                            && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code)))
                           .OrderByDescending(x => x.IsActive)
                           .GetPaged<District>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.District
                           .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                            && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code)))
                           .OrderByDescending(s => s.Name)
                           .GetPaged<District>(model.PageNumber, model.PageSize);
                        break;
                    case ("CODE"):
                        data = null;
                        data = await _dbContext.District
                           .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                            && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code)))
                           .OrderByDescending(x => x.Code)
                           .GetPaged<District>(model.PageNumber, model.PageSize);
                        break;
                   
                    case ("ISACTIVE"):
                        data = null;
                        data = await _dbContext.District
                           .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                            && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code)))
                           .OrderBy(x => x.IsActive)
                           .GetPaged<District>(model.PageNumber, model.PageSize);
                        break;
                      
                }
            }
            return data;
        }
        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.District.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        }

    }
}
