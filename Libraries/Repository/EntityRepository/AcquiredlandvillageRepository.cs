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
  public  class AcquiredlandvillageRepository:GenericRepository<Acquiredlandvillage>,IAcquiredlandvillageRepository
    {
        public AcquiredlandvillageRepository(DataContext dbContext) : base(dbContext)
        {

        }



        public async Task<List<District>> GetAllDistrict()
        {
            List<District> districtList = await _dbContext.District.ToListAsync();
            return districtList;
        }
        public async Task<List<Tehsil>> GetAllTehsil()
        {
            List<Tehsil> tehsilList = await _dbContext.Tehsil.ToListAsync();
            return tehsilList;
        }

        public async Task<List<Villagetype>> GetAllVillagetype()
        {
            List<Villagetype> villagetypelist = await _dbContext.Villagetype.ToListAsync();
            return villagetypelist;
        }


       

        public async Task<List<Acquiredlandvillage>> GetAcquiredlandvillage()
        {
            return await _dbContext.Acquiredlandvillage.Include(x => x.District).Include(x => x.Tehsil).Include(x=>x.Villagetype).OrderByDescending(x => x.Id).ToListAsync();
        }






    }
}
