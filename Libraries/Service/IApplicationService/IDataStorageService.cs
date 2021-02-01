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
        //Task<PagedResult<Datastoragedetails>> GetFileStatusReportData(FileStatusReportSearchDto fileStatusReportSearchDto);
        Task<List<FileStatusReportListDataDto>> GetPagedFileStatusReportData(FileStatusReportSearchDto fileStatusReportSearchDto);
        Task<List<Almirah>> GetAlmirahs();
        Task<List<Row>> GetRows();
        Task<List<Column>> GetColumns();
        Task<List<Bundle>> GetBundles();
        Task<List<Locality>> GetLocalities();
        Task<List<Department>> GetDepartment();
        Task<List<Branch>> GetBranch();

        Task<List<Zone>> GetZones();

        Task<List<Scheme>> GetSchemes();
        Task<List<ListofTotalFileReportListDataDto>> GetPagedListofReportFile(ListOfTotalFilesReportUserWiseSearchDto model, int UserId);
    }
}
