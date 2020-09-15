using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Libraries.Repository.IEntityRepository
{
    public interface IKhasraRepository : IGenericRepository<Khasra>
    {

        Task<List<Khasra>> GetAllKhasra();
       
        Task<List<LandCategory>> GetAllLandCategory();
        Task<List<Village>> GetAllVillage();
       

    }
}
