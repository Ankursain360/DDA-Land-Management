using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
namespace Libraries.Repository.IEntityRepository
{

    public interface IPaymentverificationRepository : IGenericRepository<Paymentverification>
    {
        Task<List<Paymentverification>> GetAllPaymentList();
        Task<List<Paymentverification>> GetPaymentVerificationDoneByAccList(PaymentVerificationAccountSection model);
        Task<List<Paymentverification>> GetAllPaymentVerificationList(ManualPaymentSearchDto model);
        Task<PagedResult<Paymentverification>> GetPagedPaymentListUnverified(PaymentverificationSearchDto model);
        Task<PagedResult<Paymentverification>> GetPagedPaymentListVerified(PaymentverificationSearchDto model);
        Task<PagedResult<Paymentverification>> GetPagedPaymentVerificationDoneByAcc(PaymentVerificationAccountSection model);
        //Task<PagedResult<Paymentverification>> GetPaymentTransactionReportData(PaymentTransactionReportSearchDto paymentTransactionReportSearchDto);
        Task<List<PaymentTransactionReportListDataDto>> GetPagedPaymentTransactionReportData(PaymentTransactionReportSearchDto model);
        Task<List<PaymentTransactionReportListDataDto>> GetPagedPaidReportData(DueVsPaidReportSearchDto model);

        Task<List<Paymentverification>> BindFileNoList();
        Task<List<Locality>> BindLoclityList();

        Task<PagedResult<Paymentverification>> GetPagedPaymentVerification(ManualPaymentSearchDto model);

    }
}
