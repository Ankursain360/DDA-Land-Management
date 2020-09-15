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

        public async Task<List<Villagetype>> GetAllVillagetype()
        {
            List<Villagetype> villagetypelist = await _dbContext.Villagetype.ToListAsync();
            return villagetypelist;
        }




        public async Task<List<Khasra>> GetAllKhasra()
        {
            return await _dbContext.Khasra.Include(x => x.LandCategory).Include(x => x.LandCategory).Include(x => x.Villagetype).OrderByDescending(x => x.Id).ToListAsync();
        }






    }
}
