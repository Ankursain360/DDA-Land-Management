using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ApplicationService
{
     public class KycformService : EntityService<Kycform>, IKycformService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IKycformRepository _kycformRepository;
        
        public KycformService(IUnitOfWork unitOfWork, IKycformRepository kycformRepository)
        : base(unitOfWork, kycformRepository)
        {
            _unitOfWork = unitOfWork;
            _kycformRepository = kycformRepository;
            
        }

        public async Task<List<Leasetype>> GetAllLeasetypeList()
        {
            List<Leasetype> List = await _kycformRepository.GetAllLeasetypeList();
            return List;
        }
        public async Task<List<Branch>> GetAllBranchList()
        {
            List<Branch> List = await _kycformRepository.GetAllBranchList();
            return List;
        }
        public async Task<List<PropertyType>> GetAllPropertyTypeList()
        {
            List<PropertyType> List = await _kycformRepository.GetAllPropertyTypeList();
            return List;
        }
        public async Task<List<Zone>> GetAllZoneList()
        {
            List<Zone> List = await _kycformRepository.GetAllZoneList();
            return List;
        }
        public async Task<List<Locality>> GetLocalityList()
        {
            List<Locality> List = await _kycformRepository.GetLocalityList();
            return List;
        }
        public async Task<bool> Create(Kycform kyc)
        {
            kyc.IsActive = 1;
            kyc.CreatedDate = DateTime.Now;
            _kycformRepository.Add(kyc);
            return await _unitOfWork.CommitAsync() > 0;
        }
        //********* rpt ! Kycleasepaymentrpt Details **********

        
       
        public async Task<bool> Saveleasepayment(Kycleasepaymentrpt payment)
        {
            payment.CreatedBy = payment.CreatedBy;
            payment.CreatedDate = DateTime.Now;
            payment.IsActive = 1;
            return await _kycformRepository.Saveleasepayment(payment);
        }
        //********* rpt ! Kyclicensepaymentrpt Details **********
        public async Task<bool> Savelicensepayment(Kyclicensepaymentrpt payment)
        {
            payment.CreatedBy = payment.CreatedBy;
            payment.CreatedDate = DateTime.Now;
            payment.IsActive = 1;
            return await _kycformRepository.Savelicensepayment(payment);
        }
    }
}
