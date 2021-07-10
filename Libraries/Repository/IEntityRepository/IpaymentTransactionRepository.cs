using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;

namespace Libraries.Repository.IEntityRepository
{
    public interface IpaymentTransactionRepository : IGenericRepository<Payment>
    {
        Task<PagedResult<Payment>> GetPagedPaymentTransactionReport(PaymentTranscationReportDto model);
        Task<List<Allotmententry>> GetAllAllotmententry();
        Task<PagedResult<Payment>> GetPagedPaymentLedgerReport(PaymentLedgerSearchDto model);
    }
}
