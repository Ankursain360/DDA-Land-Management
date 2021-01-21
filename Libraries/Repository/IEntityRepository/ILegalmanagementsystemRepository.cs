using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Libraries.Repository.IEntityRepository
{
    public interface ILegalmanagementsystemRepository : IGenericRepository<Legalmanagementsystem>
    {
        Task<List<Zone>> GetZoneList();
        Task<List<Locality>> GetLocalityList (int zoneId);
        Task<List<Casestatus>> GetCasestatusList();
        Task<List<Courttype>> GetCourttypeList();

        Task<List<Legalmanagementsystem>> GetFileNoList();
        Task<List<Legalmanagementsystem>> GetCourtCaseNoList(int filenoId);

        Task<PagedResult<Legalmanagementsystem>> GetPagedLegalReport(LegalReportSearchDto model);
        Task<List<Legalmanagementsystem>> GetLegalmanagementsystemList();


        Task<PagedResult<Legalmanagementsystem>> GetLegalmanagementsystemReportData(HearingReportSearchDto hearingReportSearchDto);

        string GetDownload(int id);
        string GetDocDownload(int id);
        string GetJDocDownload(int id);
        Task<PagedResult<Legalmanagementsystem>> GetPagedLegalmanagementsystem(LegalManagementSystemSearchDto model);
        Task<bool> AnyCode(int id, string name);
        Task<List<Legalmanagementsystem>> GetAllLegalmanagementsystem();
    }
}
