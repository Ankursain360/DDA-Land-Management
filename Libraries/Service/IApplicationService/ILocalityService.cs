using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface ILocalityService
    {
        Task<List<Locality>> GetAllLocality(); // To Get all data added by Praveen
        Task<List<Zone>> GetAllZone(int departmentId); // To Get all data added by Praveen
        Task<List<Department>> GetAllDepartment(); // To Get all data added by Praveen
        Task<List<Locality>> GetLocalityUsingRepo();
        Task<bool> Update(int id, Locality locality); // To Upadte Particular data added by Praveen
        Task<bool> Create(Locality locality);
        Task<Locality> FetchSingleResult(int id);  // To fetch Particular data added by Praveen
        Task<bool> Delete(int id);    // To Delete Data  added by Praveen
        Task<bool> CheckUniqueName(int id, string name);   // To check Unique Value  for Village
        Task<bool> CheckUniqueCode(int id, string code);

        Task<PagedResult<Locality>> GetPagedLocality(LocalitySearchDto model);
        Task<List<Division>> GetAllDivisionList(int zone);
    }
}
