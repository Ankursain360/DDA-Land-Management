using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
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
        Task<List<Locality>> GetLocalityDropDownList2(int divisionId);
        Task<List<Landuse>> GetLandUseDropDownList();
        Task<List<Disposaltype>> GetDisposalTypeDropDownList();
        Task<List<Propertyregistration>> GetAllPropertyregistration(int UserId);
        Task<List<Propertyregistration>> GetUnverifiedList(int UserId);
        Task<List<Propertyregistration>> GetAllUnverified(PropertyRegisterationSearchDto model, int UserId);
        Task<List<Propertyregistration>> GetAllPropertInventorylist(int UserId);
        Task<List<Propertyregistration>> GetAllPropertInventory(PropertyRegisterationSearchDto model, int UserId);
        Task<List<Propertyregistration>> GetAllDeletedPropertyList();
        string GetFile(int id);
        string GetGeoFile(int id);
        Task<List<Department>> GetDepartmentDropDownList();
        Task<PagedResult<Propertyregistration>> GetPropertyRegisterationReportData(PropertyRegisterationReportSearchDto model);
        Task<List<Division>> GetDivisionDropDownList(int zoneId);
        Task<List<Propertyregistration>> GetPrimaryListNoList(int divisionId);
        string GetDisposalFile(int id);
        string GetHandedOverFile(int id);
        string GetTakenOverFile(int id);
        Task<PagedResult<Propertyregistration>> GetRestoreLandReportData(PropertyRegisterationSearchDto model);
        Task<PagedResult<Propertyregistration>> GetRestorePropertyReportData(PropertyRegisterationSearchDto model);

        Task<bool> InsertInDeletedProperty(Deletedproperty model);
        Task<bool> InsertInRestoreProperty(Restoreproperty model);
        Task<PagedResult<Propertyregistration>> GetPagedPropertyRegisteration(PropertyRegisterationSearchDto model, int UserId);
        Task<List<Department>> GetTakenDepartmentDropDownList();
        Task<List<Department>> GetHandedDepartmentDropDownList();
        Task<List<Classificationofland>> GetClassificationOfLandDropDownListMOR();
        Task<PagedResult<Propertyregistration>> GetPagedPropertyRegisterationMOR(PropertyRegisterationSearchDto model, int userId);
        Task<List<Classificationofland>> GetClassificationOfLandDropDownListReport();
        string GetEncroachAtr(int id);
        string GetHandedOverCopyofOrderFile(int id);
        Task<bool> InsertInDisposedProperty(Disposedproperty model);
        Task<Disposedproperty> FetchSingleRecord(int id);
        Task<List<Propertyregistration>> GetKhasraReportList();
        Task<List<Propertyregistration>> GetAllRestorePropertyReportList();
        Task<List<Propertyregistration>> GetAllRestoreLandReportData();
        Task<List<Propertyregistration>> GetAllPropertyRegistrationReportList();
        Task<List<Propertyregistration>> GetAllPropertyRegistrationMORlist(int UserId);
        Task<PagedResult<Propertyregistration>> GetInventoryUnverifiedVerified(InvnentoryUnverifiedVerifiedSearchDto model, int userId,int? roleId);
        Task<PagedResult<Propertyregistration>> GetDeletedLandReportData(PropertyRegisterationSearchDto model);
        Task<List<Propertyregistration>> GetPrimaryListForAPI(int deptid, int zoneid, int divisionid);// for api added by renu
        string GetMobileNo(int UseId);

        Task<List<Landbankdetails>> GetLandBankdata(string landCategory);
        Task<List<LandDashboardDataDto>> GetLandDashboardData();
    }
}