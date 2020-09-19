using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.Common;


namespace Libraries.Service.IApplicationService
{
    public interface IPropertyRegistrationService : IEntityService<Propertyregistration>
    {
        Task<List<Classificationofland>> GetClassificationOfLandDropDownList();
        Task<List<Zone>> GetZoneDropDownList();
        Task<List<Locality>> GetLocalityDropDownList();
        Task<List<Landuse>> GetLandUseDropDownList();
        Task<List<Disposaltype>> GetDisposalTypeDropDownList();
        Task<List<Propertyregistration>> GetAllPropertyregistration(int UserId);

        Task<bool> Update(int id, Propertyregistration propertyregistration); // To Upadte Particular data added by renu

        Task<bool> Create(Propertyregistration propertyregistration);

        Task<Propertyregistration> FetchSingleResult(int id);  // To fetch Particular data added by renu

        Task<bool> Delete(int id);    // To Delete Data  added by renu

        Task<bool> CheckDeleteAuthority(int id);
        string GetFile(int id);
        string GetGeoFile(int id);
        Task<List<Department>> GetDepartmentDropDownList();
        Task<List<Propertyregistration>> GetPropertyRegisterationReportData(int department, int landUse, int litigation, int encroached);
        Task<List<Division>> GetDivisionDropDownList();
    }
}
