using Dto.Master;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class LegalmanagementsystemRepository : GenericRepository<Legalmanagementsystem>, ILegalmanagementsystemRepository
    {
        public LegalmanagementsystemRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public int GetCaseStatusByName(string name)
        {

            int File = 0;
            
            if ((name == "W.P.(C)".ToLower()) || (name == "Write Petition (Civil)".ToLower()) || (name == "W.P.(CRL)".ToLower()))
            {
                File = 1;     //**********Writ Pitition ***************//
            }
            else if ((name == "SLP (Civil)".ToLower()) || (name == "SLP (Civil) CC".ToLower()) || (name == "SLP (Civil) D".ToLower()) || (name == "SLP (Criminal) CRLMP".ToLower()) || (name == "Diary No And Diary Yr (SLPD)".ToLower()))
            {
                File = 2;   //**********SLP ***************//
            }
            else if ((name == "REVIEW PET.".ToLower()) || (name == "Review Petition (Civil)".ToLower()) || (name == "Review Petition (Civil) D".ToLower()))
            {
                File = 3;   //**********Review***************//
            }
            else if (name == "Curative Petition (Civil)".ToLower())
            {
                 File = 4;  //**********Curative***************//
            }
            else if ((name == "Contempt Petition".ToLower()) || (name == "Contempt Petition (Civil)".ToLower()) || (name == "Mediation Petition No".ToLower()) || (name == "Transfer Petition (Civil)".ToLower()))
            {
                 File = 5;  //********** Pitition ***************//
            }
            else if ((name == "Appeal".ToLower()) || (name == "Appeal (THC)".ToLower()) || (name == "CA - CRIMINAL APPEAL".ToLower()) || (name == "Civil Appeal".ToLower()) || (name == "Civil Appeal D".ToLower()) ||
                    (name == "M C A - MISC. CIVIL APPEAL FOR CJ".ToLower()) ||
                    (name == "MCA DJ - MISC. CIVIL APPEAL DJ AND  ADJ".ToLower()) ||
                    (name == "MCA DJ - MISC. CIVIL APPEAL FOR DJ ADJ".ToLower()) ||
                    (name == " MCA DJ ADJ -MISC.CIVIL APPEAL FOR DJ ADJ".ToLower()) ||
                    (name == "MCA SCJ - MISC. CIVIL APPEAL FOR CJ".ToLower()) ||
                    (name == "MCA SCJ - MISC. CIVIL APPEAL FOR SCJ".ToLower()) ||
                    (name == "MCD APPL - MCD APPEAL".ToLower()) ||
                    (name == "R.C.A. - REGULAR CIVIL APPEAL CJ".ToLower()) ||
                    (name == "VAT APPEAL".ToLower()) ||
                    (name == "RCA CIVIL DJ ADJ - REG. CIVIL APPEAL FOR DJ ADJ".ToLower()) ||
                    (name == "RCA DJ - REGULAR CIVIL APPEAL FOR DJ".ToLower()) ||
                    (name == "RCA DJ - REG CIVIL APPEAL  DJ and ADJ".ToLower()) ||
                    (name == "RCA DJ - REG. CIVIL APPEAL FOR DJ ADJ".ToLower()) ||
                    (name == "RCA DJ - REG CIVIL APPEAL FOR DJ ADJ".ToLower()) ||
                    (name == "RCA SCJ - CIVIL APPEAL FOR CJ".ToLower()) ||
                    (name == "RCA SCJ - REGULAR CIVIL APPEAL FOR CJ".ToLower()) ||
                    (name == "RCA - REGULAR CIVIL APPEAL FOR DJ".ToLower()) ||
                    (name == "R.C.A. - REGULAR CIVIL APPEAL CJ".ToLower()) ||
                    (name == "RCA DJ - REGULAR CIVIL APPEAL DJ ADJ".ToLower()) ||
                    (name == "RCA - REGULAR CIVIL APPEAL  DJ ADJ".ToLower())) 


            {
                 File =6;  //**********Appeal***************//
            }
            else
            {
                 File = 7;  //**********Other ***************//
            }

            return File;
        }
        public int GetCourtTypeByName(string name)
        {
            int File = 0;

            if ((name == "Delhi High Court".ToLower().Trim()))
            {
                File = 1;     //**********high court ***************//
            }
            else if ((name == "Supreme Court".ToLower().Trim()))
            {
                File = 3;    //**********Supreme court ***************//

            }
            else
            {
                File = 2;  //*********District court ***************//
            }


            return File;
        }
        public int GetZoneByName(string name)
        {
            var file = (from n in _dbContext.Zone where n.Name.Contains(name.ToUpper().Trim()) select n.Id).FirstOrDefault();
            return file;
        }
        public int GetVillgeByName(string name)
        {
            var file = (from n in _dbContext.Locality where n.Name.Contains(name.ToUpper().Trim()) select n.Id).FirstOrDefault();
            return file;
        }
        public async Task<Legalmanagementsystem> fetchLegalAllRecord(int id)
        {
            return await _dbContext.Legalmanagementsystem
                                   .Include(x => x.CaseStatus)
                                   .Include(x => x.CourtType)
                                   .Include(x => x.Zone)
                                   .Include(x => x.Locality)
                                   .Where(x => x.Id == id && x.IsActive == 1).FirstOrDefaultAsync();
                                     
                                       
            
        }
        public async Task<List<Zone>> GetZoneList()
        {
            var zoneList = await _dbContext.Zone.Where(x => x.IsActive == 1).ToListAsync();
            return zoneList;
        }

        public async Task<List<Casestatus>> GetCasestatusList()
        {
            var casestatusList = await _dbContext.Casestatus.Where(x => x.IsActive == 1).ToListAsync();
            return casestatusList;
        }
        public async Task<List<Courttype>> GetCourttypeList()
        {
            var courttypeList = await _dbContext.Courttype.Where(x => x.IsActive == 1).ToListAsync();
            return courttypeList;
        }
        public async Task<bool> AnyCode(int id, string code)
        {
            return await _dbContext.Legalmanagementsystem.AnyAsync((t => t.Id != id && t.FileNo.ToLower() == code.ToLower()));
        }
        public async Task<int> checkUniqueUpload(string fileno, string caseno)
        {
            // return await _dbContext.Legalmanagementsystem.Select((x => x.FileNo.Trim().ToLower() == fileno || x.CourtCaseNo.Trim().ToLower() == caseno)).FirstOrDefaultAsync();
            return await _dbContext.Legalmanagementsystem.AsNoTracking().Where(x => x.FileNo == fileno.Trim() && x.CourtCaseNo == caseno.Trim()).Select(x=>x.Id).FirstOrDefaultAsync();
        }
        public async Task<List<Locality>> GetLocalityList(int zoneId)
        {
            List<Locality> localityList = await _dbContext.Locality.Where(x => x.ZoneId == zoneId && x.IsActive == 1).ToListAsync();

            return localityList;
        }
        public async Task<List<Acquiredlandvillage>> GetAcquiredlandvillageList()
        {

            var data = (from d in _dbContext.Acquiredlandvillage
                         join c in _dbContext.Khasra
                         on d.Id equals c.AcquiredlandvillageId
                         select new { d.Id, d.Name }).Distinct();
            var list = await data.ToListAsync();
            List<Acquiredlandvillage> model = data
                        .Select(o => new Acquiredlandvillage
                        {
                            Id = o.Id,
                            Name = o.Name
                        }).ToList();
            return model;
        }
        public async Task<List<Khasra>> GetKhasralist(int acquiredVillageId)
        {
            var data = await _dbContext.Khasra.Where(x => x.AcquiredlandvillageId == acquiredVillageId && x.IsActive == 1).ToListAsync();
            List<Khasra> list = data
                        .Select(o => new Khasra
                        {
                            Id = o.Id,
                            Name = o.RectNo+" // " + o.Name
                        }).ToList();
            return list;
        }
        public async Task<List<Locality>> GetAllLocalityList()
        { 
             List < Locality > List = await _dbContext.Locality.Where(x => x.IsActive == 1).ToListAsync();
            return List;
        }
        public async Task<List<Legalmanagementsystem>> GetFileNoList()
        {
            var fileNoList = await _dbContext.Legalmanagementsystem.Where(x => x.IsActive == 1).ToListAsync();
            return fileNoList;
        }
        public async Task<List<Legalmanagementsystem>> GetCourtCaseNoList(int filenoId)
        {
            List<Legalmanagementsystem> caseNoList = await _dbContext.Legalmanagementsystem.Where(x => x.Id == filenoId).ToListAsync();

            return caseNoList;
        }

        public async Task<PagedResult<Legalmanagementsystem>> GetPagedLegalReportForDownload(LegalReportSearchDto model)
        {

            var data = await _dbContext.Legalmanagementsystem
                                        .Include(x => x.CaseStatus)
                                        .Include(x => x.CourtType)
                                        .Include(x => x.Locality)
                                        .Include(x => x.Zone)
                .Where(x => (x.Id == (model.FileNo == 0 ? x.Id : model.FileNo))
                && (x.Id == (model.CaseNo == 0 ? x.Id : model.CaseNo))
                && (x.ContemptOfCourt == (model.ContemptOfCourt == 0 ? x.ContemptOfCourt : model.ContemptOfCourt))
                && (x.CourtTypeId == (model.CourtType == 0 ? x.CourtTypeId : model.CourtType))
                && (x.CaseStatusId == (model.CaseStatus == 0 ? x.CaseStatusId : model.CaseStatus))
                && x.HearingDate == model.HearingDate
                && x.NextHearingDate == model.NextHearingDate
                && (string.IsNullOrEmpty(model.Subject)||x.Subject.Contains(model.Subject))
                &&(string.IsNullOrEmpty(model.CaseTitle) || x.CourtCaseTitle.Contains(model.CaseTitle))
                &&(string.IsNullOrEmpty(model.LastDecision)||x.LastDecision.Contains(model.LastDecision))
                &&(string.IsNullOrEmpty(model.CaseType)|| x.CaseType.Contains(model.CaseType))
                &&(string.IsNullOrEmpty(model.InFavour)|| x.InFavour.Contains(model.InFavour))
                &&(string.IsNullOrEmpty(model.PanelLawyer)|| x.PanelLawyer.Contains(model.PanelLawyer))
                &&(string.IsNullOrEmpty(model.Remarks)|| x.Remarks.Contains(model.Remarks))
                 && (string.IsNullOrEmpty(model.LMFileNO) || x.LMFileNO.Contains(model.LMFileNO))
                && (string.IsNullOrEmpty(model.BriefDetailsOfDescription) || x.BriefDetailsOfDescription.Contains(model.BriefDetailsOfDescription))
                && (x.ZoneId == (model.Zone == 0 ? x.ZoneId : model.Zone))
                && (x.LocalityId == (model.Locality == 0 ? x.LocalityId : model.Locality))
                && (x.StayInterimGranted == (model.StayInterimGranted == 0 ? x.StayInterimGranted : model.StayInterimGranted))
                && (x.Judgement == (model.Judgement == 0 ? x.Judgement : model.Judgement))
                && x.CreatedDate >= model.FromDate
                && x.CreatedDate <= model.ToDate)
                .OrderByDescending(x => x.Id)

                .GetPaged(model.PageNumber, model.PageSize);



            return data;


        }
        public async Task<PagedResult<Legalmanagementsystem>> GetPagedLegalReport(LegalReportSearchDto model)
        {

            var data = await _dbContext.Legalmanagementsystem
                                        .Include(x => x.CaseStatus)
                                        .Include(x => x.CourtType)
                                        .Include(x => x.Locality)
                                        .Include(x => x.Zone)
                .Where(x => (x.Id == (model.FileNo == 0 ? x.Id : model.FileNo))
                && (x.Id == (model.CaseNo == 0 ? x.Id : model.CaseNo))
                && (x.ContemptOfCourt == (model.ContemptOfCourt == 0 ? x.ContemptOfCourt : model.ContemptOfCourt))
                && (x.CourtTypeId == (model.CourtType == 0 ? x.CourtTypeId : model.CourtType))
                && (x.CaseStatusId == (model.CaseStatus == 0 ? x.CaseStatusId : model.CaseStatus))
                && (x.ZoneId == (model.Zone == 0 ? x.ZoneId : model.Zone))
                && (x.LocalityId == (model.Locality == 0 ? x.LocalityId : model.Locality))
                && (x.StayInterimGranted == (model.StayInterimGranted == 0 ? x.StayInterimGranted : model.StayInterimGranted))
                && (x.Judgement == (model.Judgement == 0 ? x.Judgement : model.Judgement))
                && x.CreatedDate >= (model.FromDate == null ? x.CreatedDate : model.FromDate)
                && x.CreatedDate <= (model.ToDate == null ? x.CreatedDate : model.ToDate))
                .OrderByDescending(x => x.Id)

                .GetPaged(model.PageNumber, model.PageSize);


            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("DATE"):
                        data.Results = data.Results.OrderBy(x => x.HearingDate).ToList();
                        break;
                    case ("FILE"):
                        data.Results = data.Results.OrderBy(x => x.FileNo).ToList();
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("DATE"):
                        data.Results = data.Results.OrderByDescending(x => x.HearingDate).ToList();
                        break;
                    case ("FILE"):
                        data.Results = data.Results.OrderByDescending(x => x.FileNo).ToList();
                        break;
                }
            }

            return data;
        }
        public async Task<List<Legalmanagementsystem>> GetLegalmanagementsystemList()
        {
            var legalmanagementsytemlist = await _dbContext.Legalmanagementsystem.Where(x => x.IsActive == 1).ToListAsync();
            return legalmanagementsytemlist;
        }
        public async Task<PagedResult<Legalmanagementsystem>> GetLegalmanagementsystemReportData(HearingReportSearchDto hearingReportSearchDto)
        {
            var data = await _dbContext.Legalmanagementsystem
                    .Where(x => x.HearingDate >= (hearingReportSearchDto.hearingDate.HasValue ? hearingReportSearchDto.hearingDate : x.HearingDate)
                    && x.HearingDate <= (hearingReportSearchDto.nextHearingDate.HasValue ? hearingReportSearchDto.nextHearingDate : x.HearingDate))
                    .OrderByDescending(x => x.Id)
                    .GetPaged(hearingReportSearchDto.PageNumber, hearingReportSearchDto.PageSize);


            int SortOrder = (int)hearingReportSearchDto.SortOrder;
            if (SortOrder == 1)
            {
                switch (hearingReportSearchDto.SortBy.ToUpper())
                {
                    case ("HEARINGDATE"):
                        data.Results = data.Results.OrderBy(x => x.HearingDate).ToList();
                        break;
                    case ("FILENO"):
                        data.Results = data.Results.OrderBy(x => x.FileNo).ToList();
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (hearingReportSearchDto.SortBy.ToUpper())
                {
                    case ("HEARINGDATE"):
                        data.Results = data.Results.OrderByDescending(x => x.HearingDate).ToList();
                        break;
                    case ("FILENO"):
                        data.Results = data.Results.OrderByDescending(x => x.FileNo).ToList();
                        break;
                }
            }

            return data;
        }

        /*************************************** chnges for Module ********************************/
        public async Task<PagedResult<Legalmanagementsystem>> GetPagedLegalmanagementsystem(LegalManagementSystemSearchDto model)
        {

            var data = await _dbContext.Legalmanagementsystem
                     .Include(x => x.Zone)
                     .Include(x => x.Locality)
                     .Include(x => x.CaseStatus)
                      .Include(x => x.CourtType)
                      
                     .Where(x => (string.IsNullOrEmpty(model.fileNo) || x.FileNo.Contains(model.fileNo))
                          &&(string.IsNullOrEmpty(model.lmfileno)|| x.LMFileNO.Contains(model.lmfileno))
                          && (string.IsNullOrEmpty(model.courtCaseNo)||x.CourtCaseNo.Contains(model.courtCaseNo))
                          &&(string.IsNullOrEmpty(model.courtType)||x.CourtType.CourtType.Trim().Contains(model.courtType))
                          && (string.IsNullOrEmpty(model.courtCaseTitle) || x.CourtCaseTitle.Contains(model.courtCaseTitle))
                          && (string.IsNullOrEmpty(model.caseStatus) || x.CaseStatus.CaseStatus.Contains(model.caseStatus))
                     )
                      //.OrderByDescending(s => s.IsActive == 1)
                      .GetPaged<Legalmanagementsystem>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("FILE"):
                        data.Results = data.Results.OrderBy(x => x.FileNo).ToList();
                        break;
                    case ("STATUS"):
                        data.Results = data.Results.OrderByDescending(x => x.IsActive).ToList();
                        break;
                      
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("FILE"):
                        data.Results = data.Results.OrderByDescending(x => x.FileNo).ToList();
                        break;
                    case ("STATUS"):
                        data.Results = data.Results.OrderBy(x => x.IsActive).ToList();
                        break;
                                       }
            }
            return data;
        }

        public async Task<List<Legalmanagementsystem>> GetAllLegalmanagementsystem()
        {
            return await _dbContext.Legalmanagementsystem
                    .Where(x => x.IsActive == 1)
                    .Include(x => x.Zone)
                    .Include(x => x.Locality)
                    .Include(x => x.CaseStatus)
                    .Include(x => x.CourtType)
                    .ToListAsync(); 
        }
        public async Task<List<Legalmanagementsystem>> getlegalmanagementlist(LegalManagementSystemSearchDto model)
        {            
            var data = await _dbContext.Legalmanagementsystem
                                         .Include(x => x.CaseStatus)
                                         .Include(x => x.CourtType)
                                         .Include(x => x.Locality)
                                         .Include(x => x.Zone)
                 .Where(x => (string.IsNullOrEmpty(model.fileNo) || x.FileNo.Contains(model.fileNo))
                  && (string.IsNullOrEmpty(model.lmfileno) || x.LMFileNO.Contains(model.lmfileno))
                  && (string.IsNullOrEmpty(model.courtCaseNo) || x.CourtCaseNo.Contains(model.courtCaseNo))
                  && (string.IsNullOrEmpty(model.courtType) || x.CourtType.CourtType.Trim().Contains(model.courtType))
                 && (string.IsNullOrEmpty(model.courtCaseTitle) || x.CourtCaseTitle.Contains(model.courtCaseTitle))
                 && (string.IsNullOrEmpty(model.caseStatus) || x.CaseStatus.CaseStatus.Contains(model.caseStatus))).ToListAsync();
            return data;

        }


        //********CourtCaseMapping************//
        public async Task<bool> SaveDetails(Courtcasesmapping courtCaseDetails)
        {
            _dbContext.Courtcasesmapping.Add(courtCaseDetails);
            var Result = await _dbContext.SaveChangesAsync();
           // _dbContext.Entry(courtCaseDetails).State = EntityState.Detached;
            return Result > 0 ? true : false;
        }
        public async Task<Courtcasesmapping> fetchSingleRecord(int id)
        {
            return await _dbContext.Courtcasesmapping.Where(x => x.LegalManagementId == id).FirstOrDefaultAsync();
        }
        public async Task<List<Courtcasesmapping>> GetvillageKhasraDetails(int id)
        {
            return await _dbContext.Courtcasesmapping.Where(x => x.LegalManagementId == id && x.IsActive == 1).ToListAsync();
        }
        public async Task<bool> Deleteddl(int Id)
        {
            _dbContext.RemoveRange(_dbContext.Courtcasesmapping.Where(x => x.LegalManagementId==Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<bool> Saveddl(Courtcasesmapping data)
        {
            _dbContext.Courtcasesmapping.Add(data);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public string GetDownload(int id)
        {
            var File = (from f in _dbContext.Legalmanagementsystem
                        where f.Id == id
                        select f.StayInterimGrantedDocument).First();

            return File;
        }
        public string GetDocDownload(int id)
        {
            var File = (from f in _dbContext.Legalmanagementsystem
                        where f.Id == id
                        select f.DocumentFilePath).First();

            return File;
        }
        public string GetJDocDownload(int id)
        {
            var File = (from f in _dbContext.Legalmanagementsystem
                        where f.Id == id
                        select f.JudgementFilePath).First();

            return File;
        }
    }
}
