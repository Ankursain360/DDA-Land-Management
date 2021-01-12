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

        public async Task<List<Paymentverification>> GetAllPaymentList()
        {
            return await _paymentverificationRepository.GetAllPaymentList();
        }
        public async Task<PagedResult<Paymentverification>> GetPagedPaymentList(PaymentverificationSearchDto model)
        {
            return await _paymentverificationRepository.GetPagedPaymentList(model);
        }
        public async Task<PagedResult<Paymentverification>> GetPagedPaymentList2(PaymentverificationSearchDto model)
        {
            return await _paymentverificationRepository.GetPagedPaymentList2(model);
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
    }
}
