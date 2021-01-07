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
    }
}
