using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
namespace Libraries.Repository.EntityRepository
{
    public class PaymentdetailRepository : GenericRepository<Paymentdetail>, IPaymentdetailRepository
    {
        public PaymentdetailRepository(DataContext dbcontext) : base(dbcontext)
        { }

        public async Task<List<Paymentdetail>> GetPaymentdetail()
        {
            return await _dbContext.Paymentdetail.Where(x => x.IsActive == 1).ToListAsync();
        }

        public async Task<PagedResult<Paymentdetail>> GetPagedPaymentdetail(PaymentdetailSearchDto model)
        {
            var data = await _dbContext.Paymentdetail
                  .Where(x => (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                  && (string.IsNullOrEmpty(model.bankName) || x.BankName.Contains(model.bankName))
                    && (string.IsNullOrEmpty(model.chequeNo) || x.ChequeNo.Contains(model.chequeNo)))
                 .GetPaged<Paymentdetail>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("DEMANDLISTNO"):
                        data = null;
                        data = await _dbContext.Paymentdetail
                             .Where(x => (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                  && (string.IsNullOrEmpty(model.bankName) || x.BankName.Contains(model.bankName))
                    && (string.IsNullOrEmpty(model.chequeNo) || x.ChequeNo.Contains(model.chequeNo)))
                           .OrderBy(s => s.DemandListNo)
                           .GetPaged<Paymentdetail>(model.PageNumber, model.PageSize);

                        break;
                    case ("BANKNAME"):
                        data = null;
                        data = await _dbContext.Paymentdetail
                             .Where(x => (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                  && (string.IsNullOrEmpty(model.bankName) || x.BankName.Contains(model.bankName))
                    && (string.IsNullOrEmpty(model.chequeNo) || x.ChequeNo.Contains(model.chequeNo)))
                           .OrderBy(s => s.BankName)
                           .GetPaged<Paymentdetail>(model.PageNumber, model.PageSize);
                        break;
                    case ("CHEQUENO"):
                        data = null;
                        data = await _dbContext.Paymentdetail
                             .Where(x => (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                  && (string.IsNullOrEmpty(model.bankName) || x.BankName.Contains(model.bankName))
                    && (string.IsNullOrEmpty(model.chequeNo) || x.ChequeNo.Contains(model.chequeNo)))
                           .OrderBy(s => s.ChequeNo)
                           .GetPaged<Paymentdetail>(model.PageNumber, model.PageSize);
                        break;
                    case ("VOUCHERNO"):
                        data = null;
                        data = await _dbContext.Paymentdetail
                             .Where(x => (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                  && (string.IsNullOrEmpty(model.bankName) || x.BankName.Contains(model.bankName))
                    && (string.IsNullOrEmpty(model.chequeNo) || x.ChequeNo.Contains(model.chequeNo)))
                           .OrderBy(s => s.VoucherNo)
                           .GetPaged<Paymentdetail>(model.PageNumber, model.PageSize);
                        break;
                    case ("DATE"):
                        data = null;
                        data = await _dbContext.Paymentdetail
                             .Where(x => (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                  && (string.IsNullOrEmpty(model.bankName) || x.BankName.Contains(model.bankName))
                    && (string.IsNullOrEmpty(model.chequeNo) || x.ChequeNo.Contains(model.chequeNo)))
                           .OrderBy(s => s.ChequeDate)
                           .GetPaged<Paymentdetail>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Paymentdetail
                             .Where(x => (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                  && (string.IsNullOrEmpty(model.bankName) || x.BankName.Contains(model.bankName))
                    && (string.IsNullOrEmpty(model.chequeNo) || x.ChequeNo.Contains(model.chequeNo)))
                           .OrderByDescending(x => x.IsActive)
                           .GetPaged<Paymentdetail>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("DEMANDLISTNO"):
                        data = null;
                        data = await _dbContext.Paymentdetail
                             .Where(x => (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                  && (string.IsNullOrEmpty(model.bankName) || x.BankName.Contains(model.bankName))
                    && (string.IsNullOrEmpty(model.chequeNo) || x.ChequeNo.Contains(model.chequeNo)))
                           .OrderByDescending(s => s.DemandListNo)
                           .GetPaged<Paymentdetail>(model.PageNumber, model.PageSize);

                        break;
                    case ("BANKNAME"):
                        data = null;
                        data = await _dbContext.Paymentdetail
                             .Where(x => (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                  && (string.IsNullOrEmpty(model.bankName) || x.BankName.Contains(model.bankName))
                    && (string.IsNullOrEmpty(model.chequeNo) || x.ChequeNo.Contains(model.chequeNo)))
                           .OrderByDescending(s => s.BankName)
                           .GetPaged<Paymentdetail>(model.PageNumber, model.PageSize);
                        break;
                    case ("CHEQUENO"):
                        data = null;
                        data = await _dbContext.Paymentdetail
                             .Where(x => (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                  && (string.IsNullOrEmpty(model.bankName) || x.BankName.Contains(model.bankName))
                    && (string.IsNullOrEmpty(model.chequeNo) || x.ChequeNo.Contains(model.chequeNo)))
                           .OrderByDescending(s => s.ChequeNo)
                           .GetPaged<Paymentdetail>(model.PageNumber, model.PageSize);
                        break;
                    case ("VOUCHERNO"):
                        data = null;
                        data = await _dbContext.Paymentdetail
                             .Where(x => (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                  && (string.IsNullOrEmpty(model.bankName) || x.BankName.Contains(model.bankName))
                    && (string.IsNullOrEmpty(model.chequeNo) || x.ChequeNo.Contains(model.chequeNo)))
                           .OrderByDescending(s => s.VoucherNo)
                           .GetPaged<Paymentdetail>(model.PageNumber, model.PageSize);
                        break;
                    case ("DATE"):
                        data = null;
                        data = await _dbContext.Paymentdetail
                             .Where(x => (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                  && (string.IsNullOrEmpty(model.bankName) || x.BankName.Contains(model.bankName))
                    && (string.IsNullOrEmpty(model.chequeNo) || x.ChequeNo.Contains(model.chequeNo)))
                           .OrderByDescending(s => s.ChequeDate)
                           .GetPaged<Paymentdetail>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Paymentdetail
                             .Where(x => (string.IsNullOrEmpty(model.demandListNo) || x.DemandListNo.Contains(model.demandListNo))
                  && (string.IsNullOrEmpty(model.bankName) || x.BankName.Contains(model.bankName))
                    && (string.IsNullOrEmpty(model.chequeNo) || x.ChequeNo.Contains(model.chequeNo)))
                           .OrderBy(x => x.IsActive)
                           .GetPaged<Paymentdetail>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            return data;
        }


    }
}


