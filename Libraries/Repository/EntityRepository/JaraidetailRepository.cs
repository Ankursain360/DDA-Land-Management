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
    public class JaraidetailRepository : GenericRepository<Jaraidetail>, IJaraidetailRepository
    {
        public JaraidetailRepository(DataContext dbContext) : base(dbContext)
        {

        }


        public async Task<List<Khewat>> GetAllKhewat()
        {
            List<Khewat> awardList = await _dbContext.Khewat.Where(x => x.IsActive == 1).ToListAsync();
            return awardList;
        }
        public async Task<List<Jaraidetail>> GetJaraidetail()
        {
            return await _dbContext.Jaraidetail.Include(x => x.Khewat).Include(x => x.Village).Include(x => x.Khasra).Include(x => x.Khatauni).Include(x => x.Taraf).OrderByDescending(x => x.Id).ToListAsync();
        }


        public async Task<List<Acquiredlandvillage>> GetAllVillage()
        {
            List<Acquiredlandvillage> villageList = await _dbContext.Acquiredlandvillage.Where(x => x.IsActive == 1).ToListAsync();
            return villageList;
        }



        public async Task<List<Khasra>> BindKhasra()
        {
            List<Khasra> KhasraList = await _dbContext.Khasra.Where(x => x.IsActive == 1).ToListAsync();
            return KhasraList;
        }



        public async Task<List<Taraf>> GetAllTaraf()
        {
            List<Taraf> TarafList = await _dbContext.Taraf.Where(x => x.IsActive == 1).ToListAsync();
            return TarafList;
        }



        public async Task<List<Khatauni>> GetAllKhatauni()
        {
            List<Khatauni> KhatauniList = await _dbContext.Khatauni.Where(x => x.IsActive == 1).ToListAsync();
            return KhatauniList;
        }




    }
}
