using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Libraries.Service.IApplicationService
{
   
    public interface ILegalmanagementsystemservice : IEntityService<Legalmanagementsystem>

    {
       
        Task<List<Zone>> GetZoneList();
        Task<List<Locality>> GetLocalityList(int zoneId);
        Task<List<Casestatus>> GetCasestatusList();
        Task<List<Courttype>> GetCourttypeList();
        Task<List<Legalmanagementsystem>> GetFileNoList();
        Task<List<Legalmanagementsystem>> GetCourtCaseNoList(int filenoId);
        Task<PagedResult<Legalmanagementsystem>> GetPagedLegalReport(LegalReportSearchDto model);
        Task<PagedResult<Legalmanagementsystem>> GetLegalmanagementsystemReportData(HearingReportSearchDto hearingReportSearchDto);
        Task<List<Legalmanagementsystem>> GetLegalmanagementsystemList();
        string GetDownload(int id);
        string GetDocDownload(int id);
        string GetJDocDownload(int id);
        Task<PagedResult<Legalmanagementsystem>> GetPagedLegalmanagementsystem(LegalManagementSystemSearchDto model);
       
        Task<bool> AnyCode(int id, string name);
        Task<List<Legalmanagementsystem>> GetAllLegalmanagementsystem();
        Task<Legalmanagementsystem> FetchSingleResult(int id);
        Task<bool> Update(int id, Legalmanagementsystem legalmanagementsystem);
        Task<bool> Create(Legalmanagementsystem legalmanagementsystem);
        // leTask GetAllLegalmanagementsystem();
        Task<bool> CheckUniqueName(int id, string legalmanagementsystem);
        Task<bool> Delete(int id);
    }
}
