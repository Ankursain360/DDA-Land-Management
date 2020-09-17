using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Repository.Common;


namespace Libraries.Repository.IEntityRepository
{
    public interface IPropertyRegistrationRepository : IGenericRepository<Propertyregistration>
    {
        Task<List<Classificationofland>> GetClassificationOfLandDropDownList();
        Task<List<Zone>> GetZoneDropDownList();
        Task<List<Locality>> GetLocalityDropDownList();
        Task<List<Landuse>> GetLandUseDropDownList();
        Task<List<Disposaltype>> GetDisposalTypeDropDownList();
        Task<List<Propertyregistration>> GetAllPropertyregistration(int UserId);
        string GetFile(int id);
        string GetGeoFile(int id);
        Task<bool> CheckDeleteAuthority(int id);
        Task<List<Department>> GetDepartmentDropDownList();
        Task<List<Propertyregistration>> GetPropertyRegisterationReportData(int department, int landUse, int litigation, int encroached);
    }
}