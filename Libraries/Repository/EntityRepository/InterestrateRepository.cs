using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{

    public class InterestrateRepository : GenericRepository<Interestrate>, IInterestrateRepository
    {
        public InterestrateRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Interestrate>> GetPagedInterestrate(InterestrateSearchDto model)
        {
            var data = await _dbContext.Interestrate.Include(x => x.PropertyType)
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.PropertyType.Name.Contains(model.name)) &&
                  x.FromDate >= (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                    && x.ToDate <= (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                            .GetPaged<Interestrate>(model.PageNumber, model.PageSize);

            if (model.SortBy == null)
            {
                model.SortBy = "Type";
            }
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("TYPE"):
                        data = null;
                        data = await _dbContext.Interestrate
                                               .Include(x => x.PropertyType)
                                                .Where(x => (string.IsNullOrEmpty(model.name) || x.PropertyType.Name.Contains(model.name)) &&
                                                 x.FromDate >= (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                                 && x.ToDate <= (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                                               .OrderBy(x => x.PropertyType.Name)
                                               .GetPaged<Interestrate>(model.PageNumber, model.PageSize);
                        break;
                    case ("RATE"):
                        data = null;
                        data = await _dbContext.Interestrate
                                               .Include(x => x.PropertyType)
                                             .Where(x => (string.IsNullOrEmpty(model.name) || x.PropertyType.Name.Contains(model.name)) &&
                                                 x.FromDate >= (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                                 && x.ToDate <= (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                                                .OrderBy(x => x.InterestRate)
                                               .GetPaged<Interestrate>(model.PageNumber, model.PageSize);
                        break;
                    case ("FROM"):
                        data = null;
                        data = await _dbContext.Interestrate
                                               .Include(x => x.PropertyType)
                                               .Where(x => (string.IsNullOrEmpty(model.name) || x.PropertyType.Name.Contains(model.name)) &&
                                                 x.FromDate >= (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                                 && x.ToDate <= (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                                               .OrderBy(x => x.FromDate)
                                               .GetPaged<Interestrate>(model.PageNumber, model.PageSize);
                        break;
                    case ("TO"):
                        data = null;
                        data = await _dbContext.Interestrate
                                               .Include(x => x.PropertyType)
                                               .Where(x => (string.IsNullOrEmpty(model.name) || x.PropertyType.Name.Contains(model.name)) &&
                                                 x.FromDate >= (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                                 && x.ToDate <= (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                                                .OrderBy(x => x.ToDate)
                                               .GetPaged<Interestrate>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Interestrate
                                               .Include(x => x.PropertyType)
                                               .Where(x => (string.IsNullOrEmpty(model.name) || x.PropertyType.Name.Contains(model.name)) &&
                                                 x.FromDate >= (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                                 && x.ToDate <= (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                                               .OrderByDescending(x => x.IsActive)
                                               .GetPaged<Interestrate>(model.PageNumber, model.PageSize);
                        break;


                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("TYPE"):
                        data = null;
                        data = await _dbContext.Interestrate
                                               .Include(x => x.PropertyType)
                                               .Where(x => (string.IsNullOrEmpty(model.name) || x.PropertyType.Name.Contains(model.name)) &&
                                                 x.FromDate >= (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                                 && x.ToDate <= (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                                               .OrderByDescending(x => x.PropertyType.Name)
                                               .GetPaged<Interestrate>(model.PageNumber, model.PageSize);
                        break;
                    case ("RATE"):
                        data = null;
                        data = await _dbContext.Interestrate
                                               .Include(x => x.PropertyType)
                                               .Where(x => (string.IsNullOrEmpty(model.name) || x.PropertyType.Name.Contains(model.name)) &&
                                                 x.FromDate >= (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                                 && x.ToDate <= (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                                               .OrderByDescending(x => x.InterestRate)
                                               .GetPaged<Interestrate>(model.PageNumber, model.PageSize);
                        break;
                    case ("FROM"):
                        data = null;
                        data = await _dbContext.Interestrate
                                               .Include(x => x.PropertyType)
                                                .Where(x => (string.IsNullOrEmpty(model.name) || x.PropertyType.Name.Contains(model.name)) &&
                                                 x.FromDate >= (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                                 && x.ToDate <= (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                                                .OrderByDescending(x => x.FromDate)
                                               .GetPaged<Interestrate>(model.PageNumber, model.PageSize);
                        break;
                    case ("TO"):
                        data = null;
                        data = await _dbContext.Interestrate
                                               .Include(x => x.PropertyType)
                                               .Where(x => (string.IsNullOrEmpty(model.name) || x.PropertyType.Name.Contains(model.name)) &&
                                                 x.FromDate >= (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                                 && x.ToDate <= (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                                               .OrderByDescending(x => x.ToDate)
                                               .GetPaged<Interestrate>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Interestrate
                                               .Include(x => x.PropertyType)
                                               .Where(x => (string.IsNullOrEmpty(model.name) || x.PropertyType.Name.Contains(model.name)) &&
                                                 x.FromDate >= (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                                 && x.ToDate <= (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                                               .OrderBy(x => x.IsActive)
                                               .GetPaged<Interestrate>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            return data;

        }

        public async Task<List<PropertyType>> GetAllPropertyType()
        {
            List<PropertyType> list = await _dbContext.PropertyType.Where(x => x.IsActive == 1).ToListAsync();
            return list;
        }

        public async Task<List<Interestrate>> GetAllInterestrate()
        {
            return await _dbContext.Interestrate.Include(x => x.PropertyType).Where(x => x.IsActive == 1).ToListAsync();
        }


    }
}
