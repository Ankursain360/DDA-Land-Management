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
                 .OrderByDescending(s => s.IsActive)
                .GetPaged<District>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data.Results = data.Results.OrderBy(x => x.Name).ToList();
                        break;
                    case ("CODE"):
                        data.Results = data.Results.OrderBy(x => x.Code).ToList();
                        break;
                   
                    case ("ISACTIVE"):
                        data.Results = data.Results.OrderBy(x => x.IsActive).ToList();
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data.Results = data.Results.OrderByDescending(x => x.Name).ToList();
                        break;
                    case ("CODE"):
                        data.Results = data.Results.OrderByDescending(x => x.Code).ToList();
                        break;
                   
                    case ("ISACTIVE"):
                        data.Results = data.Results.OrderBy(x => x.IsActive).ToList();
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
