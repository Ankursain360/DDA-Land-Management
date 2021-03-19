using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IOldAllotmentEntryRepository : IGenericRepository<Leaseapplication>
    {
        Task<List<PropertyType>> GetAllPropertyType();
        Task<List<Leasetype>> GetAllLeaseType();
        Task<List<Leasepurpose>> GetAllLeasepurpose();
        Task<List<Leasesubpurpose>> GetAllLeaseSubpurpose(int purposeId);
       
        //********* rpt ! Owner Details **********

        //Task<bool> SaveOwner(Jaraiowner Jaraiowner);
        //Task<List<Jaraiowner>> GetAllOwner(int id);
        //Task<bool> DeleteOwner(int Id);
    }
}
