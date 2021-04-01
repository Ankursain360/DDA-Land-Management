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

    public class HonbleRepository : GenericRepository<Honble>, IHonbleRepository
    {
        public HonbleRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<PagedResult<Honble>> GetPagedHonble(HonbleSearchDto model)
        {
            var data = await _dbContext.Honble
                       .GetPaged<Honble>(model.PageNumber, model.PageSize);

            if (model.SortBy == null)
            {
                model.SortBy = "Type";
            }
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)

            {
                switch (model.SortBy.ToUpper())
                {
                    case ("HonbleName"):
                        data = null;
                        data = await _dbContext.Honble
                                                   .Where(x => (string.IsNullOrEmpty(model.honblename) || x.HonbleName.Contains(model.honblename)))
                                               .OrderBy(x => x.HonbleName)
                                               .GetPaged<Honble>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Honble
                                                   .Where(x => (string.IsNullOrEmpty(model.honblename) || x.HonbleName.Contains(model.honblename)))
                                               .OrderByDescending(x => x.IsActive)
                                               .GetPaged<Honble>(model.PageNumber, model.PageSize);
                        break;


                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("Honble"):
                        data = null;
                        data = await _dbContext.Honble
                                                   .Where(x => (string.IsNullOrEmpty(model.honblename) || x.HonbleName.Contains(model.honblename)))
                                               .OrderByDescending(x => x.HonbleName)
                                               .GetPaged<Honble>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Honble
                                                   .Where(x => (string.IsNullOrEmpty(model.honblename) || x.HonbleName.Contains(model.honblename)))
                                               .OrderBy(x => x.IsActive)
                                               .GetPaged<Honble>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            return data;

        }
        public async Task<List<Honble>> GetAllHonble()
        {
            return await _dbContext.Honble.Where(x => x.IsActive == 1).ToListAsync();
        }

    }
}
