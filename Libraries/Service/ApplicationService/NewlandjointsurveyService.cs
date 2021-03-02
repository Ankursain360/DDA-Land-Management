using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Libraries.Repository.IEntityRepository;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using Libraries.Model;
using Dto.Search;


namespace Service.ApplicationService
{
    public class NewLandJointSurveyService : EntityService<Newlandjointsurvey>, INewLandJointSurveyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewLandJointSurveyRepository _newLandJointSurveyRepository;
       
        public NewLandJointSurveyService(IUnitOfWork unitOfWork, INewLandJointSurveyRepository newLandJointSurveyRepository)
        : base(unitOfWork, newLandJointSurveyRepository)
        {
            _unitOfWork = unitOfWork;
            _newLandJointSurveyRepository = newLandJointSurveyRepository;
           
        }
        public async Task<List<Zone>> GetAllZone()
        {
            List<Zone> zoneList = await _newLandJointSurveyRepository.GetAllZone();
            return zoneList;
        }
        public async Task<List<Newlandvillage>> GetAllVillage(int zoneId)
        {
            List<Newlandvillage> villageList = await _newLandJointSurveyRepository.GetAllVillage(zoneId);
            return villageList;
        }
        public async Task<List<Newlandkhasra>> GetAllKhasra(int? villageId)
        {
            return await _newLandJointSurveyRepository.GetAllKhasra(villageId);
        }


        public async Task<PagedResult<Newlandjointsurvey>> GetPagedNewLandJointSurvey(NewLandJointSurveySearchDto model)
            {
                return await _newLandJointSurveyRepository.GetPagedNewLandJointSurvey(model);
            }


            
            public async Task<List<Newlandjointsurvey>> GetAllNewLandJointSurvey()
            {

                return await _newLandJointSurveyRepository.GetAllNewLandJointSurvey();
            }

            public async Task<bool> Delete(int id)
            {
                var form = await _newLandJointSurveyRepository.FindBy(a => a.Id == id);
            Newlandjointsurvey model = form.FirstOrDefault();
                model.IsActive = 0;
            _newLandJointSurveyRepository.Edit(model);
                return await _unitOfWork.CommitAsync() > 0;
            }

            public async Task<Newlandjointsurvey> FetchSingleResult(int id)
            {
                var result = await _newLandJointSurveyRepository.FindBy(a => a.Id == id);
            Newlandjointsurvey model = result.FirstOrDefault();
                return model;
            }
            public async Task<bool> Update(int id, Newlandjointsurvey newlandjointsurvey)
            {
                var result = await _newLandJointSurveyRepository.FindBy(a => a.Id == id);
            Newlandjointsurvey model = result.FirstOrDefault();

            model.ZoneId = newlandjointsurvey.ZoneId;
            model.VillageId = newlandjointsurvey.VillageId;
                model.KhasraId = newlandjointsurvey.KhasraId;
              
                model.Address = newlandjointsurvey.Address;
                model.SitePosition = newlandjointsurvey.SitePosition;
                model.Bigha = newlandjointsurvey.Bigha;
                model.Biswa = newlandjointsurvey.Biswa;
                model.Biswanshi = newlandjointsurvey.Biswanshi;
                model.NatureOfStructure = newlandjointsurvey.NatureOfStructure;
            model.JointSurveyDate = newlandjointsurvey.JointSurveyDate;
         
            model.Remarks = newlandjointsurvey.Remarks;
                model.IsActive = newlandjointsurvey.IsActive;
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = newlandjointsurvey.ModifiedBy;
            _newLandJointSurveyRepository.Edit(model);
                return await _unitOfWork.CommitAsync() > 0;

            }

            public async Task<bool> Create(Newlandjointsurvey newlandjointsurvey)
            {
            newlandjointsurvey.CreatedBy = newlandjointsurvey.CreatedBy;
            newlandjointsurvey.CreatedDate = DateTime.Now;
            newlandjointsurvey.IsActive = 1;

            _newLandJointSurveyRepository.Add(newlandjointsurvey);
                return await _unitOfWork.CommitAsync() > 0;
            }

       
            //********* rpt ! attendance Details **********

            public async Task<bool> SaveAttendance(Newjointsurveyattendancedetail attendance)
            {
            attendance.CreatedBy = attendance.CreatedBy;
            attendance.CreatedDate = DateTime.Now;
            attendance.IsActive = 1;
                return await _newLandJointSurveyRepository.SaveAttendance(attendance);
            }
            public async Task<List<Newjointsurveyattendancedetail>> GetAllattendance(int id)
            {
                return await _newLandJointSurveyRepository.GetAllattendance(id);
            }
            public async Task<bool> DeleteAttendance(int Id)
            {
                return await _newLandJointSurveyRepository.DeleteAttendance(Id);
            }
        //********* rpt survey report ***********
        public async Task<bool> SaveSurveyReport(Newjointsurveyreportdetail newjointsurveyreportdetail)
        {
            newjointsurveyreportdetail.CreatedBy = 1;
            newjointsurveyreportdetail.CreatedDate = DateTime.Now;
            newjointsurveyreportdetail.IsActive = 1;
            return await _newLandJointSurveyRepository.SaveSurveyReport(newjointsurveyreportdetail);
        }

        public async Task<List<Newjointsurveyreportdetail>> GetNewjointsurveyreportdetail(int id)
        {
            return await _newLandJointSurveyRepository.GetNewjointsurveyreportdetail(id);
        }
        public async Task<bool> DeleteSurveyReport(int Id)
        {
            return await _newLandJointSurveyRepository.DeleteSurveyReport(Id);
        }

        public async Task<Newjointsurveyreportdetail> GetNewjointsurveyreportdetailFilePath(int Id)
        {
            return await _newLandJointSurveyRepository.GetNewjointsurveyreportdetailFilePath(Id);
        }
        public async Task<Newjointsurveyreportdetail> GetUploadDocumentFilePath(int Id)
        {
            return await _newLandJointSurveyRepository.GetUploadDocumentFilePath(Id);
        }
        public async Task<Newlandkhasra> FetchSingleKhasraResult(int? khasraId)
        {
            return await _newLandJointSurveyRepository.FetchSingleKhasraResult(khasraId);
        }


    }
    }
