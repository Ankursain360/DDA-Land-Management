using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface IUndersection17Service
    {

        Task<List<LandNotification>> GetAllLandNotification();
        Task<List<Undersection6>> GetAllUndersection6();
        Task<List<Undersection17>> GetUndersection17UsingRepo();
        Task<List<Undersection17>> GetAllUndersection17();

        Task<bool> Update(int id, Undersection17 undersection17);
        Task<bool> Create(Undersection17 undersection17);
        Task<Undersection17> FetchSingleResult(int id);
        Task<bool> Delete(int id);






    }
}