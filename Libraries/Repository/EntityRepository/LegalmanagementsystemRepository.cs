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
            
            if ((name == "W.P.(C)") || (name == "Write Petition (Civil)") || (name == "W.P.(CRL)"))
            {
                File = 1;     //**********Writ Pitition ***************//
            }
            else if ((name == "SLP (Civil)") || (name == "SLP (Civil) CC") || (name == "SLP (Civil) D") || (name == "SLP (Criminal) CRLMP") || (name == "Diary No And Diary Yr (SLPD)"))
            {
                File = 2;   //**********SLP ***************//
            }
            else if ((name == "REVIEW PET.") || (name == "Review Petition (Civil)") || (name == "Review Petition (Civil) D"))
            {
                File = 3;   //**********Review***************//
            }
            else if (name == "Curative Petition (Civil)")
            {
                 File = 4;  //**********Curative***************//
            }
            else if ((name == "Contempt Petition") || (name == "Contempt Petition (Civil)") || (name == "Mediation Petition No") || (name == "Transfer Petition (Civil)"))
            {
                 File = 5;  //********** Pitition ***************//
            }
            else if ((name == "Appeal") || (name == "Appeal (THC)") || (name == "CA - CRIMINAL APPEAL") || (name == "Civil Appeal") || (name == "Civil Appeal D") ||
                    (name == "M C A - MISC. CIVIL APPEAL FOR CJ") ||
                    (name == "MCA DJ - MISC. CIVIL APPEAL DJ AND  ADJ") ||
                    (name == "MCA DJ - MISC. CIVIL APPEAL FOR DJ ADJ") ||
                    (name == " MCA DJ ADJ -MISC.CIVIL APPEAL FOR DJ ADJ") ||
                    (name == "MCA SCJ - MISC. CIVIL APPEAL FOR CJ") ||
                    (name == "MCA SCJ - MISC. CIVIL APPEAL FOR SCJ") ||
                    (name == "MCD APPL - MCD APPEAL") ||
                    (name == "R.C.A. - REGULAR CIVIL APPEAL CJ") ||
                    (name == "VAT APPEAL") ||
                    (name == "RCA CIVIL DJ ADJ - REG. CIVIL APPEAL FOR DJ ADJ") ||
                    (name == "RCA DJ - REGULAR CIVIL APPEAL FOR DJ") ||
                    (name == "RCA DJ - REG CIVIL APPEAL  DJ and ADJ") ||
                    (name == "RCA DJ - REG. CIVIL APPEAL FOR DJ ADJ") ||
                    (name == "RCA DJ - REG CIVIL APPEAL FOR DJ ADJ") ||
                    (name == "RCA SCJ - CIVIL APPEAL FOR CJ") ||
                    (name == "RCA SCJ - REGULAR CIVIL APPEAL FOR CJ") ||
                    (name == "RCA - REGULAR CIVIL APPEAL FOR DJ") ||
                    (name == "R.C.A. - REGULAR CIVIL APPEAL CJ") ||
                    (name == "RCA DJ - REGULAR CIVIL APPEAL DJ ADJ") ||
                    (name == "RCA - REGULAR CIVIL APPEAL  DJ ADJ")) 


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

            if ((name == "Delhi High Court"))
            {
                File = 1;     //**********high court ***************//
            }
            else if ((name == "Supreme Court"))
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

        //public async Task<PagedResult<Legalmanagementsystem>> GetPagedLegalReportForDownload(LegalReportSearchDto model)
        //{

        //    var data = await _dbContext.Legalmanagementsystem
        //                                .Include(x => x.CaseStatus)
        //                                .Include(x => x.CourtType)
        //                                .Include(x => x.Locality)
        //                                .Include(x => x.Zone)
        //        .Where(x => (x.Id == (model.FileNo == 0 ? x.Id : model.FileNo))
        //        && (x.Id == (model.CaseNo == 0 ? x.Id : model.CaseNo))
        //        && (x.ContemptOfCourt == (model.ContemptOfCourt == 0 ? x.ContemptOfCourt : model.ContemptOfCourt))
        //        && (x.CourtTypeId == (model.CourtType == 0 ? x.CourtTypeId : model.CourtType))
        //        && (x.CaseStatusId == (model.CaseStatus == 0 ? x.CaseStatusId : model.CaseStatus))
        //        && (x.ZoneId == (model.Zone == 0 ? x.ZoneId : model.Zone))
        //        && (x.LocalityId == (model.Locality == 0 ? x.LocalityId : model.Locality))
        //        && (x.StayInterimGranted == (model.StayInterimGranted == 0 ? x.StayInterimGranted : model.StayInterimGranted))
        //        && (x.Judgement == (model.Judgement == 0 ? x.Judgement : model.Judgement))
        //        && x.CreatedDate >= model.FromDate
        //        && x.CreatedDate <= model.ToDate)
        //        .OrderByDescending(x => x.Id)

        //        .GetPaged(model.PageNumber, model.PageSize);



        //    return data;


        //}
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
                     .Where(x => ((string.IsNullOrEmpty(model.name) || x.FileNo.Contains(model.name))))
                      //.OrderByDescending(s => s.IsActive == 1)
                      .GetPaged<Legalmanagementsystem>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Legalmanagementsystem
                     .Include(x => x.Zone)
                     .Include(x => x.Locality)
                     .Include(x => x.CaseStatus)
                      .Include(x => x.CourtType)
                     .Where(x => ((string.IsNullOrEmpty(model.name) || x.FileNo.Contains(model.name))))
                      .OrderBy(x => x.FileNo)
                      .GetPaged<Legalmanagementsystem>(model.PageNumber, model.PageSize);


                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Legalmanagementsystem
                            .Include(x => x.Zone)
                            .Include(x => x.Locality)
                            .Include(x => x.CaseStatus)
                             .Include(x => x.CourtType)
                            .Where(x => ((string.IsNullOrEmpty(model.name) || x.FileNo.Contains(model.name))))
                                .OrderBy(s => s.IsActive == 0)
                                 .GetPaged<Legalmanagementsystem>(model.PageNumber, model.PageSize);


                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Legalmanagementsystem
                     .Include(x => x.Zone)
                     .Include(x => x.Locality)
                     .Include(x => x.CaseStatus)
                      .Include(x => x.CourtType)
                     .Where(x => ((string.IsNullOrEmpty(model.name) || x.FileNo.Contains(model.name))))
                         .OrderByDescending(x => x.FileNo)
                        .GetPaged<Legalmanagementsystem>(model.PageNumber, model.PageSize);

                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Legalmanagementsystem
                     .Include(x => x.Zone)
                     .Include(x => x.Locality)
                     .Include(x => x.CaseStatus)
                      .Include(x => x.CourtType)
                     .Where(x => ((string.IsNullOrEmpty(model.name) || x.FileNo.Contains(model.name))))
                                .OrderByDescending(x => x.IsActive == 0)
                                 .GetPaged<Legalmanagementsystem>(model.PageNumber, model.PageSize);


                        break;
                }
            }
            return data;
        }

        public async Task<List<Legalmanagementsystem>> GetAllLegalmanagementsystem()
        {
            return await _dbContext.Legalmanagementsystem
                    .Include(x => x.Zone)
                    .Include(x => x.Locality)
                    .Include(x => x.CaseStatus)
                    .Include(x => x.CourtType)
                    .Where(x => x.IsActive == 1)
                    .ToListAsync();
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
