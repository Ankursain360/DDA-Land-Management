using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;


namespace Libraries.Service.IApplicationService
{
    public interface IPropertyRegistrationService : IEntityService<Propertyregistration>
    {
        Task<List<Classificationofland>> GetClassificationOfLandDropDownList();
        Task<List<Zone>> GetZoneDropDownList(int DepartmentId);
        Task<List<Zone>> GetZoneDropDownListForApi(int DepartmentId);
        Task<List<Locality>> GetLocalityDropDownList(int zoneId);
        Task<List<Propertyregistration>> GetPrimaryListNoList(int DivisionId); // added by ishu
        Task<List<Locality>> GetLocalityDropDownList2(int divisionId);// added by ishu
        Task<List<Landuse>> GetLandUseDropDownList();
        Task<List<Disposaltype>> GetDisposalTypeDropDownList();
        Task<List<Classificationofland>> GetClassificationOfLandDropDownListReport();
        Task<List<Propertyregistration>> GetAllPropertyregistration(int UserId);

        Task<bool> Update(int id, Propertyregistration propertyregistration); // To Upadte Particular data added by renu

        Task<bool> Update1(int id, Propertyregistration propertyregistration); // To Upadte Particular data added by Pankaj

        Task<bool> Create(Propertyregistration propertyregistration);

        Task<Propertyregistration> FetchSingleResult(int id);  // To fetch Particular data added by renu

        Task<bool> Delete(int id);    // To Delete Data  added by renu
        Task<bool> Restore(int id);  // to restore data added by ishu
        Task<List<Propertyregistration>> GetKhasraReportList();
        string GetFile(int id);
        Task<PagedResult<Propertyregistration>> GetInventoryUnverifiedVerified(InvnentoryUnverifiedVerifiedSearchDto model, int userId, int? roleId);
        string GetGeoFile(int id);
        Task<List<Department>> GetDepartmentDropDownList();
        Task<List<Department>> GetDepartmentDropDownListForApi();
        Task<PagedResult<Propertyregistration>> GetRestoreLandReportData(PropertyRegisterationSearchDto model);// added by ishu
        Task<PagedResult<Propertyregistration>> GetRestorePropertyReportData(PropertyRegisterationSearchDto model);// added by ishu
        Task<PagedResult<Propertyregistration>> GetPropertyRegisterationReportData(PropertyRegisterationReportSearchDto model);
        Task<PagedResult<Propertyregistration>> GetDeletedLandReportData(PropertyRegisterationSearchDto model);
        Task<List<Division>> GetDivisionDropDownList(int zoneId);
        Task<List<Division>> GetDivisionDropDownListForApi(int zoneId);
        Task<PagedResult<Propertyregistration>> GetPagedPropertyRegisterationMOR(PropertyRegisterationSearchDto model, int userId);
        string GetDisposalFile(int id);
        string GetHandedOverFile(int id);
        string GetTakenOverFile(int id);
        Task<bool> InsertInDeletedProperty(int id, Deletedproperty model);
        Task<PagedResult<Propertyregistration>> GetPagedPropertyRegisteration(PropertyRegisterationSearchDto model, int UserId);
        Task<List<Propertyregistration>> GetAllPropertInventory(PropertyRegisterationSearchDto model, int UserId);
        Task<List<Classificationofland>> GetClassificationOfLandDropDownListMOR();
        Task<bool> InsertInRestoreProperty(int id, Restoreproperty model);
        Task<List<Department>> GetTakenDepartmentDropDownList();
        Task<List<Department>> GetHandedDepartmentDropDownList();
        string GetEncroachAtr(int id);
        string GetHandedOverCopyofOrderFile(int id);
        Task<bool> DisposeDetails(int id, Disposedproperty model);
        Task<Disposedproperty> FetchSingleRecord(int id); 
        Task<bool> InsertInDisposedProperty(int id, Disposedproperty model);
        Task<bool> UpdatePropertyRegistrationForLandTransfer(int id, Propertyregistration propertyregistration);
        Task<List<Propertyregistration>> GetAllPropertInventorylist(int UserId);
        Task<List<Propertyregistration>> GetUnverifiedList(int UserId);
        Task<List<Propertyregistration>> GetAllUnverified(PropertyRegisterationSearchDto model, int UserId);
        Task<List<Propertyregistration>> GetAllDeletedPropertyList();
        Task<List<Propertyregistration>> GetAllDeletedLandReportDataList(PropertyRegisterationSearchDto model);
        Task<List<Propertyregistration>> GetAllRestoreLandReportData();
        Task<List<Propertyregistration>> GetAllRestoreLandReportDataList(PropertyRegisterationSearchDto model);
        Task<List<Propertyregistration>> GetAllPropertyRegistrationReportList();
        Task<List<Propertyregistration>> GetAllPropertyRegisterationReportDataList(PropertyRegisterationReportSearchDto model);
        Task<List<Propertyregistration>> GetAllRestorePropertyReportList();
        Task<List<Propertyregistration>> GetAllRestorePropertyReportDataList(PropertyRegisterationSearchDto model);
        Task<List<Propertyregistration>> GetAllPropertyRegistrationMORlist(PropertyRegisterationSearchDto model, int UserId);
        Task<List<Propertyregistration>> GetPrimaryListForAPI(int deptid, int zoneid, int divisionid); // for api added by renu
        Task<Propertyregistration> GetPropertyregistrationDetail(int id);
        string GetMobileNo(int UserId);

        Task<List<Landbankdetails>> GetLandBankdata(string landCategory);
        Task<List<LandDashboardDataDto>> GetLandDashboardData();
        Task<List<Awardplotdetails>> GetAwardData(string village,int category, string award);
        

    }
}
