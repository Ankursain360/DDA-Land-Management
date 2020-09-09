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

namespace Libraries.Repository.EntityRepository
{
    public class LocalityRepository:GenericRepository<Locality>,ILocalityRepository
    {
        public LocalityRepository(DataContext dbContext) : base(dbContext)
        {

        }
        //public async Task<List<Village>> GetLocality()
        //{
        //    List<Village> lst = new List<Village>();
        //    List<Zone> zoneList = await _dbContext.Zone.ToListAsync();
        //    return lst;
        //    //return await _dbContext.Village.Include(x => x.Zone).OrderByDescending(x => x.Id).ToListAsync();
        //}
        public async Task<List<Zone>> GetAllZone()
        {
            List<Zone> zoneList = await _dbContext.Zone.ToListAsync();
            return zoneList;
        }
        public async Task<List<Department>> GetAllDepartment()
        {
            List<Department> departmentList = await _dbContext.Department.ToListAsync();
            return departmentList;
        }
        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Village.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        }

        Task<List<Locality>> GetLocality()
        {
            throw new NotImplementedException();
        }

        Task<List<Locality>> ILocalityRepository.GetLocality()
        {
            throw new NotImplementedException();
        }
    }
}
