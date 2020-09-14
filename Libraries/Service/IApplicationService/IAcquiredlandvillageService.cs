using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface IAcquiredlandvillageService
    {
        Task<List<Acquiredlandvillage>> GetAcquiredlandvillage();
        Task<List<District>> GetAllDistrict();
        Task<List<Tehsil>> GetAllTehsil();
        Task<List<Villagetype>> GetAllVillagetype();
        Task<List<Acquiredlandvillage>> GetACquiredlandvillageUsingRepo();
        Task<bool> Update(int id, Acquiredlandvillage acquiredlandvillage);
        Task<bool> Create(Acquiredlandvillage acquiredlandvillage);
        Task<Acquiredlandvillage> FetchSingleResult(int id);
        Task<bool> Delete(int id);
      //  Task<bool> CheckUniqueLoginName(int id, string loginname);





    }
}
