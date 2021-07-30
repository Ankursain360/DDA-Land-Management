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
   public class KycdemandpaymentdetailstableaService : EntityService<Kycdemandpaymentdetailstablea>,IKycdemandpaymentdetailstableaService 
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IKycdemandpaymentdetailstableaRespository _kycdemandpaymentdetailstableaRespository;
     public KycdemandpaymentdetailstableaService(IUnitOfWork unitOfWork, IKycdemandpaymentdetailstableaRespository kycdemandpaymentdetailstableaRespository)
    : base(unitOfWork, kycdemandpaymentdetailstableaRespository)
        {
            _unitOfWork = unitOfWork;
            _kycdemandpaymentdetailstableaRespository = kycdemandpaymentdetailstableaRespository;
        }

        public async Task<bool> SaveDemandPaymentDetails(Kycdemandpaymentdetailstablea kycdemandpaymentdetailstablea)
        {
            _kycdemandpaymentdetailstableaRespository.Add(kycdemandpaymentdetailstablea);
            return await _unitOfWork.CommitAsync() > 0;
        }


    }
}
