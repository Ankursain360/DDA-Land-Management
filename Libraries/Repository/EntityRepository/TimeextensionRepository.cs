
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

    public class TimeextensionRepository : GenericRepository<Timeextension>, ITimeextensionRepository
    {
        public TimeextensionRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<List<Timeextension>> GetAllTimeextension()
        {
            return await _dbContext.Timeextension.ToListAsync();
        }


        public async Task<PagedResult<Timeextension>> GetPagedTimeextension(TimeextensionSearchDto model)
        {
            var data = await _dbContext.Timeextension
                                       .Where(x =>  x.FromDate == (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                       && x.ToDate == (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                                       .GetPaged<Timeextension>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                   
                    case ("FEE"):
                        data = null;
                        data = await _dbContext.Timeextension
                                               .Where(x => x.FromDate == (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                               && x.ToDate == (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                                              .OrderBy(x => x.ExtensionFees)
                                              .GetPaged<Timeextension>(model.PageNumber, model.PageSize);

                                               
                        break;
                    case ("FROM"):
                        data = null;
                        data = await _dbContext.Timeextension
                                               .Where(x => x.FromDate == (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                               && x.ToDate == (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                                              .OrderBy(x => x.FromDate)
                                              .GetPaged<Timeextension>(model.PageNumber, model.PageSize);

                        break;
                    case ("TO"):
                        data = null;
                        data = await _dbContext.Timeextension
                                              .Where(x => x.FromDate == (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                              && x.ToDate == (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                                             .OrderBy(x => x.ToDate)
                                             .GetPaged<Timeextension>(model.PageNumber, model.PageSize);

                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Timeextension
                                               .Where(x => x.FromDate == (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                               && x.ToDate == (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                                              .OrderByDescending(x => x.IsActive)
                                              .GetPaged<Timeextension>(model.PageNumber, model.PageSize);

                        break;


                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("FEE"):
                        data = null;
                        data = await _dbContext.Timeextension
                                               .Where(x => x.FromDate == (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                               && x.ToDate == (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                                              .OrderByDescending(x => x.ExtensionFees)
                                              .GetPaged<Timeextension>(model.PageNumber, model.PageSize);


                        break;
                    case ("FROM"):
                        data = null;
                        data = await _dbContext.Timeextension
                                               .Where(x => x.FromDate == (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                               && x.ToDate == (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                                              .OrderByDescending(x => x.FromDate)
                                              .GetPaged<Timeextension>(model.PageNumber, model.PageSize);

                        break;
                    case ("TO"):
                        data = null;
                        data = await _dbContext.Timeextension
                                              .Where(x => x.FromDate == (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                              && x.ToDate == (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                                             .OrderByDescending(x => x.ToDate)
                                             .GetPaged<Timeextension>(model.PageNumber, model.PageSize);

                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Timeextension
                                               .Where(x => x.FromDate == (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                               && x.ToDate == (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate)))
                                              .OrderBy(x => x.IsActive)
                                              .GetPaged<Timeextension>(model.PageNumber, model.PageSize);

                        break;
                }
            }
            return data;

        }
    }
}
