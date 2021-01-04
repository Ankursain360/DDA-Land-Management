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
        
        public async Task<List<Casestatus>> GetCasestatusList(int id)
        {
            var casestatusList = await _dbContext.Casestatus.Where(x => x.IsActive == 1).ToListAsync();
            return casestatusList;
        }
        public async Task<List<Courttype>> GetCourttypeList(int id)
        {
            var courttypeList = await _dbContext.Courttype.Where(x => x.IsActive == 1).ToListAsync();
            return courttypeList;
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
                    .Include(x => x.Zone)
                    .Include(x => x.Locality)
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


    }
}
