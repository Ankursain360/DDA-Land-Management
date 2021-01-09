using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Model.Entity;
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

        public async Task<PagedResult<MonthlyRoaster>> GetAllRoasterDetails(MonthlyRoasterSearchDto monthlyRoasterSearchDto)
        {
            return await _dbContext.MonthlyRoaster.Include(x => x.Department).Include(x => x.Zone).Include(x => x.Division).Include(x => x.Locality).Include(x => x.Userprofile).ThenInclude(x => x.User).GetPaged(monthlyRoasterSearchDto.PageNumber, monthlyRoasterSearchDto.PageSize);
        }
    }
}
