using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IEncroachmentRegisterationRepository:IGenericRepository<EncroachmentRegisteration>
    {
        Task<List<EncroachmentRegisteration>> GetAllEncroachmentRegisteration();
        Task<List<Watchandward>> GetAllEncroachmentRegisterlist(int approved);
        Task<List<EncroachmentRegisteration>> GetAllEncroachmentRegisterlistForDownload();
        Task<List<Zone>> GetAllZone(int departmentId);
        Task<List<DetailsOfEncroachment>> GetDetailsOfEncroachment(int encroachmentId);
        Task<EncroachmentPhotoFileDetails> GetEncroachmentPhotoFileDetails(int encroachmentId);
        Task<EncroachmentLocationMapFileDetails> GetEncroachmentLocationMapFileDetails(int encroachmentId);
        Task<EncroachmentFirFileDetails> GetEncroachmentFirFileDetails(int encroachmentId);
        Task<bool> SaveDetailsOfEncroachment(DetailsOfEncroachment detailsOfEncroachment);
        Task<bool> DeleteDetailsOfEncroachment(int Id);
        Task<bool> SaveEncroachmentFirFileDetails(EncroachmentFirFileDetails encroachmentFirFileDetails);
        Task<bool> DeleteEncroachmentFirFileDetails(int Id);
        Task<bool> SaveEncroachmentPhotoFileDetails(EncroachmentPhotoFileDetails encroachmentPhotoFileDetails);
        Task<bool> DeleteEncroachmentPhotoFileDetails(int Id);
        Task<bool> SaveEncroachmentLocationMapFileDetails(EncroachmentLocationMapFileDetails encroachmentLocationMapFileDetails);
        Task<EncroachmentRegisteration> FetchSingleResult(int id);
        Task<bool> DeleteEncroachmentLocationMapFileDetails(int Id);
        Task<List<Division>> GetAllDivision(int zoneId);
        Task<List<Department>> GetAllDepartment();
        Task<PagedResult<Watchandward>> GetPagedEncroachmentRegisteration(EncroachmentRegisterationDto model, int approved, int zoneId);
        Task<List<Locality>> GetAllLocalityList(int divisionId);
        Task<List<Locality>> GetAllLocalityList();//for demolition report -- ishu
        Task<PagedResult<EncroachmentRegisteration>> GetPagedDemolitionReport(DemolitionReportSearchDto model);//for demolition report -- ishu

        Task<List<Khasra>> GetAllKhasraList(int localityId);
        Task<List<Propertyregistration>> GetAllKhasraListFromPropertyInventory(int ZoneId, int DepartmentId);
        Task<PagedResult<EncroachmentRegisteration>> GetEncroachmentRegisterationReportData(InspectionEncroachmentregistrationSearchDto inspectionEncroachmentregistrationSearchDto);
        Task<PagedResult<EncroachmentRegisteration>> GetEncroachmentReportData(EnchroachmentSearchDto enchroachmentSearchDto);
        Task<bool> RollBackEntryEncroachmentLocationMapFileDetails(int id);
        Task<bool> RollBackEntryEncroachmentFirFileDetails(int id);
        Task<bool> RollBackEntryEncroachmentPhotoFileDetails(int id);
        Task<bool> RollBackEntryDetailsofEncroachmentRepeater(int id);
        Task<Zone> FetchSingleResultOnZoneList(int zoneid);
    }
}