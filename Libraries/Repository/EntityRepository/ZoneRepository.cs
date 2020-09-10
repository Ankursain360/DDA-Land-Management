using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            //List<Zone> olist = new List<Zone>();

            //var Data = await (from A in _dbContext.Zone
            //                  join B in _dbContext.Department on A.DepartmentId equals B.Id
            //                  select new
            //                  {
            //                      ZoneId = A.Id,
            //                      ZoneName = A.Name,
            //                      ZoneCode = A.Code,
            //                      DepartmentName = B.Name,
            //                      IsActive = A.IsActive
            //                  }).ToListAsync();

            //if (Data != null)
            //{
            //    for (int i = 0; i < Data.Count; i++)

            //    {
            //        olist.Add(new Zone()
            //        {
            //            Id = Data[i].ZoneId,
            //            Name = Data[i].ZoneName,
            //            Code = Data[i].ZoneCode,
            //            DepartmentName = Data[i].DepartmentName,
            //            IsActive = Data[i].IsActive
            //        });
            //    }
            //}
            //return (olist);

            var data = await _dbContext.Zone.Include(s => s.Department).OrderBy( s => s.Id).ToListAsync();
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
    }


}
