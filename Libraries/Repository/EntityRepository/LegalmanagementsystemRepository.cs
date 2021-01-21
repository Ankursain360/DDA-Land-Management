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

        public async Task<List<Locality>> GetLocalityList(int zoneId)
        {
            List<Locality> localityList = await _dbContext.Locality.Where(x => x.ZoneId == zoneId && x.IsActive == 1).ToListAsync();

            return localityList;
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
                    && x.CreatedDate >= model.FromDate
                    && x.CreatedDate <= model.ToDate)
                    .OrderByDescending(x => x.Id)

                    .GetPaged(model.PageNumber, model.PageSize);

            

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
                    .Where(x => x.HearingDate >= hearingReportSearchDto.hearingDate
                    && x.NextHearingDate <= hearingReportSearchDto.nextHearingDate)
                    .OrderByDescending(x => x.Id)

                    .GetPaged(hearingReportSearchDto.PageNumber, hearingReportSearchDto.PageSize);

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
