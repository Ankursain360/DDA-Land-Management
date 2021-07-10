using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Master;

namespace Service.ApplicationService
{
  public  class PaymentTransactionService : EntityService<Payment>, IPaymentTransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IpaymentTransactionRepository _paymentTransactionRepository;

        public PaymentTransactionService(IUnitOfWork unitOfWork, IpaymentTransactionRepository paymentTransactionRepository)
   : base(unitOfWork, paymentTransactionRepository)
        {
            _unitOfWork = unitOfWork;
            _paymentTransactionRepository = paymentTransactionRepository;
        }

        public async Task<PagedResult<Payment>> GetPagedPaymentTransactionReport(PaymentTranscationReportDto model)
        {
            return await _paymentTransactionRepository.GetPagedPaymentTransactionReport(model);
        }

        public async Task<List<Allotmententry>> GetAllAllotmententry()
        {

            return await _paymentTransactionRepository.GetAllAllotmententry();
        }
        public async Task<PagedResult<Payment>> GetPagedPaymentLedgerReport(PaymentLedgerSearchDto model)//added by ishu
        {
            return await _paymentTransactionRepository.GetPagedPaymentLedgerReport(model);
        }
    }
}
