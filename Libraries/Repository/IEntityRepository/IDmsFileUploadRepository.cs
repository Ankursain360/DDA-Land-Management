using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{

    public interface IDmsFileUploadRepository : IGenericRepository<Dmsfileupload>
    {
        int GetLocalityByName(string name);
        int GetKhasraByName(string name);

        int GetZoneByName(string name);
        int GetVillageByName(string name);
        Task<bool> Any(int id, string fileNo);
        Task<PagedResult<Dmsfileupload>> GetPagedDMSRetriveFileReport(DMSRetriveFileSearchDto model);
        Task<List<Department>> GetDepartmentList();
        Task<List<Dmsfileupload>> GetAllDMSRetriveFileReportList(DMSRetriveFileSearchDto model);
        Task<List<Dmsfileupload>> GetAllDMSFileUploadList();
        Task<List<Dmsfileupload>> GetAllDMSFileUploadList1(DMSFileUploadSearchDto model);
        Task<List<Propertyregistration>> GetKhasraNoList();
        Task<List<Zone>> allZoneList();
        Task<List<Documentcategory>> allcategoryList();
        Task<List<Village>> allVillageList(int? zoneid);
        Task<List<Locality>> GetLocalityList();
        Task<PagedResult<Dmsfileupload>> GetPagedDMSFileUploadList(DMSFileUploadSearchDto model);
        Task<Dmsfileupload> FetchSingleResult(int id);
        Task<Dmsfileright> GetDMSUserRights(int userId);
    }
}
