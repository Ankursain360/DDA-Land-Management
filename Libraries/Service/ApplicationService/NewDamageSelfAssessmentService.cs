using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Model.Entity;
using Repository.IEntityRepository;
using Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.ApplicationService
{
    public class NewDamageSelfAssessmentService : EntityService<Newdamagepayeeregistration>, INewDamageSelfAssessmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewDamageSelfAssessmentRepository _assessmentRepository;
        protected readonly DataContext _dbContext;

        public NewDamageSelfAssessmentService(IUnitOfWork unitOfWork, INewDamageSelfAssessmentRepository  assessmentRepository, DataContext dbContext)
       : base(unitOfWork, assessmentRepository)
        {
            _unitOfWork = unitOfWork;
            _assessmentRepository = assessmentRepository;
            _dbContext = dbContext;
        }
        public async Task<bool> Delete(int id)
        {
            var form = await _assessmentRepository.FindBy(a => a.Id == id);
            Newdamagepayeeregistration model = form.FirstOrDefault();
           // model.RegId = null;
            _assessmentRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Newdamagepayeeregistration> FetchSingleResult(int id)
        {
            var result = await _assessmentRepository.FindBy(a => a.Id == id);
            Newdamagepayeeregistration model = result.FirstOrDefault();
            return model;
        }

        public async Task<List<New_Damage_Colony>> GetAllColony(int villageId)
        {
            List<New_Damage_Colony> colonyList = await _assessmentRepository.GetAllColony(villageId);
            return colonyList;
        }

        public async Task<List<District>> GetAllDistrict()
        {
            List<District> districtList = await _assessmentRepository.GetAllDistrict();
            return districtList;
        }
        public async Task<List<Floors>> GetFloors()
        {
            List<Floors> floorlist = await _assessmentRepository.GetFloors();
            return floorlist;
        }
        public async Task<List<Locality>> GetLocalityList()
        {
            List<Locality> localityList = await _assessmentRepository.GetLocalityList();
            return localityList;
        }
        public async Task<List<Acquiredlandvillage>> GetAllVillage(int districtId)
        {
            List<Acquiredlandvillage> villageList = await _assessmentRepository.GetAllVillage(districtId);
            return villageList;
        }

        public async Task<bool> Update(int id, Newdamagepayeeregistration selfAssessment)
        {
            var result = await _assessmentRepository.FindBy(a => a.Id == id);
            Newdamagepayeeregistration model = result.FirstOrDefault();         
            model.VillageId = selfAssessment.VillageId;
            model.ColonyId = selfAssessment.ColonyId;
            model.North = selfAssessment.North;
            model.South = selfAssessment.South;
            model.East = selfAssessment.East;
            model.West = selfAssessment.West;
            model.PlotNo = selfAssessment.PlotNo;
            model.Street = selfAssessment.Street;
            model.FirstName = selfAssessment.FirstName;
            model.MiddleName = selfAssessment.MiddleName;
            model.LastName = selfAssessment.LastName;
            model.SpouseName = selfAssessment.SpouseName;
            model.FatherName = selfAssessment.FatherName;
            model.EmailId = selfAssessment.EmailId;
            model.MobileNo = selfAssessment.MobileNo;
            model.Gender = selfAssessment.Gender;
            model.PanNo = selfAssessment.PanNo;
            model.CourtCaseStatus = selfAssessment.CourtCaseStatus;
            model.CourtName = selfAssessment.CourtName;
            model.PetitionerRespondent = selfAssessment.PetitionerRespondent;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _assessmentRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Newdamagepayeeregistration selfAssessment)
        {

           
            selfAssessment.CreatedDate = DateTime.Now;
            _assessmentRepository.Add(selfAssessment);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<List<Newdamagepayeeregistration>> GetDamageSelfAssessments(int id)
        {
            return await _assessmentRepository.GetAllDamageSelfAssessments(id);
        }

        public async Task<PagedResult<Newdamagepayeeregistration>> GetPagedDamagePayee(DamagePayeeSearchDto model , int id)
        {
            return await _assessmentRepository.GetPagedDamagePayee(model,id);
        }

        //********* Ats Self Assessment Details **********

        public async Task<bool> SaveATSDetails(Newdamageselfassessmentatsdetail AtsDetails)
        {
            AtsDetails.CreatedBy = AtsDetails.CreatedBy;
            AtsDetails.CreatedDate = DateTime.Now;
            // attendance.IsActive = 1;
            return await _assessmentRepository.SaveATSDetails(AtsDetails);
        }
        public async Task<List<Newdamageselfassessmentatsdetail>> GetAllAtsDetails(int id)
        {
            return await _assessmentRepository.GetAllAtsDetails(id);
        }
       public async Task<bool> DeleteAts(int Id)
        {
            return await _assessmentRepository.DeleteAts(Id);
        }
       public async Task<Newdamageselfassessmentatsdetail> GetAtsFilePath(int id)
        {
            return await _assessmentRepository.GetAtsFilePath(id);
        }


        //********* Gpa Self Assessment Details **********     
        public async Task<bool> SaveGPADetails(Newdamageselfassessmentgpadetail gpaDetails)
        {
            gpaDetails.CreatedBy = gpaDetails.CreatedBy;
            gpaDetails.CreatedDate = DateTime.Now;
            // attendance.IsActive = 1;
            return await _assessmentRepository.SaveGPADetails(gpaDetails);
        }       
        //public async Task<bool> SaveHolderdetails(Newdamageselfassessmentholderdetail holderDetails)
        //{
        //    holderDetails.CreatedBy = holderDetails.CreatedBy;
        //    holderDetails.CreatedDate = DateTime.Now;
        //    // attendance.IsActive = 1;
        //    return await _assessmentRepository.SaveHolderdetails(holderDetails);
        //}
      
        public async Task<List<Newdamageselfassessmentgpadetail>> GetAllGpaDetails(int id)
        {
            return await _assessmentRepository.GetAllGpaDetails(id);
        }
        public async Task<bool> DeleteGpa(int Id)
        {
            return await _assessmentRepository.DeleteGpa(Id);
        }

        public async Task<Newdamageselfassessmentgpadetail> GetGpaFilePath(int Id)
        {
            return await _assessmentRepository.GetGpaFilePath(Id);
        }


        //********* Add Floor ! Damage Details ***********
        public async Task<bool> SaveFloorDetails(Newdamageselfassessmentfloordetail attendance)
        {
            return await _assessmentRepository.SaveFloorDetails(attendance);
        }
        public async Task<bool> SaveSurveyReport(Newdamageselfassessmentfloordetail addDloorDetails)
        {
            addDloorDetails.CreatedBy = addDloorDetails.CreatedBy;
            addDloorDetails.CreatedDate = DateTime.Now;
            //atsDetails.IsActive = 1;
            return await _assessmentRepository.SaveSurveyReport(addDloorDetails);
        }
       public async Task<List<Newdamageselfassessmentfloordetail>> GetAddfloorsDetails(int id)
        {
            return await _assessmentRepository.GetAddfloorsDetails(id);
        }
       public async Task<Newdamageselfassessmentfloordetail> GetAddFloorFilePath(int Id)
        {
            return await _assessmentRepository.GetAddFloorFilePath(Id);
        }
       public async Task<bool> DeleteAddFloor(int Id)
        {
            return await _assessmentRepository.DeleteAddFloor(Id);
        }


        public async Task<Newdamagepayeeregistration> GetUploadDocumentFilePath(int Id)
        {
            return await _assessmentRepository.GetUploadDocumentFilePath(Id);
        }

        //******** occupant details *******

        //public async Task<Newdamagepayeeoccupantinfo> FetchSingleResultOccupant(int id)
        //{
        //    var data = await _assessmentRepository.FindBy(x => x.Id == id);
        //    Newdamagepayeeoccupantinfo model = data.FirstOrDefault();
        //    return model;
        //}
        public async Task<bool> SaveOccupantDetails(Newdamagepayeeoccupantinfo details)
        {

            return await _assessmentRepository.SaveOccupantDetails(details);
        }
       public async Task<List<Newdamagepayeeoccupantinfo>> GetOccupantDetails(int id)
        {
            return await _assessmentRepository.GetOccupantDetails(id);
        }
        public async Task<Newdamagepayeeoccupantinfo> GetOccupantFile(int id)
        {
            return await _assessmentRepository.GetOccupantFile(id);
        }
        //*******payment details*******
        public async Task<bool> SavePaymentdetails(Newdamagepaymenthistory paymentDetails)
        {
            paymentDetails.CreatedBy = paymentDetails.CreatedBy;
            paymentDetails.CreatedDate = DateTime.Now;
            // attendance.IsActive = 1;
            return await _assessmentRepository.SavePaymentdetails(paymentDetails);
        }
       public async Task<List<Newdamagepaymenthistory>> Getpaymentdetail(int id)
        {
            return await _assessmentRepository.Getpaymentdetail(id);
        }
        public async Task<Newdamagepaymenthistory> GetpaymentFile(int id)
        {
            return await _assessmentRepository.GetpaymentFile(id);
        }


        //************ NewDamagePayeeDashboard ******************** 

        public async Task<List<DamagePayeeDashboardList>> GetDamagePayee(DamagePayeeDashboardSearchDto model)
        {
            return await _assessmentRepository.GetDamagePayee(model);
        }

    }
}
