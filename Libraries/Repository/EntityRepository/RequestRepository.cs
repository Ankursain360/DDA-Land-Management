using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;


namespace Libraries.Repository.EntityRepository
{
  public  class RequestRepository : GenericRepository<Request>, IRequestRepository
    {
        public RequestRepository(DataContext dbContext) : base(dbContext)
        {

        }


        public async Task<List<Request>> GetAllRequest()
        {
            return await _dbContext.Request.OrderByDescending(x => x.Id).ToListAsync();
        }


        public async Task<PagedResult<Request>> GetPagedRequest(RequestSearchDto model)
        {



            var data = await _dbContext.Request
              .Where(x => (string.IsNullOrEmpty(model.name) || x.PproposalName.Contains(model.name))
                && (string.IsNullOrEmpty(model.area) || x.AreaLocality.Contains(model.area))
                 && (string.IsNullOrEmpty(model.fileno) || x.PfileNo.Contains(model.fileno))
               //  && (x.IsActive == 1)
               )




             .GetPaged<Request>(model.PageNumber, model.PageSize);



            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Request
               .Where(x => (string.IsNullOrEmpty(model.name) || x.PproposalName.Contains(model.name))
                && (string.IsNullOrEmpty(model.area) || x.AreaLocality.Contains(model.area))
                 && (string.IsNullOrEmpty(model.fileno) || x.PfileNo.Contains(model.fileno))
             
               )
                                .OrderBy(s => s.PproposalName)
                                .GetPaged<Request>(model.PageNumber, model.PageSize);
                        break;
                    case ("CODE"):
                        data = null;
                        data = await _dbContext.Request
            .Where(x => (string.IsNullOrEmpty(model.name) || x.PproposalName.Contains(model.name))
             && (string.IsNullOrEmpty(model.area) || x.AreaLocality.Contains(model.area))
              && (string.IsNullOrEmpty(model.fileno) || x.PfileNo.Contains(model.fileno))

            )
                             .OrderBy(s => s.AreaLocality)
                             .GetPaged<Request>(model.PageNumber, model.PageSize);

                        break;
                    case ("FILENO"):
                        data = null;
                        data = await _dbContext.Request
             .Where(x => (string.IsNullOrEmpty(model.name) || x.PproposalName.Contains(model.name))
              && (string.IsNullOrEmpty(model.area) || x.AreaLocality.Contains(model.area))
               && (string.IsNullOrEmpty(model.fileno) || x.PfileNo.Contains(model.fileno))

             )
                              .OrderBy(s => s.PfileNo)
                              .GetPaged<Request>(model.PageNumber, model.PageSize);

                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Request
           .Where(x => (string.IsNullOrEmpty(model.name) || x.PproposalName.Contains(model.name))
            && (string.IsNullOrEmpty(model.area) || x.AreaLocality.Contains(model.area))
             && (string.IsNullOrEmpty(model.fileno) || x.PfileNo.Contains(model.fileno)))
                                .OrderByDescending(s => s.IsActive)
                                .GetPaged<Request>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                  
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Request
               .Where(x => (string.IsNullOrEmpty(model.name) || x.PproposalName.Contains(model.name))
                && (string.IsNullOrEmpty(model.area) || x.AreaLocality.Contains(model.area))
                 && (string.IsNullOrEmpty(model.fileno) || x.PfileNo.Contains(model.fileno))

               )
                                .OrderByDescending(s => s.PproposalName)
                                .GetPaged<Request>(model.PageNumber, model.PageSize);
                        break;
                    case ("CODE"):
                        data = null;
                        data = await _dbContext.Request
            .Where(x => (string.IsNullOrEmpty(model.name) || x.PproposalName.Contains(model.name))
             && (string.IsNullOrEmpty(model.area) || x.AreaLocality.Contains(model.area))
              && (string.IsNullOrEmpty(model.fileno) || x.PfileNo.Contains(model.fileno))

            )
                             .OrderByDescending(s => s.AreaLocality)
                             .GetPaged<Request>(model.PageNumber, model.PageSize);

                        break;
                    case ("FILENO"):
                        data = null;
                        data = await _dbContext.Request
             .Where(x => (string.IsNullOrEmpty(model.name) || x.PproposalName.Contains(model.name))
              && (string.IsNullOrEmpty(model.area) || x.AreaLocality.Contains(model.area))
               && (string.IsNullOrEmpty(model.fileno) || x.PfileNo.Contains(model.fileno))

             )
                              .OrderByDescending(s => s.PfileNo)
                              .GetPaged<Request>(model.PageNumber, model.PageSize);

                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Request
           .Where(x => (string.IsNullOrEmpty(model.name) || x.PproposalName.Contains(model.name))
            && (string.IsNullOrEmpty(model.area) || x.AreaLocality.Contains(model.area))
             && (string.IsNullOrEmpty(model.fileno) || x.PfileNo.Contains(model.fileno)))
                                .OrderBy(s => s.IsActive)
                                .GetPaged<Request>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            return data;







        }



    }
}
