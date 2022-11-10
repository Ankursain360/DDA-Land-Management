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
        public async Task<List<Gramsabhaland>> GetAllGramsabhalandList(GramsabhalandSearchDto model)
        {
            var data = await _dbContext.Gramsabhaland                 
                                   .Include(x => x.Zone)
                                   .Include(x => x.Village)
                                   .Where(x => (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone))
                                    && (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village))
                                    && (string.IsNullOrEmpty(model.khasra) || x.KhasraNo.Contains(model.khasra))).ToListAsync();
            return data;
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
                                   .Where(x => (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone))
                                    && (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village))
                                    && (string.IsNullOrEmpty(model.khasra) || x.KhasraNo.Contains(model.khasra)))
                                   .GetPaged<Gramsabhaland>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("ZONE"):
                        data = null;
                        data = await _dbContext.Gramsabhaland

                                   .Include(x => x.Zone)
                                   .Include(x => x.Village)
                                   .Where(x => (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone))
                                    && (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village))
                                    && (string.IsNullOrEmpty(model.khasra) || x.KhasraNo.Contains(model.khasra)))
                                   .OrderBy(a => a.Zone.Name)
                                   .GetPaged<Gramsabhaland>(model.PageNumber, model.PageSize);
                        break;
                    case ("VILLAGE"):
                        data = null;
                        data = await _dbContext.Gramsabhaland
                                  .Include(x => x.Zone)
                                   .Include(x => x.Village)
                                   .Where(x => (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone))
                                    && (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village))
                                    && (string.IsNullOrEmpty(model.khasra) || x.KhasraNo.Contains(model.khasra)))
                                   .OrderBy(a => a.Village.Name)
                                   .GetPaged<Gramsabhaland>(model.PageNumber, model.PageSize);
                        break;

                    case ("KHASRA"):
                        data = null;
                        data = await _dbContext.Gramsabhaland
                                 .Include(x => x.Zone)
                                  .Include(x => x.Village)
                                  .Where(x => (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone))
                                   && (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village))
                                   && (string.IsNullOrEmpty(model.khasra) || x.KhasraNo.Contains(model.khasra)))
                                  .OrderBy(a => a.KhasraNo)
                                  .GetPaged<Gramsabhaland>(model.PageNumber, model.PageSize);
                        break;
                  


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Gramsabhaland
                                  .Include(x => x.Zone)
                                   .Include(x => x.Village)
                                   .Where(x => (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone))
                                    && (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village))
                                    && (string.IsNullOrEmpty(model.khasra) || x.KhasraNo.Contains(model.khasra)))
                                    .OrderByDescending(a => a.IsActive)
                                   .GetPaged<Gramsabhaland>(model.PageNumber, model.PageSize);
                        break;


                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("ZONE"):
                        data = null;
                        data = await _dbContext.Gramsabhaland

                                   .Include(x => x.Zone)
                                   .Include(x => x.Village)
                                   .Where(x => (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone))
                                    && (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village))
                                    && (string.IsNullOrEmpty(model.khasra) || x.KhasraNo.Contains(model.khasra)))
                                   .OrderByDescending(a => a.Zone.Name)
                                   .GetPaged<Gramsabhaland>(model.PageNumber, model.PageSize);
                        break;
                    case ("VILLAGE"):
                        data = null;
                        data = await _dbContext.Gramsabhaland
                                  .Include(x => x.Zone)
                                   .Include(x => x.Village)
                                   .Where(x => (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone))
                                    && (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village))
                                    && (string.IsNullOrEmpty(model.khasra) || x.KhasraNo.Contains(model.khasra)))
                                   .OrderByDescending(a => a.Village.Name)
                                   .GetPaged<Gramsabhaland>(model.PageNumber, model.PageSize);
                        break;
                    case ("KHASRA"):
                        data = null;
                        data = await _dbContext.Gramsabhaland
                                 .Include(x => x.Zone)
                                  .Include(x => x.Village)
                                  .Where(x => (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone))
                                   && (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village))
                                   && (string.IsNullOrEmpty(model.khasra) || x.KhasraNo.Contains(model.khasra)))
                                  .OrderByDescending(a => a.KhasraNo)
                                  .GetPaged<Gramsabhaland>(model.PageNumber, model.PageSize);
                        break;
                 


                      case ("STATUS"):
                        data = null;
                        data = await _dbContext.Gramsabhaland
                                  .Include(x => x.Zone)
                                   .Include(x => x.Village)
                                   .Where(x => (string.IsNullOrEmpty(model.zone) || x.Zone.Name.Contains(model.zone))
                                    && (string.IsNullOrEmpty(model.village) || x.Village.Name.Contains(model.village))
                                    && (string.IsNullOrEmpty(model.khasra) || x.KhasraNo.Contains(model.khasra)))
                                    .OrderBy(a => a.IsActive)
                                   .GetPaged<Gramsabhaland>(model.PageNumber, model.PageSize);
                        break;
                }
            }




            return data;
        }








    }
}
