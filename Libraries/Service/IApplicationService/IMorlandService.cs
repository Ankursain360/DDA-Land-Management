using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface IMorlandService
    {

        Task<List<LandNotification>> GetAllLandNotification();
        Task<List<Serialnumber>> GetAllSerialnumber();
        Task<List<Morland>> GetMorlandUsingRepo();
        Task<List<Morland>> GetAllMorland();

        Task<bool> Update(int id, Morland morland);
        Task<bool> Create(Morland morland);
        Task<Morland> FetchSingleResult(int id);
        Task<bool> Delete(int id);






    }
}