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
    public  class KycdemandpaymentdetailstablebService : EntityService<Kycdemandpaymentdetailstableb>, IKycdemandpaymentdetailstablebService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IKycdemandpaymentdetailstablebRepository _kycdemandpaymentdetailstablebRespository;
        public KycdemandpaymentdetailstablebService(IUnitOfWork unitOfWork, IKycdemandpaymentdetailstablebRepository kycdemandpaymentdetailstablebRespository)
    : base(unitOfWork,kycdemandpaymentdetailstablebRespository)
        {
            _unitOfWork = unitOfWork;
            _kycdemandpaymentdetailstablebRespository = kycdemandpaymentdetailstablebRespository;
        }

        public async Task<bool> SaveDemandPaymentAPIDetails(Kycdemandpaymentdetailstableb kycdemandpaymentdetailstableb)
        {
            _kycdemandpaymentdetailstablebRespository.Add(kycdemandpaymentdetailstableb);
            return await _unitOfWork.CommitAsync() > 0;
        }

    }
}
