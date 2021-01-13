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
    public class PaymentverificationRepository : GenericRepository<Paymentverification>, IPaymentverificationRepository
    {
        public PaymentverificationRepository(DataContext dbContext) : base(dbContext)
        {

        }
      
        //public async Task<PagedResult<Paymentverification>> GetPagedPaymentList(PaymentverificationSearchDto model)
        //{
        //    return await _dbContext.Paymentverification
        //        .Where(x => x.IsVerified == 0)
        //        .GetPaged<Paymentverification>(model.PageNumber, model.PageSize);
        //}
        public async Task<PagedResult<Paymentverification>> GetPagedPaymentListUnverified(PaymentverificationSearchDto model)
        {
            var data = await _dbContext.Paymentverification
                           // .Where(a => a.IsVerified == 0)
                          .Where(x => (string.IsNullOrEmpty(model.name) || x.PayeeName.Contains(model.name)) && (x.IsVerified == 0))
                            .OrderByDescending(s => s.IsActive)
                            .GetPaged<Paymentverification>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data.Results = data.Results.OrderBy(x => x.PayeeName).ToList();
                        break;
                    case ("STATUS"):
                        data.Results = data.Results.OrderBy(x => x.IsActive).ToList();
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data.Results = data.Results.OrderByDescending(x => x.PayeeName).ToList();
                        break;
                    case ("STATUS"):
                        data.Results = data.Results.OrderByDescending(x => x.IsActive).ToList();
                        break;
                }
            }
            return data;
           
        }
        public async Task<PagedResult<Paymentverification>> GetPagedPaymentListVerified(PaymentverificationSearchDto model)
        {
            var data = await _dbContext.Paymentverification
                           // .Where(a => a.IsVerified == 0)
                           .Where(x => (string.IsNullOrEmpty(model.name) || x.PayeeName.Contains(model.name)) && (x.IsVerified == 1))

                            .OrderByDescending(s => s.IsActive)
                            .GetPaged<Paymentverification>(model.PageNumber, model.PageSize);
         
            return data;

        }

        public async Task<List<Paymentverification>> GetAllPaymentList()
        {
            return await _dbContext.Paymentverification
           .Where(x => x.IsActive == 1)
           .ToListAsync();
        }
    }
}