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
     public class DamagepayeeregisterService : EntityService<Damagepayeeregistertemp>, IDamagepayeeregisterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDamagepayeeregisterRepository _damagepayeeregisterRepository;

        public DamagepayeeregisterService(IUnitOfWork unitOfWork, IDamagepayeeregisterRepository damagepayeeregisterRepository)
        : base(unitOfWork, damagepayeeregisterRepository)
        {
            _unitOfWork = unitOfWork;
            _damagepayeeregisterRepository = damagepayeeregisterRepository;
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

        public async Task<Damagepayeeregistertemp> GetPropertyPhotoPath(int Id)
        {
            return await _damagepayeeregisterRepository.GetPropertyPhotoPath(Id);
        }

        public async Task<List<Damagepayeeregistertemp>> GetAllDamagepayeeregisterTemp()
        {
            return await _damagepayeeregisterRepository.GetAllDamagepayeeregisterTemp();
        }



        //public async Task<List<Damagepayeeregistertemp>> GetDamagepayeeregisterUsingRepo()
        //{
        //    return await _damagepayeeregisterRepository.GetAllDamagepayeeregisterTemp();
        //}

        public async Task<Damagepayeeregistertemp> FetchSingleResult(int id)
        {
            var result = await _damagepayeeregisterRepository.FindBy(a => a.Id == id);
            Damagepayeeregistertemp model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Update(int id, Damagepayeeregistertemp damagepayeeregistertemp)
        {
            var result = await _damagepayeeregisterRepository.FindBy(a => a.Id == id);
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
            model.FloorAreaSqYard = damagepayeeregistertemp.FloorAreaSqYard;
            model.PlotAreaSqMt = damagepayeeregistertemp.PlotAreaSqMt;
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
           
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _damagepayeeregisterRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Damagepayeeregistertemp damagepayeeregistertemp)
        {
            damagepayeeregistertemp.CreatedBy = 1;
            damagepayeeregistertemp.CreatedDate = DateTime.Now;
            _damagepayeeregisterRepository.Add(damagepayeeregistertemp);
            return await _unitOfWork.CommitAsync() > 0;
        }      

        public async Task<bool> Delete(int id)
        {
            var form = await _damagepayeeregisterRepository.FindBy(a => a.Id == id);
            Damagepayeeregistertemp model = form.FirstOrDefault();
            model.IsActive = 0;
            _damagepayeeregisterRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Damagepayeeregistertemp>> GetPagedDamagepayeeregistertemp(DamagepayeeregistertempSearchDto model)
        {
            return await _damagepayeeregisterRepository.GetPagedDamagepayeeregistertemp(model);
        }

        //********* rpt 1 Persolnal info of damage assesse ***********
        public async Task<bool> SavePayeePersonalInfoTemp(Damagepayeepersonelinfotemp damagepayeepersonelinfotemp)
        {
            damagepayeepersonelinfotemp.CreatedBy = 1;
            damagepayeepersonelinfotemp.CreatedDate = DateTime.Now;
            damagepayeepersonelinfotemp.IsActive = 1;
            return await _damagepayeeregisterRepository.SavePayeePersonalInfoTemp(damagepayeepersonelinfotemp);
        }

        public async Task<List<Damagepayeepersonelinfotemp>> GetPersonalInfoTemp(int id)
        {
            return await _damagepayeeregisterRepository.GetPersonalInfoTemp(id);
        }
        public async Task<bool> DeletePayeePersonalInfoTemp(int Id)
        {
            return await _damagepayeeregisterRepository.DeletePayeePersonalInfoTemp(Id);
        }
        public async Task<Damagepayeepersonelinfotemp> GetAadharFilePath(int Id)
        {
            return await _damagepayeeregisterRepository.GetAadharFilePath(Id);
        }
        public async Task<Damagepayeepersonelinfotemp> GetPanFilePath(int Id)
        {
            return await _damagepayeeregisterRepository.GetPanFilePath(Id);
        }
        public async Task<Damagepayeepersonelinfotemp> GetPhotographPath(int Id)
        {
            return await _damagepayeeregisterRepository.GetPhotographPath(Id);
        }
        public async Task<Damagepayeepersonelinfotemp> GetSignaturePath(int Id)
        {
            return await _damagepayeeregisterRepository.GetSignaturePath(Id);
        }
        public async Task<List<Damagepayeepersonelinfotemp>> GetPreviousAssesseRepeater(int Id)
        {
            return await _damagepayeeregisterRepository.GetPreviousAssesseRepeater(Id);
        }

        //********* rpt 2 Allotte Type **********

        public async Task<bool> SaveAllotteTypeTemp(List<Allottetypetemp> allottetypetemp)
        {
            allottetypetemp.ForEach(x => x.CreatedBy = 1);
            allottetypetemp.ForEach(x => x.CreatedDate =DateTime.Now);
            allottetypetemp.ForEach(x => x.IsActive = 1);
            return await _damagepayeeregisterRepository.SaveAllotteTypeTemp(allottetypetemp);
        }
        public async Task<List<Allottetypetemp>> GetAllottetypeTemp(int id)
        {
            return await _damagepayeeregisterRepository.GetAllottetypeTemp(id);
        }
        public async Task<bool> DeleteAllotteTypeTemp(int Id)
        {
            return await _damagepayeeregisterRepository.DeleteAllotteTypeTemp(Id);
        }
        public async Task<Allottetypetemp> GetATSFilePath(int Id)
        {
            return await _damagepayeeregisterRepository.GetATSFilePath(Id);
        }
        public async Task<List<Allottetypetemp>> GetNewAlloteeRepeater(int Id)
        {
            return await _damagepayeeregisterRepository.GetNewAlloteeRepeater(Id);
        }


        //********* rpt 3 Damage payment history ***********

        public async Task<bool> SavePaymentHistoryTemp(List<Damagepaymenthistorytemp> damagepaymenthistorytemp)
        {
            damagepaymenthistorytemp.ForEach(x => x.CreatedBy = 1);
            damagepaymenthistorytemp.ForEach(x => x.CreatedDate = DateTime.Now);
            damagepaymenthistorytemp.ForEach(x => x.IsActive = 1);
            return await _damagepayeeregisterRepository.SavePaymentHistoryTemp(damagepaymenthistorytemp);
        }
        public async Task<List<Damagepaymenthistorytemp>> GetPaymentHistoryTemp(int id)
        {
            return await _damagepayeeregisterRepository.GetPaymentHistoryTemp(id);
        }
        public async Task<bool> DeletePaymentHistoryTemp(int Id)
        {
            return await _damagepayeeregisterRepository.DeletePaymentHistoryTemp(Id);
        }

    }
}
