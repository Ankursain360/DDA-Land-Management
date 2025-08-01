﻿using System;
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
        public async Task<List<Interest>> GetAllInterest()
        {
            return (await _dbContext.Interest.Include(x => x.Property).ToListAsync())
                          .GroupBy(x => x.PropertyId)
                          .SelectMany(g => g.OrderByDescending(d => d.ToDate).Take(1)).ToList();

        }
        public async Task<List<Interest>> GetAllDetails()
        {
            List<Interest> olist = new List<Interest>();

            var Data = await (from A in _dbContext.Interest
                              join B in _dbContext.PropertyType on A.PropertyId equals B.Id
                              where A.IsActive == 1
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
            Interest interest = new Interest();
            int count = _dbContext.Interest.Where(A => A.PropertyId == propertyId).Count();
            interest.IsRecordExist = count;

            DateTime result = _dbContext.Interest
                        .Where(A => A.PropertyId == propertyId)
                        .Select(A => (DateTime?)A.FromDate)
                        .Max() ?? DateTime.Now;

            //(from A in _dbContext.Interest
            //          where A.PropertyId == propertyId
            //          select A.FromDate).Max();


            //var result = _dbContext.Interest.Find(propertyId).FromDate.ToString("dd-MMM-yyyy");
            return result;
        }

        public async Task<PagedResult<Interest>> GetPagedInterest(InterestSearchDto model)
        {
            try
            {
                //  await _dbContext.LoadStoredProcedure("").WithSqlParams(("para", "values"), ("5456", "")).ExecuteStoredProcedureAsync<Designation>();
                var data = await _dbContext.LoadStoredProcedure("GetInterestIndexDetails")
                                .ExecuteStoredProcedureAsync<InterestIndexDataDetails>();
                //  var data1 =data.GetPaged<Interest>(model.PageNumber, model.PageSize);
                
                return (PagedResult<Interest>)data;
            }
            catch (Exception ex)
            {
                throw;
            }
            //return await _dbContext.Interest.Include(x => x.Property).GroupBy(x => x.PropertyTypeName).SelectMany(g => g.OrderByDescending(d => d.ToDate).Take(1)).GetPaged<Interest>(model.PageNumber, model.PageSize);
            
        }

        public async Task<List<PropertyType>> GetPropertyTypeList()
        {
            var propertyTypeList = await _dbContext.PropertyType.Where(x => x.IsActive == 1).ToListAsync();
            return propertyTypeList;
        }

        public int IsRecordExist(int propertyId)
        {
            return _dbContext.Interest.Where(A => A.PropertyId == propertyId).Count();
        }
       
        public async Task<List<Interest>> GetSearchResult(InterestSearchDto model)
        {
            var data= (await _dbContext.Interest.Include(x => x.Property)
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
