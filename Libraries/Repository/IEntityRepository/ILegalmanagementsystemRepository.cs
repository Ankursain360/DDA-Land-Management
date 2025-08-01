﻿using Dto.Search;
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
        int GetCaseStatusByName(string name); 
        int GetCourtTypeByName(string name);
        int GetZoneByName(string name);
        int GetVillgeByName(string name);
        Task<Legalmanagementsystem> fetchLegalAllRecord(int id);
        Task<List<Zone>> GetZoneList();
        Task<List<Acquiredlandvillage>> GetAcquiredlandvillageList();
        Task<List<Khasra>> GetKhasralist(int acquiredVillageId);
        Task<List<Locality>> GetLocalityList (int zoneId);
        Task<List<Locality>> GetAllLocalityList();
        Task<List<Casestatus>> GetCasestatusList();
        Task<List<Courttype>> GetCourttypeList();

        Task<List<Legalmanagementsystem>> GetFileNoList();
        Task<List<Legalmanagementsystem>> GetCourtCaseNoList(int filenoId);
        Task<List<Legalmanagementsystem>> GetAllLegalReportList(LegalReportSearchDto model);

        Task<PagedResult<Legalmanagementsystem>> GetPagedLegalReport(LegalReportSearchDto model);

        Task<PagedResult<Legalmanagementsystem>> GetPagedLegalReportForDownload(LegalReportSearchDto model);
        Task<List<Legalmanagementsystem>> GetLegalmanagementsystemList();


        Task<PagedResult<Legalmanagementsystem>> GetLegalmanagementsystemReportData(HearingReportSearchDto hearingReportSearchDto);

        string GetDownload(int id);
        string GetDocDownload(int id);
        string GetJDocDownload(int id);
        Task<PagedResult<Legalmanagementsystem>> GetPagedLegalmanagementsystem(LegalManagementSystemSearchDto model);
        Task<bool> AnyCode(int id, string name);
        Task<int> checkUniqueUpload(string fileno , string caseno);
         Task<List<Legalmanagementsystem>> GetAllLegalmanagementsystem();
        Task<List<Legalmanagementsystem>> GetAllLegalReportDataList(HearingReportSearchDto hearingReportSearchDto);
        Task<List<Legalmanagementsystem>> getlegalmanagementlist(LegalManagementSystemSearchDto model);

        //********CourtCaseMapping************//
        Task<bool> SaveDetails(Courtcasesmapping courtCaseDetails);
        Task<Courtcasesmapping> fetchSingleRecord(int id);
        Task<List<Courtcasesmapping>> GetvillageKhasraDetails(int id);
        Task<bool> Deleteddl(int Id);
        Task<bool> Saveddl(Courtcasesmapping data);

    }
}
