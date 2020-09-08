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

        public async Task<List<Zone>> GetZone()
        {
            return await _dbContext.Zone.ToListAsync();
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
            var resultlist = await (from Department in _dbContext.Department select Department).ToListAsync();
           // var res = await _dbContext.Department.Select(x => new { x.Id, x.Name }).ToListAsync();

            //var resultlist = await (from u in _dbContext.Department
            //                        where u.Id == 1
            //                        orderby u.Name
            //                        select u).ToListAsync();

            //resultlist.Insert(0, new Department { Id = 0, Name = "Select" });


            //var  countries =await _dbContext.Department.AsNoTracking()
            //        .OrderBy(n => n.Name)
            //            .Select(n =>
            //            new SelectListItem
            //            {
            //                Value = n.Id.ToString(),
            //                Text = n.Name
            //            }).ToListAsync();
            //var countrytip = new SelectListItem()
            //{
            //    Value = null,
            //    Text = "--- select country ---"
            //};
            //countries.Insert(0, countrytip);

            return resultlist;
        }
    }


}
