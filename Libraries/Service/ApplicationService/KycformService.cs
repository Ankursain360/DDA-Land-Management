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
        public async Task<List<Locality>> GetLocalityList(int? zoneid)
        {
            List<Locality> List = await _kycformRepository.GetLocalityList(zoneid);
            return List;
        }
        public async Task<bool> Create(Kycform kyc)
        {
            kyc.IsActive = 1;
            kyc.CreatedDate = DateTime.Now;
            _kycformRepository.Add(kyc);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<List<Kycform>> GetAllKycform()
        {
            return await _kycformRepository.GetAllKycform();
        }

        public async Task<Kycform> FetchSingleResult(int id)
        {
            var result = await _kycformRepository.FindBy(a => a.Id == id);
            Kycform model = result.FirstOrDefault();
            return model;
        }

        public async Task<Kycform> FetchKYCSingleResult(int id)
        {
            var result = await _kycformRepository.FetchKYCSingleResult(id);
            //Kycform model = result.FirstOrDefault();
            return result;
        }

        public async Task<bool> Update(int id, Kycform kyc)
        {
            var result = await _kycformRepository.FindBy(a => a.Id == id);
            Kycform model = result.FirstOrDefault();
            model.Property = kyc.Property;
            model.PropertyTypeId = kyc.PropertyTypeId;
            model.FileNo = kyc.FileNo;
            model.BranchId = kyc.BranchId;
            model.LeaseTypeId = kyc.LeaseTypeId;
            model.LeaseGroundRentDepositFrequency = kyc.LeaseGroundRentDepositFrequency;
            model.LicenseFrequency = kyc.LicenseFrequency;
            model.TenureFrom = kyc.TenureFrom;
            model.TenureTo = kyc.TenureTo;
            model.LicenseFrom = kyc.LicenseFrom;
            model.LicenseTo = kyc.LicenseTo;
            model.PlotNo = kyc.PlotNo;
            model.PlotDescription = kyc.PlotDescription;
            model.ZoneId = kyc.ZoneId;
            model.LocalityId = kyc.LocalityId;
            model.Phase = kyc.Phase;
            model.Sector = kyc.Sector;
            model.Block = kyc.Block;
            model.Pocket = kyc.Pocket;
            model.Name = kyc.Name;
            model.FatherName = kyc.FatherName;
            model.Gender = kyc.Gender;
            model.EmailId = kyc.EmailId;
            model.MobileNo = kyc.MobileNo;
            model.Address = kyc.Address;
            model.AadhaarNo = kyc.AadhaarNo;
            model.Relationship = kyc.Relationship;
            model.AllotteeApplicantDetailsSame = kyc.AllotteeApplicantDetailsSame;
            model.AllotteeLicenseeName = kyc.AllotteeLicenseeName;
            model.AllotteeLicenseeAddress = kyc.AllotteeLicenseeAddress;
            model.AllotteeLicenseeMobileNo = kyc.AllotteeLicenseeMobileNo;
            model.AllotteeLicenseeEmailId = kyc.AllotteeLicenseeEmailId;
            model.Area = kyc.Area;
            model.AreaUnit = kyc.AreaUnit;
            model.AllotmentLetterDate = kyc.AllotmentLetterDate;
            model.PossessionDate = kyc.PossessionDate;
            model.LeaseLicenseExecutionDate = kyc.LeaseLicenseExecutionDate;
            model.LandPremiumAmount = kyc.LandPremiumAmount;
            model.GroundRentAmount = kyc.GroundRentAmount;
            model.LicenseFeePayable = kyc.LicenseFeePayable;
            model.AadhaarNoPath = kyc.AadhaarNoPath;
            model.LetterPath = kyc.LetterPath;
            model.AadhaarPanapplicantPath = kyc.AadhaarPanapplicantPath;

            model.IsActive = kyc.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = kyc.ModifiedBy;
            _kycformRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Kycform>> GetPagedKycform(KycformSearchDto model)
        {

            return await _kycformRepository.GetPagedKycform(model);
           
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _kycformRepository.FindBy(a => a.Id == id);
            Kycform model = form.FirstOrDefault();
            model.IsActive = 0;
            _kycformRepository.Edit(model);
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

        //KYC Approval process methods : Added by ishu 20/7/2021


        public async Task<Kycworkflowtemplate> FetchSingleResultOnProcessGuid(string processguid)
        {
            return await _kycformRepository.FetchSingleResultOnProcessGuid(processguid);
        }
       
        public async Task<List<Kycworkflowtemplate>> GetWorkFlowDataOnGuid(string processguid)
        {
            return await _kycformRepository.GetWorkFlowDataOnGuid(processguid);
        }

        public async Task<bool> UpdateBeforeApproval(int id, Kycform kyc)
        {
            var result = await _kycformRepository.FindBy(a => a.Id == id);
            Kycform model = result.FirstOrDefault();

            model.ApprovedStatus = kyc.ApprovedStatus;
            model.PendingAt = kyc.PendingAt;
            _kycformRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<bool> CreatekycApproval(Kycapprovalproccess kycapproval, int userId)
        {

            kycapproval.CreatedBy = userId;
            kycapproval.CreatedDate = DateTime.Now;
            return  await _kycformRepository.CreatekycApproval(kycapproval);
           //await _unitOfWork.CommitAsync() > 0;
        }
    }
}
