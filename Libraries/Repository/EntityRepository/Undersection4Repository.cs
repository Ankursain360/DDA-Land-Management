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

namespace Libraries.Repository.EntityRepository
{
    public class Undersection4Repository : GenericRepository<Undersection4>, IUndersection4Repository
    {
        public Undersection4Repository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Purpose>> GetAllPurpose()
        {
            List<Purpose> purposeList = await _dbContext.Purpose.Where(x => x.IsActive == 1).ToListAsync();
            return purposeList;
        }
        public async Task<List<Undersection4>> GetAllUndersection4()
        {
            return await _dbContext.Undersection4.Include(x => x.Purpose).OrderByDescending(x => x.Id).ToListAsync();
        }



   

        public async Task<bool> Any(int id, string number)
        {
            return await _dbContext.Undersection4.AnyAsync(t => t.Id != id && t.Number == number);
        }
    }
}
