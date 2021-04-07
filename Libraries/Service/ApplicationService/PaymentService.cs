using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Libraries.Repository.IEntityRepository;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using Dto.Search;
using Dto.Master;
using AutoMapper;

namespace Libraries.Service.ApplicationService
{

    public class PaymentService : EntityService<Payment>, IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        public PaymentService(IUnitOfWork unitOfWork,
            IPaymentRepository paymentRepository,
            IMapper mapper)
        : base(unitOfWork, paymentRepository)
        {
            _unitOfWork = unitOfWork;
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        public async Task<Possesionplan> GetAllotteeDetails(int userId)
        {
            return await _paymentRepository.GetAllotteeDetails( userId);
        }

        public async Task<List<PaymentPremiumListDataDto>> GetGroundRentDrDetails(int userId)
        {
            return await _paymentRepository.GetGroundRentDrDetails(userId);
        }
        public async Task<List<PaymentPremiumListDataDto>> GetPremiumDrDetails(int AllotmentId, int LeasePaymentTypeId, int userId)
        {
            return await _paymentRepository.GetPremiumDrDetails( AllotmentId,  LeasePaymentTypeId,  userId);
        }
    }
}

