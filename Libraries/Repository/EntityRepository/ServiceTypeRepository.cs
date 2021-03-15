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
    public class ServiceTypeRepository : GenericRepository<Servicetype>, IServiceTypeRepository
    {
        public ServiceTypeRepository(DataContext dbcontext) : base(dbcontext)
        { }

        public async Task<List<Servicetype>> GetServicetype()
        {
            return await _dbContext.Servicetype.Where(x => x.IsActive == 1).ToListAsync();
        }

        public async Task<PagedResult<Servicetype>> GetPagedServicetype(ServiceTypeSearchDto model)
        {
            var data = await _dbContext.Servicetype
                  .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))
                 
                 .GetPaged<Servicetype>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Servicetype
                             .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))
                           .OrderBy(s => s.Name)
                           .GetPaged<Servicetype>(model.PageNumber, model.PageSize);

                        break;
                  
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Servicetype
                             .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))
                           .OrderByDescending(x => x.IsActive)
                           .GetPaged<Servicetype>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Servicetype
                             .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))
                           .OrderByDescending(s => s.Name)
                           .GetPaged<Servicetype>(model.PageNumber, model.PageSize);

                        break;

                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Servicetype
                             .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))
                           .OrderBy(x => x.IsActive)
                           .GetPaged<Servicetype>(model.PageNumber, model.PageSize);
                        break;


                }
            }
            return data;
        }


    }
}

