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
        Task<bool> Update(int id, EncroachmentRegisteration encroachmentRegisteration); // To Upadte Particular data added by Praveen
        Task<bool> Create(EncroachmentRegisteration encroachmentRegisteration);
        Task<EncroachmentRegisteration> FetchSingleResult(int id);  // To fetch Particular data added by Praveen
        Task<bool> Delete(int id);    //To Delete Data added by Praveen
        Task<List<Locality>> GetAllLocalityList(int divisionId);
        Task<PagedResult<EncroachmentRegisteration>> GetPagedEncroachmentRegisteration(EncroachmentRegisterationDto model);
        Task<List<Division>> GetAllDivisionList(int zone);
        Task<List<EncroachmentRegisteration>> GetAllEncroachmentRegisteration();
        Task<List<Khasra>> GetAllKhasraList(int localityId);
        Task<bool> SaveDetailsOfEncroachment(DetailsOfEncroachment detailsOfEncroachment);
        Task<bool> DeleteDetailsOfEncroachment(DetailsOfEncroachment detailsOfEncroachments);
        Task<bool> SaveEncroachmentFirFileDetails(EncroachmentFirFileDetails encroachmentFirFileDetails);
        Task<bool> DeleteEncroachmentFirFileDetails(EncroachmentFirFileDetails encroachmentFirFileDetails);
        Task<bool> SaveEncroachmentPhotoFileDetails(EncroachmentPhotoFileDetails encroachmentPhotoFileDetails);
        Task<bool> DeleteEncroachmentPhotoFileDetails(EncroachmentPhotoFileDetails encroachmentPhotoFileDetails);
        Task<bool> SaveEncroachmentLocationMapFileDetails(EncroachmentLocationMapFileDetails encroachmentLocationMapFileDetails);
        Task<bool> DeleteEncroachmentLocationMapFileDetails(EncroachmentLocationMapFileDetails encroachmentLocationMapFileDetails);
        Task<List<DetailsOfEncroachment>> GetDetailsOfEncroachment(int encroachmentId);
        Task<EncroachmentPhotoFileDetails> GetEncroachmentPhotoFileDetails(int encroachmentId);
        Task<EncroachmentLocationMapFileDetails> GetEncroachmentLocationMapFileDetails(int encroachmentId);
        Task<EncroachmentFirFileDetails> GetEncroachmentFirFileDetails(int encroachmentId);
    }
}
