using Libraries.Model.Entity;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface IVillageService : IEntityService<Village>
    {
        Task<List<Village>> GetAllVillage(); // To Get all data added by Praveen
        Task<List<Zone>> GetAllZone(); // To Get all data added by Praveen
        Task<List<Village>> GetVillageUsingRepo();
        Task<bool> Update(int id, Village village); // To Upadte Particular data added by Praveen
        Task<bool> Create(Village Village);
        bool CheckUniqueName(int id, Village village);   // To check Unique Value  for designation
        Task<Village> FetchSingleResult(int id);  // To fetch Particular data added by Praveen
        Task<bool> Delete(int id);    // To Delete Data  added by Praveen
    }
}
