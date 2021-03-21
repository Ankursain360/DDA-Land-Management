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

        //********* save in table  Allotmententry  **********

       
        Task<int> SaveAllotmentDetails(Allotmententry entry);
        Task<List<Allotmententry>> GetAllAllotmententry(int id);
        Task<bool> DeleteEntry(int Id);

        //********* save in table  possesionplan  **********
        Task<bool> SavepossessionDetails(Possesionplan entry);
        //Task<List<Possesionplan>> GetAllPossesionplan(int id);
        //Task<bool> DeletePlan(int Id);
    }
}
