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

     public class DisposallandRepository : GenericRepository<Disposalland>, IDisposallandRepository
    {
        public DisposallandRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Disposalland>> GetDisposalland()
        {
            return await _dbContext.Disposalland.ToListAsync();
        }
        public async Task<List<Disposalland>> GetAllDisposalland()
        {
            return await _dbContext.Disposalland.Include(x => x.Utilizationtype).Include(x => x.Village).Include(x => x.Khasra).ToListAsync();


        }
        public async Task<List<Utilizationtype>> GetAllUtilizationtype()
        {
            List<Utilizationtype> utilizationtypeList = await _dbContext.Utilizationtype.Where(x => x.IsActive == 1).ToListAsync();
            return utilizationtypeList;
        }
        public async Task<List<Village>> GetAllVillage()
        {
            List<Village> villageList = await _dbContext.Village.Where(x => x.IsActive == 1).ToListAsync();
            return villageList;
        }

        public async Task<List<Khasra>> GetAllKhasra()
        {
            List<Khasra> khasraList = await _dbContext.Khasra.Where(x => x.IsActive == 1).ToListAsync();
            return khasraList;
        }

       
    }
}
