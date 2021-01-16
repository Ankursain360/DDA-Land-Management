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
  
    public class Legalmanagementsystemservice : EntityService<Legalmanagementsystem>, ILegalmanagementsystemservice
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILegalmanagementsystemRepository _legalmanagementsystemRepository;

        public Legalmanagementsystemservice(IUnitOfWork unitOfWork, ILegalmanagementsystemRepository legalmanagementsystemRepository)
        : base(unitOfWork, legalmanagementsystemRepository)
        {
            _unitOfWork = unitOfWork;
            _legalmanagementsystemRepository = legalmanagementsystemRepository;
        }

        // * *********** methods for legal report added by ishu ************
        public async Task<List<Zone>> GetZoneList()  
        {
            List<Zone> zoneList = await _legalmanagementsystemRepository.GetZoneList();
            return zoneList;
        }
        public async Task<List<Locality>> GetLocalityList(int zoneId)    
        {
            List<Locality> localityList = await _legalmanagementsystemRepository.GetLocalityList(zoneId);
            return localityList;
        }

       
        public async Task<List<Casestatus>> GetCasestatusList()
        {
            List<Casestatus> casestatusList = await _legalmanagementsystemRepository.GetCasestatusList();
            return casestatusList;
        }

        public async Task<List<Courttype>> GetCourttypeList()
        {
            List<Courttype> courttypeList = await _legalmanagementsystemRepository.GetCourttypeList();
            return courttypeList;
        }

        public async Task<List<Legalmanagementsystem>> GetFileNoList()   
        {
            List<Legalmanagementsystem> fileNoList = await _legalmanagementsystemRepository.GetFileNoList();
            return fileNoList;
        }
        public async Task<List<Legalmanagementsystem>> GetCourtCaseNoList(int filenoId)     
        {
            List<Legalmanagementsystem> caseNoList = await _legalmanagementsystemRepository.GetCourtCaseNoList(filenoId);
            return caseNoList;
        }
        public async Task<PagedResult<Legalmanagementsystem>> GetPagedLegalReport(LegalReportSearchDto model)
        {
            return await _legalmanagementsystemRepository.GetPagedLegalReport(model);
        }
        public async Task<PagedResult<Legalmanagementsystem>> GetLegalmanagementsystemReportData(HearingReportSearchDto hearingReportSearchDto)
        {
            return await _legalmanagementsystemRepository.GetLegalmanagementsystemReportData(hearingReportSearchDto);
        }
        public async Task<List<Legalmanagementsystem>> GetLegalmanagementsystemList()
        {
            List<Legalmanagementsystem> legalmanagementsytemlist = await _legalmanagementsystemRepository.GetLegalmanagementsystemList();
            return legalmanagementsytemlist;
        }
        public async Task<Legalmanagementsystem> FetchSingleResult(int id)
        {
            var result = await _legalmanagementsystemRepository.FindBy(a => a.Id == id);
            Legalmanagementsystem model = result.FirstOrDefault();
            return model;
        }
        public async Task<bool> Update(int id, Legalmanagementsystem legalmanagementsystem)
        {
            var result = await _legalmanagementsystemRepository.FindBy(a => a.Id == id);
            Legalmanagementsystem model = result.FirstOrDefault();
            model.FileNo = legalmanagementsystem.FileNo;
            model.CourtCaseNo = legalmanagementsystem.CourtCaseNo;
            model.CourtCaseTitle = legalmanagementsystem.CourtCaseTitle;
            model.Subject = legalmanagementsystem.Subject;
            model.HearingDate = legalmanagementsystem.HearingDate;
            model.NextHearingDate = legalmanagementsystem.NextHearingDate;
            model.ContemptOfCourt = legalmanagementsystem.ContemptOfCourt;
            model.CaseType = legalmanagementsystem.CaseType;
            // model.CourtType = legalmanagementsystem.CourtType;
            model.CourtTypeId = legalmanagementsystem.CourtTypeId;
            model.CaseStatusId = legalmanagementsystem.CaseStatusId;
            // model.CaseStatus = legalmanagementsystem.CaseStatus;
            model.LastDecision = legalmanagementsystem.LastDecision;
            model.ZoneId = legalmanagementsystem.ZoneId;
            model.LocalityId = legalmanagementsystem.LocalityId;
            model.InFavour = legalmanagementsystem.InFavour;
            model.PanelLawyer = legalmanagementsystem.PanelLawyer;
            //    model.StayInterimGrantedDocument = legalmanagementsystem.StayInterimGrantedDocument;
            model.StayInterimGrantedDocument = legalmanagementsystem.StayInterimGranted != null ? legalmanagementsystem.StayInterimGrantedDocument : model.StayInterimGrantedDocument;

            model.StayInterimGranted = legalmanagementsystem.StayInterimGranted;
            model.StayInterimGrantedRemarks = legalmanagementsystem.StayInterimGrantedRemarks;
            model.Judgement = legalmanagementsystem.Judgement;
            model.JudgementRemarks = legalmanagementsystem.JudgementRemarks;
            model.JudgementFilePath = legalmanagementsystem.JudgementFilePath != null ? legalmanagementsystem.JudgementFilePath : model.JudgementFilePath;
            //model.JudgementFilePath = legalmanagementsystem.JudgementFile != null ? legalmanagementsystem.JudgementFilePath : model.JudgementFilePath;
            //  model.DocumentFilePath = legalmanagementsystem.DocumentFile != null ? legalmanagementsystem.DocumentFilePath : model.DocumentFilePath;

            //  model.DocumentFilePath = legalmanagementsystem.DocumentFilePath;
            model.DocumentFilePath = legalmanagementsystem.DocumentFile != null ? legalmanagementsystem.DocumentFilePath : model.DocumentFilePath;
            model.Remarks = legalmanagementsystem.Remarks;
            model.IsActive = legalmanagementsystem.IsActive;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = 1;
            _legalmanagementsystemRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> Create(Legalmanagementsystem legalmanagementsystem)
        {
            legalmanagementsystem.CreatedBy = 1;
            legalmanagementsystem.IsActive = 1;
            legalmanagementsystem.CreatedDate = DateTime.Now;
            _legalmanagementsystemRepository.Add(legalmanagementsystem);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> CheckUniqueCode(int id, string code)
        {
            bool result = await _legalmanagementsystemRepository.AnyCode(id, code);
            return result;
        }
        public async Task<bool> Delete(int id)
        {
            var form = await _legalmanagementsystemRepository.FindBy(a => a.Id == id);
            Legalmanagementsystem model = form.FirstOrDefault();
            model.IsActive = 0;
            _legalmanagementsystemRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<List<Legalmanagementsystem>> GetLegalmanagementsystemUsingReppo()
        {
            return await _legalmanagementsystemRepository.GetAllLegalmanagementsystem();
        }
        public async Task<List<Legalmanagementsystem>> GetAllLegalmanagementsystem()
        {
            return await _legalmanagementsystemRepository.GetAllLegalmanagementsystem();
        }
        public async Task<bool> CheckUniqueName(int id, string legalmanagementsystem)
        {
            bool result = await _legalmanagementsystemRepository.AnyCode(id, legalmanagementsystem);
            return result;
        }

        public async Task<PagedResult<Legalmanagementsystem>> GetPagedLegalmanagementsystem(LegalManagementSystemSearchDto model)
        {
            return await _legalmanagementsystemRepository.GetPagedLegalmanagementsystem(model);
        }
        public string GetDownload(int id)
        {
            return _legalmanagementsystemRepository.GetDownload(id);
        }
        public string GetDocDownload(int id)
        {
            return _legalmanagementsystemRepository.GetDocDownload(id);
        }
        public string GetJDocDownload(int id)
        {
            return _legalmanagementsystemRepository.GetJDocDownload(id);
        }

        public Task<bool> AnyCode(int id, string name)
        {
            throw new NotImplementedException();
        }
    }
}
