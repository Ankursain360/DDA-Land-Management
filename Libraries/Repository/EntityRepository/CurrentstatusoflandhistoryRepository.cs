using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Repository.IEntityRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class CurrentstatusoflandhistoryRepository : GenericRepository<Currentstatusoflandhistory>, ICurrentstatusoflandhistoryRepository

    {

        public CurrentstatusoflandhistoryRepository(DataContext dbContext) : base(dbContext)
        {

        }
       
        public async Task<List<Currentstatusoflandhistory>> GetCurrentstatusoflandhistory(int id)
        {
            return await _dbContext.Currentstatusoflandhistory.Where(x => x.LandTransferId == id).Include(x=>x.LandTransfer).ToListAsync();
        }

        public async Task<Currentstatusoflandhistory> FetchSingleResult(int id)
        {
            return await _dbContext.Currentstatusoflandhistory
               .Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
