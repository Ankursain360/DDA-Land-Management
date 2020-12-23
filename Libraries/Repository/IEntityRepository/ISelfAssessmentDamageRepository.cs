using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{

    public interface ISelfAssessmentDamageRepository : IGenericRepository<Damagepayeeregistertemp>
    {
        //Task<bool> Any(int id, string name);
        Task<List<Locality>> GetLocalityList();
        Task<List<District>> GetDistrictList();
        Task<List<Damagepayeeregistertemp>> GetAllDamagepayeeregisterTemp();
        Task<PagedResult<Damagepayeeregister>> GetPagedDamagepayeeregister(DamagepayeeregistertempSearchDto model);

        //********* rpt 1 Persolnal info of damage assesse ***********
        Task<bool> SavePayeePersonalInfoTemp(Damagepayeepersonelinfotemp damagepayeepersonelinfotemp);
        Task<List<Damagepayeepersonelinfotemp>> GetPersonalInfoTemp(int id);
        Task<bool> DeletePayeePersonalInfoTemp(int Id);
        Task<Damagepayeepersonelinfotemp> GetPersonelInfoFilePath(int Id);

        //********* rpt 2 Allotte Type **********

        Task<bool> SaveAllotteTypeTemp(List<Allottetypetemp> allottetypetemp);
        Task<List<Allottetypetemp>> GetAllottetypeTemp(int id);
        Task<bool> DeleteAllotteTypeTemp(int Id);


        //********* rpt 3 Damage payment history ***********

        Task<bool> SavePaymentHistoryTemp(List<Damagepaymenthistorytemp> damagepaymenthistorytemp);
        Task<List<Damagepaymenthistorytemp>> GetPaymentHistoryTemp(int id);
        Task<bool> DeletePaymentHistoryTemp(int Id);
        Task<Damagepayeeregistertemp> FetchSelfAssessmentUserId(int userId);
        Task<Rebate> GetRebateValue();
    }
}
