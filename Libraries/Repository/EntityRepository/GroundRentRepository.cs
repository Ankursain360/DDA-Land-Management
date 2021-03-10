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

    public class GroundRentRepository : GenericRepository<Groundrent>, IGroundRentRepository
    {
        public GroundRentRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<List<PropertyType>> GetAllPropertyTypeList()
        {
            return await _dbContext.PropertyType.Where(x=> x.IsActive==1).OrderByDescending(x => x.Id).ToListAsync();
        }
        public async Task<PagedResult<Groundrent>> GetPagedGroundRent(GroundrentSearchDto model)
        {
            var data = await _dbContext.Groundrent
                                        
                            .GetPaged<Groundrent>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("GROUNDRATE"):
                   
                        data = null;
                        data = await _dbContext.Groundrent
                               .Include(x => x.PropertyType)
                                .Where(x => (string.IsNullOrEmpty(model.name) || x.PropertyType.Name.Contains(model.name)))
                               //&& (string.IsNullOrEmpty(model.fromdate) || x.FromDate.Contains(model.fromdate))
                               //&& (string.IsNullOrEmpty(model.todate) || x.ToDate.Contains(model.todate)))
                               .OrderBy(x => x.GroundRate)
                                .GetPaged<Groundrent>(model.PageNumber, model.PageSize);
                        break;
                    case ("FROMDATE"):
                    
                        data = null;
                        data = await _dbContext.Groundrent
                               .Include(x => x.PropertyType)

                                .Where(x => (string.IsNullOrEmpty(model.name) || x.PropertyType.Name.Contains(model.name)))
                               //&& (string.IsNullOrEmpty(model.fromdate) || x.FromDate.Contains(model.fromdate))
                               //&& (string.IsNullOrEmpty(model.todate) || x.ToDate.Contains(model.todate)))
                               .OrderBy(x => x.FromDate)
                                .GetPaged<Groundrent>(model.PageNumber, model.PageSize);
                        break;
                    case ("TODATE"):
                         data = null;
                        data = await _dbContext.Groundrent
                               .Include(x => x.PropertyType)

                                .Where(x => (string.IsNullOrEmpty(model.name) || x.PropertyType.Name.Contains(model.name)))
                               //&& (string.IsNullOrEmpty(model.fromdate) || x.FromDate.Contains(model.fromdate))
                               //&& (string.IsNullOrEmpty(model.todate) || x.ToDate.Contains(model.todate)))
                               .OrderBy(x => x.ToDate)
                                .GetPaged<Groundrent>(model.PageNumber, model.PageSize);
                        break;
                    case ("PROPERTYTYPE"):
                         data = null;
                        data = await _dbContext.Groundrent
                               .Include(x => x.PropertyType)

                                .Where(x => (string.IsNullOrEmpty(model.name) || x.PropertyType.Name.Contains(model.name)))
                                 //&& (string.IsNullOrEmpty(model.fromdate) || x.FromDate.Contains(model.fromdate))
                                 //&& (string.IsNullOrEmpty(model.todate) || x.ToDate.Contains(model.todate)))
                               .OrderBy(x => x.PropertyType.Name)
                                .GetPaged<Groundrent>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Groundrent
                               .Include(x => x.PropertyType)

                                .Where(x => (string.IsNullOrEmpty(model.name) || x.PropertyType.Name.Contains(model.name)))
                               //&& (string.IsNullOrEmpty(model.fromdate) || x.FromDate.Contains(model.fromdate))
                               //&& (string.IsNullOrEmpty(model.todate) || x.ToDate.Contains(model.todate)))
                               .OrderByDescending(x => x.IsActive)
                                .GetPaged<Groundrent>(model.PageNumber, model.PageSize);
                        break;
                                    }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("GROUNDRATE"):

                        data = null;
                        data = await _dbContext.Groundrent
                               .Include(x => x.PropertyType)
                                .Where(x => (string.IsNullOrEmpty(model.name) || x.PropertyType.Name.Contains(model.name)))
                               //&& (string.IsNullOrEmpty(model.fromdate) || x.FromDate.Contains(model.fromdate))
                               //&& (string.IsNullOrEmpty(model.todate) || x.ToDate.Contains(model.todate)))
                               .OrderByDescending(x => x.GroundRate)
                                .GetPaged<Groundrent>(model.PageNumber, model.PageSize);
                        break;
                    case ("FROMDATE"):

                        data = null;
                        data = await _dbContext.Groundrent
                               .Include(x => x.PropertyType)

                                .Where(x => (string.IsNullOrEmpty(model.name) || x.PropertyType.Name.Contains(model.name)))
                               //&& (string.IsNullOrEmpty(model.fromdate) || x.FromDate.Contains(model.fromdate))
                               //&& (string.IsNullOrEmpty(model.todate) || x.ToDate.Contains(model.todate)))
                               .OrderByDescending(x => x.FromDate)
                                .GetPaged<Groundrent>(model.PageNumber, model.PageSize);
                        break;
                    case ("TODATE"):
                        data = null;
                        data = await _dbContext.Groundrent
                               .Include(x => x.PropertyType)

                                .Where(x => (string.IsNullOrEmpty(model.name) || x.PropertyType.Name.Contains(model.name)))
                               //&& (string.IsNullOrEmpty(model.fromdate) || x.FromDate.Contains(model.fromdate))
                               //&& (string.IsNullOrEmpty(model.todate) || x.ToDate.Contains(model.todate)))
                               .OrderByDescending(x => x.ToDate)
                                .GetPaged<Groundrent>(model.PageNumber, model.PageSize);
                        break;
                    case ("PROPERTYTYPE"):
                        data = null;
                        data = await _dbContext.Groundrent
                               .Include(x => x.PropertyType)

                                .Where(x => (string.IsNullOrEmpty(model.name) || x.PropertyType.Name.Contains(model.name)))
                               //&& (string.IsNullOrEmpty(model.fromdate) || x.FromDate.Contains(model.fromdate))
                               //&& (string.IsNullOrEmpty(model.todate) || x.ToDate.Contains(model.todate)))
                               .OrderByDescending(x => x.PropertyType.Name)
                                .GetPaged<Groundrent>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Groundrent
                               .Include(x => x.PropertyType)

                                .Where(x => (string.IsNullOrEmpty(model.name) || x.PropertyType.Name.Contains(model.name)))
                               //&& (string.IsNullOrEmpty(model.fromdate) || x.FromDate.Contains(model.fromdate))
                               //&& (string.IsNullOrEmpty(model.todate) || x.ToDate.Contains(model.todate)))
                               .OrderBy(x => x.IsActive)
                                .GetPaged<Groundrent>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            return data;

        }



        public async Task<List<Groundrent>> GetAllGroundRent()
        {
            return await _dbContext.Groundrent.Where(x => x.IsActive == 1).ToListAsync();
        }


    }
}
