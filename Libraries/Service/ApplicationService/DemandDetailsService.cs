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
   public  class DemandDetailsService : EntityService<Kycdemandpaymentdetails>, IDemandDetailsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDemandDetailsRepository _demandDetailsRepository;


        public DemandDetailsService(IUnitOfWork unitOfWork, IDemandDetailsRepository demandDetailsRepository)
: base(unitOfWork, demandDetailsRepository)
        {
            _unitOfWork = unitOfWork;
            _demandDetailsRepository = demandDetailsRepository;
        }



        public async Task<PagedResult<Kycform>> GetPagedDemandDetails(DemandDetailsSearchDto model,string MobileNo)
        {
            return await _demandDetailsRepository.GetPagedDemandDetails(model,MobileNo);
        }
        public async Task<PagedResult<Kycform>> GetDemandPaymentDetails(DemandDetailsSearchDto model, string MobileNo)
        {
            return await _demandDetailsRepository.GetDemandPaymentDetails(model, MobileNo);
        }
        

        public async Task<List<DemandPaymentDetailsDto>> GetPaymentDetails(int Id)
        {
            return await _demandDetailsRepository.GetPaymentDetails(Id);
        }

        public async Task<List<LeasePaymentDemandLetterDetailsSearchDto>> GetPaymentDemandLetter(int Id)
        {
            return await _demandDetailsRepository.GetPaymentDemandLetter(Id);
        }


        public async Task<bool> Create(Kycdemandpaymentdetails kycdemandpaymentdetails)
        {

         
            _demandDetailsRepository.Add(kycdemandpaymentdetails);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> UpdateBeforeApproval(int id, Kycdemandpaymentdetails kycdemandpaymentdetails)
        {
            var result = await _demandDetailsRepository.FindBy(a => a.Id == id);
            Kycdemandpaymentdetails model = result.FirstOrDefault();

            model.ApprovedStatus = kycdemandpaymentdetails.ApprovedStatus;
            model.PendingAt = kycdemandpaymentdetails.PendingAt;
            _demandDetailsRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }




    }
}
