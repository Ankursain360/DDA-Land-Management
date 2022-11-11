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
    public interface IDmsFileUploadService : IEntityService<Dmsfileupload>
    {
        Task<List<Department>> GetDepartmentList();
        Task<List<Propertyregistration>> GetKhasraNoList();
        Task<List<Locality>> GetLocalityList();
        Task<List<Documentcategory>> allcategoryList();
        Task<PagedResult<Dmsfileupload>> GetPagedDMSFileUploadList(DMSFileUploadSearchDto model);
        Task<bool> Create(Dmsfileupload dmsfileupload);
        Task<Dmsfileupload> FetchSingleResult(int id);
        Task<bool> Update(int id, Dmsfileupload dmsfileupload);
        Task<bool> Delete(int id, int userId);
        int GetLocalityByName(string name);
        int GetKhasraByName(string name);

        int GetZoneByName(string name);
        int GetVillageByName(string name);
        Task<bool> CheckUniqueName(int id, string fileNo);
        Task<PagedResult<Dmsfileupload>> GetPagedDMSRetriveFileReport(DMSRetriveFileSearchDto model);
        Task<Dmsfileright> GetDMSUserRights(int userId);
        Task<List<Dmsfileupload>> GetAllDMSFileUploadList();
        Task<List<Dmsfileupload>> GetAllDMSRetriveFileReportList(DMSRetriveFileSearchDto model);
        Task<List<Dmsfileupload>> GetAllDMSFileUploadList1(DMSFileUploadSearchDto model);
        Task<List<Zone>> allZoneList();
        Task<List<Village>> allVillageList(int? zoneid);
    }
}
