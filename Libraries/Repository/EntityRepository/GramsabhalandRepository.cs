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
using Dto.Search;

using Repository.Common;

namespace Libraries.Repository.EntityRepository
{
  public  class GramsabhalandRepository : GenericRepository<Gramsabhaland>, IGramsabhalandRepository
    {

        public GramsabhalandRepository(DataContext dbContext) : base(dbContext)
        {

        }

      
      public async Task<List<Gramsabhaland>> GetAllGramsabhaland()
        {
            return await _dbContext.Gramsabhaland.Include(x => x.Zone).Include(x => x.Village).OrderByDescending(x => x.Id).ToListAsync();
        }
        public async Task<List<Zone>> GetAllZone()
        {
            List<Zone> zonelist = await _dbContext.Zone.Where(x => x.IsActive == 1).ToListAsync();
            return zonelist;
        }


        public async Task<List<Acquiredlandvillage>> GetAllVillage(int? zoneId)
        {
            List<Acquiredlandvillage> villageList = await _dbContext.Acquiredlandvillage.Where(x => x.ZoneId == zoneId && x.IsActive == 1).ToListAsync();
            return villageList;
        }




        public async Task<PagedResult<Gramsabhaland>> GetPagedGramsabhaland(GramsabhalandSearchDto model)
        {
            var data = await _dbContext.Gramsabhaland
                                
                                   .Include(x => x.Zone)
                                   .Include(x => x.Village)
                                   //.Where(x => (string.IsNullOrEmpty(model.Zone) || x.Name.Contains(model.name))
                                   // && (string.IsNullOrEmpty(model.tehsil) || x.Tehsil.Name.Contains(model.tehsil))
                                   // && (string.IsNullOrEmpty(model.district) || x.District.Name.Contains(model.district)))
                                   .GetPaged<Gramsabhaland>(model.PageNumber, model.PageSize);

            //int SortOrder = (int)model.SortOrder;
            //if (SortOrder == 1)
            //{
            //    switch (model.SortBy.ToUpper())
            //    {
            //        case ("CODE"):
            //            data = null;
            //            data = await _dbContext.Acquiredlandvillage
            //                       .Include(x => x.District)
            //                       .Include(x => x.Tehsil)
            //                       .Include(x => x.Zone)
            //                       .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
            //                        && (string.IsNullOrEmpty(model.tehsil) || x.Tehsil.Name.Contains(model.tehsil))
            //                        && (string.IsNullOrEmpty(model.district) || x.District.Name.Contains(model.district)))
            //                       .OrderBy(a => a.Code)
            //                       .GetPaged<Acquiredlandvillage>(model.PageNumber, model.PageSize);
            //            break;
            //        case ("NAME"):
            //            data = null;
            //            data = await _dbContext.Acquiredlandvillage
            //                       .Include(x => x.District)
            //                       .Include(x => x.Tehsil)
            //                       .Include(x => x.Zone)
            //                       .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
            //                        && (string.IsNullOrEmpty(model.tehsil) || x.Tehsil.Name.Contains(model.tehsil))
            //                        && (string.IsNullOrEmpty(model.district) || x.District.Name.Contains(model.district)))
            //                       .OrderBy(a => a.Name)
            //                       .GetPaged<Acquiredlandvillage>(model.PageNumber, model.PageSize);
            //            break;

            //        case ("TEHSIL"):
            //            data = null;
            //            data = await _dbContext.Acquiredlandvillage
            //                       .Include(x => x.District)
            //                       .Include(x => x.Tehsil)
            //                       .Include(x => x.Zone)
            //                       .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
            //                        && (string.IsNullOrEmpty(model.tehsil) || x.Tehsil.Name.Contains(model.tehsil))
            //                        && (string.IsNullOrEmpty(model.district) || x.District.Name.Contains(model.district)))
            //                       .OrderBy(a => a.Tehsil.Name)
            //                       .GetPaged<Acquiredlandvillage>(model.PageNumber, model.PageSize);
            //            break;
            //        case ("DISTRICT"):
            //            data = null;
            //            data = await _dbContext.Acquiredlandvillage
            //                       .Include(x => x.District)
            //                       .Include(x => x.Tehsil)
            //                       .Include(x => x.Zone)
            //                       .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
            //                        && (string.IsNullOrEmpty(model.tehsil) || x.Tehsil.Name.Contains(model.tehsil))
            //                        && (string.IsNullOrEmpty(model.district) || x.District.Name.Contains(model.district)))
            //                       .OrderBy(a => a.District.Name)
            //                       .GetPaged<Acquiredlandvillage>(model.PageNumber, model.PageSize);
            //            break;


            //        case ("STATUS"):
            //            data = null;
            //            data = await _dbContext.Acquiredlandvillage
            //                       .Include(x => x.District)
            //                       .Include(x => x.Tehsil)
            //                       .Include(x => x.Zone)
            //                       .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
            //                        && (string.IsNullOrEmpty(model.tehsil) || x.Tehsil.Name.Contains(model.tehsil))
            //                        && (string.IsNullOrEmpty(model.district) || x.District.Name.Contains(model.district)))
            //                       .OrderByDescending(a => a.IsActive)
            //                       .GetPaged<Acquiredlandvillage>(model.PageNumber, model.PageSize);
            //            break;


            //    }
            //}
            //else if (SortOrder == 2)
            //{
            //    switch (model.SortBy.ToUpper())
            //    {
            //        case ("CODE"):
            //            data = null;
            //            data = await _dbContext.Acquiredlandvillage
            //                       .Include(x => x.District)
            //                       .Include(x => x.Tehsil)
            //                       .Include(x => x.Zone)
            //                       .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
            //                        && (string.IsNullOrEmpty(model.tehsil) || x.Tehsil.Name.Contains(model.tehsil))
            //                        && (string.IsNullOrEmpty(model.district) || x.District.Name.Contains(model.district)))
            //                       .OrderByDescending(a => a.Code)
            //                       .GetPaged<Acquiredlandvillage>(model.PageNumber, model.PageSize);
            //            break;
            //        case ("NAME"):
            //            data = null;
            //            data = await _dbContext.Acquiredlandvillage
            //                       .Include(x => x.District)
            //                       .Include(x => x.Tehsil)
            //                       .Include(x => x.Zone)
            //                       .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
            //                        && (string.IsNullOrEmpty(model.tehsil) || x.Tehsil.Name.Contains(model.tehsil))
            //                        && (string.IsNullOrEmpty(model.district) || x.District.Name.Contains(model.district)))
            //                       .OrderByDescending(a => a.Name)
            //                       .GetPaged<Acquiredlandvillage>(model.PageNumber, model.PageSize);
            //            break;

            //        case ("TEHSIL"):
            //            data = null;
            //            data = await _dbContext.Acquiredlandvillage
            //                       .Include(x => x.District)
            //                       .Include(x => x.Tehsil)
            //                       .Include(x => x.Zone)
            //                       .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
            //                        && (string.IsNullOrEmpty(model.tehsil) || x.Tehsil.Name.Contains(model.tehsil))
            //                        && (string.IsNullOrEmpty(model.district) || x.District.Name.Contains(model.district)))
            //                       .OrderByDescending(a => a.Tehsil.Name)
            //                       .GetPaged<Acquiredlandvillage>(model.PageNumber, model.PageSize);
            //            break;
            //        case ("DISTRICT"):
            //            data = null;
            //            data = await _dbContext.Acquiredlandvillage
            //                       .Include(x => x.District)
            //                       .Include(x => x.Tehsil)
            //                       .Include(x => x.Zone)
            //                       .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
            //                        && (string.IsNullOrEmpty(model.tehsil) || x.Tehsil.Name.Contains(model.tehsil))
            //                        && (string.IsNullOrEmpty(model.district) || x.District.Name.Contains(model.district)))
            //                       .OrderByDescending(a => a.District.Name)
            //                       .GetPaged<Acquiredlandvillage>(model.PageNumber, model.PageSize);
            //            break;


            //        case ("STATUS"):
            //            data = null;
            //            data = await _dbContext.Acquiredlandvillage
            //                       .Include(x => x.District)
            //                       .Include(x => x.Tehsil)
            //                       .Include(x => x.Zone)
            //                       .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
            //                        && (string.IsNullOrEmpty(model.tehsil) || x.Tehsil.Name.Contains(model.tehsil))
            //                        && (string.IsNullOrEmpty(model.district) || x.District.Name.Contains(model.district)))
            //                       .OrderBy(a => a.IsActive)
            //                       .GetPaged<Acquiredlandvillage>(model.PageNumber, model.PageSize);
            //            break;
            //    }
            //}
           
            
            
            
            return data;
        }








    }
}
