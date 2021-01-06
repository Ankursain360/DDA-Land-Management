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
   
     public interface IDamagepayeeregisterService : IEntityService<Damagepayeeregistertemp>
    {
        Task<List<Damagepayeeregistertemp>> GetAllDamagepayeeregisterTemp();
       // Task<List<Damagepayeeregister>> GetDamagepayeeregisterUsingRepo();
        Task<bool> Update(int id, Damagepayeeregistertemp damagepayeeregistertemp);
        Task<bool> Create(Damagepayeeregistertemp damagepayeeregistertemp);
        Task<Damagepayeeregistertemp> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<List<Locality>> GetLocalityList();
        Task<List<District>> GetDistrictList();
        Task<PagedResult<Damagepayeeregistertemp>> GetPagedDamagepayeeregistertemp(DamagepayeeregistertempSearchDto model);
       
        //********* rpt 1 Persolnal info of damage assesse ***********
        Task<bool> SavePayeePersonalInfoTemp(Damagepayeepersonelinfotemp damagepayeepersonelinfotemp);
        Task<List<Damagepayeepersonelinfotemp>> GetPersonalInfoTemp(int id);
        Task<bool> DeletePayeePersonalInfoTemp(int Id);
        Task<Damagepayeepersonelinfotemp> GetPersonelInfoFilePath(int Id);
        Task<Damagepayeepersonelinfotemp> GetAadharFilePath(int Id);
        Task<Damagepayeepersonelinfotemp> GetPanFilePath(int Id);
        Task<Damagepayeepersonelinfotemp> GetPhotographPath(int Id);
        Task<Damagepayeepersonelinfotemp> GetSignaturePath(int Id);
        Task<Damagepayeeregistertemp> GetPropertyPhotoPath(int Id);
        Task<List<Damagepayeepersonelinfotemp>> GetPreviousAssesseRepeater(int id);


        //********* rpt 2 Allotte Type **********

        Task<bool> SaveAllotteTypeTemp(List<Allottetypetemp> allottetypetemp);
        Task<List<Allottetypetemp>> GetAllottetypeTemp(int id);
        Task<bool> DeleteAllotteTypeTemp(int Id);
        Task<Allottetypetemp> GetATSFilePath(int Id);
        Task<List<Allottetypetemp>> GetNewAlloteeRepeater(int id);


        //********* rpt 3 Damage payment history ***********

        Task<bool> SavePaymentHistoryTemp(List<Damagepaymenthistorytemp> damagepaymenthistorytemp);
        Task<List<Damagepaymenthistorytemp>> GetPaymentHistoryTemp(int id);
        Task<bool> DeletePaymentHistoryTemp(int Id);
        Task<Damagepaymenthistorytemp> GetReceiptFilePath(int Id);
        Task<bool> UpdateBeforeApproval(int id, Damagepayeeregistertemp damagepayeeregistertemp);
        Task<bool> CreateApprovedDamagepayeeRegister(Damagepayeeregistertemp damagepayeeregistertemp, Damagepayeeregister model);
        Task<bool> SavePersonelInfo(List<Damagepayeepersonelinfo> data);
        Task<bool> SaveAllotteType(List<Allottetype> allottetype);
        Task<bool> SavePaymentHistory(List<Damagepaymenthistory> damagepaymenthistory);

       // *************** to create user ************************
       // Task<bool> CreateUser(Damagepayeepersonelinfotemp user);
    }
}
