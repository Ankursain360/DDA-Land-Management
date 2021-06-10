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
    public class NewLandPaymentdetailService : EntityService<Newlandpaymentdetail>, INewLandPaymentdetailService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly INewLandPaymentDetailRepository _newLandPaymentDetailRepository;
        protected readonly DataContext _dbContext;


        public NewLandPaymentdetailService(IUnitOfWork unitOfWork, INewLandPaymentDetailRepository newLandPaymentDetailRepository, DataContext dbContext)
       : base(unitOfWork, newLandPaymentDetailRepository)
        {
            _unitOfWork = unitOfWork;
            _newLandPaymentDetailRepository = newLandPaymentDetailRepository;
            _dbContext = dbContext;
        }



        public async Task<List<Newlandpaymentdetail>> GetAllPaymentdetail()
        {
            return await _newLandPaymentDetailRepository.GetAll();
        }

        public async Task<List<Newlandpaymentdetail>> GetPaymentdetailUsingRepo()
        {
            return await _newLandPaymentDetailRepository.GetPaymentdetail();
        }

        public async Task<bool> Update(int id, Newlandpaymentdetail newlandpaymentdetail)
        {
            var result = await _newLandPaymentDetailRepository.FindBy(a => a.Id == id);
            Newlandpaymentdetail model = result.FirstOrDefault();
            model.DemandListNo = newlandpaymentdetail.DemandListNo;
            model.EnmSno = newlandpaymentdetail.EnmSno;
            model.AmountPaid = newlandpaymentdetail.AmountPaid;
            model.ChequeDate = newlandpaymentdetail.ChequeDate;
            model.ChequeNo = newlandpaymentdetail.ChequeNo;
            model.BankName = newlandpaymentdetail.BankName;
            model.VoucherNo = newlandpaymentdetail.VoucherNo;
            model.PercentPaid = newlandpaymentdetail.PercentPaid;
            model.PaymentProofDocumentName = newlandpaymentdetail.PaymentProofDocumentName;
            model.ModifiedDate = DateTime.Now;
            model.IsActive = newlandpaymentdetail.IsActive;
            model.ModifiedBy = newlandpaymentdetail.ModifiedBy;
            _newLandPaymentDetailRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }



        public async Task<Newlandpaymentdetail> FetchSingleResult(int id)
        {
            var result = await _newLandPaymentDetailRepository.FindBy(a => a.Id == id);
            Newlandpaymentdetail model = result.FirstOrDefault();
            return model;
        }



        public async Task<bool> Create(Newlandpaymentdetail newlandpaymentdetail)
        {
            newlandpaymentdetail.CreatedDate = DateTime.Now;
            _newLandPaymentDetailRepository.Add(newlandpaymentdetail);
            return await _unitOfWork.CommitAsync() > 0;
        }




        public async Task<bool> Delete(int id)
        {
            var form = await _newLandPaymentDetailRepository.FindBy(a => a.Id == id);
            Newlandpaymentdetail model = form.FirstOrDefault();
            model.IsActive = 0;
            _newLandPaymentDetailRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Newlandpaymentdetail>> GetPagedPaymentdetail(NewLandPaymentDetailSearchDto model)
        {
            return await _newLandPaymentDetailRepository.GetPagedPaymentdetail(model);
        }

    }
}


