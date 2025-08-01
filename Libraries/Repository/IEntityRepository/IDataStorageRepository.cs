﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;

namespace Libraries.Repository.IEntityRepository
{
    public interface IDataStorageRepository : IGenericRepository<Datastoragedetails>
    {
        Task<List<Datastoragedetails>> GetDataStorageDetails();
        Task<List<Datastoragedetails>> GetAllDataStorageDetailsList(DataStorgaeDetailsSearchDto model);
        Task<List<Datastoragedetails>> GetAllDisplayLabelList(DisplayLabelSearchDto model);
        Task<bool> Any(int id, string name);
        Task<PagedResult<Datastoragedetails>> GetPagedDataStorageDetails(DataStorgaeDetailsSearchDto model);
        Task<List<Almirah>> GetAlmirahs();
        Task<List<Row>> GetRows();
        Task<List<Column>> GetColumns();
        Task<List<Bundle>> GetBundles();
        Task<List<Locality>> GetLocalities();
        Task<List<Department>> GetDepartment(int? roleId, int? userDepartmentId);
        Task<List<Branch>> GetBranch();
        Task<List<Zone>> GetZones();
        Task<List<Scheme>> GetSchemes();
        Task<List<Department>> GetDepartments();
        Task<bool> SaveDetailsOfPartFile(List<Datastoragepartfilenodetails> datastoragepartfilenodetails);
         
        //Task<PagedResult<Datastoragedetails>> GetFileStatusReportData(FileStatusReportSearchDto fileStatusReportSearchDto);
        Task<List<FileStatusReportListDataDto>> GetPagedFileStatusReportData(FileStatusReportSearchDto fileStatusReportSearchDto, int UserId);

        Task<List<ListofTotalFileReportListDataDto>> GetPagedListofReportFile(ListOfTotalFilesReportUserWiseSearchDto model, int UserId);
        Task<List<ListofTotalDocReportListDataDto>> GetPagedListofReportDoc(ListOfTotalDocReportUserWiseSearchDto model, int UserId);
       
        Task <List<SearchByParticularListDataDto>> GetPagedListofSearchByParticular(SearchByParticularSearchDto model, int UserId);
        Task<List<SearchByParticularFileHistoryListDataDto>> GetPagedListofFileHistory(SearchByParticularFileHistorySearchDto model);
        Task<List<SearchByParticularDocListDataDto>> GetPagedListofSearchByParticularDoc(SearchByParticularDocSearchDto model, int UserId);
        Task<List<SearchByParticularDocHistoryListDataDto>> GetPagedListofDocHistory(SearchByParticularDocHistorySearchDto model);
        Task<Datastoragedetails> GetDatastorageListName(int id);

        int? GetDepartmentIdFromProfile(int userId);


        // ********* DISPLAY LABEL**********

        Task<PagedResult<Datastoragedetails>> GetPagedDisplayLabel(DisplayLabelSearchDto model);
        Task<Datastoragedetails> FetchPrintLabel(int id);
        Task<List<Schemefileloading>> GetSchemesFileLoading();

        Task<bool> DeleteDataStoragePartFile(int Id);

        Task<List<Datastoragepartfilenodetails>> GetDetailsOfPartFileDetails(int encroachmentId);
    }
}
