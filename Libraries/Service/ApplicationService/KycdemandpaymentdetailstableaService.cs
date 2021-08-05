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
        public async Task<Kycdemandpaymentdetailstablea> FetchSingleResult(int id)
        {
            var result = await _kycdemandpaymentdetailstableaRespository.FindBy(a => a.Id == id);
            Kycdemandpaymentdetailstablea model = result.FirstOrDefault();
            return model;
        }
        public async Task<List<Kycdemandpaymentdetailstablea>> FetchResultOnDemandId(int id)
        {
            var result = await _kycdemandpaymentdetailstableaRespository.FetchResult(id);
            
            return result;
        }
        public async Task<bool> Update(int id, Kycdemandpaymentdetailstablea payment)
        {
           // var result = await _kycdemandpaymentdetailstableaRespository.FetchSingleResult(id);
            Kycdemandpaymentdetailstablea model = new Kycdemandpaymentdetailstablea();
            model.DemandPeriod = payment.DemandPeriod;
            model.GroundRent = payment.GroundRent;
            model.InterestRate = payment.InterestRate;
            model.TotdalDues = payment.TotdalDues;
            model.IsActive = 1;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _kycdemandpaymentdetailstableaRespository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

    }
}
