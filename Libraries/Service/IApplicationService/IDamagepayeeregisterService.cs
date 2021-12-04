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
      
        Task<bool> Update(int id, Damagepayeeregister damagepayeeregister);
        Task<bool> UpdateUserId(int id, int UserId);
        Task<bool> Create(Damagepayeeregister damagepayeeregister);
        Task<Damagepayeeregister> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<List<Locality>> GetLocalityList();
        Task<List<District>> GetDistrictList();
        Task<PagedResult<Damagepayeeregister>> GetPagedDamagepayeeregister(DamagepayeeregistertempSearchDto model);
       
        //********* rpt 1 Persolnal info of damage assesse ***********
        Task<bool> SavePayeePersonalInfo(Damagepayeepersonelinfo damagepayeepersonelinfo);
        Task<List<Damagepayeepersonelinfo>> GetPersonalInfo(int id);
        Task<bool> DeletePayeePersonalInfo(int Id);
        Task<Damagepayeepersonelinfo> GetPersonelInfoFilePath(int Id);
        Task<Damagepayeepersonelinfo> GetAadharFilePath(int Id);
        Task<Damagepayeepersonelinfo> GetPanFilePath(int Id);
        Task<Damagepayeepersonelinfo> GetPhotographPath(int Id);
        Task<Damagepayeepersonelinfo> GetSignaturePath(int Id);
        Task<Damagepayeeregister> GetPropertyPhotoPath(int Id);
        Task<List<Damagepayeepersonelinfo>> GetPreviousAssesseRepeater(int id);


        //********* rpt 2 Allotte Type **********

        Task<bool> SaveAllotteType(List<Allottetype> allottetype);
        Task<List<Allottetype>> GetAllottetype(int id);
        Task<bool> DeleteAllotteType(int Id);
        Task<Allottetype> GetATSFilePath(int Id);
        Task<List<Allottetype>> GetNewAlloteeRepeater(int id);


        //********* rpt 3 Damage payment history ***********

        Task<bool> SavePaymentHistory(List<Damagepaymenthistory> damagepaymenthistory);
        Task<List<Damagepaymenthistory>> GetPaymentHistory(int id);
        Task<bool> DeletePaymentHistory(int Id);
        Task<Damagepaymenthistory> GetReceiptFilePath(int Id);
        Task<bool> UpdateBeforeApproval(int id, Damagepayeeregister damagepayeeregister);
        Task<bool> CreateApprovedDamagepayeeRegister(Damagepayeeregister damagepayeeregister, Damagepayeeregister model);
        Task<bool> SavePersonelInfo(List<Damagepayeepersonelinfo> data);
        //Task<bool> SaveAllotteType(List<Allottetype> allottetype);
        //Task<bool> SavePaymentHistory(List<Damagepaymenthistory> damagepaymenthistory);

       // *************** to create user ************************
        Task<string> CreateUser(Damagepayeeregister model);

        string GetPropertyNo(string FileNo);
        string GetFileNo(int userid);

    }
}
