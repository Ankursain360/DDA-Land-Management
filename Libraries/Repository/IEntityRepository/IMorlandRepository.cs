using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Libraries.Repository.IEntityRepository
{
    public interface IMorlandRepository : IGenericRepository<Morland>
    {

        Task<List<Morland>> GetAllMorland();

        Task<List<LandNotification>> GetAllLandNotification();
        Task<List<Serialnumber>> GetAllSerialnumber();


    }
}
