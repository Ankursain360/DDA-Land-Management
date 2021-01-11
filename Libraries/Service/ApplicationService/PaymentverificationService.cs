using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
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


    }
}
