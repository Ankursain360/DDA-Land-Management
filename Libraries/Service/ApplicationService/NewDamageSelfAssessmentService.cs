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
    public class NewDamageSelfAssessmentService : EntityService<NewDamageSelfAssessment>, INewDamageSelfAssessmentService
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
            NewDamageSelfAssessment model = form.FirstOrDefault();
            model.RegId = null;
            _assessmentRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<NewDamageSelfAssessment> FetchSingleResult(int id)
        {
            var result = await _assessmentRepository.FindBy(a => a.Id == id);
            NewDamageSelfAssessment model = result.FirstOrDefault();
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

        public async Task<bool> Update(int id, NewDamageSelfAssessment selfAssessment)
        {
            var result = await _assessmentRepository.FindBy(a => a.Id == id);
            NewDamageSelfAssessment model = result.FirstOrDefault();
            model.RegId = selfAssessment.RegId;
            model.Districtid = selfAssessment.Districtid;
            model.VillageId = selfAssessment.VillageId;
            model.ColonyId = selfAssessment.ColonyId;
            model.Latestatsname = selfAssessment.Latestatsname;
            model.Pin = selfAssessment.Pin;
            model.North = selfAssessment.North;
            model.South = selfAssessment.South;
            model.East = selfAssessment.East;
            model.West = selfAssessment.West;
            model.TypeProperty = selfAssessment.TypeProperty;
            model.ConstructedArea = selfAssessment.ConstructedArea;
            model.HouseNo = selfAssessment.HouseNo;
            model.PlotNo = selfAssessment.PlotNo;
            model.Street = selfAssessment.Street;
            model.LocalityId = selfAssessment.LocalityId;
            model.NosFloor = selfAssessment.NosFloor;
            model.BuildingFootprintArea = selfAssessment.BuildingFootprintArea;
            model.ConstructionYear = selfAssessment.ConstructionYear;
            model.FrontRoadWidth = selfAssessment.FrontRoadWidth;
            model.FirstName = selfAssessment.FirstName;
            model.MiddleName = selfAssessment.MiddleName;
            model.LastName = selfAssessment.LastName;
            model.SpouseName = selfAssessment.SpouseName;
            model.FatherName = selfAssessment.FatherName;
            model.MotherName = selfAssessment.MotherName;
            model.EpicIdNumber = selfAssessment.EpicIdNumber;
            model.EmailId = selfAssessment.EmailId;
            model.MobileNo = selfAssessment.MobileNo;
            model.AadhaarNo = selfAssessment.AadhaarNo;
            model.DateOfBirth = selfAssessment.DateOfBirth;
            model.Gender = selfAssessment.Gender;
            model.PanNo = selfAssessment.PanNo;
            model.OwnershipColony = selfAssessment.OwnershipColony;
            model.OwnershipDistrictId = selfAssessment.OwnershipDistrictId;
            model.PropertyShare = selfAssessment.PropertyShare;
            model.OwnerPhoto = selfAssessment.OwnerPhoto;
            model.DoesLandLitigation = selfAssessment.DoesLandLitigation;
            model.LitigationStatus = selfAssessment.LitigationStatus;
            model.CourtCaseStatus = selfAssessment.CourtCaseStatus;
            model.DetailCourtCase = selfAssessment.DetailCourtCase;
            model.CourtName = selfAssessment.CourtName;
            model.CaseNumber = selfAssessment.CaseNumber;
            model.PetitionerRespondent = selfAssessment.PetitionerRespondent;
            model.NameOppositeParty = selfAssessment.NameOppositeParty;
            model.PhotographProperty = selfAssessment.PhotographProperty;
            model.PhotographOwner = selfAssessment.PhotographOwner;
            model.Gpa = selfAssessment.Gpa;
            model.Ats = selfAssessment.Ats;
            model.ElectricityBill = selfAssessment.ElectricityBill;
            model.PaymentDocument = selfAssessment.PaymentDocument;
            model.WillDocument = selfAssessment.WillDocument;
            model.PossessionDocument = selfAssessment.PossessionDocument;
            model.MutationDocument = selfAssessment.MutationDocument;
            model.CoordinateDocument = selfAssessment.CoordinateDocument;
            model.Declaration1 = selfAssessment.Declaration1;
            model.Declaration2 = selfAssessment.Declaration2;
            model.Declaration3 = selfAssessment.Declaration3;
            model.Col1 = selfAssessment.Col1;
            model.Col2 = selfAssessment.Col2;
            model.RecordStatus = selfAssessment.RecordStatus;
            model.ChainDocument = selfAssessment.ChainDocument;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _assessmentRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(NewDamageSelfAssessment selfAssessment)
        {

            selfAssessment.CreatedBy = 1;
            selfAssessment.CreatedDate = DateTime.Now;
            _assessmentRepository.Add(selfAssessment);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<List<NewDamageSelfAssessment>> GetDamageSelfAssessments()
        {
            return await _assessmentRepository.GetAllDamageSelfAssessments();
        }



        //********* Ats Self Assessment Details **********

       public async Task<bool> SaveAttendance(NewDamageSelfAssessmentAtsDetails atsDetails)
        {
            atsDetails.CreatedBy = atsDetails.CreatedBy;
            atsDetails.CreatedDate = DateTime.Now;
            //atsDetails.IsActive = 1;
            return await _assessmentRepository.SaveAttendance(atsDetails);
        }
       public async Task<List<NewDamageSelfAssessmentAtsDetails>> GetAllAtsDetails(int id)
        {
            return await _assessmentRepository.GetAllAtsDetails(id);
        }
       public async Task<bool> DeleteAts(int Id)
        {
            return await _assessmentRepository.DeleteAts(Id);
        }

        //********* Gpa Self Assessment Details **********

       public async Task<bool> SaveAttendance(NewDamageSelfAssessmentGpaDetails gpaDetails)
        {
            gpaDetails.CreatedBy = gpaDetails.CreatedBy;
            gpaDetails.CreatedDate = DateTime.Now;
            //atsDetails.IsActive = 1;
            return await _assessmentRepository.SaveAttendance(gpaDetails);
        }
        public async Task<List<NewDamageSelfAssessmentGpaDetails>> GetAllGpaDetails(int id)
        {
            return await _assessmentRepository.GetAllGpaDetails(id);
        }
        public async Task<bool> DeleteGpa(int Id)
        {
            return await _assessmentRepository.DeleteGpa(Id);
        }

        //********* Add Floor ! Damage Details ***********
       public async Task<bool> SaveSurveyReport(NewdamageAddfloor addDloorDetails)
        {
            addDloorDetails.CreatedBy = addDloorDetails.CreatedBy;
            addDloorDetails.CreatedDate = DateTime.Now;
            //atsDetails.IsActive = 1;
            return await _assessmentRepository.SaveSurveyReport(addDloorDetails);
        }
       public async Task<List<NewdamageAddfloor>> GetAddfloorsDetails(int id)
        {
            return await _assessmentRepository.GetAddfloorsDetails(id);
        }
       public async Task<NewdamageAddfloor> GetAddFloorFilePath(int Id)
        {
            return await _assessmentRepository.GetAddFloorFilePath(Id);
        }
       public async Task<bool> DeleteAddFloor(int Id)
        {
            return await _assessmentRepository.DeleteAddFloor(Id);
        }


        public async Task<NewDamageSelfAssessment> GetUploadDocumentFilePath(int Id)
        {
            return await _assessmentRepository.GetUploadDocumentFilePath(Id);
        }
    }
}
