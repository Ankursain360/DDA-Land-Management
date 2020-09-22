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
        //Task<bool> Restore(int id);  // to restore data added by ishu

       
        Task<bool> CheckDeleteAuthority(int id);
        string GetFile(int id);
        string GetGeoFile(int id);
        Task<List<Department>> GetDepartmentDropDownList();
        Task<List<Propertyregistration>> GetPropertyRegisterationReportData(int classificationofland, int department, int zone, int division, int locality, string plannedUnplannedLand, int mainLandUse, int litigation, int encroached);
        Task<List<Propertyregistration>> GetRestoreLandReportData(int department, int zone, int division);
          

        Task<List<Division>> GetDivisionDropDownList();
        string GetDisposalFile(int id);
        string GetHandedOverFile(int id);
        string GetTakenOverFile(int id);
    }
}
