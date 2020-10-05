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
    public class NazulRepository : GenericRepository<Nazul>, INazulRepository
    {
        public NazulRepository(DataContext dbContext) : base(dbContext)
        {

        }




       

        public async Task<List<Village>> GetAllVillage()
        {
            List<Village> villagelist = await _dbContext.Village.ToListAsync();
            return villagelist;
        }




        public async Task<List<Nazul>> GetAllNazul()
        {
            return await _dbContext.Nazul.Include(x => x.Village).OrderByDescending(x => x.Id).ToListAsync();
        }

        





    }
}
