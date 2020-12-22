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
    public interface ISelfAssessmentDamageService : IEntityService<Damagepayeeregistertemp>
    {
        Task<List<Damagepayeeregistertemp>> GetAllDamagepayeeregisterTemp();
        // Task<List<Damagepayeeregister>> GetDamagepayeeregisterUsingRepo();
        //Task<bool> Update(int id, Damagepayeeregister damagepayeeregister);
        Task<bool> Create(Damagepayeeregistertemp damagepayeeregistertemp);
        Task<Damagepayeeregistertemp> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<List<Locality>> GetLocalityList();
        Task<List<District>> GetDistrictList();
        Task<PagedResult<Damagepayeeregister>> GetPagedDamagepayeeregister(DamagepayeeregisterSearchDto model);

        //********* rpt 1 Persolnal info of damage assesse ***********
        Task<bool> SavePayeePersonalInfoTemp(Damagepayeepersonelinfotemp damagepayeepersonelinfotemp);
        Task<List<Damagepayeepersonelinfotemp>> GetPersonalInfoTemp(int id);
        Task<bool> DeletePayeePersonalInfoTemp(int Id);


        //********* rpt 2 Allotte Type **********

        Task<bool> SaveAllotteTypeTemp(List<Allottetypetemp> allottetypetemp);
        Task<List<Allottetypetemp>> GetAllottetypeTemp(int id);
        Task<bool> DeleteAllotteTypeTemp(int Id);


        //********* rpt 3 Damage payment history ***********

        Task<bool> SavePaymentHistoryTemp(List<Damagepaymenthistorytemp> damagepaymenthistorytemp);
        Task<List<Damagepaymenthistorytemp>> GetPaymentHistoryTemp(int id);
        Task<Damagepayeeregistertemp> FetchSelfAssessmentUserId(int userId);
        Task<bool> DeletePaymentHistoryTemp(int Id);
        Task<Rebate> GetRebateValue();
    }
}
