using System;
using System.Collections.Generic;
using System.Text;
//using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;

namespace Libraries.Repository.EntityRepository
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {

        public DepartmentRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Department>> GetDepartment()
        {
            return await _dbContext.Department.ToListAsync();
        }
    }


}
