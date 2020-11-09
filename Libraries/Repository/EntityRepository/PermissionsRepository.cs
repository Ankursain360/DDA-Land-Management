using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Libraries.Repository.EntityRepository
{
    public class PermissionsRepository : GenericRepository<Menuactionrolemap>, IPermissionsRepository
    {

        public PermissionsRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Module>> GetModuleList()
        {
            return await _dbContext.Module.Where(x => x.IsActive == 1).ToListAsync();
        }
    }


}
