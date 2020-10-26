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
            return await _dbContext.Department
                            .Where(x => x.IsActive == 1)
                            .OrderBy(s => s.Name)
                        .GetPaged<Department>(model.PageNumber, model.PageSize);
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
