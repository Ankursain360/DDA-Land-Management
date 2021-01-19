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
                var data = await _dbContext.Division
                            .Include(x => x.Zone)
                            .Include(x => x.Department)
                             .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                      && (string.IsNullOrEmpty(model.code) || x.Code.Contains(model.code))
                                        )

                                .OrderByDescending(s =>s.IsActive)
                                .ThenBy(s => s.Department.Name)
                                .ThenBy(s => s.Zone.Name)
                                .ThenBy(s => s.Name)
                                
                            .GetPaged<Division>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("DEPARTMENT"):
                        data.Results = data.Results.OrderBy(x => x.Department.Name).ToList();
                        break;
                    case ("ZONE"):
                        data.Results = data.Results.OrderBy(x => x.Zone.Name).ToList();
                        break;
                    case ("CODE"):
                        data.Results = data.Results.OrderBy(x => x.Code).ToList();
                        break;
                    case ("NAME"):
                        data.Results = data.Results.OrderBy(x => x.Name).ToList();
                        break;
                  
                    case ("STATUS"):
                        data.Results = data.Results.OrderBy(x => x.IsActive).ToList();
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("DEPARTMENT"):
                        data.Results = data.Results.OrderByDescending(x => x.Department.Name).ToList();
                        break;
                    case ("ZONE"):
                        data.Results = data.Results.OrderByDescending(x => x.Zone.Name).ToList();
                        break;
                    case ("CODE"):
                        data.Results = data.Results.OrderByDescending(x => x.Code).ToList();
                        break;
                    case ("NAME"):
                        data.Results = data.Results.OrderByDescending(x => x.Name).ToList();
                        break;
                   
                    case ("STATUS"):
                        data.Results = data.Results.OrderByDescending(x => x.IsActive).ToList();
                        break;
                }
            }
            return data;

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
            List<Zone> zoneList = await _dbContext.Zone.Where(x => x.DepartmentId == departmentId && x.IsActive==1).ToListAsync();
            return zoneList;
        }
        public async Task<List<Department>> GetAllDepartment()
        {
            List<Department> departmentList = await _dbContext.Department.Where(x => x.IsActive == 1).ToListAsync();
            return departmentList;
        }


     



    }
}
