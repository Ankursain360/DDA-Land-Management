using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Microsoft.EntityFrameworkCore;
using Libraries.Repository.IEntityRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{  
    public class PageRepository : GenericRepository<Page>, IPageRepository
    {
        public PageRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Page>> GetPage()
        {
            return await _dbContext.Page.ToListAsync();
        }
        public async Task<List<Page>> GetAllPage()
        {
            return await _dbContext.Page.Include(x=>x.Module).ToListAsync();
        }
        public async Task<List<Module>> GetAllModule()
        {
            List<Module> moduleList = await _dbContext.Module.ToListAsync();
            return moduleList;
        }
        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Page.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        }

       
    }
}
