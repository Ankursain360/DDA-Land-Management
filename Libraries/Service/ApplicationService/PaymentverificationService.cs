using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ApplicationService
{
    public class PaymentverificationService : EntityService<Paymentverification>, IPaymentverificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentverificationRepository _paymentverificationRepository;
        //private readonly UserManager<ApplicationUser> _userManager;
        public PaymentverificationService(IUnitOfWork unitOfWork, IPaymentverificationRepository paymentverificationRepository)
            //UserManager<ApplicationUser> userManager)
        : base(unitOfWork, paymentverificationRepository)
        {
            _unitOfWork = unitOfWork;
            _paymentverificationRepository = paymentverificationRepository;
           // _userManager = userManager;
        }
        public async Task<List<Paymentverification>> BindFileNoList()
        {
            return await _paymentverificationRepository.BindFileNoList();
        }

        public async Task<List<Locality>> BindLoclityList()
        {
            return await _paymentverificationRepository.BindLoclityList();
        }

        public async Task<List<Paymentverification>> GetAllPaymentList()
        {
            return await _paymentverificationRepository.GetAllPaymentList();
        }
        public async Task<PagedResult<Paymentverification>> GetPagedPaymentListUnverified(PaymentverificationSearchDto model)
        {
            return await _paymentverificationRepository.GetPagedPaymentListUnverified(model);
        }
        public async Task<PagedResult<Paymentverification>> GetPagedPaymentListVerified(PaymentverificationSearchDto model)
        {
            return await _paymentverificationRepository.GetPagedPaymentListVerified(model);
        }

        public async Task<PagedResult<Paymentverification>> GetPagedPaymentVerificationDoneByAcc(PaymentVerificationAccountSection model)
        {
            return await _paymentverificationRepository.GetPagedPaymentVerificationDoneByAcc(model);
        }
        //public async Task<PagedResult<Paymentverification>> GetPaymentTransactionReportData(PaymentTransactionReportSearchDto paymentTransactionReportSearchDto)
        //{
        //    return await _paymentverificationRepository.GetPaymentTransactionReportData(paymentTransactionReportSearchDto);
        //}
        public async Task<List<PaymentTransactionReportListDataDto>> GetPagedPaymentTransactionReportData(PaymentTransactionReportSearchDto model)
        {
            return await _paymentverificationRepository.GetPagedPaymentTransactionReportData(model);
        }

        public async Task<Paymentverification> FetchSingleResult(int id)
        {
            var result = await _paymentverificationRepository.FindBy(a => a.Id == id);
            Paymentverification model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Verify(int id, int userid)
        {
            var result = await _paymentverificationRepository.FindBy(a => a.Id == id);
            Paymentverification model = result.FirstOrDefault();
            model.IsVerified = 1;

            model.VerifiedOn = DateTime.Now;
            model.ModifiedBy = userid;
            model.ModifiedDate= DateTime.Now;
            model.VerifiedBy = userid;
            _paymentverificationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<bool> Unverify(int id, int userid)
        {
            var result = await _paymentverificationRepository.FindBy(a => a.Id == id);
            Paymentverification model = result.FirstOrDefault();
            model.IsVerified = 0;

            //model.VerifiedOn = DateTime.Now;
            model.ModifiedBy = userid;
            model.ModifiedDate = DateTime.Now;
            //model.VerifiedBy = userid;
            _paymentverificationRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
        
        public Task<PagedResult<Paymentverification>> GetPaymentTransactionReportData(PaymentTransactionReportSearchDto paymentTransactionReportSearchDto)
        {
            throw new NotImplementedException();
        }
       
        public async Task<List<PaymentTransactionReportListDataDto>> GetPagedPaidReportData(DueVsPaidReportSearchDto model)
        {
            return await _paymentverificationRepository.GetPagedPaidReportData(model);
        }
    }
}
