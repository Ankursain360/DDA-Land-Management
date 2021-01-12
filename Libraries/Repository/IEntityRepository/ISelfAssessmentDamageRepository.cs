using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{

    public interface ISelfAssessmentDamageRepository : IGenericRepository<Damagepayeeregister>
    {
        //Task<bool> Any(int id, string name);
        Task<List<Locality>> GetLocalityList();
        Task<List<District>> GetDistrictList();
        Task<List<Damagepayeeregister>> GetAllDamagepayeeregisterTemp();
        Task<PagedResult<Damagepayeeregister>> GetPagedDamagepayeeregister(DamagepayeeregistertempSearchDto model);

        //********* rpt 1 Persolnal info of damage assesse ***********
        Task<bool> SavePayeePersonalInfoTemp(Damagepayeepersonelinfo damagepayeepersonelinfotemp);
        Task<List<Damagepayeepersonelinfo>> GetPersonalInfoTemp(int id);
        Task<bool> DeletePayeePersonalInfoTemp(int Id);
        Task<Damagepayeepersonelinfo> GetPersonelInfoFilePath(int Id);

        //********* rpt 2 Allotte Type **********

        Task<bool> SaveAllotteTypeTemp(List<Allottetype> allottetypetemp);
        Task<List<Allottetype>> GetAllottetypeTemp(int id);
        Task<bool> DeleteAllotteTypeTemp(int Id);
        Task<Allottetype> GetAllotteTypeSingleResult(int id);


        //********* rpt 3 Damage payment history ***********

        Task<bool> SavePaymentHistoryTemp(List<Damagepaymenthistory> damagepaymenthistorytemp);
        Task<List<Damagepaymenthistory>> GetPaymentHistoryTemp(int id);
        Task<bool> DeletePaymentHistoryTemp(int Id);
        Task<Damagepayeeregister> FetchSelfAssessmentUserId(int userId);
        Task<Rebate> GetRebateValue();
        Task<Damagepaymenthistory> GetPaymentHistorySingleResult(int id);
        string GetLocalityName(int? localityId);
    }
}
