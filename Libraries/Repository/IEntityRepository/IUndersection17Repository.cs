using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Libraries.Repository.IEntityRepository
{
    public interface IUndersection17Repository : IGenericRepository<Undersection17>
    {

        Task<List<Undersection17>> GetAllUndersection17();

        Task<List<LandNotification>> GetAllLandNotification();
        Task<List<Undersection6>> GetAllUndersection6();


    }
}
