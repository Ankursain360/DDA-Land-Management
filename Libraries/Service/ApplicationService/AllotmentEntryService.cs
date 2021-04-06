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


            model.PremiumAmount = allotmententry.PremiumAmount;
            model.PremiumRate = allotmententry.PremiumRate;
            model.DocumentCharge = allotmententry.DocumentCharge;
            model.GroundRate = allotmententry.GroundRate;
            model.NoOfYears = allotmententry.NoOfYears;
            model.LicenceFees = allotmententry.LicenceFees;
            model.AmountLicFee = allotmententry.AmountLicFee;
            model.AmountGroundRate = allotmententry.AmountGroundRate; 
            model.AllotmentDate = allotmententry.AllotmentDate;
            model.TotalArea = allotmententry.TotalArea;
            model.PhaseNo = allotmententry.PhaseNo;

            model.SectorNo = allotmententry.SectorNo;
            model.PlotNo = allotmententry.PlotNo;
            model.PocketNo = allotmententry.PocketNo;
           
            model.BuildingArea = allotmententry.BuildingArea;

            model.IsActive = allotmententry.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
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



    }
}

