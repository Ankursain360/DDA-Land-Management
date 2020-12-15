using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    
    public interface IDamagepayeeregisterRepository : IGenericRepository<Damagepayeeregister>
    {

        //Task<bool> Any(int id, string name);
        Task<List<Locality>> GetLocalityList();
        Task<List<District>> GetDistrictList();
        Task<List<Damagepayeeregister>> GetAllDamagepayeeregister();
        Task<PagedResult<Damagepayeeregister>> GetPagedDamagepayeeregister(DamagepayeeregisterSearchDto model);

        //********* rpt 1 Persolnal info of damage assesse ***********
        Task<bool> SavePayeePersonalInfo(Damagepayeepersonelinfo damagepayeepersonelinfo);
        Task<List<Damagepayeepersonelinfo>> GetPersonalInfo(int id);
        Task<bool> DeletePayeePersonalInfo(int Id);


        //********* rpt 2 Allotte Type **********

        Task<bool> SaveAllotteType(List<Allottetype> allottetype);
        Task<List<Allottetype>> GetAllottetype(int id);
        Task<bool> DeleteAllotteType(int Id);


        //********* rpt 3 Damage payment history ***********

        Task<bool> SavePaymentHistory(Damagepaymenthistory Damagepaymenthistory);
        Task<List<Damagepaymenthistory>> GetPaymentHistory(int id);
        Task<bool> DeletePaymentHistory(int Id);
    }
}
