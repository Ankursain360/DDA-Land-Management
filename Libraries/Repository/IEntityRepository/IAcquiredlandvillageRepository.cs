using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Libraries.Repository.IEntityRepository
{
    public interface IAcquiredlandvillageRepository : IGenericRepository<Acquiredlandvillage>
    {

        Task<List<Acquiredlandvillage>> GetAcquiredlandvillage();
        Task<List<District>> GetAllDistrict();
        Task<List<Tehsil>> GetAllTehsil();
        Task<List<Villagetype>> GetAllVillagetype();
       // Task<bool> AnyLoginName(int id, string loginname);

    }
}
