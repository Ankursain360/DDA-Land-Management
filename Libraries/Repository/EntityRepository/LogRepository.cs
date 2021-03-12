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
    public class LogRepository : GenericRepository<Log>, ILogRepository
    {
        public LogRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Log>> GetLog()
        {
            return await _dbContext.Log.ToListAsync();
        }


        public async Task<PagedResult<Log>> GetPagedLog(LogSearchDto model)
        {
            var data = await _dbContext.Log
                 .Where(x => (string.IsNullOrEmpty(model.application) || x.Application.Contains(model.application))
                 && (string.IsNullOrEmpty(model.logger) || x.Logger.Contains(model.logger))
                  )
                .GetPaged<Log>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("APPLICATION"):
                        data = null;
                        data = await _dbContext.Log
                            .Where(x => (string.IsNullOrEmpty(model.application) || x.Application.Contains(model.application))
                  && (string.IsNullOrEmpty(model.logger) || x.Logger.Contains(model.logger))
                   )
                           .OrderBy(s => s.Application)
                           .GetPaged<Log>(model.PageNumber, model.PageSize);

                        break;
                    case ("LOGGER"):
                        data = null;
                        data = await _dbContext.Log
                            .Where(x => (string.IsNullOrEmpty(model.application) || x.Application.Contains(model.application))
                  && (string.IsNullOrEmpty(model.logger) || x.Logger.Contains(model.logger))
                   )
                           .OrderBy(s => s.Logger)
                           .GetPaged<Log>(model.PageNumber, model.PageSize);
                        break;
                   
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("APPLICATION"):
                        data = null;
                        data = await _dbContext.Log
                            .Where(x => (string.IsNullOrEmpty(model.application) || x.Application.Contains(model.application))
                  && (string.IsNullOrEmpty(model.logger) || x.Logger.Contains(model.logger))
                   )
                           .OrderByDescending(s => s.Application)
                           .GetPaged<Log>(model.PageNumber, model.PageSize);

                        break;
                    case ("LOGGER"):
                        data = null;
                        data = await _dbContext.Log
                            .Where(x => (string.IsNullOrEmpty(model.application) || x.Application.Contains(model.application))
                  && (string.IsNullOrEmpty(model.logger) || x.Logger.Contains(model.logger))
                   )
                           .OrderByDescending(s => s.Logger)
                           .GetPaged<Log>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            return data;
        }


    }
}
