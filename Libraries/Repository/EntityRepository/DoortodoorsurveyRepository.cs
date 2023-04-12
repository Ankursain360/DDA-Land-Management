using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class DoortodoorsurveyRepository : GenericRepository<Doortodoorsurvey>, IDoortodoorsurveyRepository
    {
        public DoortodoorsurveyRepository(DataContext dbContext) : base(dbContext)
        {

        }



        public async Task<List<Presentuse>> GetAllPresentuse()
        {
            List<Presentuse> presentuseList = await _dbContext.Presentuse.Where(x => x.IsActive == 1).ToListAsync();
            return presentuseList;
        }
        public async Task<List<Areaunit>> GetAllAreaunit()
        {
            var list = await _dbContext.Areaunit.Where(x => x.IsActive == 1).ToListAsync();
            return list;
        }
        public async Task<List<Floors>> GetAllFloor()
        {
            var list = await _dbContext.Floors.Where(x => x.IsActive == 1).ToListAsync();
            return list;
        }
        public async Task<List<Doortodoorsurvey>> GetDoortodoorsurvey()
        {
            //return await _dbContext.Doortodoorsurvey.Where(x => x.IsActive == 1).Include(x => x.PresentUseNavigation).Include(x => x.AreaUnitNavigation).Include(x => x.NumberOfFloorsNavigation)
            //   .OrderByDescending(x => x.Id).ToListAsync();
            DoortodoorsurveySearchDto model = new DoortodoorsurveySearchDto();
            var data = await _dbContext.Doortodoorsurvey.Where(x => x.IsActive == 1)
                                       .Include(x => x.PresentUseNavigation)
                                       .Include(x => x.CreatedByNavigation)
                                       .Include(x => x.AreaUnitNavigation)
                                       .Include(a => a.NumberOfFloorsNavigation)
                                       .Where(x => (string.IsNullOrEmpty(model.location) || x.PropertyAddress.Contains(model.location))
                                          && (string.IsNullOrEmpty(model.occupantname) || x.OccupantName.Contains(model.occupantname))
                                          && (string.IsNullOrEmpty(model.Mobileno) || x.MobileNo.Contains(model.Mobileno))
                                          && (string.IsNullOrEmpty(model.presentuse) || x.PresentUseNavigation.Name.Contains(model.presentuse))
                                          && (string.IsNullOrEmpty(model.createdByNavigation) || x.CreatedByNavigation.UserName.Contains(model.createdByNavigation))
                                           && (x.CreatedDate.Date >= ((model.FromDate.HasValue) ? Convert.ToDateTime(model.FromDate).Date : x.CreatedDate.Date))
                                          && (x.CreatedDate.Date <= ((model.ToDate.HasValue) ? Convert.ToDateTime(model.ToDate).Date : x.CreatedDate.Date))
                                        ).ToListAsync();
                                      
            return data;
        }
        
        public async Task<List<Doortodoorsurvey>> GetDoortodoorsurveyList(DoortodoorsurveySearchDto model)
        {
            var data = await _dbContext.Doortodoorsurvey.Include(x => x.PresentUseNavigation)
                                        .Include(x => x.CreatedByNavigation)
                                        .Include(x => x.AreaUnitNavigation)
                                        .Include(a => a.NumberOfFloorsNavigation)
                                        .Where(x=>x.IsActive == 1 
                                           && (string.IsNullOrEmpty(model.location) || x.PropertyAddress.Contains(model.location))
                                           && (string.IsNullOrEmpty(model.occupantname) || x.OccupantName.Contains(model.occupantname))
                                           && (string.IsNullOrEmpty(model.Mobileno) || x.MobileNo.Contains(model.Mobileno))
                                           && (string.IsNullOrEmpty(model.presentuse) || x.PresentUseNavigation.Name.Contains(model.presentuse))
                                           && (string.IsNullOrEmpty(model.createdByNavigation) || x.CreatedByNavigation.UserName.Contains(model.createdByNavigation))
                                            && (x.CreatedDate.Date >= ((model.FromDate.HasValue) ? Convert.ToDateTime(model.FromDate).Date : x.CreatedDate.Date))
                                  && (x.CreatedDate.Date <= ((model.ToDate.HasValue) ? Convert.ToDateTime(model.ToDate).Date : x.CreatedDate.Date))
                                         ).ToListAsync();

            return data;
        }

        public async Task<PagedResult<Doortodoorsurvey>> GetPagedDoortodoorsurvey(DoortodoorsurveySearchDto model)
        {
            var data = await _dbContext.Doortodoorsurvey.Include(x => x.PresentUseNavigation)
                                        .Include(x => x.CreatedByNavigation)
                                        .Include(x => x.AreaUnitNavigation)
                                        .Include(a => a.NumberOfFloorsNavigation)
                                        .Where(x => (string.IsNullOrEmpty(model.location) || x.PropertyAddress.Contains(model.location))
                                           && (string.IsNullOrEmpty(model.occupantname) || x.OccupantName.Contains(model.occupantname))
                                           && (string.IsNullOrEmpty(model.Mobileno) || x.MobileNo.Contains(model.Mobileno))
                                           && (string.IsNullOrEmpty(model.presentuse) || x.PresentUseNavigation.Name.Contains(model.presentuse))
                                           && (string.IsNullOrEmpty(model.createdByNavigation) || x.CreatedByNavigation.UserName.Contains(model.createdByNavigation))
                                            && (x.CreatedDate.Date >= ((model.FromDate.HasValue) ? Convert.ToDateTime(model.FromDate).Date : x.CreatedDate.Date))
                                  && (x.CreatedDate.Date <= ((model.ToDate.HasValue) ? Convert.ToDateTime(model.ToDate).Date : x.CreatedDate.Date))
                                         )
                                        .GetPaged<Doortodoorsurvey>(model.PageNumber, model.PageSize);


            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("CREATEDDATE"):
                        data.Results = data.Results.OrderBy(x => x.CreatedDate).ToList();
                        break;

                    case ("LOCATION"):
                        data.Results = data.Results.OrderBy(x => x.PropertyAddress).ToList();
                        //data = null;
                        //data = await _dbContext.Doortodoorsurvey.Include(x => x.PresentUseNavigation)
                        //                        .Include(x => x.CreatedByNavigation)
                        //                        .Include(x => x.AreaUnitNavigation)
                        //                        .Include(a => a.NumberOfFloorsNavigation)
                        //       .Where(x => (string.IsNullOrEmpty(model.location) || x.PropertyAddress.Contains(model.location))
                        //                   && (string.IsNullOrEmpty(model.occupantname) || x.OccupantName.Contains(model.occupantname))
                        //          )
                        //        .OrderBy(s => s.PropertyAddress)
                        //        .GetPaged<Doortodoorsurvey>(model.PageNumber, model.PageSize);
                        break;
                    case ("OCCUPANTNAME"):
                        data.Results = data.Results.OrderBy(x => x.OccupantName).ToList();
                        //data = null;
                        //data = await _dbContext.Doortodoorsurvey.Include(x => x.PresentUseNavigation)
                        //                        .Include(x => x.CreatedByNavigation)
                        //                        .Include(x => x.AreaUnitNavigation)
                        //                        .Include(a => a.NumberOfFloorsNavigation)
                        //            .Where(x => (string.IsNullOrEmpty(model.location) || x.PropertyAddress.Contains(model.location))
                        //                   && (string.IsNullOrEmpty(model.occupantname) || x.OccupantName.Contains(model.occupantname))
                        //          )
                        //        .OrderBy(s => s.OccupantName)
                        //        .GetPaged<Doortodoorsurvey>(model.PageNumber, model.PageSize);

                        break;
                    case ("MOBILE"):
                        data.Results = data.Results.OrderBy(X => X.MobileNo).ToList();
                        break; 
                    case ("PRESENTUSE"):
                        data.Results = data.Results.OrderBy(x => x.PresentUseNavigation.Name).ToList();
                        break;
                    case ("CREATEDBYNAVIGATION"):
                        data.Results = data.Results.OrderBy(x => x.CreatedByNavigation.UserName).ToList();
                        break; 
                        
                    case ("STATUS"):
                        //data = null;
                        //data = await _dbContext.Doortodoorsurvey.Include(x => x.PresentUseNavigation)
                        //                        .Include(x => x.CreatedByNavigation)
                        //                        .Include(x => x.AreaUnitNavigation)
                        //                        .Include(a => a.NumberOfFloorsNavigation)
                        //        .Where(x => (string.IsNullOrEmpty(model.location) || x.PropertyAddress.Contains(model.location))
                        //                   && (string.IsNullOrEmpty(model.occupantname) || x.OccupantName.Contains(model.occupantname))
                        //          )
                        //        .OrderByDescending(s => s.IsActive)
                        //        .GetPaged<Doortodoorsurvey>(model.PageNumber, model.PageSize);
                        data.Results = data.Results.OrderByDescending(x => x.IsActive).ToList();
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("CREATEDDATE"):
                        data.Results = data.Results.OrderByDescending(x => x.CreatedDate).ToList();
                        break;
                    case ("LOCATION"):
                        data.Results = data.Results.OrderByDescending(x => x.PropertyAddress).ToList();
                        //data = null;
                        //data = await _dbContext.Doortodoorsurvey.Include(x => x.PresentUseNavigation)
                        //                        .Include(x => x.CreatedByNavigation)
                        //                        .Include(x => x.AreaUnitNavigation)
                        //                        .Include(a => a.NumberOfFloorsNavigation)
                        //       .Where(x => (string.IsNullOrEmpty(model.location) || x.PropertyAddress.Contains(model.location))
                        //                   && (string.IsNullOrEmpty(model.occupantname) || x.OccupantName.Contains(model.occupantname))
                        //          )
                        //        .OrderBy(s => s.PropertyAddress)
                        //        .GetPaged<Doortodoorsurvey>(model.PageNumber, model.PageSize);
                        break;
                    case ("OCCUPANTNAME"):
                        data.Results = data.Results.OrderByDescending(x => x.OccupantName).ToList();
                        //data = null;
                        //data = await _dbContext.Doortodoorsurvey.Include(x => x.PresentUseNavigation)
                        //                        .Include(x => x.CreatedByNavigation)
                        //                        .Include(x => x.AreaUnitNavigation)
                        //                        .Include(a => a.NumberOfFloorsNavigation)
                        //            .Where(x => (string.IsNullOrEmpty(model.location) || x.PropertyAddress.Contains(model.location))
                        //                   && (string.IsNullOrEmpty(model.occupantname) || x.OccupantName.Contains(model.occupantname))
                        //          )
                        //        .OrderBy(s => s.OccupantName)
                        //        .GetPaged<Doortodoorsurvey>(model.PageNumber, model.PageSize);

                        break;
                    case ("MOBILE"):
                        data.Results = data.Results.OrderByDescending(X => X.MobileNo).ToList();
                        break;
                    case ("PRESENTUSE"):
                        data.Results = data.Results.OrderByDescending(x => x.PresentUseNavigation.Name).ToList();
                        break;
                    case ("CREATEDBYNAVIGATION"):
                        data.Results = data.Results.OrderByDescending(x => x.CreatedByNavigation.UserName).ToList();
                        break;

                    case ("STATUS"):
                        //data = null;
                        //data = await _dbContext.Doortodoorsurvey.Include(x => x.PresentUseNavigation)
                        //                        .Include(x => x.CreatedByNavigation)
                        //                        .Include(x => x.AreaUnitNavigation)
                        //                        .Include(a => a.NumberOfFloorsNavigation)
                        //        .Where(x => (string.IsNullOrEmpty(model.location) || x.PropertyAddress.Contains(model.location))
                        //                   && (string.IsNullOrEmpty(model.occupantname) || x.OccupantName.Contains(model.occupantname))
                        //          )
                        //        .OrderByDescending(s => s.IsActive)
                        //        .GetPaged<Doortodoorsurvey>(model.PageNumber, model.PageSize);
                        data.Results = data.Results.OrderBy(x => x.IsActive).ToList();
                        break;

                }
            }
            return data;



        }


        public async Task<PagedResult<Doortodoorsurvey>> GetPagedDoortodoorsurveyReport(DoorToDoorSurveyReportSearchDto model)
        {
            try
            {
                var data = await _dbContext.Doortodoorsurvey
                                    .Include(x => x.PresentUseNavigation)
                                    .Include(x => x.CreatedByNavigation)
                                 .Where(x => x.PresentUseId == (model.Presentuse == 0 ? x.PresentUseId : model.Presentuse)
                                 && x.CreatedDate >= model.FromDate
                                 && x.CreatedDate <= model.ToDate)
                                .GetPaged<Doortodoorsurvey>(model.PageNumber, model.PageSize);

                int SortOrder = (int)model.SortOrder;

                if (SortOrder == 1)
                {
                    switch (model.SortBy.ToUpper())
                    {

                        case ("PRESENTUSE"):
                            data = null;
                            data = await _dbContext.Doortodoorsurvey
                                              .Include(x => x.PresentUseNavigation)
                                              .Include(x => x.CreatedByNavigation)
                                          .Where(x => x.PresentUseId == (model.Presentuse == 0 ? x.PresentUseId : model.Presentuse)
                                          && x.CreatedDate >= model.FromDate
                                          && x.CreatedDate <= model.ToDate)
                                          .OrderBy(s => s.Id)
                                          .GetPaged<Doortodoorsurvey>(model.PageNumber, model.PageSize);
                            break;

                        case ("FLOOR NO"):
                            data = null;
                            data = await _dbContext.Doortodoorsurvey
                                .Include(x => x.PresentUseNavigation)
                                .Include(x => x.CreatedByNavigation)
                                          .Where(x => x.PresentUseId == (model.Presentuse == 0 ? x.PresentUseId : model.Presentuse)
                                          && x.CreatedDate >= model.FromDate
                                          && x.CreatedDate <= model.ToDate)
                                          .OrderBy(s => s.NumberOfFloors)
                                          .GetPaged<Doortodoorsurvey>(model.PageNumber, model.PageSize);
                            break;
                        case ("LONGITUDE"):
                            data = null;
                            data = await _dbContext.Doortodoorsurvey
                                .Include(x => x.PresentUseNavigation)
                                          .Where(x => x.PresentUseId == (model.Presentuse == 0 ? x.PresentUseId : model.Presentuse)
                                          && x.CreatedDate >= model.FromDate
                                          && x.CreatedDate <= model.ToDate)
                                          .OrderBy(s => s.Longitude)
                                          .GetPaged<Doortodoorsurvey>(model.PageNumber, model.PageSize);
                            break;


                    }
                }
                else if (SortOrder == 2)
                {
                    switch (model.SortBy.ToUpper())
                    {

                        case ("PRESENTUSE"):
                            data = null;
                            data = await _dbContext.Doortodoorsurvey
                                .Include(x => x.PresentUseNavigation)
                                .Include(x => x.CreatedByNavigation)
                                          .Where(x => x.PresentUseId == (model.Presentuse == 0 ? x.PresentUseId : model.Presentuse)
                                          && x.CreatedDate >= model.FromDate
                                          && x.CreatedDate <= model.ToDate)
                                          .OrderByDescending(s => s.Id)
                                          .GetPaged<Doortodoorsurvey>(model.PageNumber, model.PageSize);
                            break;

                        case ("FLOOR NO"):
                            data = null;
                            data = await _dbContext.Doortodoorsurvey
                                .Include(x => x.PresentUseNavigation)
                                .Include(x => x.CreatedByNavigation)
                                          .Where(x => x.PresentUseId == (model.Presentuse == 0 ? x.PresentUseId : model.Presentuse)
                                          && x.CreatedDate >= model.FromDate
                                          && x.CreatedDate <= model.ToDate)
                                          .OrderByDescending(s => s.NumberOfFloors)
                                          .GetPaged(model.PageNumber, model.PageSize);
                            break;
                        case ("LONGITUDE"):
                            data = null;
                            data = await _dbContext.Doortodoorsurvey
                                .Include(x => x.PresentUseNavigation)
                                .Include(x => x.CreatedByNavigation)
                                          .Where(x => x.PresentUseId == (model.Presentuse == 0 ? x.PresentUseId : model.Presentuse)
                                          && x.CreatedDate >= model.FromDate
                                          && x.CreatedDate <= model.ToDate)
                                          .OrderByDescending(s => s.Longitude)
                                         .GetPaged<Doortodoorsurvey>(model.PageNumber, model.PageSize);
                            break;



                    }
                }

                return data;

            }
            catch (System.Exception ex)
            {

                return null;
            }

        }

        public async Task<bool> SaveDoorToDoorSurveyIdentityProofs(Doortodoorsurveyidentityproof item)
        {
            _dbContext.Doortodoorsurveyidentityproof.Add(item);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<bool> SaveDoorToDoorSurveyPropertyProofs(Doortodoorsurveypropertyproof item)
        {
            _dbContext.Doortodoorsurveypropertyproof.Add(item);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<bool> DeleteDoorToDoorSurveyIdentityProofs(int id)
        {
            _dbContext.RemoveRange(_dbContext.Doortodoorsurveyidentityproof.Where(x => x.DoorToDoorSurveyId == id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<bool> DeleteDoorToDoorSurveyPropertyProofs(int id)
        {
            _dbContext.RemoveRange(_dbContext.Doortodoorsurveypropertyproof.Where(x => x.DoorToDoorSurveyId == id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<Doortodoorsurvey> FetchSingleResult(int id)
        {
            return await _dbContext.Doortodoorsurvey
                                    .Include(x=> x.NumberOfFloorsNavigation)
                                    .Include(x => x.Doortodoorsurveyidentityproof)
                                    .Include(x => x.Doortodoorsurveypropertyproof)
                                    .Include(x=>x.AreaUnitNavigation)
                                    .Where(x => x.Id == id && x.IsActive == 1)
                                    .FirstOrDefaultAsync();
        }

        public async Task<Doortodoorsurveyidentityproof> FetchSingleResultDoor2DoorSurveyIdentity(int id)
        {
            return await _dbContext.Doortodoorsurveyidentityproof
                                    .Where(x => x.Id == id)
                                    .FirstOrDefaultAsync();
        }

        public async Task<Doortodoorsurveypropertyproof> FetchSingleResultDoor2DoorSurveyProperty(int id)
        {
            return await _dbContext.Doortodoorsurveypropertyproof
                                   .Where(x => x.Id == id)
                                   .FirstOrDefaultAsync();
        }
    }
}
