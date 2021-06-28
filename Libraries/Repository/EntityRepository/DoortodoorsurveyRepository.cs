using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
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


        public async Task<List<Doortodoorsurvey>> GetDoortodoorsurvey()
        {
            return await _dbContext.Doortodoorsurvey. Where(x=>x.IsActive==1).Include(x => x.PresentUseNavigation ) .OrderByDescending(x => x.Id).ToListAsync();
        }


        public async Task<PagedResult<Doortodoorsurvey>> GetPagedDoortodoorsurvey(DoortodoorsurveySearchDto model)
        {
            var data = await _dbContext.Doortodoorsurvey.Include(x => x.PresentUseNavigation)
                                        .Where(x => (string.IsNullOrEmpty(model.location) || x.PropertyAddress.Contains(model.location))
                                           && (string.IsNullOrEmpty(model.occupantname) || x.OccupantName.Contains(model.occupantname))
                                         )
                                        .GetPaged<Doortodoorsurvey>(model.PageNumber, model.PageSize);


            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("LOCATION"):
                        data = null;
                        data = await _dbContext.Doortodoorsurvey.Include(x => x.PresentUseNavigation)
                               .Where(x => (string.IsNullOrEmpty(model.location) || x.PropertyAddress.Contains(model.location))
                                           && (string.IsNullOrEmpty(model.occupantname) || x.OccupantName.Contains(model.occupantname))
                                  )
                                .OrderBy(s => s.PropertyAddress)
                                .GetPaged<Doortodoorsurvey>(model.PageNumber, model.PageSize);
                        break;
                    case ("OCCUPANTNAME"):
                        data = null;
                        data = await _dbContext.Doortodoorsurvey.Include(x => x.PresentUseNavigation)
                                    .Where(x => (string.IsNullOrEmpty(model.location) || x.PropertyAddress.Contains(model.location))
                                           && (string.IsNullOrEmpty(model.occupantname) || x.OccupantName.Contains(model.occupantname))
                                  )
                                .OrderBy(s => s.OccupantName)
                                .GetPaged<Doortodoorsurvey>(model.PageNumber, model.PageSize);

                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Doortodoorsurvey.Include(x => x.PresentUseNavigation)
                                .Where(x => (string.IsNullOrEmpty(model.location) || x.PropertyAddress.Contains(model.location))
                                           && (string.IsNullOrEmpty(model.occupantname) || x.OccupantName.Contains(model.occupantname))
                                  )
                                .OrderByDescending(s => s.IsActive)
                                .GetPaged<Doortodoorsurvey>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("LOCATION"):
                        data = null;
                        data = await _dbContext.Doortodoorsurvey.Include(x => x.PresentUseNavigation)
                                .Where(x => (string.IsNullOrEmpty(model.location) || x.PropertyAddress.Contains(model.location))
                                           && (string.IsNullOrEmpty(model.occupantname) || x.OccupantName.Contains(model.occupantname))
                                  )
                                .OrderByDescending(s => s.PropertyAddress)
                                .GetPaged<Doortodoorsurvey>(model.PageNumber, model.PageSize);
                        break;
                    case ("OCCUPANTNAME"):
                        data = null;
                        data = await _dbContext.Doortodoorsurvey.Include(x => x.PresentUseNavigation)
                               .Where(x => (string.IsNullOrEmpty(model.location) || x.PropertyAddress.Contains(model.location))
                                           && (string.IsNullOrEmpty(model.occupantname) || x.OccupantName.Contains(model.occupantname))
                                  )
                                .OrderByDescending(s => s.OccupantName)
                                .GetPaged<Doortodoorsurvey>(model.PageNumber, model.PageSize);

                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Doortodoorsurvey.Include(x => x.PresentUseNavigation)
                                .Where(x => (string.IsNullOrEmpty(model.location) || x.PropertyAddress.Contains(model.location))
                                           && (string.IsNullOrEmpty(model.occupantname) || x.OccupantName.Contains(model.occupantname))
                                           )
                                .OrderBy(s => s.IsActive)
                                .GetPaged<Doortodoorsurvey>(model.PageNumber, model.PageSize);
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
                                 .Where(x => x.Id == (model.Presentuse == 0 ? x.Id : model.Presentuse)
                                 && x.CreatedDate >= model.FromDate
                                 && x.CreatedDate <= model.ToDate)
                                .GetPaged(model.PageNumber, model.PageSize);

                int SortOrder = (int)model.SortOrder;
                if (SortOrder == 1)
                {
                    switch (model.SortBy.ToUpper())
                    {

                        case ("PRESENTUSE"):
                            data = null;
                            data = await _dbContext.Doortodoorsurvey
                                          .Where(x => x.Id == (model.Presentuse == 0 ? x.Id : model.Presentuse)
                                          && x.CreatedDate >= model.FromDate
                                          && x.CreatedDate <= model.ToDate)
                                          .OrderBy(s => s.Id)
                                          .GetPaged(model.PageNumber, model.PageSize);
                            break;

                        case ("FLOOR NO"):
                            data = null;
                            data = await _dbContext.Doortodoorsurvey
                                          .Where(x => x.Id == (model.Presentuse == 0 ? x.Id : model.Presentuse)
                                          && x.CreatedDate >= model.FromDate
                                          && x.CreatedDate <= model.ToDate)
                                          .OrderBy(s => s.NumberOfFloors)
                                          .GetPaged(model.PageNumber, model.PageSize);
                            break;
                        case ("LONGITUDE"):
                            data = null;
                            data = await _dbContext.Doortodoorsurvey
                                          .Where(x => x.Id == (model.Presentuse == 0 ? x.Id : model.Presentuse)
                                          && x.CreatedDate >= model.FromDate
                                          && x.CreatedDate <= model.ToDate)
                                          .OrderBy(s => s.Longitude)
                                          .GetPaged(model.PageNumber, model.PageSize);
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
                                          .Where(x => x.Id == (model.Presentuse == 0 ? x.Id : model.Presentuse)
                                          && x.CreatedDate >= model.FromDate
                                          && x.CreatedDate <= model.ToDate)
                                          .OrderByDescending(s => s.Id)
                                          .GetPaged(model.PageNumber, model.PageSize);
                            break;

                        case ("FLOOR NO"):
                            data = null;
                            data = await _dbContext.Doortodoorsurvey
                                          .Where(x => x.Id == (model.Presentuse == 0 ? x.Id : model.Presentuse)
                                          && x.CreatedDate >= model.FromDate
                                          && x.CreatedDate <= model.ToDate)
                                          .OrderByDescending(s => s.NumberOfFloors)
                                          .GetPaged(model.PageNumber, model.PageSize);
                            break;
                        case ("LONGITUDE"):
                            data = null;
                            data = await _dbContext.Doortodoorsurvey
                                          .Where(x => x.Id == (model.Presentuse == 0 ? x.Id : model.Presentuse)
                                          && x.CreatedDate >= model.FromDate
                                          && x.CreatedDate <= model.ToDate)
                                          .OrderByDescending(s => s.Longitude)
                                          .GetPaged(model.PageNumber, model.PageSize);
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

    }
}
