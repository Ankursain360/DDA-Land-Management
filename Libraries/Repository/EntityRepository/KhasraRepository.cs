using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
namespace Libraries.Repository.EntityRepository
{
    public class KhasraRepository : GenericRepository<Khasra>, IKhasraRepository
    {
        public KhasraRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Khasra>> GetPagedKhasra(KhasraMasterSearchDto model)
        {
            //return await _dbContext.Khasra.
            //    Where(x => x.IsActive == 1).Include(x => x.Acquiredlandvillage).
            //    Include(x => x.LandCategory).GetPaged<Khasra>(model.PageNumber, model.PageSize);
            var data = await _dbContext.Khasra
                         .Include(x => x.Acquiredlandvillage)
                         .Include(x => x.LandCategory)
                        
                         .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                          && (string.IsNullOrEmpty(model.village) || x.Acquiredlandvillage.Name.Contains(model.village))
                          && (string.IsNullOrEmpty(model.rectNo) || x.RectNo.Contains(model.rectNo)))
                         .GetPaged<Khasra>(model.PageNumber, model.PageSize);


            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Khasra
                                     .Include(x => x.Acquiredlandvillage)
                         .Include(x => x.LandCategory)

                         .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                          && (string.IsNullOrEmpty(model.village) || x.Acquiredlandvillage.Name.Contains(model.village))
                          && (string.IsNullOrEmpty(model.rectNo) || x.RectNo.Contains(model.rectNo)))
                                    .OrderBy(a => a.Name)
                                    .GetPaged<Khasra>(model.PageNumber, model.PageSize);
                        break;

                    case ("ACQUIREDLANDVILLAGE"):
                        data = null;
                        data = await _dbContext.Khasra
                                     .Include(x => x.Acquiredlandvillage)
                         .Include(x => x.LandCategory)

                         .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                          && (string.IsNullOrEmpty(model.village) || x.Acquiredlandvillage.Name.Contains(model.village))
                          && (string.IsNullOrEmpty(model.rectNo) || x.RectNo.Contains(model.rectNo)))
                                    .OrderBy(a => a.Acquiredlandvillage.Name)
                                    .GetPaged<Khasra>(model.PageNumber, model.PageSize);
                        break;
                    case ("RECTNO"):
                        data = null;
                        data = await _dbContext.Khasra
                                    .Include(x => x.Acquiredlandvillage)
                        .Include(x => x.LandCategory)

                        .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                         && (string.IsNullOrEmpty(model.village) || x.Acquiredlandvillage.Name.Contains(model.village))
                         && (string.IsNullOrEmpty(model.rectNo) || x.RectNo.Contains(model.rectNo)))
                                   .OrderBy(a => a.RectNo)
                                    .GetPaged<Khasra>(model.PageNumber, model.PageSize);
                        break;


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Khasra
                                    .Include(x => x.Acquiredlandvillage)
                        .Include(x => x.LandCategory)

                        .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                         && (string.IsNullOrEmpty(model.village) || x.Acquiredlandvillage.Name.Contains(model.village))
                         && (string.IsNullOrEmpty(model.rectNo) || x.RectNo.Contains(model.rectNo)))
                                    .OrderByDescending(a => a.IsActive)
                                    .GetPaged<Khasra>(model.PageNumber, model.PageSize);
                        break;


                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Khasra
                                     .Include(x => x.Acquiredlandvillage)
                         .Include(x => x.LandCategory)

                         .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                          && (string.IsNullOrEmpty(model.village) || x.Acquiredlandvillage.Name.Contains(model.village))
                          && (string.IsNullOrEmpty(model.rectNo) || x.RectNo.Contains(model.rectNo)))
                                    .OrderByDescending(a => a.Name)
                                    .GetPaged<Khasra>(model.PageNumber, model.PageSize);
                        break;

                    case ("VILLAGE"):
                        data = null;
                        data = await _dbContext.Khasra
                                     .Include(x => x.Acquiredlandvillage)
                         .Include(x => x.LandCategory)

                         .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                          && (string.IsNullOrEmpty(model.village) || x.Acquiredlandvillage.Name.Contains(model.village))
                          && (string.IsNullOrEmpty(model.rectNo) || x.RectNo.Contains(model.rectNo)))
                                    .OrderByDescending(a => a.Acquiredlandvillage.Name)
                                    .GetPaged<Khasra>(model.PageNumber, model.PageSize);
                        break;
                    case ("RECTNO"):
                        data = null;
                        data = await _dbContext.Khasra
                                    .Include(x => x.Acquiredlandvillage)
                        .Include(x => x.LandCategory)

                        .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                         && (string.IsNullOrEmpty(model.village) || x.Acquiredlandvillage.Name.Contains(model.village))
                         && (string.IsNullOrEmpty(model.rectNo) || x.RectNo.Contains(model.rectNo)))
                                   .OrderByDescending(a => a.RectNo)
                                    .GetPaged<Khasra>(model.PageNumber, model.PageSize);
                        break;


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Khasra
                                    .Include(x => x.Acquiredlandvillage)
                        .Include(x => x.LandCategory)

                        .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                         && (string.IsNullOrEmpty(model.village) || x.Acquiredlandvillage.Name.Contains(model.village))
                         && (string.IsNullOrEmpty(model.rectNo) || x.RectNo.Contains(model.rectNo)))
                                    .OrderBy(a => a.IsActive)
                                    .GetPaged<Khasra>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            return data;
        }



        public async Task<List<LandCategory>> GetAllLandCategory()
        {
            List<LandCategory> landcategoryList = await _dbContext.LandCategory.ToListAsync();
            return landcategoryList;
        }

        public async Task<List<Acquiredlandvillage>> GetAllVillageList()
        {
            List<Acquiredlandvillage> villageList = await _dbContext.Acquiredlandvillage.ToListAsync();
            return villageList;
        }




        public async Task<List<Khasra>> GetAllKhasra()
        {
            return await _dbContext.Khasra.Include(x => x.LandCategory).Include(x => x.Acquiredlandvillage).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<PagedResult<Khasra>> GetPagedVillageKhasraReport(VillageDetailsKhasraWiseReportSearchDto model)
        {
            var data = await _dbContext.Khasra 
                .Include(x => x.Acquiredlandvillage)
                 .Where(x => (x.AcquiredlandvillageId == (model.villageId == 0 ? x.AcquiredlandvillageId : model.villageId))
                 && (x.Id == (model.Name == 0 ? x.Id : model.Name))
                   && (x.IsActive == 1)).Distinct()
                 .GetPaged<Khasra>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                        case ("VILLAGE"):
                        data = null;
                        data = await _dbContext.Khasra
                                   .Include(x => x.Acquiredlandvillage)
                 .Where(x => (x.AcquiredlandvillageId == (model.villageId == 0 ? x.AcquiredlandvillageId : model.villageId))
                 && (x.Id == (model.Name == 0 ? x.Id : model.Name))
                   && (x.IsActive == 1))
                                   .OrderBy(a => a.Acquiredlandvillage.Name)
                                   .GetPaged<Khasra>(model.PageNumber, model.PageSize);
                        break;
                    case ("KHASRA"):
                        data = null;
                        data = await _dbContext.Khasra
                                   .Include(x => x.Acquiredlandvillage)
                 .Where(x => (x.AcquiredlandvillageId == (model.villageId == 0 ? x.AcquiredlandvillageId : model.villageId))
                 && (x.Id == (model.Name == 0 ? x.Id : model.Name))
                   && (x.IsActive == 1))
                                   .OrderBy(a => a.Name)
                                   .GetPaged<Khasra>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {

                    case ("VILLAGE"):
                        data = null;
                        data = await _dbContext.Khasra
                                   .Include(x => x.Acquiredlandvillage)
                 .Where(x => (x.AcquiredlandvillageId == (model.villageId == 0 ? x.AcquiredlandvillageId : model.villageId))
                 && (x.Id == (model.Name == 0 ? x.Id : model.Name))
                   && (x.IsActive == 1))
                                   .OrderByDescending(a => a.Acquiredlandvillage.Name)
                                   .GetPaged<Khasra>(model.PageNumber, model.PageSize);
                        break;
                    case ("KHASRA"):
                        data = null;
                        data = await _dbContext.Khasra
                                   .Include(x => x.Acquiredlandvillage)
                 .Where(x => (x.AcquiredlandvillageId == (model.villageId == 0 ? x.AcquiredlandvillageId : model.villageId))
                 && (x.Id == (model.Name == 0 ? x.Id : model.Name))
                   && (x.IsActive == 1))
                                   .OrderByDescending(a => a.Name)
                                   .GetPaged<Khasra>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            return data;
        }

        public async Task<List<Khasra>> getAllVillageDetailsKhasraWise(VillageDetailsKhasraWiseReportSearchDto model)
        {
            var data = await _dbContext.Khasra
                                       .Include(x => x.Acquiredlandvillage)
                                       .Where(x => (x.AcquiredlandvillageId == (model.villageId == 0 ? x.AcquiredlandvillageId : model.villageId))
                                          && (x.Id == (model.Name == 0 ? x.Id : model.Name))
                                          && (x.IsActive == 1)).ToListAsync();
            return data; 
        }
        public async Task<List<Khasra>> GetAllKhasraList(int? villageId)
        {
            List<Khasra> khasraList = await _dbContext.Khasra
                .Where(x => (x.AcquiredlandvillageId == villageId && x.IsActive == 1))
                 .ToListAsync();
            return khasraList;
        }


    }
}
