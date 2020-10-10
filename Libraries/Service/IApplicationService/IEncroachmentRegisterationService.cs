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
        Task<PagedResult<EncroachmentRegisteration>> GetPagedLandTransfer(EncroachmentRegisterationDto model);
        Task<List<Division>> GetAllDivisionList(int zone);
        Task<List<EncroachmentRegisteration>> GetAllEncroachmentRegisteration();
    }
}
