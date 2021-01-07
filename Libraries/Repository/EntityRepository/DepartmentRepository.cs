using System;
using System.Collections.Generic;
using System.Text;
using Dto.Search;
using System.Threading.Tasks;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Libraries.Repository.EntityRepository
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {

        public DepartmentRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Department>> GetPagedDepartment(DepartmentSearchDto model)
        {
            var data = await _dbContext.Department
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))
                            .OrderBy(s => s.Name)
                            .OrderByDescending(s => s.IsActive)

                        .GetPaged<Department>(model.PageNumber, model.PageSize); ;
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data.Results = data.Results.OrderBy(x => x.Name).ToList();
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data.Results = data.Results.OrderByDescending(x => x.Name).ToList();
                        break;
                   
                }
            }
            return data;
        }
        public async Task<List<Department>> GetDepartment()
        {
            return await _dbContext.Department.Where(x => x.IsActive == 1).ToListAsync();
        }

        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Department.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        }
    }

}
