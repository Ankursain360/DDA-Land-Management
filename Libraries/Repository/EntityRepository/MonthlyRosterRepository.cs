using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class MonthlyRosterRepository : GenericRepository<MonthlyRoaster>, IMonthlyRosterRepository
    {
        public MonthlyRosterRepository(DataContext dbcontext) : base(dbcontext)
        { }
        public async Task<List<Department>> GetAllDepartmentList()
        {
            return (await _dbContext.Department.Where(x => x.IsActive == 1).ToListAsync());
        }
        public async Task<List<Division>> GetAllDivisionList(int zoneId)
        {
            return (await _dbContext.Division.Where(x => x.IsActive == 1 && x.ZoneId == zoneId).ToListAsync());
        }
        public async Task<List<Locality>> GetAllLocalityList(int divisionId)
        {
            return (await _dbContext.Locality.Where(x => x.IsActive == 1 && x.DivisionId == divisionId).ToListAsync());
        }
        public async Task<List<Zone>> GetAllZone(int departmentId)
        {
            return (await _dbContext.Zone.Where(x => x.IsActive == 1 && x.DepartmentId == departmentId).ToListAsync());
        }
        public async Task<List<Userprofile>> SecurityGuardList()
        {
            return (await _dbContext.Userprofile.Include(x => x.User).Include(x => x.Role).Where(x => x.IsActive == 1 && x.RoleId == 13).ToListAsync());
        }
        public async Task<List<Propertyregistration>> GetPrimaryListNoList(int divisionId, int departmentId, int zoneId, int localityId)
        {
            return await _dbContext.Propertyregistration.Where(x => x.DepartmentId == departmentId && x.ZoneId == zoneId && x.DivisionId == divisionId && x.LocalityId == localityId && x.IsActive == 1).ToListAsync();
        }

        public async Task<PagedResult<MonthlyRoaster>> GetAllRoasterDetails(MonthlyRoasterSearchDto model)
        {
            var data = await _dbContext.MonthlyRoaster
                                       .Where(x => x.IsActive == 1)
                                       .Include(x => x.Department)
                                       .Include(x => x.Zone)
                                       .Include(x => x.Division)
                                       .Include(x => x.Locality)
                                       .Include(x => x.Userprofile)
                                       .ThenInclude(x => x.User)
                                       .Where(x => (string.IsNullOrEmpty(model.department) || x.Department.Name.Contains(model.department))
                                       && (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone))
                                       && (string.IsNullOrEmpty(model.division) || x.Division.Name.Contains(model.division))
                                       && (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                                       && (string.IsNullOrEmpty(model.guard) || x.Userprofile.User.NormalizedUserName.Contains(model.guard)))
                                       //&& (string.IsNullOrEmpty(model.year) || Convert.ToString(x.Year).Contains(model.year)))
                                      .GetPaged(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("DEP"):
                        data = null;
                        data = await _dbContext.MonthlyRoaster
                                               .Where(x => x.IsActive == 1)
                                               .Include(x => x.Department)
                                               .Include(x => x.Zone)
                                               .Include(x => x.Division)
                                               .Include(x => x.Locality)
                                               .Include(x => x.Userprofile)
                                               .ThenInclude(x => x.User)
                                               .Where(x => (string.IsNullOrEmpty(model.department) || x.Department.Name.Contains(model.department))
                                               && (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone))
                                               && (string.IsNullOrEmpty(model.division) || x.Division.Name.Contains(model.division))
                                               && (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                                               && (string.IsNullOrEmpty(model.guard) || x.Userprofile.User.NormalizedUserName.Contains(model.guard)))
                                                //&& (string.IsNullOrEmpty(model.year) || Convert.ToString(x.Year).Contains(model.year)))
                                               .OrderBy(s => s.Department.Name)
                                               .GetPaged(model.PageNumber, model.PageSize);
                        break;

                    case ("ZONE"):
                        data = null;
                        data = await _dbContext.MonthlyRoaster
                                               .Where(x => x.IsActive == 1)
                                               .Include(x => x.Department)
                                               .Include(x => x.Zone)
                                               .Include(x => x.Division)
                                               .Include(x => x.Locality)
                                               .Include(x => x.Userprofile)
                                               .ThenInclude(x => x.User)
                                               .Where(x => (string.IsNullOrEmpty(model.department) || x.Department.Name.Contains(model.department))
                                               && (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone))
                                               && (string.IsNullOrEmpty(model.division) || x.Division.Name.Contains(model.division))
                                               && (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                                               && (string.IsNullOrEmpty(model.guard) || x.Userprofile.User.NormalizedUserName.Contains(model.guard)))
                                               //&& (string.IsNullOrEmpty(model.year) || x.Year.Contains(model.year)))
                                               .OrderBy(s => s.Zone.Name)
                                               .GetPaged(model.PageNumber, model.PageSize);
                        break;
                    case ("DIV"):
                        data = null;
                        data = await _dbContext.MonthlyRoaster
                                               .Where(x => x.IsActive == 1)
                                               .Include(x => x.Department)
                                               .Include(x => x.Zone)
                                               .Include(x => x.Division)
                                               .Include(x => x.Locality)
                                               .Include(x => x.Userprofile)
                                               .ThenInclude(x => x.User)
                                               .Where(x => (string.IsNullOrEmpty(model.department) || x.Department.Name.Contains(model.department))
                                               && (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone))
                                               && (string.IsNullOrEmpty(model.division) || x.Division.Name.Contains(model.division))
                                               && (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                                               && (string.IsNullOrEmpty(model.guard) || x.Userprofile.User.NormalizedUserName.Contains(model.guard)))
                                               //&& (string.IsNullOrEmpty(model.year) || x.Year.Contains(model.year)))
                                               .OrderBy(s => s.Division.Name)
                                               .GetPaged(model.PageNumber, model.PageSize);
                        break;
                    case ("LOC"):
                        data = null;
                        data = await _dbContext.MonthlyRoaster
                                               .Where(x => x.IsActive == 1)
                                               .Include(x => x.Department)
                                               .Include(x => x.Zone)
                                               .Include(x => x.Division)
                                               .Include(x => x.Locality)
                                               .Include(x => x.Userprofile)
                                               .ThenInclude(x => x.User)
                                               .Where(x => (string.IsNullOrEmpty(model.department) || x.Department.Name.Contains(model.department))
                                               && (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone))
                                               && (string.IsNullOrEmpty(model.division) || x.Division.Name.Contains(model.division))
                                               && (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                                               && (string.IsNullOrEmpty(model.guard) || x.Userprofile.User.NormalizedUserName.Contains(model.guard)))
                                               //&& (string.IsNullOrEmpty(model.year) || x.Year.Contains(model.year)))
                                               .OrderBy(s => s.Locality.Name)
                                               .GetPaged(model.PageNumber, model.PageSize);
                        break;
                    case ("SECURITY"):
                        data = null;
                        data = await _dbContext.MonthlyRoaster
                                               .Where(x => x.IsActive == 1)
                                               .Include(x => x.Department)
                                               .Include(x => x.Zone)
                                               .Include(x => x.Division)
                                               .Include(x => x.Locality)
                                               .Include(x => x.Userprofile)
                                               .ThenInclude(x => x.User)
                                               .Where(x => (string.IsNullOrEmpty(model.department) || x.Department.Name.Contains(model.department))
                                               && (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone))
                                               && (string.IsNullOrEmpty(model.division) || x.Division.Name.Contains(model.division))
                                               && (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                                               && (string.IsNullOrEmpty(model.guard) || x.Userprofile.User.NormalizedUserName.Contains(model.guard)))
                                               //&& (string.IsNullOrEmpty(model.year) || x.Year.Contains(model.year)))
                                               .OrderBy(s => s.Userprofile.User.NormalizedUserName)
                                               .GetPaged(model.PageNumber, model.PageSize);
                        break;
                    case ("YEAR"):
                        data = null;
                        data = await _dbContext.MonthlyRoaster
                                               .Where(x => x.IsActive == 1)
                                               .Include(x => x.Department)
                                               .Include(x => x.Zone)
                                               .Include(x => x.Division)
                                               .Include(x => x.Locality)
                                               .Include(x => x.Userprofile)
                                               .ThenInclude(x => x.User)
                                               .Where(x => (string.IsNullOrEmpty(model.department) || x.Department.Name.Contains(model.department))
                                               && (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone))
                                               && (string.IsNullOrEmpty(model.division) || x.Division.Name.Contains(model.division))
                                               && (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                                               && (string.IsNullOrEmpty(model.guard) || x.Userprofile.User.NormalizedUserName.Contains(model.guard)))
                                               //&& (string.IsNullOrEmpty(model.year) || x.Year.Contains(model.year)))
                                               .OrderBy(s => s.Year)
                                               .GetPaged(model.PageNumber, model.PageSize);
                        break;
                    case ("MONTH"):
                        data = null;
                        data = await _dbContext.MonthlyRoaster
                                               .Where(x => x.IsActive == 1)
                                               .Include(x => x.Department)
                                               .Include(x => x.Zone)
                                               .Include(x => x.Division)
                                               .Include(x => x.Locality)
                                               .Include(x => x.Userprofile)
                                               .ThenInclude(x => x.User)
                                               .Where(x => (string.IsNullOrEmpty(model.department) || x.Department.Name.Contains(model.department))
                                               && (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone))
                                               && (string.IsNullOrEmpty(model.division) || x.Division.Name.Contains(model.division))
                                               && (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                                               && (string.IsNullOrEmpty(model.guard) || x.Userprofile.User.NormalizedUserName.Contains(model.guard)))
                                               //&& (string.IsNullOrEmpty(model.year) || x.Year.Contains(model.year)))
                                               .OrderBy(s => s.Month)
                                               .GetPaged(model.PageNumber, model.PageSize);
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("DEP"):
                        data = null;
                        data = await _dbContext.MonthlyRoaster
                                               .Where(x => x.IsActive == 1)
                                               .Include(x => x.Department)
                                               .Include(x => x.Zone)
                                               .Include(x => x.Division)
                                               .Include(x => x.Locality)
                                               .Include(x => x.Userprofile)
                                               .ThenInclude(x => x.User)
                                               .Where(x => (string.IsNullOrEmpty(model.department) || x.Department.Name.Contains(model.department))
                                               && (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone))
                                               && (string.IsNullOrEmpty(model.division) || x.Division.Name.Contains(model.division))
                                               && (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                                               && (string.IsNullOrEmpty(model.guard) || x.Userprofile.User.NormalizedUserName.Contains(model.guard)))
                                               //&& (string.IsNullOrEmpty(model.year) || x.Year.Contains(model.year)))
                                               .OrderByDescending(s => s.Department.Name)
                                               .GetPaged(model.PageNumber, model.PageSize);
                        break;

                    case ("ZONE"):
                        data = null;
                        data = await _dbContext.MonthlyRoaster
                                               .Where(x => x.IsActive == 1)
                                               .Include(x => x.Department)
                                               .Include(x => x.Zone)
                                               .Include(x => x.Division)
                                               .Include(x => x.Locality)
                                               .Include(x => x.Userprofile)
                                               .ThenInclude(x => x.User)
                                               .Where(x => (string.IsNullOrEmpty(model.department) || x.Department.Name.Contains(model.department))
                                               && (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone))
                                               && (string.IsNullOrEmpty(model.division) || x.Division.Name.Contains(model.division))
                                               && (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                                               && (string.IsNullOrEmpty(model.guard) || x.Userprofile.User.NormalizedUserName.Contains(model.guard)))
                                               //&& (string.IsNullOrEmpty(model.year) || x.Year.Contains(model.year)))
                                               .OrderByDescending(s => s.Zone.Name)
                                               .GetPaged(model.PageNumber, model.PageSize);
                        break;
                    case ("DIV"):
                        data = null;
                        data = await _dbContext.MonthlyRoaster
                                               .Where(x => x.IsActive == 1)
                                               .Include(x => x.Department)
                                               .Include(x => x.Zone)
                                               .Include(x => x.Division)
                                               .Include(x => x.Locality)
                                               .Include(x => x.Userprofile)
                                               .ThenInclude(x => x.User)
                                               .Where(x => (string.IsNullOrEmpty(model.department) || x.Department.Name.Contains(model.department))
                                               && (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone))
                                               && (string.IsNullOrEmpty(model.division) || x.Division.Name.Contains(model.division))
                                               && (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                                               && (string.IsNullOrEmpty(model.guard) || x.Userprofile.User.NormalizedUserName.Contains(model.guard)))
                                               //&& (string.IsNullOrEmpty(model.year) || x.Year.Contains(model.year)))
                                               .OrderByDescending(s => s.Division.Name)
                                               .GetPaged(model.PageNumber, model.PageSize);
                        break;
                    case ("LOC"):
                        data = null;
                        data = await _dbContext.MonthlyRoaster
                                               .Where(x => x.IsActive == 1)
                                               .Include(x => x.Department)
                                               .Include(x => x.Zone)
                                               .Include(x => x.Division)
                                               .Include(x => x.Locality)
                                               .Include(x => x.Userprofile)
                                               .ThenInclude(x => x.User)
                                               .Where(x => (string.IsNullOrEmpty(model.department) || x.Department.Name.Contains(model.department))
                                               && (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone))
                                               && (string.IsNullOrEmpty(model.division) || x.Division.Name.Contains(model.division))
                                               && (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                                               && (string.IsNullOrEmpty(model.guard) || x.Userprofile.User.NormalizedUserName.Contains(model.guard)))
                                               //&& (string.IsNullOrEmpty(model.year) || x.Year.Contains(model.year)))
                                               .OrderByDescending(s => s.Locality.Name)
                                               .GetPaged(model.PageNumber, model.PageSize);
                        break;
                    case ("SECURITY"):
                        data = null;
                        data = await _dbContext.MonthlyRoaster
                                               .Where(x => x.IsActive == 1)
                                               .Include(x => x.Department)
                                               .Include(x => x.Zone)
                                               .Include(x => x.Division)
                                               .Include(x => x.Locality)
                                               .Include(x => x.Userprofile)
                                               .ThenInclude(x => x.User)
                                               .Where(x => (string.IsNullOrEmpty(model.department) || x.Department.Name.Contains(model.department))
                                               && (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone))
                                               && (string.IsNullOrEmpty(model.division) || x.Division.Name.Contains(model.division))
                                               && (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                                               && (string.IsNullOrEmpty(model.guard) || x.Userprofile.User.NormalizedUserName.Contains(model.guard)))
                                               //&& (string.IsNullOrEmpty(model.year) || x.Year.Contains(model.year)))
                                               .OrderByDescending(s => s.Userprofile.User.NormalizedUserName)
                                               .GetPaged(model.PageNumber, model.PageSize);
                        break;
                    case ("YEAR"):
                        data = null;
                        data = await _dbContext.MonthlyRoaster
                                               .Where(x => x.IsActive == 1)
                                               .Include(x => x.Department)
                                               .Include(x => x.Zone)
                                               .Include(x => x.Division)
                                               .Include(x => x.Locality)
                                               .Include(x => x.Userprofile)
                                               .ThenInclude(x => x.User)
                                               .Where(x => (string.IsNullOrEmpty(model.department) || x.Department.Name.Contains(model.department))
                                               && (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone))
                                               && (string.IsNullOrEmpty(model.division) || x.Division.Name.Contains(model.division))
                                               && (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                                               && (string.IsNullOrEmpty(model.guard) || x.Userprofile.User.NormalizedUserName.Contains(model.guard)))
                                               //&& (string.IsNullOrEmpty(model.year) || x.Year.Contains(model.year)))
                                               .OrderByDescending(s => s.Year)
                                               .GetPaged(model.PageNumber, model.PageSize);
                        break;
                    case ("MONTH"):
                        data = null;
                        data = await _dbContext.MonthlyRoaster
                                               .Where(x => x.IsActive == 1)
                                               .Include(x => x.Department)
                                               .Include(x => x.Zone)
                                               .Include(x => x.Division)
                                               .Include(x => x.Locality)
                                               .Include(x => x.Userprofile)
                                               .ThenInclude(x => x.User)
                                               .Where(x => (string.IsNullOrEmpty(model.department) || x.Department.Name.Contains(model.department))
                                               && (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone))
                                               && (string.IsNullOrEmpty(model.division) || x.Division.Name.Contains(model.division))
                                               && (string.IsNullOrEmpty(model.locality) || x.Locality.Name.Contains(model.locality))
                                               && (string.IsNullOrEmpty(model.guard) || x.Userprofile.User.NormalizedUserName.Contains(model.guard)))
                                               //&& (string.IsNullOrEmpty(model.year) || x.Year.Contains(model.year)))
                                               .OrderByDescending(s => s.Month)
                                               .GetPaged(model.PageNumber, model.PageSize);
                        break;

                }
            }
            return data;
        }
    }
}
