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
    public class DamagepayeeregisterService : EntityService<Damagepayeeregister>, IDamagepayeeregisterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDamagepayeeregisterRepository _damagepayeeregisterRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public DamagepayeeregisterService(IUnitOfWork unitOfWork, IDamagepayeeregisterRepository damagepayeeregisterRepository, UserManager<ApplicationUser> userManager)
        : base(unitOfWork, damagepayeeregisterRepository)
        {
            _unitOfWork = unitOfWork;
            _damagepayeeregisterRepository = damagepayeeregisterRepository;
            _userManager = userManager;
        }

        public async Task<List<Locality>> GetLocalityList()
        {
            List<Locality> localityList = await _damagepayeeregisterRepository.GetLocalityList();
            return localityList;
        }
        public async Task<List<District>> GetDistrictList()
        {
            List<District> districtList = await _damagepayeeregisterRepository.GetDistrictList();
            return districtList;
        }

        public async Task<Damagepayeeregister> GetPropertyPhotoPath(int Id)
        {
            return await _damagepayeeregisterRepository.GetPropertyPhotoPath(Id);
        }

        public async Task<List<Damagepayeeregister>> GetAllDamagepayeeregister()
        {
            return await _damagepayeeregisterRepository.GetAllDamagepayeeregister();
        }



        public async Task<Damagepayeeregister> FetchSingleResult(int id)
        {
            return await _damagepayeeregisterRepository.FetchSingleResult(id);
            //var result = await _damagepayeeregisterRepository.FindBy(a => a.Id == id);
            //Damagepayeeregistertemp model = result.FirstOrDefault();
            //return model;
        }

        public async Task<bool> Update(int id, Damagepayeeregister damagepayeeregister)
        {
            var result = await _damagepayeeregisterRepository.FindBy(a => a.Id == id);
            Damagepayeeregister model = result.FirstOrDefault();
            model.FileNo = damagepayeeregister.FileNo;
            model.TypeOfDamageAssessee = damagepayeeregister.TypeOfDamageAssessee;
            model.PropertyNo = damagepayeeregister.PropertyNo;
            model.LocalityId = damagepayeeregister.LocalityId;
            model.FloorNo = damagepayeeregister.FloorNo;
            model.StreetNo = damagepayeeregister.StreetNo;
            model.PinCode = damagepayeeregister.PinCode;
            model.DistrictId = damagepayeeregister.DistrictId;
            model.PlotAreaSqYard = damagepayeeregister.PlotAreaSqYard;
            model.FloorAreaSqYard = damagepayeeregister.FloorAreaSqYard;
            model.PlotAreaSqMt = damagepayeeregister.PlotAreaSqMt;
            model.FloorAreaSqMt = damagepayeeregister.FloorAreaSqMt;
            model.PropertyPhotoPath = damagepayeeregister.PropertyPhotoPath;
            model.UseOfProperty = damagepayeeregister.UseOfProperty;
            model.ResidentialSqYard = damagepayeeregister.ResidentialSqYard;
            model.ResidentialSqMt = damagepayeeregister.ResidentialSqMt;
            model.CommercialSqYard = damagepayeeregister.CommercialSqYard;
            model.CommercialSqMt = damagepayeeregister.CommercialSqMt;
            model.LitigationStatus = damagepayeeregister.LitigationStatus;
            model.CourtName = damagepayeeregister.CourtName;
            model.CaseNo = damagepayeeregister.CaseNo;

            model.OppositionName = damagepayeeregister.OppositionName;
            model.PetitionerRespondent = damagepayeeregister.PetitionerRespondent;
            model.IsDdadamagePayee = damagepayeeregister.IsDdadamagePayee;
            model.IsApplyForMutation = damagepayeeregister.IsApplyForMutation;
            model.ShowCauseNoticePath = damagepayeeregister.ShowCauseNoticePath;
            model.FgformPath = damagepayeeregister.FgformPath;
            model.IsDocumentFor = damagepayeeregister.IsDocumentFor;
            model.DocumentForFilePath = damagepayeeregister.DocumentForFilePath;

            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _damagepayeeregisterRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Damagepayeeregister damagepayeeregister)
        {
            damagepayeeregister.IsActive = 1;
            damagepayeeregister.CreatedBy = 1;
            damagepayeeregister.CreatedDate = DateTime.Now;
            _damagepayeeregisterRepository.Add(damagepayeeregister);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var form = await _damagepayeeregisterRepository.FindBy(a => a.Id == id);
            Damagepayeeregister model = form.FirstOrDefault();
            model.IsActive = 0;
            _damagepayeeregisterRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Damagepayeeregister>> GetPagedDamagepayeeregister(DamagepayeeregistertempSearchDto model)
        {
            return await _damagepayeeregisterRepository.GetPagedDamagepayeeregister(model);
        }

        //********* rpt 1 Persolnal info of damage assesse ***********
        public async Task<bool> SavePayeePersonalInfo(Damagepayeepersonelinfo damagepayeepersonelinfo)
        {
            damagepayeepersonelinfo.CreatedBy = 1;
            damagepayeepersonelinfo.CreatedDate = DateTime.Now;
            damagepayeepersonelinfo.IsActive = 1;
            return await _damagepayeeregisterRepository.SavePayeePersonalInfo(damagepayeepersonelinfo);
        }

        public async Task<List<Damagepayeepersonelinfo>> GetPersonalInfo(int id)
        {
            return await _damagepayeeregisterRepository.GetPersonalInfo(id);
        }
        public async Task<bool> DeletePayeePersonalInfo(int Id)
        {
            return await _damagepayeeregisterRepository.DeletePayeePersonalInfo(Id);
        }

        public async Task<Damagepayeepersonelinfo> GetPersonelInfoFilePath(int Id)
        {
            return await _damagepayeeregisterRepository.GetPersonelInfoFilePath(Id);
        }
        public async Task<Damagepayeepersonelinfo> GetAadharFilePath(int Id)
        {
            return await _damagepayeeregisterRepository.GetAadharFilePath(Id);
        }
        public async Task<Damagepayeepersonelinfo> GetPanFilePath(int Id)
        {
            return await _damagepayeeregisterRepository.GetPanFilePath(Id);
        }
        public async Task<Damagepayeepersonelinfo> GetPhotographPath(int Id)
        {
            return await _damagepayeeregisterRepository.GetPhotographPath(Id);
        }
        public async Task<Damagepayeepersonelinfo> GetSignaturePath(int Id)
        {
            return await _damagepayeeregisterRepository.GetSignaturePath(Id);
        }
        public async Task<List<Damagepayeepersonelinfo>> GetPreviousAssesseRepeater(int Id)
        {
            return await _damagepayeeregisterRepository.GetPreviousAssesseRepeater(Id);
        }

        //********* rpt 2 Allotte Type **********

        public async Task<bool> SaveAllotteType(List<Allottetype> allottetype)
        {
            allottetype.ForEach(x => x.CreatedBy = 1);
            allottetype.ForEach(x => x.CreatedDate = DateTime.Now);
            allottetype.ForEach(x => x.IsActive = 1);
            return await _damagepayeeregisterRepository.SaveAllotteType(allottetype);
        }
        public async Task<List<Allottetype>> GetAllottetype(int id)
        {
            return await _damagepayeeregisterRepository.GetAllottetype(id);
        }
        public async Task<bool> DeleteAllotteType(int Id)
        {
            return await _damagepayeeregisterRepository.DeleteAllotteType(Id);
        }
        public async Task<Allottetype> GetATSFilePath(int Id)
        {
            return await _damagepayeeregisterRepository.GetATSFilePath(Id);
        }
        public async Task<List<Allottetype>> GetNewAlloteeRepeater(int Id)
        {
            return await _damagepayeeregisterRepository.GetNewAlloteeRepeater(Id);
        }


        //********* rpt 3 Damage payment history ***********

        public async Task<bool> SavePaymentHistory(List<Damagepaymenthistory> damagepaymenthistory)
        {
            damagepaymenthistory.ForEach(x => x.CreatedBy = 1);
            damagepaymenthistory.ForEach(x => x.CreatedDate = DateTime.Now);
            damagepaymenthistory.ForEach(x => x.IsActive = 1);
            return await _damagepayeeregisterRepository.SavePaymentHistory(damagepaymenthistory);
        }
        public async Task<List<Damagepaymenthistory>> GetPaymentHistory(int id)
        {
            return await _damagepayeeregisterRepository.GetPaymentHistory(id);
        }
        public async Task<bool> DeletePaymentHistory(int Id)
        {
            return await _damagepayeeregisterRepository.DeletePaymentHistory(Id);
        }

        public async Task<Damagepaymenthistory> GetReceiptFilePath(int Id)
        {
            return await _damagepayeeregisterRepository.GetReceiptFilePath(Id);
        }

        public async Task<bool> UpdateBeforeApproval(int id, Damagepayeeregister damagepayeeregister)
        {
            var result = await _damagepayeeregisterRepository.FindBy(a => a.Id == id);
            Damagepayeeregister model = result.FirstOrDefault();
            model.ApprovedStatus = damagepayeeregister.ApprovedStatus;
            model.PendingAt = damagepayeeregister.PendingAt;
            model.ModifiedDate = DateTime.Now;
            _damagepayeeregisterRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> CreateApprovedDamagepayeeRegister(Damagepayeeregister damagepayeeregister, Damagepayeeregister model)
        {
            model.FileNo = damagepayeeregister.FileNo;
            model.TypeOfDamageAssessee = damagepayeeregister.TypeOfDamageAssessee;
            model.PropertyNo = damagepayeeregister.PropertyNo;
            model.LocalityId = damagepayeeregister.LocalityId;
            model.FloorNo = damagepayeeregister.FloorNo;
            model.StreetNo = damagepayeeregister.StreetNo;
            model.PinCode = damagepayeeregister.PinCode;
            model.DistrictId = damagepayeeregister.DistrictId;
            model.PlotAreaSqYard = damagepayeeregister.PlotAreaSqYard;
            model.PlotAreaSqMt = damagepayeeregister.PlotAreaSqMt;
            model.FloorAreaSqYard = damagepayeeregister.FloorAreaSqYard;
            model.FloorAreaSqMt = damagepayeeregister.FloorAreaSqMt;
            model.PropertyPhotoPath = damagepayeeregister.PropertyPhotoPath;
            model.UseOfProperty = damagepayeeregister.UseOfProperty;
            model.ResidentialSqYard = damagepayeeregister.ResidentialSqYard;
            model.ResidentialSqMt = damagepayeeregister.ResidentialSqMt;
            model.CommercialSqYard = damagepayeeregister.CommercialSqYard;
            model.CommercialSqMt = damagepayeeregister.CommercialSqMt;
            model.LitigationStatus = damagepayeeregister.LitigationStatus;
            model.CourtName = damagepayeeregister.CourtName;
            model.CaseNo = damagepayeeregister.CaseNo;
            model.OppositionName = damagepayeeregister.OppositionName;
            model.PetitionerRespondent = damagepayeeregister.PetitionerRespondent;
            model.IsDdadamagePayee = damagepayeeregister.IsDdadamagePayee;
            model.IsApplyForMutation = damagepayeeregister.IsApplyForMutation;
            model.ShowCauseNoticePath = damagepayeeregister.ShowCauseNoticePath;
            model.FgformPath = damagepayeeregister.FgformPath;
            model.IsDocumentFor = damagepayeeregister.IsDocumentFor;
            model.DocumentForFilePath = damagepayeeregister.DocumentForFilePath;
            model.InterestDueAmountCompund = damagepayeeregister.InterestDueAmountCompund;
            model.TotalValueWithInterest = damagepayeeregister.TotalValueWithInterest;
            model.Rebate = damagepayeeregister.Rebate;
            model.TotalPayable = damagepayeeregister.TotalPayable;
            model.CalculatorValue = damagepayeeregister.CalculatorValue;
            model.Declaration1 = damagepayeeregister.Declaration1;
            model.Declaration2 = damagepayeeregister.Declaration2;
            model.Declaration3 = damagepayeeregister.Declaration3;
            model.Otp = damagepayeeregister.Otp;
            model.ProceedToPay = damagepayeeregister.ProceedToPay;
            model.Signature = damagepayeeregister.Signature;
            model.Achknowledgement = damagepayeeregister.Achknowledgement;
            model.IsActive = 1;
            model.UserId = damagepayeeregister.UserId;
            model.CreatedDate = DateTime.Now;
            model.CreatedBy = damagepayeeregister.CreatedBy;
            return await _damagepayeeregisterRepository.CreateApprovedDamagepayeeRegister(model);
        }

        public async Task<bool> SavePersonelInfo(List<Damagepayeepersonelinfo> data)
        {
            data.ForEach(x => x.CreatedDate = DateTime.Now);
            data.ForEach(x => x.IsActive = 1);
            return await _damagepayeeregisterRepository.SavePersonelInfo(data);
        }

      

        // ************* to create user **************

        public async Task<string>CreateUser(Damagepayeeregister model)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Name = model.payeeName[0],
                UserName = model.payeeName[0],
                Email = model.EmailId[0],
                PhoneNumber = model.MobileNo[0],
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
    }
}