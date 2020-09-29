using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;

namespace Libraries.Repository.EntityRepository
{
    public class RateRepository : GenericRepository<Rate>, IRateRepository
    {

        public RateRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Rate>> GetAllDetails()
        {
            List<Rate> olist = new List<Rate>();

            var Data = await (from A in _dbContext.Rate
                              join B in _dbContext.PropertyType on A.PropertyId equals B.Id
                              select new
                              {
                                  Id = A.Id,
                                  PropertyTypeName = B.Name,
                                  FromDate = A.FromDate,
                                  ToDate = A.ToDate,
                                  RatePercentage = A.RatePercentage,
                                  IsActive = A.IsActive
                              }).ToListAsync();

            if (Data != null)
            {
                for (int i = 0; i < Data.Count; i++)

                {
                    olist.Add(new Rate()
                    {
                        Id = Data[i].Id,
                        PropertyTypeName = Data[i].PropertyTypeName,
                        FromDate = Data[i].FromDate,
                        ToDate = Data[i].ToDate,
                        RatePercentage = Data[i].RatePercentage,
                        IsActive = Data[i].IsActive
                    });
                }
            }
            return (olist.GroupBy(x => x.PropertyTypeName).SelectMany(g => g.OrderByDescending(d => d.ToDate).Take(1)).ToList());
        }
        public async Task<List<PropertyType>> GetPropertyTypeList()
        {
            var propertyTypeList = await _dbContext.PropertyType.Where(x => x.IsActive == 1).ToListAsync();
            return propertyTypeList;
        }

        public object GetFromDateData(int propertyId)
        {
            var result = (from A in _dbContext.Rate
                          where A.PropertyId == propertyId
                          select A.FromDate).Max();
            return result;
        }

        public Task<PagedResult<Rate>> GetPagedRate(RateSearchDto model)
        {
            throw new NotImplementedException();
        }
    }


}
