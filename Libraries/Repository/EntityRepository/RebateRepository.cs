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

        public Task<PagedResult<Rebate>> GetPagedRebate(RebateSearchDto model)
        {
            throw new NotImplementedException();
        }

        public int IsRecordExist(int propertyId)
        {
            return _dbContext.Rebate.Where(A => A.IsRebateOn == propertyId).Count();
        }
    }


}
