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
    public class RebateRepository : GenericRepository<Rebate>, IRebateRepository
    {

        public RebateRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Rebate>> GetAllDetails()
        {
            List<Rebate> olist = new List<Rebate>();
            olist =await _dbContext.Rebate.Where(x => x.IsActive == 1).ToListAsync();

            return (olist.GroupBy(x => x.IsRebateOn).SelectMany(g => g.OrderByDescending(d => d.ToDate).Take(1)).ToList());
        }
        public async Task<List<PropertyType>> GetPropertyTypeList()
        {
            var propertyTypeList = await _dbContext.PropertyType.Where(x => x.IsActive == 1).ToListAsync();
            return propertyTypeList;
        }

        public object GetFromDateData(int propertyId)
        {
            Rebate rebate = new Rebate();
            int count = _dbContext.Rebate.Where(A => A.IsRebateOn == propertyId).Count();
            rebate.IsRecordExist = count;
            DateTime result = _dbContext.Rebate
                       .Where(A => A.IsRebateOn == propertyId)
                       .Select(A => (DateTime?)A.FromDate)
                       .Max() ?? DateTime.Now;
            //var result = (from A in _dbContext.Rebate
            //              where A.IsRebateOn == propertyId
            //              select A.FromDate).Max();

           
            return result;
        }

        public async Task<List<Rebate>> GetPagedRebate(RebateSearchDto model)
        {
            List<Rebate> olist = new List<Rebate>();
            olist = await _dbContext.Rebate
                                    .Where(x => x.FromDate >= (model.FromDate == "" ? x.FromDate : Convert.ToDateTime(model.FromDate))
                                    && x.ToDate <= (model.ToDate == "" ? x.ToDate : Convert.ToDateTime(model.ToDate))
                                    && x.RebatePercentage == (model.RebatePercentage == "" ? x.RebatePercentage : Convert.ToDecimal(model.RebatePercentage))
                                    )
                                    .ToListAsync();

            var data= (olist.GroupBy(x => x.IsRebateOn).SelectMany(g => g.OrderByDescending(d => d.ToDate).Take(1)).ToList());

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("REBATEON"):
                        data = data.OrderBy(x => x.IsRebateOn).ToList();
                        break;
                    case ("FROMDATE"):
                        data = data.OrderBy(x => x.FromDate).ToList();
                        break;
                    case ("TODATE"):
                        data = data.OrderBy(x => x.ToDate).ToList();
                        break;
                    case ("PERCENTAGE"):
                        data = data.OrderBy(x => x.RebatePercentage).ToList();
                        break;
                    case ("ISACTIVE"):
                        data = data.OrderByDescending(x => x.IsActive).ToList();
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("REBATEON"):
                        data = data.OrderByDescending(x => x.IsRebateOn).ToList();
                        break;
                    case ("FROMDATE"):
                        data = data.OrderByDescending(x => x.FromDate).ToList();
                        break;
                    case ("TODATE"):
                        data = data.OrderByDescending(x => x.ToDate).ToList();
                        break;
                    case ("PERCENTAGE"):
                        data = data.OrderByDescending(x => x.RebatePercentage).ToList();
                        break;
                    case ("ISACTIVE"):
                        data = data.OrderBy(x => x.IsActive).ToList();
                        break;

                }
            }
            return data;
        }

        public int IsRecordExist(int propertyId)
        {
            return _dbContext.Rebate.Where(A => A.IsRebateOn == propertyId).Count();
        }
    }


}
