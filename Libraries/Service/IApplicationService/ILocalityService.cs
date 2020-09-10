using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface ILocalityService
    {
        Task<List<Locality>> GetAllLocality(); // To Get all data added by Praveen
        Task<List<Zone>> GetAllZone(); // To Get all data added by Praveen
        Task<List<Zone>> GetAllDepartment(); // To Get all data added by Praveen
        Task<List<Locality>> GetLocalityUsingRepo();
        Task<bool> Update(int id, Locality locality); // To Upadte Particular data added by Praveen
        Task<bool> Create(Locality locality);
        Task<Locality> FetchSingleResult(int id);  // To fetch Particular data added by Praveen
        Task<bool> Delete(int id);    // To Delete Data  added by Praveen
        Task<bool> CheckUniqueName(int id, string name);   // To check Unique Value  for Village
    }
}
