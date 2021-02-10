using Libraries.Model.Entity;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
     public interface IUndersection22plotdetailsService : IEntityService<Undersection22plotdetails>
     {
        Task<List<Undersection22plotdetails>> GetAllUS22PlotDetails();
        Task<List<Acquiredlandvillage>> GetAllAcquiredlandvillage();
        Task<List<Khasra>> GetAllKhasra(int? villageId);
        Task<List<Undersection4>> GetAllUndersection4();
        Task<List<Undersection6>> GetAllUndersection6();
        Task<List<Undersection17>> GetAllUndersection17();
        Task<List<Undersection22>> GetAllUndersection22();

        // Task<PagedResult<Undersection22plotdetails>> GetPagedUndersection22plotdetails(Undersection22plotdetailsSearchDto model);
        Task<bool> Create(Undersection22plotdetails us22plot);
    }
}
