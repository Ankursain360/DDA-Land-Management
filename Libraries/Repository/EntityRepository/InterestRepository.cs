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
using Repository.Common;

namespace Libraries.Repository.EntityRepository
{
    public class InterestRepository : GenericRepository<Interest>, IInterestRepository
    {

        public InterestRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Interest>> GetAllDetails()
        {
            List<Interest> olist = new List<Interest>();

            var Data = await (from A in _dbContext.Interest
                              join B in _dbContext.PropertyType on A.PropertyId equals B.Id
                              select new
                              {
                                  Id = A.Id,
                                  PropertyTypeName = B.Name,
                                  FromDate = A.FromDate,
                                  ToDate = A.ToDate,
                                  Percentage = A.Percentage,
                                  IsActive = A.IsActive
                              }).OrderByDescending(x => x.Id).ToListAsync();

            if (Data != null)
            {
                for (int i = 0; i < Data.Count; i++)

                {
                    olist.Add(new Interest()
                    {
                        Id = Data[i].Id,
                        PropertyTypeName = Data[i].PropertyTypeName,
                        FromDate = Data[i].FromDate,
                        ToDate = Data[i].ToDate,
                        Percentage = Data[i].Percentage,
                        IsActive = Data[i].IsActive
                    });
                }
            }
            return (olist.GroupBy(x => x.PropertyTypeName).SelectMany(g => g.OrderByDescending(d => d.ToDate).Take(1)).ToList());


        }

        public object GetFromDateData(int propertyId)
        {
            var result = (from A in _dbContext.Interest
                          where A.PropertyId == propertyId
                          select A.FromDate).Max();
            //var result = _dbContext.Interest.Find(propertyId).FromDate.ToString("dd-MMM-yyyy");
            return result;
        }

        public async Task<PagedResult<Interest>> GetPagedInterest(InterestSearchDto model)
        {
            try
            {
                //  await _dbContext.LoadStoredProcedure("").WithSqlParams(("para", "values"), ("5456", "")).ExecuteStoredProcedureAsync<Designation>();
                var data = await _dbContext.LoadStoredProcedure("GetInterestIndexDetails")
                                .WithOutParams().ExecuteStoredProcedureAsync<InterestIndexDataDetails>();
                //  var data1 =data.GetPaged<Interest>(model.PageNumber, model.PageSize);
                
                //return data;
            }
            catch (Exception ex)
            {
                throw;
            }
            return await _dbContext.Interest.Include(x => x.Property).GroupBy(x => x.PropertyTypeName).SelectMany(g => g.OrderByDescending(d => d.ToDate).Take(1)).GetPaged<Interest>(model.PageNumber, model.PageSize);

        }

        public async Task<List<PropertyType>> GetPropertyTypeList()
        {
            var propertyTypeList = await _dbContext.PropertyType.Where(x => x.IsActive == 1).ToListAsync();
            return propertyTypeList;
        }
    }


}
