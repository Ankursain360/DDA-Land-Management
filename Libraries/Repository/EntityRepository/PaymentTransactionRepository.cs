using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Master;

namespace Libraries.Repository.EntityRepository
{
    public class PaymentTransactionRepository : GenericRepository<Payment>, IpaymentTransactionRepository
    {
        public PaymentTransactionRepository(DataContext dbContext) : base(dbContext)
        {
        }


        public async Task<PagedResult<Payment>> GetPagedPaymentTransactionReport(PaymentTranscationReportDto model)
        {
            var data = await _dbContext.Payment
                                     .Include(x => x.LeasePaymentType)
                                     .Include(x=>x.Allotment)
                                      .Include(x => x.Allotment.Application)
                                      .Where(x => (x.IsActive == 1)
                                       && (x.TransactionDate >= model.FromDate && x.TransactionDate <= model.ToDate)                                      
                                    )
                                    .OrderByDescending(x => x.Id)
                                .GetPaged<Payment>(model.PageNumber, model.PageSize);
                                    


            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("RECEIPTNO"):
                        data = null;
                        data = await _dbContext.Payment
                                     .Include(x => x.LeasePaymentType)
                                     .Include(x => x.Allotment)
                                       .Include(x => x.Allotment.Application)
                                      .Where(x => (x.IsActive == 1)                                      
                                       && (x.TransactionDate >= model.FromDate && x.TransactionDate <= model.ToDate)
                                    )
                                    .OrderBy(x => x.RecieptNo)
                                .GetPaged<Payment>(model.PageNumber, model.PageSize);
                        break;
                    case ("AMOUNT"):
                        data = null;
                        data = await _dbContext.Payment
                                     .Include(x => x.LeasePaymentType)
                                     .Include(x => x.Allotment)
                                       .Include(x => x.Allotment.Application)
                                      .Where(x => (x.IsActive == 1)
                                       && (x.TransactionDate >= model.FromDate && x.TransactionDate <= model.ToDate)
                                    )
                                    .OrderBy(x => x.TotalAmount)
                                .GetPaged<Payment>(model.PageNumber, model.PageSize);

                        break;
                    case ("TRANSACTIONDATE"):
                        data = null;
                        data = await _dbContext.Payment
                                     .Include(x => x.LeasePaymentType)
                                     .Include(x => x.Allotment)
                                       .Include(x => x.Allotment.Application)
                                      .Where(x => (x.IsActive == 1)
                                       && (x.TransactionDate >= model.FromDate && x.TransactionDate <= model.ToDate)
                                    )
                                    .OrderBy(x => x.TransactionDate)
                                .GetPaged<Payment>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("RECEIPTNO"):
                        data = null;
                        data = await _dbContext.Payment
                                     .Include(x => x.LeasePaymentType)
                                     .Include(x => x.Allotment)
                                       .Include(x => x.Allotment.Application)
                                      .Where(x => (x.IsActive == 1)                                      
                                       && (x.TransactionDate >= model.FromDate && x.TransactionDate <= model.ToDate)
                                    )
                                    .OrderByDescending(x => x.RecieptNo)
                                .GetPaged<Payment>(model.PageNumber, model.PageSize);
                        break;
                    case ("AMOUNT"):
                        data = null;
                        data = await _dbContext.Payment
                                     .Include(x => x.LeasePaymentType)
                                     .Include(x => x.Allotment)
                                       .Include(x => x.Allotment.Application)
                                      .Where(x => (x.IsActive == 1)
                                       && (x.TransactionDate >= model.FromDate && x.TransactionDate <= model.ToDate)
                                    )
                                    .OrderByDescending(x => x.Amount)
                                .GetPaged<Payment>(model.PageNumber, model.PageSize);

                        break;
                    case ("TRANSACTIONDATE"):
                        data = null;
                        data = await _dbContext.Payment
                                     .Include(x => x.LeasePaymentType)
                                     .Include(x => x.Allotment)
                                       .Include(x => x.Allotment.Application)
                                      .Where(x => (x.IsActive == 1)
                                       && (x.TransactionDate >= model.FromDate && x.TransactionDate <= model.ToDate)
                                    )
                                    .OrderByDescending(x => x.TransactionDate)
                                .GetPaged<Payment>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            return data;


        }



    }
}
