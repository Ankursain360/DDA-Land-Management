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
    public class DmsFileUploadService : EntityService<Dmsfileupload>, IDmsFileUploadService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDmsFileUploadRepository _dmsFileUploadRepository;

        public DmsFileUploadService(IUnitOfWork unitOfWork, IDmsFileUploadRepository dmsFileUploadRepository)
        : base(unitOfWork, dmsFileUploadRepository)
        {
            _unitOfWork = unitOfWork;
            _dmsFileUploadRepository = dmsFileUploadRepository;
        }

        public async Task<List<Department>> GetDepartmentList()
        {
            return await _dmsFileUploadRepository.GetDepartmentList();
        }

        public async Task<List<Propertyregistration>> GetKhasraNoList()
        {
            return await _dmsFileUploadRepository.GetKhasraNoList();
        }

        public async Task<List<Locality>> GetLocalityList()
        {
            return await _dmsFileUploadRepository.GetLocalityList();
        }

        public async Task<PagedResult<Dmsfileupload>> GetPagedDMSFileUploadList(DMSFileUploadSearchDto model)
        {
            return await _dmsFileUploadRepository.GetPagedDMSFileUploadList(model);
        }

        public async Task<bool> Create(Dmsfileupload dmsfileupload)
        {
            Dmsfileupload model = new Dmsfileupload();
            model.FileNo = dmsfileupload.FileNo;
            model.IsFileBulkUpload = dmsfileupload.IsFileBulkUpload;
            model.AlloteeName = dmsfileupload.AlloteeName;
            model.DepartmentId = dmsfileupload.DepartmentId;
            model.LocalityId = dmsfileupload.LocalityId;
            model.KhasraNoId = dmsfileupload.KhasraNoId;
            model.PropertyNoAddress = dmsfileupload.PropertyNoAddress;
            model.AlmirahNo = dmsfileupload.AlmirahNo;
            model.Title = dmsfileupload.Title;
            model.FileName = dmsfileupload.FileName;
            model.FilePath = dmsfileupload.FilePath;
            model.IsActive =  1;
            model.CreatedDate = DateTime.Now;
            _dmsFileUploadRepository.Add(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Dmsfileupload> FetchSingleResult(int id)
        {
            return await _dmsFileUploadRepository.FetchSingleResult(id);
        }

        public async Task<bool> Update(int id, Dmsfileupload dmsfileupload)
        {
            var result = await _dmsFileUploadRepository.FindBy(a => a.Id == id);
            Dmsfileupload model = result.FirstOrDefault();
            model.FileNo = dmsfileupload.FileNo;
            model.IsFileBulkUpload = dmsfileupload.IsFileBulkUpload;
            model.AlloteeName = dmsfileupload.AlloteeName;
            model.DepartmentId = dmsfileupload.DepartmentId;
            model.LocalityId = dmsfileupload.LocalityId;
            model.KhasraNoId = dmsfileupload.KhasraNoId;
            model.PropertyNoAddress = dmsfileupload.PropertyNoAddress;
            model.AlmirahNo = dmsfileupload.AlmirahNo;
            model.Title = dmsfileupload.Title;
            model.FileName = dmsfileupload.FileName;
            model.FilePath = dmsfileupload.FilePath;
            model.IsActive = dmsfileupload.IsActive;
            model.ModifiedDate = DateTime.Now;
            _dmsFileUploadRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Delete(int id, int userId)
        {
            var result = await _dmsFileUploadRepository.FindBy(a => a.Id == id);
            Dmsfileupload model = result.FirstOrDefault();
            model.IsActive = 0;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = userId;
            _dmsFileUploadRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        //public async Task<List<Locality>> GetLocalityList()
        //{
        //    List<Locality> localityList = await _dmsFileUploadRepository.GetLocalityList();
        //    return localityList;
        //}
        //public async Task<List<District>> GetDistrictList()
        //{
        //    List<District> districtList = await _dmsFileUploadRepository.GetDistrictList();
        //    return districtList;
        //}
        //public async Task<List<Damagepayeeregister>> GetAllDamagepayeeregisterTemp()
        //{
        //    return await _dmsFileUploadRepository.GetAllDamagepayeeregisterTemp();
        //}



        //public async Task<List<Damagepayeeregistertemp>> GetDamagepayeeregisterUsingRepo()
        //{
        //    return await _dmsFileUploadRepository.GetAllDamagepayeeregisterTemp();
        //}

        //public async Task<Damagepayeeregister> FetchSingleResult(int id)
        //{
        //    var result = await _dmsFileUploadRepository.FindBy(a => a.Id == id);
        //    Damagepayeeregister model = result.FirstOrDefault();
        //    return model;
        //}

        //public async Task<bool> Update(Damagepayeeregister damagepayeeregistertemp)
        //{
        //    var result = await _dmsFileUploadRepository.FindBy(a => a.Id == damagepayeeregistertemp.Id);
        //    Damagepayeeregister model = result.FirstOrDefault();
        //    model.FileNo = damagepayeeregistertemp.FileNo;
        //    model.TypeOfDamageAssessee = damagepayeeregistertemp.TypeOfDamageAssessee;
        //    model.PropertyNo = damagepayeeregistertemp.PropertyNo;
        //    model.LocalityId = damagepayeeregistertemp.LocalityId;
        //    model.FloorNo = damagepayeeregistertemp.FloorNo;
        //    model.StreetNo = damagepayeeregistertemp.StreetNo;
        //    model.PinCode = damagepayeeregistertemp.PinCode;
        //    model.DistrictId = damagepayeeregistertemp.DistrictId;
        //    model.PlotAreaSqYard = damagepayeeregistertemp.PlotAreaSqYard;
        //    model.PlotAreaSqMt = damagepayeeregistertemp.PlotAreaSqMt;
        //    model.FloorAreaSqYard = damagepayeeregistertemp.FloorAreaSqYard;
        //    model.FloorAreaSqMt = damagepayeeregistertemp.FloorAreaSqMt;
        //    model.PropertyPhotoPath = damagepayeeregistertemp.PropertyPhotoPath;
        //    model.UseOfProperty = damagepayeeregistertemp.UseOfProperty;
        //    model.ResidentialSqYard = damagepayeeregistertemp.ResidentialSqYard;
        //    model.ResidentialSqMt = damagepayeeregistertemp.ResidentialSqMt;
        //    model.CommercialSqYard = damagepayeeregistertemp.CommercialSqYard;
        //    model.CommercialSqMt = damagepayeeregistertemp.CommercialSqMt;
        //    model.LitigationStatus = damagepayeeregistertemp.LitigationStatus;
        //    model.CourtName = damagepayeeregistertemp.CourtName;
        //    model.CaseNo = damagepayeeregistertemp.CaseNo;
        //    model.OppositionName = damagepayeeregistertemp.OppositionName;
        //    model.PetitionerRespondent = damagepayeeregistertemp.PetitionerRespondent;
        //    model.IsDdadamagePayee = damagepayeeregistertemp.IsDdadamagePayee;
        //    model.IsApplyForMutation = damagepayeeregistertemp.IsApplyForMutation;
        //    model.ShowCauseNoticePath = damagepayeeregistertemp.ShowCauseNoticePath;
        //    model.FgformPath = damagepayeeregistertemp.FgformPath;
        //    model.IsDocumentFor = damagepayeeregistertemp.IsDocumentFor;
        //    model.DocumentForFilePath = damagepayeeregistertemp.DocumentForFilePath;
        //    model.InterestDueAmountCompund = damagepayeeregistertemp.InterestDueAmountCompund;
        //    model.TotalValueWithInterest = damagepayeeregistertemp.TotalValueWithInterest;
        //    model.Rebate = damagepayeeregistertemp.Rebate;
        //    model.TotalPayable = damagepayeeregistertemp.TotalPayable;
        //    model.CalculatorValue = damagepayeeregistertemp.CalculatorValue;
        //    model.Declaration1 = damagepayeeregistertemp.Declaration1;
        //    model.Declaration2 = damagepayeeregistertemp.Declaration2;
        //    model.Declaration3 = damagepayeeregistertemp.Declaration3;
        //    model.Otp = damagepayeeregistertemp.Otp;
        //    model.ProceedToPay = damagepayeeregistertemp.ProceedToPay;
        //    model.Signature = damagepayeeregistertemp.Signature;
        //    model.Achknowledgement = damagepayeeregistertemp.Achknowledgement;
        //    model.IsActive = 1;
        //    model.UserId = damagepayeeregistertemp.UserId;

        //    model.ModifiedDate = DateTime.Now;
        //    model.ModifiedBy = damagepayeeregistertemp.ModifiedBy;
        //    _dmsFileUploadRepository.Edit(model);
        //    return await _unitOfWork.CommitAsync() > 0;
        //}

        //public async Task<bool> Create(Damagepayeeregister damagepayeeregistertemp)
        //{
        //    damagepayeeregistertemp.CreatedBy = 1;
        //    damagepayeeregistertemp.CreatedDate = DateTime.Now;
        //    _dmsFileUploadRepository.Add(damagepayeeregistertemp);
        //    return await _unitOfWork.CommitAsync() > 0;
        //}



        //public async Task<bool> Delete(int id)
        //{
        //    var form = await _dmsFileUploadRepository.FindBy(a => a.Id == id);
        //    Damagepayeeregister model = form.FirstOrDefault();
        //    model.IsActive = 0;
        //    _dmsFileUploadRepository.Edit(model);
        //    return await _unitOfWork.CommitAsync() > 0;
        //}

        //public async Task<PagedResult<Damagepayeeregister>> GetPagedDamagepayeeregister(DamagepayeeregistertempSearchDto model)
        //{
        //    return await _dmsFileUploadRepository.GetPagedDamagepayeeregister(model);
        //}

        ////********* rpt 1 Persolnal info of damage assesse ***********
        //public async Task<bool> SavePayeePersonalInfoTemp(Damagepayeepersonelinfo damagepayeepersonelinfotemp)
        //{
        //    damagepayeepersonelinfotemp.CreatedBy = 1;
        //    damagepayeepersonelinfotemp.CreatedDate = DateTime.Now;
        //    damagepayeepersonelinfotemp.IsActive = 1;
        //    return await _dmsFileUploadRepository.SavePayeePersonalInfoTemp(damagepayeepersonelinfotemp);
        //}

        //public async Task<List<Damagepayeepersonelinfo>> GetPersonalInfoTemp(int id)
        //{
        //    return await _dmsFileUploadRepository.GetPersonalInfoTemp(id);
        //}
        //public async Task<bool> DeletePayeePersonalInfoTemp(int Id)
        //{
        //    return await _dmsFileUploadRepository.DeletePayeePersonalInfoTemp(Id);
        //}
        //public async Task<Damagepayeepersonelinfo> GetPersonelInfoFilePath(int Id)
        //{
        //    return await _dmsFileUploadRepository.GetPersonelInfoFilePath(Id);
        //}

        ////********* rpt 2 Allotte Type **********

        //public async Task<bool> SaveAllotteTypeTemp(List<Allottetype> allottetypetemp)
        //{
        //    allottetypetemp.ForEach(x => x.CreatedBy = 1);
        //    allottetypetemp.ForEach(x => x.CreatedDate = DateTime.Now);
        //    allottetypetemp.ForEach(x => x.IsActive = 1);
        //    return await _dmsFileUploadRepository.SaveAllotteTypeTemp(allottetypetemp);
        //}
        //public async Task<List<Allottetype>> GetAllottetypeTemp(int id)
        //{
        //    return await _dmsFileUploadRepository.GetAllottetypeTemp(id);
        //}
        //public async Task<bool> DeleteAllotteTypeTemp(int Id)
        //{
        //    return await _dmsFileUploadRepository.DeleteAllotteTypeTemp(Id);
        //}
        //public async Task<Allottetype> GetAllotteTypeSingleResult(int id)
        //{
        //    return await _dmsFileUploadRepository.GetAllotteTypeSingleResult(id);
        //}

        ////********* rpt 3 Damage payment history ***********

        //public async Task<bool> SavePaymentHistoryTemp(List<Damagepaymenthistory> damagepaymenthistorytemp)
        //{
        //    damagepaymenthistorytemp.ForEach(x => x.CreatedBy = 1);
        //    damagepaymenthistorytemp.ForEach(x => x.CreatedDate = DateTime.Now);
        //    damagepaymenthistorytemp.ForEach(x => x.IsActive = 1);
        //    return await _dmsFileUploadRepository.SavePaymentHistoryTemp(damagepaymenthistorytemp);
        //}
        //public async Task<List<Damagepaymenthistory>> GetPaymentHistoryTemp(int id)
        //{
        //    return await _dmsFileUploadRepository.GetPaymentHistoryTemp(id);
        //}
        //public async Task<bool> DeletePaymentHistoryTemp(int Id)
        //{
        //    return await _dmsFileUploadRepository.DeletePaymentHistoryTemp(Id);
        //}
        //public async Task<Damagepaymenthistory> GetPaymentHistorySingleResult(int id)
        //{
        //    return await _dmsFileUploadRepository.GetPaymentHistorySingleResult(id);
        //}

        //public async Task<Damagepayeeregister> FetchSelfAssessmentUserId(int userId)
        //{
        //    return await _dmsFileUploadRepository.FetchSelfAssessmentUserId(userId);
        //}

        //public async Task<Rebate> GetRebateValue()
        //{
        //    return await _dmsFileUploadRepository.GetRebateValue();
        //}

        //public string GetLocalityName(int? localityId)
        //{
        //    return _dmsFileUploadRepository.GetLocalityName(localityId);
        //}

        //public async Task<bool> UpdateBeforeApproval(int id, Damagepayeeregister damagepayeeregister)
        //{
        //    var result = await _dmsFileUploadRepository.FindBy(a => a.Id == id);
        //    Damagepayeeregister model = result.FirstOrDefault();
        //    model.ApprovedStatus = damagepayeeregister.ApprovedStatus;
        //    model.PendingAt = damagepayeeregister.PendingAt;
        //    model.ModifiedDate = DateTime.Now;
        //    _dmsFileUploadRepository.Edit(model);
        //    return await _unitOfWork.CommitAsync() > 0;
        //}
    }
}
