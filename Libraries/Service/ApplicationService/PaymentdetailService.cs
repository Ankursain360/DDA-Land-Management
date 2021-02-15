using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Libraries.Repository.IEntityRepository;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using Libraries.Model;
using Dto.Search;

namespace Libraries.Service.ApplicationService
{
    public class PaymentdetailService : EntityService<Paymentdetail>, IPaymentdetailService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentdetailRepository _paymentdetailRepository;
        protected readonly DataContext _dbContext;


        public PaymentdetailService(IUnitOfWork unitOfWork, IPaymentdetailRepository paymentdetailRepository, DataContext dbContext)
       : base(unitOfWork, paymentdetailRepository)
        {
            _unitOfWork = unitOfWork;
            _paymentdetailRepository = paymentdetailRepository;
            _dbContext = dbContext;
        }



        public async Task<List<Paymentdetail>> GetAllPaymentdetail()
        {
            return await _paymentdetailRepository.GetAll();
        }

        public async Task<List<Paymentdetail>> GetPaymentdetailUsingRepo()
        {
            return await _paymentdetailRepository.GetPaymentdetail();
        }

        public async Task<bool> Update(int id, Paymentdetail paymentdetail)
        {
            var result = await _paymentdetailRepository.FindBy(a => a.Id == id);
            Paymentdetail model = result.FirstOrDefault();
            model.DemandListNo = paymentdetail.DemandListNo;
            model.EnmSno = paymentdetail.EnmSno;
            model.AmountPaid = paymentdetail.AmountPaid;
            model.ChequeDate = paymentdetail.ChequeDate;
            model.ChequeNo = paymentdetail.ChequeNo;
            model.BankName = paymentdetail.BankName;
            model.VoucherNo = paymentdetail.VoucherNo;
            model.PercentPaid = paymentdetail.PercentPaid;
          

            model.ModifiedDate = DateTime.Now;
            model.IsActive = paymentdetail.IsActive;
            model.ModifiedBy = 1;
            _paymentdetailRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }



        public async Task<Paymentdetail> FetchSingleResult(int id)
        {
            var result = await _paymentdetailRepository.FindBy(a => a.Id == id);
            Paymentdetail model = result.FirstOrDefault();
            return model;
        }



        public async Task<bool> Create(Paymentdetail paymentdetail)
        {

            paymentdetail.CreatedBy = 1;
            paymentdetail.CreatedDate = DateTime.Now;
            _paymentdetailRepository.Add(paymentdetail);
            return await _unitOfWork.CommitAsync() > 0;
        }




        public async Task<bool> Delete(int id)
        {
            var form = await _paymentdetailRepository.FindBy(a => a.Id == id);
            Paymentdetail model = form.FirstOrDefault();
            model.IsActive = 0;
            _paymentdetailRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Paymentdetail>> GetPagedPaymentdetail(PaymentdetailSearchDto model)
        {
            return await _paymentdetailRepository.GetPagedPaymentdetail(model);
        }

    }
}


