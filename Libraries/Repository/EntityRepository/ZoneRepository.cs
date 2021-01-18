using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Libraries.Repository.EntityRepository
{
    public class ZoneRepository : GenericRepository<Zone>, IZoneRepository
    {

        public ZoneRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Zone>> GetAllDetails()
        {
            var data = await _dbContext.Zone.Include(s => s.Department).Where(s => s.IsActive == 1).OrderBy( s => s.Id).ToListAsync();
            return data;
        }
        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Zone.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        }

        public async Task<bool> anyCode(int id, string code)
        {
            return await _dbContext.Zone.AnyAsync(t => t.Id != id && t.Code.ToLower() == code.ToLower());
        }

        public async Task<List<Department>> GetDepartmentList()
        {
            var departmentList = await _dbContext.Department.Where(x => x.IsActive == 1).ToListAsync();
            return departmentList;
        }

        public async Task<PagedResult<Zone>> GetPagedZone(ZoneSearchDto model)
        {
            var data = await _dbContext.Zone.Include(s => s.Department)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code)))
                            .OrderBy(s => s.Department.Name)
                            .OrderBy(s => s.Name)
                             .OrderByDescending(s => s.IsActive)
                        .GetPaged<Zone>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("DEPARTMENT"):
                        data.Results = data.Results.OrderBy(x => x.Department.Name).ToList();
                        break;
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
                    case ("DEPARTMENT"):
                        data.Results = data.Results.OrderByDescending(x => x.Department.Name).ToList();
                        break;
                    case ("NAME"):
                        data.Results = data.Results.OrderByDescending(x => x.Name).ToList();
                        break;
                    case ("CODE"):
                        data.Results = data.Results.OrderByDescending(x => x.Code).ToList();
                        break;
                    case ("ISACTIVE"):
                        data.Results = data.Results.OrderByDescending(x => x.IsActive).ToList();
                        break;


                }
            }
            return data;
        }
    }


}
