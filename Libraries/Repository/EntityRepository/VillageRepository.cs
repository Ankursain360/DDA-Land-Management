using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class VillageRepository : GenericRepository<Village>, IVillageRepository
    {
        public VillageRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Village>> GetPagedVillage(VillageSearchDto model)
        {
            var data = await _dbContext.Village
                            .Include(x=>x.Zone)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                            && (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone)))
                            .GetPaged<Village>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {

                    case ("VILLAGENAME"):
                        data = null;
                        data = await _dbContext.Village
                            .Include(x => x.Zone)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                            && (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone)))
                            .OrderBy(x => x.Name)
                            .GetPaged<Village>(model.PageNumber, model.PageSize);
                        break;
                    case ("ZONENAME"):
                        data = null;
                        data = await _dbContext.Village
                            .Include(x => x.Zone)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                            && (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone)))
                            .OrderBy(x => x.Zone.Name)
                            .GetPaged<Village>(model.PageNumber, model.PageSize);
                       
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Village
                            .Include(x => x.Zone)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                            && (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone)))
                            .OrderByDescending(x => x.IsActive)
                            .GetPaged<Village>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {

                    case ("VILLAGENAME"):
                        data = null;
                        data = await _dbContext.Village
                            .Include(x => x.Zone)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                            && (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone)))
                            .OrderByDescending(x => x.Name)
                            .GetPaged<Village>(model.PageNumber, model.PageSize);
                        break;
                    case ("ZONENAME"):
                        data = null;
                        data = await _dbContext.Village
                            .Include(x => x.Zone)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                            && (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone)))
                            .OrderByDescending(x => x.Zone.Name)
                            .GetPaged<Village>(model.PageNumber, model.PageSize);

                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Village
                            .Include(x => x.Zone)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                            && (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone)))
                            .OrderBy(x => x.IsActive)
                            .GetPaged<Village>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            return data;
        }

        public async Task<List<Village>> GetVillage()
        {
            return await _dbContext.Village.Include(x => x.Zone).OrderByDescending(x=>x.Id).ToListAsync();
        }
        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Village.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        }

        public async Task<List<Zone>> GetAllZone(int departmentId)
        {
            List<Zone> zoneList = await _dbContext.Zone.Where(x=>x.DepartmentId== departmentId && x.IsActive == 1).ToListAsync();
            return zoneList;
        }

        public async Task<List<Division>> GetAllDivisionList(int zoneId)
        {
            List<Division> divisionList = await _dbContext.Division.Where(x => x.ZoneId == zoneId && x.IsActive == 1).ToListAsync();
            return divisionList;
        }

        public async Task<List<Department>> GetAllDepartmentList()
        {
            List<Department> departmentList = await _dbContext.Department.Where(x => x.IsActive == 1).ToListAsync();
            return departmentList;
        }
    }
}
