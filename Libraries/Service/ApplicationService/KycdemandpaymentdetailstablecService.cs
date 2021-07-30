using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Microsoft.EntityFrameworkCore;
using Libraries.Repository.IEntityRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Dto.Master;

namespace Libraries.Service.ApplicationService
{
   public class KycdemandpaymentdetailstablecService : EntityService<Kycdemandpaymentdetailstablec>, IKycdemandpaymentdetailstablecService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IKycdemandpaymentdetailstablecRepository _kycdemandpaymentdetailstablecRepository;

        public KycdemandpaymentdetailstablecService(IUnitOfWork unitOfWork, IKycdemandpaymentdetailstablecRepository kycdemandpaymentdetailstablecRepository)
          : base(unitOfWork, kycdemandpaymentdetailstablecRepository)
        {
            _unitOfWork = unitOfWork;
            _kycdemandpaymentdetailstablecRepository = kycdemandpaymentdetailstablecRepository;
        }

        public async Task<bool> SaveKycChallanDetails(Kycdemandpaymentdetailstablec kycdemandpaymentdetailstablec)
        {
            _kycdemandpaymentdetailstablecRepository.Add(kycdemandpaymentdetailstablec);
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}
