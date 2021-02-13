using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{

    public interface IDemandListDetailsRepository : IGenericRepository<Demandlistdetails>
    {
        int GetLocalityByName(string name);
        int GetKhasraByName(string name);
        Task<bool> Any(int id, string fileNo);
        Task<PagedResult<Dmsfileupload>> GetPagedDMSRetriveFileReport(DMSRetriveFileSearchDto model);
        Task<List<Department>> GetDepartmentList();
        Task<List<Propertyregistration>> GetKhasraNoList();
        Task<List<Locality>> GetLocalityList();
        Task<PagedResult<Dmsfileupload>> GetPagedDMSFileUploadList(DMSFileUploadSearchDto model);
        Task<Dmsfileupload> FetchSingleResult(int id);
        Task<Dmsfileright> GetDMSUserRights(int userId);
    }
}
