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
            return await _dbContext.Planning.Where(x =>x.IsActive == 1).GetPaged(1,10);
        }
        public async Task<List<Zone>> GetAllZone(int DepartmentId)
        {
            return await _dbContext.Zone.Where(x => (x.IsActive == 1) && (x.DepartmentId == DepartmentId)).ToListAsync();
        }
    }
}