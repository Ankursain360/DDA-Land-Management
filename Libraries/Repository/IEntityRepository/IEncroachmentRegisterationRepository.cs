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
        Task<List<Zone>> GetAllZone(int departmentId);
        Task<List<DetailsOfEncroachment>> GetDetailsOfEncroachment(int encroachmentId);
        Task<EncroachmentPhotoFileDetails> GetEncroachmentPhotoFileDetails(int encroachmentId);
        Task<EncroachmentLocationMapFileDetails> GetEncroachmentLocationMapFileDetails(int encroachmentId);
        Task<EncroachmentFirFileDetails> GetEncroachmentFirFileDetails(int encroachmentId);
        Task<bool> SaveDetailsOfEncroachment(DetailsOfEncroachment detailsOfEncroachment);
        Task<bool> DeleteDetailsOfEncroachment(DetailsOfEncroachment detailsOfEncroachments);
        Task<bool> SaveEncroachmentFirFileDetails(EncroachmentFirFileDetails encroachmentFirFileDetails);
        Task<bool> DeleteEncroachmentFirFileDetails(EncroachmentFirFileDetails encroachmentFirFileDetails);
        Task<bool> SaveEncroachmentPhotoFileDetails(EncroachmentPhotoFileDetails encroachmentPhotoFileDetails);
        Task<bool> DeleteEncroachmentPhotoFileDetails(EncroachmentPhotoFileDetails encroachmentPhotoFileDetails);
        Task<bool> SaveEncroachmentLocationMapFileDetails(EncroachmentLocationMapFileDetails encroachmentLocationMapFileDetails);
        Task<EncroachmentRegisteration> FetchSingleResult(int id);
        Task<bool> DeleteEncroachmentLocationMapFileDetails(EncroachmentLocationMapFileDetails encroachmentLocationMapFileDetails);
        Task<List<Division>> GetAllDivision(int zoneId);
        Task<List<Department>> GetAllDepartment();
        Task<PagedResult<EncroachmentRegisteration>> GetPagedEncroachmentRegisteration(EncroachmentRegisterationDto model);
        Task<List<Locality>> GetAllLocalityList(int divisionId);
        Task<List<Khasra>> GetAllKhasraList(int localityId);
    }
}