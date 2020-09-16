using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Libraries.Repository.EntityRepository
{
    public class KhasraRepository : GenericRepository<Khasra>, IKhasraRepository
    {
        public KhasraRepository(DataContext dbContext) : base(dbContext)
        {

        }



        
        public async Task<List<LandCategory>> GetAllLandCategory()
        {
            List<LandCategory> landcategoryList = await _dbContext.LandCategory.ToListAsync();
            return landcategoryList;
        }

        public async Task<List<Village>> GetAllVillage()
        {
            List<Village> villagelist = await _dbContext.Village.ToListAsync();
            return villagelist;
        }




        public async Task<List<Khasra>> GetAllKhasra()
        {
            return await _dbContext.Khasra.Include(x => x.LandCategory).Include(x => x.Village).OrderByDescending(x => x.Id).ToListAsync();
        }






    }
}
