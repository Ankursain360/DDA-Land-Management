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
        public async Task<List<Rate>> GetAllRate()
        {
            return (await _dbContext.Rate.Include(x => x.Property)
                .ToListAsync())
                .GroupBy(x => x.PropertyId).SelectMany(g => g.OrderByDescending(d => d.ToDate).Take(1)).ToList();

        }

        public async Task<List<Rate>> GetAllDetails()
        {
            List<Rate> olist = new List<Rate>();

            var Data = await (from A in _dbContext.Rate
                              join B in _dbContext.PropertyType on A.PropertyId equals B.Id
                              where A.IsActive ==1
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
            Rate rate = new Rate();
            int count = _dbContext.Rate.Where(A => A.PropertyId == propertyId).Count();
            rate.IsRecordExist = count;

            DateTime result = _dbContext.Rate
                        .Where(A => A.PropertyId == propertyId)
                        .Select(A => (DateTime?)A.FromDate)
                        .Max() ?? DateTime.Now;
            return result;
        }

        public Task<PagedResult<Rate>> GetPagedRate(RateSearchDto model)
        {
            throw new NotImplementedException();
        }

        public int IsRecordExist(int propertyId)
        {
            return _dbContext.Rate.Where(A => A.PropertyId == propertyId).Count();
        }

        public async Task<List<Rate>> GetSearchResult(RateSearchDto model)
        {
                       
            var data = (await _dbContext.Rate.Include(x => x.Property)
                               .Where(x => x.Property.Name.ToUpper().Contains((model.property ?? "").ToUpper()))
                               .ToListAsync()).GroupBy(x => x.PropertyId).SelectMany(g => g.OrderByDescending(d => d.ToDate).Take(1)).ToList();

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("PROPERTYTYPE"):
                        data = data.OrderBy(x => x.Property.Name).ToList();
                        break;
                    case ("FROMDATE"):
                        data = data.OrderBy(x => x.FromDate).ToList();
                        break;
                    case ("TODATE"):
                        data = data.OrderBy(x => x.ToDate).ToList();
                        break;
                    case ("STATUS"):
                        data = data.OrderBy(x => x.IsActive).ToList();
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("PROPERTYTYPE"):
                        data = data.OrderByDescending(x => x.Property.Name).ToList();
                        break;
                    case ("FROMDATE"):
                        data = data.OrderByDescending(x => x.FromDate).ToList();
                        break;
                    case ("TODATE"):
                        data = data.OrderByDescending(x => x.ToDate).ToList();
                        break;
                    case ("STATUS"):
                        data = data.OrderByDescending(x => x.IsActive).ToList();
                        break;
                }
            }
            return data;
        }
    }


}
