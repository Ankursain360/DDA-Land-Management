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
        Task<Damagepayeeregister> GetPropertyPhotoPath(int Id);

        Task<List<Damagepayeeregister>> GetAllDamagepayeeregister();
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
        Task<Damagepayeeregister> FetchSingleResult(int id);
        Task<bool> DeletePaymentHistory(int Id);
        Task<Damagepaymenthistory> GetReceiptFilePath(int Id);
        Task<bool> CreateApprovedDamagepayeeRegister(Damagepayeeregister model);
        Task<bool> SavePersonelInfo(List<Damagepayeepersonelinfo> data);
       
    }
}
