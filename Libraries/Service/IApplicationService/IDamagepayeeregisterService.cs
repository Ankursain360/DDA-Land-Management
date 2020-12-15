using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
   
     public interface IDamagepayeeregisterService : IEntityService<Damagepayeeregister>
    {
        Task<List<Damagepayeeregister>> GetAllDamagepayeeregister();
        Task<List<Damagepayeeregister>> GetDamagepayeeregisterUsingRepo();
        Task<bool> Update(int id, Damagepayeeregister damagepayeeregister);
        Task<bool> Create(Damagepayeeregister damagepayeeregister);
        Task<Damagepayeeregister> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<List<Locality>> GetLocalityList();
        Task<List<District>> GetDistrictList();
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
