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
    public class PageRoleRepository : GenericRepository<PageRole>, IPageRoleRepository
    {
        public PageRoleRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Module>> GetAllModule()
        {
            return await _dbContext.Module.ToListAsync();
        }

        public async Task<List<PageRole>> GetAllPageRole()
        {
            return await _dbContext.PageRole.Include(x=>x.Role).Include(x=>x.User).ToListAsync();
        }

        public async Task<List<Role>> GetAllRole()
        {
            return await _dbContext.Role.ToListAsync();
        }

        public async Task<List<User>> GetAllUser(int role)
        {
            return await _dbContext.User.Where(x=>x.RoleId==role).ToListAsync();
        }
    }
}
