
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

    public class LeasedeedRepository : GenericRepository<Leasedeed>, ILeasedeedRepository
    {
        public LeasedeedRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<List<Allotmententry>> GetAllApplications()
        {
            List<Allotmententry> list = await _dbContext.Allotmententry
                                        .Include(x => x.Application)
                                        .Where(x => x.IsActive == 1)
                                        .ToListAsync();
            return list;
        }


        public async Task<Allotmententry> FetchSingleDetails(int? Id)
        {
            var result = await _dbContext.Allotmententry
                .Include(x => x.Application)
                .Where(x => x.Id == Id).SingleOrDefaultAsync();
             return result;
        }

        
        public async Task<List<Leasedeed>> GetAllLeasedeed()
        {
            return await _dbContext.Leasedeed.Where(x => x.IsActive == 1).ToListAsync();
        }
      
        public async Task<PagedResult<Leasedeed>> GetPagedLeasedeed(LeasedeedSearchDto model)
        {
            var data = await _dbContext.Leasedeed
                                .Include(x => x.Allotment)
                                .Include(x => x.Allotment.LeasesType)
                                .Include(x => x.Allotment.Application)
                                .Where(x => (string.IsNullOrEmpty(model.name) || x.Allotment.Application.RefNo.Contains(model.name)))
                                .GetPaged<Leasedeed>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("REF"):
                        data = null;
                        data = await _dbContext.Leasedeed
                                               .Include(x => x.Allotment)
                                               .Include(x => x.Allotment.LeasesType)
                                                .Include(x => x.Allotment.Application)
                                               .Where(x => (string.IsNullOrEmpty(model.name) || x.Allotment.Application.RefNo.Contains(model.name)))
                                               .OrderBy(x => x.Allotment.Application.RefNo)
                                               .GetPaged<Leasedeed>(model.PageNumber, model.PageSize);

                        break;
                    case ("DATE"):
                        data = null;
                        data = await _dbContext.Leasedeed
                                               .Include(x => x.Allotment)
                                               .Include(x => x.Allotment.LeasesType)
                                                .Include(x => x.Allotment.Application)
                                               .Where(x => (string.IsNullOrEmpty(model.name) || x.Allotment.Application.RefNo.Contains(model.name)))
                                               .OrderBy(x => x.LeaseDeedDate)
                                               .GetPaged<Leasedeed>(model.PageNumber, model.PageSize);

                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Leasedeed
                                                .Include(x => x.Allotment)
                                               .Include(x => x.Allotment.LeasesType)
                                                 .Include(x => x.Allotment.Application)
                                                .Where(x => (string.IsNullOrEmpty(model.name) || x.Allotment.Application.RefNo.Contains(model.name)))
                                                .OrderByDescending(x => x.IsActive)
                                                .GetPaged<Leasedeed>(model.PageNumber, model.PageSize);

                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("REF"):
                        data = null;
                        data = await _dbContext.Leasedeed
                                               .Include(x => x.Allotment)
                                               .Include(x => x.Allotment.LeasesType)
                                                .Include(x => x.Allotment.Application)
                                               .Where(x => (string.IsNullOrEmpty(model.name) || x.Allotment.Application.RefNo.Contains(model.name)))
                                               .OrderByDescending(x => x.Allotment.Application.RefNo)
                                               .GetPaged<Leasedeed>(model.PageNumber, model.PageSize);

                        break;
                    case ("DATE"):
                        data = null;
                        data = await _dbContext.Leasedeed
                                               .Include(x => x.Allotment)
                                               .Include(x => x.Allotment.LeasesType)
                                                .Include(x => x.Allotment.Application)
                                               .Where(x => (string.IsNullOrEmpty(model.name) || x.Allotment.Application.RefNo.Contains(model.name)))
                                               .OrderByDescending(x => x.LeaseDeedDate)
                                               .GetPaged<Leasedeed>(model.PageNumber, model.PageSize);

                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Leasedeed
                                                .Include(x => x.Allotment)
                                               .Include(x => x.Allotment.LeasesType)
                                                 .Include(x => x.Allotment.Application)
                                                .Where(x => (string.IsNullOrEmpty(model.name) || x.Allotment.Application.RefNo.Contains(model.name)))
                                                .OrderBy(x => x.IsActive)
                                                .GetPaged<Leasedeed>(model.PageNumber, model.PageSize);

                        break;
                }
            }
            return data;
           
        }
    }
}
