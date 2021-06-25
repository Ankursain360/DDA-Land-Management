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



    }
}
