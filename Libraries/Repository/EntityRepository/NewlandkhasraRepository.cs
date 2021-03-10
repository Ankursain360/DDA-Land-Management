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
    public class NewlandkhasraRepository : GenericRepository<Newlandkhasra>, INewlandkhasraRepository
    {
        public NewlandkhasraRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Newlandkhasra>> GetPagedKhasra(NewlandkhasraSearchDto model)
        {
   
            var data = await _dbContext.Newlandkhasra
                         .Include(x => x.Newlandvillage)
                         .Include(x => x.LandCategory)
                           .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                          && (string.IsNullOrEmpty(model.village) || x.Newlandvillage.Name.Contains(model.village))
                          && (string.IsNullOrEmpty(model.rectNo) || x.RectNo.Contains(model.rectNo)))
                           .GetPaged<Newlandkhasra>(model.PageNumber, model.PageSize);


            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Newlandkhasra
                         .Include(x => x.Newlandvillage)
                         .Include(x => x.LandCategory)
                           .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                          && (string.IsNullOrEmpty(model.village) || x.Newlandvillage.Name.Contains(model.village))
                          && (string.IsNullOrEmpty(model.rectNo) || x.RectNo.Contains(model.rectNo)))
                             .OrderBy(a => a.Name)
                           .GetPaged<Newlandkhasra>(model.PageNumber, model.PageSize);
                                  
                                   
                        break;

                    case ("VILLAGE"):
                        data = null;
                        data = await _dbContext.Newlandkhasra
                         .Include(x => x.Newlandvillage)
                         .Include(x => x.LandCategory)
                           .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                          && (string.IsNullOrEmpty(model.village) || x.Newlandvillage.Name.Contains(model.village))
                          && (string.IsNullOrEmpty(model.rectNo) || x.RectNo.Contains(model.rectNo)))
                             .OrderBy(a => a.Newlandvillage.Name)
                           .GetPaged<Newlandkhasra>(model.PageNumber, model.PageSize);
                        break;
                    case ("RECTNO"):
                        data = null;
                        data = await _dbContext.Newlandkhasra
                         .Include(x => x.Newlandvillage) 
                         .Include(x => x.LandCategory)
                           .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                          && (string.IsNullOrEmpty(model.village) || x.Newlandvillage.Name.Contains(model.village))
                          && (string.IsNullOrEmpty(model.rectNo) || x.RectNo.Contains(model.rectNo)))
                             .OrderBy(a => a.RectNo)
                           .GetPaged<Newlandkhasra>(model.PageNumber, model.PageSize);
                        break;


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Newlandkhasra
                         .Include(x => x.Newlandvillage)
                         .Include(x => x.LandCategory)
                           .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                          && (string.IsNullOrEmpty(model.village) || x.Newlandvillage.Name.Contains(model.village))
                          && (string.IsNullOrEmpty(model.rectNo) || x.RectNo.Contains(model.rectNo)))
                             .OrderByDescending(a => a.IsActive)
                           .GetPaged<Newlandkhasra>(model.PageNumber, model.PageSize);
                        break;


                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Newlandkhasra
                          .Include(x => x.Newlandvillage)
                          .Include(x => x.LandCategory)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                           && (string.IsNullOrEmpty(model.village) || x.Newlandvillage.Name.Contains(model.village))
                           && (string.IsNullOrEmpty(model.rectNo) || x.RectNo.Contains(model.rectNo)))
                              .OrderByDescending(a => a.Name)
                            .GetPaged<Newlandkhasra>(model.PageNumber, model.PageSize);
                        break;

                    case ("VILLAGE"):
                        data = null;
                        data = await _dbContext.Newlandkhasra
                        .Include(x => x.Newlandvillage)
                        .Include(x => x.LandCategory)
                          .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                         && (string.IsNullOrEmpty(model.village) || x.Newlandvillage.Name.Contains(model.village))
                         && (string.IsNullOrEmpty(model.rectNo) || x.RectNo.Contains(model.rectNo)))
                            .OrderByDescending(a => a.Newlandvillage.Name)
                          .GetPaged<Newlandkhasra>(model.PageNumber, model.PageSize);
                        break;
                    case ("RECTNO"):
                        data = null;
                        data = await _dbContext.Newlandkhasra
                         .Include(x => x.Newlandvillage)
                         .Include(x => x.LandCategory)
                           .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                          && (string.IsNullOrEmpty(model.village) || x.Newlandvillage.Name.Contains(model.village))
                          && (string.IsNullOrEmpty(model.rectNo) || x.RectNo.Contains(model.rectNo)))
                             .OrderByDescending(a => a.RectNo)
                           .GetPaged<Newlandkhasra>(model.PageNumber, model.PageSize);
                        break;


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Newlandkhasra
                         .Include(x => x.Newlandvillage)
                         .Include(x => x.LandCategory)
                           .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                          && (string.IsNullOrEmpty(model.village) || x.Newlandvillage.Name.Contains(model.village))
                          && (string.IsNullOrEmpty(model.rectNo) || x.RectNo.Contains(model.rectNo)))
                           .OrderBy(a => a.IsActive)
                           .GetPaged<Newlandkhasra>(model.PageNumber, model.PageSize);
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

        public async Task<List<Newlandvillage>> GetAllVillageList()
        {
            List<Newlandvillage> villageList = await _dbContext.Newlandvillage.ToListAsync();
            return villageList;
        }




        public async Task<List<Newlandkhasra>> GetAllKhasra()
        {
            return await _dbContext.Newlandkhasra.Include(x => x.LandCategory).Include(x => x.Newlandvillage).OrderByDescending(x => x.Id).ToListAsync();
        }

        
        public async Task<List<Newlandkhasra>> GetAllKhasraList(int? villageId)
        {
            List<Newlandkhasra> khasraList = await _dbContext.Newlandkhasra
                   .Where(x => (x.NewLandvillageId == villageId && x.IsActive == 1))
                .ToListAsync();
            return khasraList;
        }


       



        public async Task<PagedResult<Newlandkhasra>> GetPagednewlandVillageKhasraReport(NewlandVillageDetailsKhasraWiseReportSearchDto model)
        {
            var data = await _dbContext.Newlandkhasra
                .Include(x => x.Newlandvillage)
                 .Where(x => (x.NewLandvillageId == (model.villageId == 0 ? x.NewLandvillageId : model.villageId))
                 && (x.Id == (model.Name == 0 ? x.Id : model.Name)))
                 .GetPaged<Newlandkhasra>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {

                    //case ("VILLAGE"):
                    //    data.Results = data.Results.OrderBy(x => x.Name).ToList();
                    //    break;
                    //case ("KHASRA"):
                    //    data.Results = data.Results.OrderBy(x => x.Name).ToList();
                    //    break;
                    case ("VILLAGE"):
                        data = null;
                        data = await _dbContext.Newlandkhasra
                                   .Include(x => x.Newlandvillage)
                 .Where(x => (x.NewLandvillageId == (model.villageId == 0 ? x.NewLandvillageId : model.villageId))
                 && (x.Id == (model.Name == 0 ? x.Id : model.Name)))
                                   .OrderBy(a => a.Newlandvillage.Name)
                                   .GetPaged<Newlandkhasra>(model.PageNumber, model.PageSize);
                        break;
                    case ("KHASRA"):
                        data = null;
                        data = await _dbContext.Newlandkhasra
                                   .Include(x => x.Newlandvillage)
                 .Where(x => (x.NewLandvillageId == (model.villageId == 0 ? x.NewLandvillageId : model.villageId))
                 && (x.Id == (model.Name == 0 ? x.Id : model.Name)))
                                   .OrderBy(a => a.Name)
                                   .GetPaged<Newlandkhasra>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {

                    case ("VILLAGE"):
                        data = null;
                        data = await _dbContext.Newlandkhasra
                                   .Include(x => x.Newlandvillage)
                 .Where(x => (x.NewLandvillageId == (model.villageId == 0 ? x.NewLandvillageId : model.villageId))
                 && (x.Id == (model.Name == 0 ? x.Id : model.Name)))
                                   .OrderByDescending(a => a.Newlandvillage.Name)
                                   .GetPaged<Newlandkhasra>(model.PageNumber, model.PageSize);
                        break;
                    case ("KHASRA"):
                        data = null;
                        data = await _dbContext.Newlandkhasra
                                   .Include(x => x.Newlandvillage)
                 .Where(x => (x.NewLandvillageId == (model.villageId == 0 ? x.NewLandvillageId : model.villageId))
                 && (x.Id == (model.Name == 0 ? x.Id : model.Name)))
                                   .OrderByDescending(a => a.Name)
                                   .GetPaged<Newlandkhasra>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            return data;
        }


    }
}
