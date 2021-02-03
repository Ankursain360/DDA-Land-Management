using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;

namespace Libraries.Repository.IEntityRepository
{
    public interface IDataStorageRepository : IGenericRepository<Datastoragedetails>
    {
        Task<List<Datastoragedetails>> GetDataStorageDetails();
        Task<bool> Any(int id, string name);
        Task<PagedResult<Datastoragedetails>> GetPagedDataStorageDetails(DataStorgaeDetailsSearchDto model);
        Task<List<Almirah>> GetAlmirahs();
        Task<List<Row>> GetRows();
        Task<List<Column>> GetColumns();
        Task<List<Bundle>> GetBundles();
        Task<List<Locality>> GetLocalities();
        Task<List<Department>> GetDepartment();
        Task<List<Branch>> GetBranch();
        Task<List<Zone>> GetZones();
        Task<List<Scheme>> GetSchemes();
        Task<bool> SaveDetailsOfPartFile(List<Datastoragepartfilenodetails> datastoragepartfilenodetails);

        //Task<PagedResult<Datastoragedetails>> GetFileStatusReportData(FileStatusReportSearchDto fileStatusReportSearchDto);
        Task<List<FileStatusReportListDataDto>> GetPagedFileStatusReportData(FileStatusReportSearchDto fileStatusReportSearchDto, int UserId);

        Task<List<ListofTotalFileReportListDataDto>> GetPagedListofReportFile(ListOfTotalFilesReportUserWiseSearchDto model,int UserId);

        // ********* DISPLAY LABEL**********

        Task<PagedResult<Datastoragedetails>> GetPagedDisplayLabel(DisplayLabelSearchDto model);
        
        Task<Datastoragedetails> FetchPrintLabel(int id);
    }
}
