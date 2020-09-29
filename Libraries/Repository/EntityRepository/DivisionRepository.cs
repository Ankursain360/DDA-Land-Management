using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ubiety.Dns.Core.Records.NotUsed;
namespace Libraries.Repository.EntityRepository
{
    public class DivisionRepository : GenericRepository<Division>, IDivisionRepository
    {
        public DivisionRepository(DataContext dbcontext) : base(dbcontext)
        { }
        public async Task<PagedResult<Division>> GetPagedDivision(DivisionSearchDto model)
        {
            return await _dbContext.Division.GetPaged<Division>(model.PageNumber, model.PageSize);
        }
        public async Task<List<Division>> GetDivisions()
        {
            var data = await _dbContext.Division.Include(x => x.Zone).Include(x => x.Department).OrderByDescending(x => x.Id).ToListAsync();
            return data;
        }


     

        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Division.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        }



        public async Task<List<Zone>> GetAllZone(int departmentId)
        {
            List<Zone> zoneList = await _dbContext.Zone.Where(x => x.DepartmentId == departmentId).ToListAsync();
            return zoneList;
        }
        public async Task<List<Department>> GetAllDepartment()
        {
            List<Department> departmentList = await _dbContext.Department.ToListAsync();
            return departmentList;
        }


     



    }
}
