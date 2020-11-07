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
    public class PlanningRepositry : GenericRepository<Planning>, IPlanningRepositry
    {
        public PlanningRepositry(DataContext dbcontext) : base(dbcontext)
        { }
        public async Task<List<Department>> GetAllDepartment()
        {
            return await _dbContext.Department.Where(x => x.IsActive == 1).ToListAsync();
        }
        public async Task<List<Division>> GetAllDivision(int ZoneId)
        {
            return await _dbContext.Division.Where(x => x.IsActive == 1 && x.ZoneId == ZoneId).ToListAsync();
        }
        public async Task<PagedResult<Planning>> GetPagedPlanning()
        {
                return await _dbContext.Planning.Include(x => x.PlanningProperties).ThenInclude(x => x.PropertyRegistration).Include(x => x.Department).Include(x => x.Zone).Include(x => x.Division).Include(x => x.Zone).Where(x => x.IsActive == 1).GetPaged(1, 10);
        }
        public async Task<List<Zone>> GetAllZone(int DepartmentId)
        {
            return await _dbContext.Zone.Where(x => (x.IsActive == 1) && (x.DepartmentId == DepartmentId)).ToListAsync();
        }
        public async Task<List<Propertyregistration>> GetPlannedProperties(int departmentId, int zoneId, int divisionId)
        {
            return await _dbContext.Propertyregistration.Where(x => (x.IsActive == 1) && (x.DepartmentId == departmentId) && (x.ZoneId == zoneId) && (x.DivisionId == divisionId) && (x.IsValidate==1) && (x.PlannedUnplannedLand== "Planned Land")).ToListAsync();
        }
        public async Task<List<Propertyregistration>> GetUnplannedProperties(int departmentId, int zoneId, int divisionId)
        {
            return await _dbContext.Propertyregistration.Where(x => (x.IsActive == 1) && (x.DepartmentId == departmentId) && (x.ZoneId == zoneId) && (x.DivisionId == divisionId)&& (x.IsValidate == 1) && (x.PlannedUnplannedLand == "Unplanned Land")).ToListAsync();
        }
        public async Task<bool> CreateProperties(List<PlanningProperties> planningProperties)
        {
            int res = planningProperties.Select(x => x.PlanningId).FirstOrDefault();
            var data = await _dbContext.PlanningProperties.Where(x => x.PlanningId == res).ToListAsync();
            _dbContext.PlanningProperties.RemoveRange(data);
            var result = await _dbContext.SaveChangesAsync();
            await _dbContext.PlanningProperties.AddRangeAsync(planningProperties);
            result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }
        public async Task<List<int>> FetchUnplannedProperties(int id)
        {
            return await _dbContext.PlanningProperties.Where(x=>x.PlanningId==id && x.PropertyType==0&& x.IsActive==1).Select(x => x.PropertyRegistrationId).ToListAsync();
        }

        public async Task<List<int>> FetchPlannedProperties(int id)
        {
            return await _dbContext.PlanningProperties.Where(x => x.PlanningId == id && x.PropertyType == 1 && x.IsActive == 1).Select(x => x.PropertyRegistrationId).ToListAsync();
        }
    }
}