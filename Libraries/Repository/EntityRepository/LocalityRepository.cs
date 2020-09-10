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
    public class LocalityRepository:GenericRepository<Locality>,ILocalityRepository
    {
        public LocalityRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<List<Zone>> GetAllZone(int departmentId)
        {
            List<Zone> zoneList = await _dbContext.Zone.Where(x=>x.DepartmentId==departmentId).ToListAsync();
            return zoneList;
        }
        public async Task<List<Department>> GetAllDepartment()
        {
            List<Department> departmentList = await _dbContext.Department.ToListAsync();
            return departmentList;
        }
        public async Task<bool> AnyName(int id, string name)
        {
            return await _dbContext.Locality.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        }
        public async Task<bool> AnyCode(int id, string code)
        {
            return await _dbContext.Locality.AnyAsync(t => t.Id != id && t.LocalityCode.ToLower() == code.ToLower());
        }

        public async Task<List<Locality>> GetAllLocality()
        {
            var data = await _dbContext.Locality.Include(x => x.Zone).Include(x => x.Department).OrderByDescending(x => x.Id).ToListAsync();
            return data;
        }
    }
}
