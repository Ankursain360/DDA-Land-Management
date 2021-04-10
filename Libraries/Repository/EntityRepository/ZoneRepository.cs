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
        public async Task<bool> Any(int id,int DepartmentId, string name)
        {
            return await _dbContext.Zone.AnyAsync(t => t.Id != id && t.DepartmentId == DepartmentId && t.Name.ToLower() == name.ToLower());
        }

        public async Task<bool> anyCode(int id,int DepartmentId, string code)
        {
            return await _dbContext.Zone.AnyAsync(t => t.Id != id && t.DepartmentId == DepartmentId && t.Code.ToLower() == code.ToLower());
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
                        .GetPaged<Zone>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("DEPARTMENT"):
                        data = null;
                        data = await _dbContext.Zone
                            .Include(s => s.Department)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code)))
                            .OrderBy(x => x.Department.Name)
                            .GetPaged<Zone>(model.PageNumber, model.PageSize);
                        break;
                       
                      
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Zone
                            .Include(s => s.Department)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code)))
                            .OrderBy(x => x.Name)
                            .GetPaged<Zone>(model.PageNumber, model.PageSize);
                        break;
                    case ("CODE"):
                        data = null;
                        data = await _dbContext.Zone
                            .Include(s => s.Department)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code)))
                            .OrderBy(x => x.Code)
                            .GetPaged<Zone>(model.PageNumber, model.PageSize);
                        break;
                    case ("ISACTIVE"):
                        data = null;
                        data = await _dbContext.Zone
                            .Include(s => s.Department)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code)))
                            .OrderByDescending(x => x.IsActive)
                            .GetPaged<Zone>(model.PageNumber, model.PageSize);
                        break;


                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("DEPARTMENT"):
                        data = null;
                        data = await _dbContext.Zone
                            .Include(s => s.Department)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code)))
                            .OrderByDescending(x => x.Department.Name)
                            .GetPaged<Zone>(model.PageNumber, model.PageSize);
                        break;


                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Zone
                            .Include(s => s.Department)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code)))
                            .OrderByDescending(x => x.Name)
                            .GetPaged<Zone>(model.PageNumber, model.PageSize);
                        break;
                    case ("CODE"):
                        data = null;
                        data = await _dbContext.Zone
                            .Include(s => s.Department)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code)))
                            .OrderByDescending(x => x.Code)
                            .GetPaged<Zone>(model.PageNumber, model.PageSize);
                        break;
                    case ("ISACTIVE"):
                        data = null;
                        data = await _dbContext.Zone
                            .Include(s => s.Department)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code)))
                            .OrderBy(x => x.IsActive)
                            .GetPaged<Zone>(model.PageNumber, model.PageSize);
                        break;


                }
            }
            return data;
        }
    }


}
