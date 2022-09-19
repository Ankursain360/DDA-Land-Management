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

        int GetCourtCaseByName(string name); 
        int GetCaseStatusByName(string name);
        int GetZoneByName(string name);
        int GetVillgeByName(string name);
        Task<Legalmanagementsystem> fetchLegalRecord(int id);
        Task<bool> UpdateBulkUploadFile(int id, Legalmanagementsystem legalmanagementsystem);
        Task<List<Zone>> GetZoneList();
        Task<List<Acquiredlandvillage>> GetAcquiredlandvillageList();
        Task<List<Khasra>> GetKhasralist(int acquiredVillageId);

        //********CourtCaseMapping************//
        Task<bool> SaveDetails(Courtcasesmapping courtCaseDetails);
        Task<Courtcasesmapping> fetchSingleRecord(int id);
        Task<List<Courtcasesmapping>> GetvillageKhasraDetails(int id);
        Task<bool> Deleteddl(int Id);
        Task<bool> Saveddl(Courtcasesmapping data);
        Task<List<Locality>> GetLocalityList(int zoneId);
        Task<List<Locality>> GetAllLocalityList();
        Task<List<Casestatus>> GetCasestatusList();
        Task<List<Courttype>> GetCourttypeList();
        Task<List<Legalmanagementsystem>> GetFileNoList();
        Task<List<Legalmanagementsystem>> GetCourtCaseNoList(int filenoId);
        Task<PagedResult<Legalmanagementsystem>> GetPagedLegalReport(LegalReportSearchDto model);
        Task<PagedResult<Legalmanagementsystem>> GetPagedLegalReportForDownload(LegalReportSearchDto model);


        Task<PagedResult<Legalmanagementsystem>> GetLegalmanagementsystemReportData(HearingReportSearchDto hearingReportSearchDto);
        Task<List<Legalmanagementsystem>> GetLegalmanagementsystemList();
        string GetDownload(int id);
        string GetDocDownload(int id);
        string GetJDocDownload(int id);
        Task<PagedResult<Legalmanagementsystem>> GetPagedLegalmanagementsystem(LegalManagementSystemSearchDto model);

        //Task<bool> AnyCode(int id, string name);
        Task<bool> CheckUniqueName(int id, string legalmanagementsystem);
        Task<List<Legalmanagementsystem>> GetAllLegalmanagementsystem();
        Task<List<Legalmanagementsystem>> getlegalmanagementlist();
        Task<Legalmanagementsystem> FetchSingleResult(int id);
        Task<bool> Update(int id, Legalmanagementsystem legalmanagementsystem);
        Task<bool> Create(Legalmanagementsystem legalmanagementsystem);
        // leTask GetAllLegalmanagementsystem();
        Task<int> checkUniqueUpload(string fileno, string caseno );
        Task<bool> Delete(int id);
    }
}
