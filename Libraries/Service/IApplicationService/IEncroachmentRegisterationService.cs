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
    public interface IEncroachmentRegisterationService : IEntityService<EncroachmentRegisteration>
    {
        Task<List<Zone>> GetAllZone(int departmentId); // To Get all data added by Praveen
        Task<List<Department>> GetAllDepartment(); // To Get all data added by Praveen
        Task<List<Locality>> GetAllLocalityList();//for demolition report -- ishu
        Task<bool> Update(int id, EncroachmentRegisteration encroachmentRegisteration); // To Upadte Particular data added by Praveen
        Task<bool> Create(EncroachmentRegisteration encroachmentRegisteration);
        Task<EncroachmentRegisteration> FetchSingleResult(int id);  // To fetch Particular data added by Praveen
        Task<bool> Delete(int id);    //To Delete Data added by Praveen
        Task<List<Locality>> GetAllLocalityList(int divisionId);
        Task<PagedResult<Watchandward>> GetPagedEncroachmentRegisteration(EncroachmentRegisterationDto model, int approved, int zoneId);
        Task<List<Division>> GetAllDivisionList(int zone);
        Task<List<EncroachmentRegisteration>> GetAllEncroachmentRegisteration();
        Task<List<Watchandward>> GetAllEncroachmentRegisterlist(int approved);
        Task<List<EncroachmentRegisteration>> GetAllEncroachmentRegisterlistForDownload(); 
          Task<List<Khasra>> GetAllKhasraList(int localityId);


        Task<List<Propertyregistration>> GetAllKhasraListFromPropertyInventory(int ZoneId,int DepartmentId);
        Task<bool> SaveDetailsOfEncroachment(DetailsOfEncroachment detailsOfEncroachment);
        Task<bool> DeleteDetailsOfEncroachment(int Id);
        Task<bool> SaveEncroachmentFirFileDetails(EncroachmentFirFileDetails encroachmentFirFileDetails);
        Task<bool> DeleteEncroachmentFirFileDetails(int Id);
        Task<bool> SaveEncroachmentPhotoFileDetails(EncroachmentPhotoFileDetails encroachmentPhotoFileDetails);
        Task<bool> DeleteEncroachmentPhotoFileDetails(int Id);
        Task<bool> SaveEncroachmentLocationMapFileDetails(EncroachmentLocationMapFileDetails encroachmentLocationMapFileDetails);
        Task<bool> DeleteEncroachmentLocationMapFileDetails(int Id);
        Task<List<DetailsOfEncroachment>> GetDetailsOfEncroachment(int encroachmentId);
        Task<EncroachmentPhotoFileDetails> GetEncroachmentPhotoFileDetails(int encroachmentId);
        Task<EncroachmentLocationMapFileDetails> GetEncroachmentLocationMapFileDetails(int encroachmentId);
        Task<EncroachmentFirFileDetails> GetEncroachmentFirFileDetails(int encroachmentId);
        Task<PagedResult<EncroachmentRegisteration>> GetEncroachmentReportData(EnchroachmentSearchDto enchroachmentSearchDto);
        Task<PagedResult<EncroachmentRegisteration>> GetEncroachmentRegisterationReportData(InspectionEncroachmentregistrationSearchDto inspectionEncroachmentregistrationSearchDto);
        Task<bool> UpdateBeforeApproval(int id, EncroachmentRegisteration encroachmentRegisterations);
        Task<PagedResult<EncroachmentRegisteration>> GetPagedDemolitionReport(DemolitionReportSearchDto model);
        Task<bool> RollBackEntry(int id);
        Task<bool> RollBackEntryEncroachmentLocationMapFileDetails(int id);
        Task<bool> RollBackEntryEncroachmentFirFileDetails(int id);
        Task<bool> RollBackEntryEncroachmentPhotoFileDetails(int id);
        Task<bool> RollBackEntryDetailsofEncroachmentRepeater(int id);
        Task<Zone> FetchSingleResultOnZoneList(int v);
    }
}
