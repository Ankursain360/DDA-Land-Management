using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Dto.Master;
using Repository.Common;
using System.Linq.Expressions;

namespace Libraries.Repository.EntityRepository
{
    public class RestorationEntryRepository : GenericRepository<Restorationentry>, IRestorationEntryRepository
    {
        public RestorationEntryRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Restorationentry.AnyAsync(t => t.Id != id);
        }
    }
}
