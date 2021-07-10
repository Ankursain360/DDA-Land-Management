

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

    public class ApplyForServicesRepository : GenericRepository<Servicetype>, IApplyForServicesRepository
    {
        public ApplyForServicesRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Servicetype>> GetPagedServicetype(ServiceSearchDto model)

        {
            var data = await _dbContext.Servicetype
                                       .Where(x => string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                       .Where(x => x.IsActive == 1)
                                       .GetPaged<Servicetype>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("TYPE"):
                        data = null;
                        data = await _dbContext.Servicetype
                                        .Where(x => string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                          .Where(x => x.IsActive == 1)
                                        .OrderBy(x => x.Name)
                                        .GetPaged<Servicetype>(model.PageNumber, model.PageSize);


                        break;



                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Servicetype
                                        .Where(x => string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                          .Where(x => x.IsActive == 1)
                                        .OrderByDescending(x => x.IsActive)
                                        .GetPaged<Servicetype>(model.PageNumber, model.PageSize);

                        break;


                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("TYPE"):
                        data = null;
                        data = await _dbContext.Servicetype
                                        .Where(x => string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                          .Where(x => x.IsActive == 1)
                                        .OrderByDescending(x => x.Name)
                                        .GetPaged<Servicetype>(model.PageNumber, model.PageSize);


                        break;



                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Servicetype
                                        .Where(x => string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                          .Where(x => x.IsActive == 1)
                                          .Where(x => x.IsActive == 1)
                                        .OrderBy(x => x.IsActive)
                                        .GetPaged<Servicetype>(model.PageNumber, model.PageSize);

                        break;

                }
            }
            return data;

        }



    }
}
