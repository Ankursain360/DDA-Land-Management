using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
     public class DisposallandtypeRepository : GenericRepository<Disposallandtype>, IDisposallandtypeRepository
    {
        public DisposallandtypeRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Disposallandtype>> GetDisposallandtype()
        {
            return await _dbContext.Disposallandtype.ToListAsync();
        }
        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Disposallandtype.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        }
    }
}
