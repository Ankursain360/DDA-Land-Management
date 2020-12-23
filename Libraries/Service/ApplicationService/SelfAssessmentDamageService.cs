using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ApplicationService
{
    public class SelfAssessmentDamageService : EntityService<Damagepayeeregistertemp>, ISelfAssessmentDamageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISelfAssessmentDamageRepository _selfAssessmentDamageRepository;

        public SelfAssessmentDamageService(IUnitOfWork unitOfWork, ISelfAssessmentDamageRepository selfAssessmentDamageRepository)
        : base(unitOfWork, selfAssessmentDamageRepository)
        {
            _unitOfWork = unitOfWork;
            _selfAssessmentDamageRepository = selfAssessmentDamageRepository;
        }

        public async Task<List<Locality>> GetLocalityList()
        {
            List<Locality> localityList = await _selfAssessmentDamageRepository.GetLocalityList();
            return localityList;
        }
        public async Task<List<District>> GetDistrictList()
        {
            List<District> districtList = await _selfAssessmentDamageRepository.GetDistrictList();
            return districtList;
        }
        public async Task<List<Damagepayeeregistertemp>> GetAllDamagepayeeregisterTemp()
        {
            return await _selfAssessmentDamageRepository.GetAllDamagepayeeregisterTemp();
        }



        //public async Task<List<Damagepayeeregistertemp>> GetDamagepayeeregisterUsingRepo()
        //{
        //    return await _selfAssessmentDamageRepository.GetAllDamagepayeeregisterTemp();
        //}

        public async Task<Damagepayeeregistertemp> FetchSingleResult(int id)
        {
            var result = await _selfAssessmentDamageRepository.FindBy(a => a.Id == id);
            Damagepayeeregistertemp model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(Damagepayeeregistertemp damagepayeeregistertemp)
        {
            var result = await _selfAssessmentDamageRepository.FindBy(a => a.Id == damagepayeeregistertemp.Id);
            Damagepayeeregistertemp model = result.FirstOrDefault();
            model.FileNo = damagepayeeregistertemp.FileNo;
            model.TypeOfDamageAssessee = damagepayeeregistertemp.TypeOfDamageAssessee;
            model.PropertyNo = damagepayeeregistertemp.PropertyNo;
            model.LocalityId = damagepayeeregistertemp.LocalityId;
            model.FloorNo = damagepayeeregistertemp.FloorNo;
            model.StreetNo = damagepayeeregistertemp.StreetNo;
            model.PinCode = damagepayeeregistertemp.PinCode;
            model.DistrictId = damagepayeeregistertemp.DistrictId;
            model.PlotAreaSqYard = damagepayeeregistertemp.PlotAreaSqYard;
            model.PlotAreaSqMt = damagepayeeregistertemp.PlotAreaSqMt;
            model.FloorAreaSqYard = damagepayeeregistertemp.FloorAreaSqYard;
            model.FloorAreaSqMt = damagepayeeregistertemp.FloorAreaSqMt;
            model.PropertyPhotoPath = damagepayeeregistertemp.PropertyPhotoPath;
            model.UseOfProperty = damagepayeeregistertemp.UseOfProperty;
            model.ResidentialSqYard = damagepayeeregistertemp.ResidentialSqYard;
            model.ResidentialSqMt = damagepayeeregistertemp.ResidentialSqMt;
            model.CommercialSqYard = damagepayeeregistertemp.CommercialSqYard;
            model.CommercialSqMt = damagepayeeregistertemp.CommercialSqMt;
            model.LitigationStatus = damagepayeeregistertemp.LitigationStatus;
            model.CourtName = damagepayeeregistertemp.CourtName;
            model.CaseNo = damagepayeeregistertemp.CaseNo;
            model.OppositionName = damagepayeeregistertemp.OppositionName;
            model.PetitionerRespondent = damagepayeeregistertemp.PetitionerRespondent;
            model.IsDdadamagePayee = damagepayeeregistertemp.IsDdadamagePayee;
            model.IsApplyForMutation = damagepayeeregistertemp.IsApplyForMutation;
            model.ShowCauseNoticePath = damagepayeeregistertemp.ShowCauseNoticePath;
            model.FgformPath = damagepayeeregistertemp.FgformPath;
            model.IsDocumentFor = damagepayeeregistertemp.IsDocumentFor;
            model.DocumentForFilePath = damagepayeeregistertemp.DocumentForFilePath;
            model.InterestDueAmountCompund = damagepayeeregistertemp.InterestDueAmountCompund;
            model.TotalValueWithInterest = damagepayeeregistertemp.TotalValueWithInterest;
            model.Rebate = damagepayeeregistertemp.Rebate;
            model.TotalPayable = damagepayeeregistertemp.TotalPayable;
            model.CalculatorValue = damagepayeeregistertemp.CalculatorValue;
            model.Declaration1 = damagepayeeregistertemp.Declaration1;
            model.Declaration2 = damagepayeeregistertemp.Declaration2;
            model.Declaration3 = damagepayeeregistertemp.Declaration3;
            model.Otp = damagepayeeregistertemp.Otp;
            model.ProceedToPay = damagepayeeregistertemp.ProceedToPay;
            model.Signature = damagepayeeregistertemp.Signature;
            model.Achknowledgement = damagepayeeregistertemp.Achknowledgement;
            model.IsActive = 1;
            model.UserId = damagepayeeregistertemp.UserId;

            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = damagepayeeregistertemp.ModifiedBy;
            _selfAssessmentDamageRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Damagepayeeregistertemp damagepayeeregistertemp)
        {
            damagepayeeregistertemp.CreatedBy = 1;
            damagepayeeregistertemp.CreatedDate = DateTime.Now;
            _selfAssessmentDamageRepository.Add(damagepayeeregistertemp);
            return await _unitOfWork.CommitAsync() > 0;
        }



        public async Task<bool> Delete(int id)
        {
            var form = await _selfAssessmentDamageRepository.FindBy(a => a.Id == id);
            Damagepayeeregistertemp model = form.FirstOrDefault();
            model.IsActive = 0;
            _selfAssessmentDamageRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Damagepayeeregister>> GetPagedDamagepayeeregister(DamagepayeeregisterSearchDto model)
        {
            return await _selfAssessmentDamageRepository.GetPagedDamagepayeeregister(model);
        }

        //********* rpt 1 Persolnal info of damage assesse ***********
        public async Task<bool> SavePayeePersonalInfoTemp(Damagepayeepersonelinfotemp damagepayeepersonelinfotemp)
        {
            damagepayeepersonelinfotemp.CreatedBy = 1;
            damagepayeepersonelinfotemp.CreatedDate = DateTime.Now;
            damagepayeepersonelinfotemp.IsActive = 1;
            return await _selfAssessmentDamageRepository.SavePayeePersonalInfoTemp(damagepayeepersonelinfotemp);
        }

        public async Task<List<Damagepayeepersonelinfotemp>> GetPersonalInfoTemp(int id)
        {
            return await _selfAssessmentDamageRepository.GetPersonalInfoTemp(id);
        }
        public async Task<bool> DeletePayeePersonalInfoTemp(int Id)
        {
            return await _selfAssessmentDamageRepository.DeletePayeePersonalInfoTemp(Id);
        }


        //********* rpt 2 Allotte Type **********

        public async Task<bool> SaveAllotteTypeTemp(List<Allottetypetemp> allottetypetemp)
        {
            allottetypetemp.ForEach(x => x.CreatedBy = 1);
            allottetypetemp.ForEach(x => x.CreatedDate = DateTime.Now);
            allottetypetemp.ForEach(x => x.IsActive = 1);
            return await _selfAssessmentDamageRepository.SaveAllotteTypeTemp(allottetypetemp);
        }
        public async Task<List<Allottetypetemp>> GetAllottetypeTemp(int id)
        {
            return await _selfAssessmentDamageRepository.GetAllottetypeTemp(id);
        }
        public async Task<bool> DeleteAllotteTypeTemp(int Id)
        {
            return await _selfAssessmentDamageRepository.DeleteAllotteTypeTemp(Id);
        }



        //********* rpt 3 Damage payment history ***********

        public async Task<bool> SavePaymentHistoryTemp(List<Damagepaymenthistorytemp> damagepaymenthistorytemp)
        {
            damagepaymenthistorytemp.ForEach(x => x.CreatedBy = 1);
            damagepaymenthistorytemp.ForEach(x => x.CreatedDate = DateTime.Now);
            damagepaymenthistorytemp.ForEach(x => x.IsActive = 1);
            return await _selfAssessmentDamageRepository.SavePaymentHistoryTemp(damagepaymenthistorytemp);
        }
        public async Task<List<Damagepaymenthistorytemp>> GetPaymentHistoryTemp(int id)
        {
            return await _selfAssessmentDamageRepository.GetPaymentHistoryTemp(id);
        }
        public async Task<bool> DeletePaymentHistoryTemp(int Id)
        {
            return await _selfAssessmentDamageRepository.DeletePaymentHistoryTemp(Id);
        }

        public async Task<Damagepayeeregistertemp> FetchSelfAssessmentUserId(int userId)
        {
            return await _selfAssessmentDamageRepository.FetchSelfAssessmentUserId(userId);
        }

        public async Task<Rebate> GetRebateValue()
        {
            return await _selfAssessmentDamageRepository.GetRebateValue();
        }

    }
}
