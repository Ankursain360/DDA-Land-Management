using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;


namespace Libraries.Repository.IEntityRepository
{
    public interface IPropertyRegistrationRepository : IGenericRepository<Propertyregistration>
    {
        Task<List<Classificationofland>> GetClassificationOfLandDropDownList();
        Task<List<Zone>> GetZoneDropDownList(int DepartmentId);
        Task<List<Locality>> GetLocalityDropDownList(int zoneId);
        Task<List<Landuse>> GetLandUseDropDownList();
        Task<List<Disposaltype>> GetDisposalTypeDropDownList();
        Task<List<Propertyregistration>> GetAllPropertyregistration(int UserId);
        string GetFile(int id);
        string GetGeoFile(int id);
        Task<bool> CheckDeleteAuthority(int id);
        Task<List<Department>> GetDepartmentDropDownList();
        Task<PagedResult<Propertyregistration>> GetPropertyRegisterationReportData(PropertyRegisterationReportSearchDto model);
        Task<List<Division>> GetDivisionDropDownList(int zoneId);
        Task<List<Propertyregistration>> GetPrimaryListNoList(int divisionId);
        string GetDisposalFile(int id);
        string GetHandedOverFile(int id);
        string GetTakenOverFile(int id);
        Task<List<Propertyregistration>> GetRestoreLandReportData( int department, int zone, int division,int primaryListNo);
        Task<bool> InsertInDeletedProperty(Deletedproperty model);
        Task<bool> InsertInRestoreProperty(Restoreproperty model);
        Task<PagedResult<Propertyregistration>> GetPagedPropertyRegisteration(PropertyRegisterationSearchDto model, int UserId);
    }
}