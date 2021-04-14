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
using Microsoft.AspNetCore.Identity;
namespace Libraries.Service.ApplicationService
{
    public class AllotmentEntryService : EntityService<Allotmententry>, IAllotmentEntryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAllotmentEntryRepository _allotmentEntryRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public AllotmentEntryService(IUnitOfWork unitOfWork, IAllotmentEntryRepository allotmentEntryRepository, UserManager<ApplicationUser> userManager)
      : base(unitOfWork, allotmentEntryRepository)
        {
            _unitOfWork = unitOfWork;
            _allotmentEntryRepository = allotmentEntryRepository;
            _userManager = userManager;
        }





        public async Task<bool> Delete(int id)
        {
            var form = await _allotmentEntryRepository.FindBy(a => a.Id == id);
            Allotmententry model = form.FirstOrDefault();
            model.IsActive = 0;
            _allotmentEntryRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Allotmententry> FetchSingleResult(int id)
        {
            var result = await _allotmentEntryRepository.FindBy(a => a.Id == id);
            Allotmententry model = result.FirstOrDefault();
            return model;
        }



        public async Task<List<Allotmententry>> GetAllAllotmententry()
        {

            return await _allotmentEntryRepository.GetAllAllotmententry();
        }



       


        public async Task<List<Leaseapplication>> GetAllLeaseapplication()
        {
            List<Leaseapplication> leaseappList = await _allotmentEntryRepository.GetAllLeaseapplication();
            return leaseappList;
        }
        public async Task<List<Leasetype>> GetAllLeasetype()
        {
            List<Leasetype> leaseTypeList = await _allotmentEntryRepository.GetAllLeasetype();
            return leaseTypeList;
        }
        
        public async Task<List<Leasepurpose>> GetAllLeasepurpose()
        {
            List<Leasepurpose> leasePurposeList = await _allotmentEntryRepository.GetAllLeasepurpose();
            return leasePurposeList;
        }

        public async Task<List<Leasesubpurpose>> GetAllLeaseSubpurpose(int purposeUseId)
        {
            List<Leasesubpurpose> leaseSubPurposeList = await _allotmentEntryRepository.GetAllLeaseSubpurpose(purposeUseId);
            return leaseSubPurposeList;
        }


        public async Task<List<Allotmententry>> GetAllotmententryUsingRepo()
        {
            return await _allotmentEntryRepository.GetAllAllotmententry();
        }
        public async Task<PagedResult<Allotmententry>> GetPagedAllotmententry(AllotmentEntrySearchDto model)
        {
            return await _allotmentEntryRepository.GetPagedAllotmententry(model);
        }

        public async Task<bool> Update(int id, Allotmententry allotmententry)
        {
            var result = await _allotmentEntryRepository.FindBy(a => a.Id == id);
            Allotmententry model = result.FirstOrDefault();

            model.ApplicationId = allotmententry.ApplicationId;
          
            model.Name = allotmententry.Name;
            model.Address = allotmententry.Address;
            model.ContactNo = allotmententry.ContactNo;
            model.LandAreaSqMt = allotmententry.LandAreaSqMt;

            model.TotalArea = allotmententry.TotalArea;
            model.LeasesTypeId = allotmententry.LeasesTypeId;
            model.BuildingArea = allotmententry.BuildingArea;
            model.PlayGroundArea = allotmententry.PlayGroundArea;
            model.PhaseNo = allotmententry.PhaseNo;
            model.SectorNo = allotmententry.SectorNo;
            model.PocketNo = allotmententry.PocketNo;
            model.PlotNo = allotmententry.PlotNo;

            model.LeasePurposesTypeId = allotmententry.LeasePurposesTypeId;
            model.LeaseSubPurposeId = allotmententry.LeaseSubPurposeId;
            model.AllotmentDate = allotmententry.AllotmentDate;
            model.PremiumRate = allotmententry.PremiumRate;
            model.PremiumAmount = allotmententry.PremiumAmount;
            model.GroundRate = allotmententry.GroundRate;
            model.AmountGroundRate = allotmententry.AmountGroundRate;
            model.NoOfYears = allotmententry.NoOfYears;
            model.LicenceFees = allotmententry.LicenceFees;
            model.AmountLicFee = allotmententry.AmountLicFee;
            model.DocumentCharge = allotmententry.DocumentCharge;
            model.TotalAmount = allotmententry.TotalAmount;
            model.Remarks = allotmententry.Remarks;
            model.IsActive = allotmententry.IsActive;


            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = allotmententry.ModifiedBy;
            _allotmentEntryRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;

        }

        public async Task<bool> Create(Allotmententry allotmententry)
        {
            allotmententry.CreatedBy = allotmententry.CreatedBy;
            allotmententry.CreatedDate = DateTime.Now;
            _allotmentEntryRepository.Add(allotmententry);
            return await _unitOfWork.CommitAsync() > 0;
        }


        public async Task<Leaseapplication> FetchSingleLeaseapplicationResult(int? applicationId)
        {
            return await _allotmentEntryRepository.FetchSingleLeaseapplicationResult(applicationId);
        }

       
        public async Task<Documentcharges> FetchSingledocumentResult(int? leasePurposeId, int? leaseSubPurposeId, string allotmentDate)
        {
            return await _allotmentEntryRepository.FetchSingledocumentResult(leasePurposeId, leaseSubPurposeId, allotmentDate);
        }
        public async Task<Premiumrate> FetchSinglerateResult(int? leasePurposeId, int? leaseSubPurposeId, string allotmentDate)
        {
            return await _allotmentEntryRepository.FetchSinglerateResult(leasePurposeId, leaseSubPurposeId, allotmentDate);
        }
        public async Task<Groundrent> FetchSinglegroundrentResult(int? leasePurposeId, int? leaseSubPurposeId, string allotmentDate)
        {
            return await _allotmentEntryRepository.FetchSinglegroundrentResult(leasePurposeId, leaseSubPurposeId, allotmentDate);
        }
        public async Task<Licencefees> FetchSinglefeeResult(int? leasePurposeId, int? leaseSubPurposeId, string allotmentDate)
        {
            return await _allotmentEntryRepository.FetchSinglefeeResult(leasePurposeId, leaseSubPurposeId, allotmentDate);
        }
        public async Task<Leaseapplication> FetchLeaseApplicationmailDetails(int id)
        {
            return await _allotmentEntryRepository.FetchLeaseApplicationmailDetails(id);
        }

        // ************* to create user **************

        public async Task<string> CreateUser(Allotmententry model, string username)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = username,
                Email ="email",
                Name = username,
                PasswordSetDate = DateTime.Now.AddDays(30),
                CreatedBy = 1,
                CreatedDate = DateTime.Now,
                IsDefaultPassword = 1
            };
            string password = "Pass123$";
           

            var userSavedResult = await _userManager.CreateAsync(user, password);
            //  var userSavedResult = await _userManager.CreateAsync(user, userDto.Password);
            if (userSavedResult.Succeeded)
            {
                return password;
            }
            return "False";

        }



        public async Task<List<DemandletterdatalistDto>> Getdemandletteralldata(DemandletterDateSearchDto model)
        {
            return await _allotmentEntryRepository.Getdemandletteralldata(model);
        }

        public async Task<bool> CreatePaymentPremiumDr(Allotmententry allotmententry,int LeasePaymentTypeId, int userId)
        {
            Payment model = new Payment();
            model.AllotmentId = allotmententry.Id;
            model.LeasePaymentTypeId = LeasePaymentTypeId;
            model.FromDate = DateTime.Now;
            model.ToDate = DateTime.Now.AddMonths(2);
            model.TransactionType = "Dr";
            model.Amount = (decimal)allotmententry.PremiumAmount;
            model.IsActive = 1;
            model.CreatedBy = userId;
            model.CreatedDate = DateTime.Now;
            return await _allotmentEntryRepository.CreatePaymentPremiumDr(model);
        }

        public async Task<bool> CreatePaymentGroundRentDr(Allotmententry allotmententry, int LeasePaymentTypeId, int userId)
        {
            Payment model = new Payment();
            model.AllotmentId = allotmententry.Id;
            model.LeasePaymentTypeId = LeasePaymentTypeId;
            model.FromDate = DateTime.Now;
            model.ToDate = DateTime.Now.AddMonths(2);
            model.TransactionType = "Dr";
            model.Amount = (decimal)allotmententry.AmountGroundRate;
            model.IsActive = 1;
            model.CreatedBy = userId;
            model.CreatedDate = DateTime.Now;
            return await _allotmentEntryRepository.CreatePaymentPremiumDr(model);
        }

        public async Task<bool> CreatePaymentDocumentChargesDr(Allotmententry allotmententry, int LeasePaymentTypeId, int userId)
        {
            Payment model = new Payment();
            model.AllotmentId = allotmententry.Id;
            model.LeasePaymentTypeId = LeasePaymentTypeId;
            model.FromDate = DateTime.Now;
            model.ToDate = DateTime.Now.AddMonths(2);
            model.TransactionType = "Dr";
            model.Amount = (decimal)allotmententry.DocumentCharge;
            model.IsActive = 1;
            model.CreatedBy = userId;
            model.CreatedDate = DateTime.Now;
            return await _allotmentEntryRepository.CreatePaymentPremiumDr(model);
        }

        public async Task<bool> CreatePaymentLicenceFeesDr(Allotmententry allotmententry, int LeasePaymentTypeId, int userId)
        {
            Payment model = new Payment();
            model.AllotmentId = allotmententry.Id;
            model.LeasePaymentTypeId = LeasePaymentTypeId;
            model.FromDate = DateTime.Now;
            model.ToDate = DateTime.Now.AddMonths(2);
            model.TransactionType = "Dr";
            model.Amount = (decimal)allotmententry.LicenceFees;
            model.IsActive = 1;
            model.CreatedBy = userId;
            model.CreatedDate = DateTime.Now;
            return await _allotmentEntryRepository.CreatePaymentPremiumDr(model);
        }


        public async Task<List<PayemntDescriptionListDto>> GetPagedPaymentReport(PaymentdetailssearchDto model)
        {
            return await _allotmentEntryRepository.GetPagedPaymentReport(model);

        }


    }
}

