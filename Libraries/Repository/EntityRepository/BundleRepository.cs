using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;


namespace Libraries.Repository.EntityRepository
{
    public class BundleRespository : GenericRepository<Bundle>, IBundleRepository
    {
        public BundleRespository(DataContext dbcontext) : base(dbcontext)
        { }

        public async Task<List<Bundle>> GetBundle()
        {
            return await _dbContext.Bundle.Where(x => x.IsActive == 1).ToListAsync();
        }
        public async Task<PagedResult<Bundle>> GetPagedBundle(BundleSearchDto model)
        {
            var data = await _dbContext.Bundle
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.BundleNo.Contains(model.name)))
                  .GetPaged<Bundle>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Bundle
                           .Where(x => (string.IsNullOrEmpty(model.name) || x.BundleNo.Contains(model.name)))
                           .OrderBy(s => s.Id)
                           .GetPaged<Bundle>(model.PageNumber, model.PageSize);

                        break;
                    case ("ISACTIVE"):
                        data = null;
                        data = await _dbContext.Bundle
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.BundleNo.Contains(model.name)))
                            .OrderBy(s => s.Id)
                            .GetPaged<Bundle>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Bundle
                           .Where(x => (string.IsNullOrEmpty(model.name) || x.BundleNo.Contains(model.name)))
                           .OrderByDescending(s => s.BundleNo)
                           .GetPaged<Bundle>(model.PageNumber, model.PageSize);
                        break;


                    case ("ISACTIVE"):
                        data = null;
                        data = await _dbContext.Bundle
                           .Where(x => (string.IsNullOrEmpty(model.name) || x.BundleNo.Contains(model.name)))
                           .OrderByDescending(s => s.BundleNo)
                           .GetPaged<Bundle>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            return data;
        }
        
        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Bundle.AnyAsync(t => t.Id != id && t.BundleNo.ToLower() == name.ToLower());
        }
    }
}
