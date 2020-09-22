using System.Collections.Generic;
using System.Threading.Tasks;
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
        Task<List<Propertyregistration>> GetPropertyRegisterationReportData(int classificationofland, int department, int zone, int division, int locality, string plannedUnplannedLand, int mainLandUse, int litigation, int encroached);
        Task<List<Division>> GetDivisionDropDownList(int zoneId);
        string GetDisposalFile(int id);
        string GetHandedOverFile(int id);
        string GetTakenOverFile(int id);
    }
}