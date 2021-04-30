using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;

namespace Libraries.Service.IApplicationService
{
    public interface IDataStorageService : IEntityService<Datastoragedetails>
    {
        Task<List<Datastoragedetails>> GetAllDataStorageDetail();
        Task<List<Datastoragedetails>> GetDataStorageDetailsUsingReport();

        Task<bool> Update(int id, Datastoragedetails dataStorageDetails);
        Task<bool> Create(Datastoragedetails dataStorageDetails);

        Task<bool> SaveDetailsOfPartFile(List<Datastoragepartfilenodetails> datastoragepartfilenodetails);


        Task<Datastoragedetails> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<bool> CheckUniqueName(int id, string dataStorageDetails);

        Task<PagedResult<Datastoragedetails>> GetPagedDataStorageDetails(DataStorgaeDetailsSearchDto model);
        int? GetDepartmentIdFromProfile(int userId);

        //Task<PagedResult<Datastoragedetails>> GetFileStatusReportData(FileStatusReportSearchDto fileStatusReportSearchDto);
        Task<List<FileStatusReportListDataDto>> GetPagedFileStatusReportData(FileStatusReportSearchDto fileStatusReportSearchDto, int UserId);

        Task<List<Almirah>> GetAlmirahs();
        Task<List<Row>> GetRows();
        Task<List<Department>> GetDepartments();
        Task<List<Column>> GetColumns();
        Task<List<Bundle>> GetBundles();
        Task<List<Locality>> GetLocalities();
        Task<List<Department>> GetDepartment(int? roleId, int? userDepartmentId);
        Task<List<Branch>> GetBranch();

        Task<List<Zone>> GetZones();

        //   Task<List<Scheme>> GetSchemes();

        Task<List<Datastoragepartfilenodetails>> GetDetailsOfPartFileDetails(int encroachmentId);
        Task<List<Schemefileloading>> GetSchemesFileLoading();

        Task<bool> DeleteDataStoragePartFile(int Id);

        Task<List<ListofTotalFileReportListDataDto>> GetPagedListofReportFile(ListOfTotalFilesReportUserWiseSearchDto model, int UserId);

        Task<List<ListofTotalDocReportListDataDto>> GetPagedListofReportDoc(ListOfTotalDocReportUserWiseSearchDto model, int UserId);
        Task<List<SearchByParticularListDataDto>> GetPagedListofSearchByParticular(SearchByParticularSearchDto model, int UserId);
        Task<List<SearchByParticularFileHistoryListDataDto>> GetPagedListofFileHistory(SearchByParticularFileHistorySearchDto model);
        Task<List<SearchByParticularDocListDataDto>> GetPagedListofSearchByParticularDoc(SearchByParticularDocSearchDto model, int UserId);
        Task<List<SearchByParticularDocHistoryListDataDto>> GetPagedListofDocHistory(SearchByParticularDocHistorySearchDto model);
        Task<Datastoragedetails> GetDatastorageListName(int id);
        // ********* DISPLAY LABEL**********

        Task<PagedResult<Datastoragedetails>> GetPagedDisplayLabel(DisplayLabelSearchDto model);
        Task<Datastoragedetails> FetchPrintLabel(int id);

        Task<List<Datastoragedetails>> GetDataStorageDetails();



    }
}
