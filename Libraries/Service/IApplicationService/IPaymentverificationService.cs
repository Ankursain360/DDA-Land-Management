using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Master;

namespace Libraries.Service.IApplicationService
{

    public interface IPaymentverificationService : IEntityService<Paymentverification>
    {

        Task<List<Paymentverification>> GetAllPaymentList();
        Task<PagedResult<Paymentverification>> GetPagedPaymentListUnverified(PaymentverificationSearchDto model);
        Task<PagedResult<Paymentverification>> GetPagedPaymentListVerified(PaymentverificationSearchDto model);
        Task<bool> Verify(int id,int userid);
        Task<bool> Unverify(int id, int userid);
        Task<Paymentverification> FetchSingleResult(int id);
        //Task<PagedResult<Paymentverification>> GetPaymentTransactionReportData(PaymentTransactionReportSearchDto paymentTransactionReportSearchDto);
        Task<List<PaymentTransactionReportListDataDto>> GetPagedPaymentTransactionReportData(PaymentTransactionReportSearchDto model);
        Task<List<PaymentTransactionReportListDataDto>> GetPagedPaidReportData(DueVsPaidReportSearchDto model);
        Task<List<Paymentverification>> BindFileNoList();
        Task<PagedResult<Paymentverification>> GetPagedPaymentVerificationDoneByAcc(PaymentVerificationAccountSection model);
        Task<List<Locality>> BindLoclityList();

        Task<bool> Create(VerifyPaymentApiStatusDto paymentApiStatusDto);
    }
}
