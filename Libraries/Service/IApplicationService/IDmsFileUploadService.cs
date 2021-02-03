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
    public interface IDmsFileUploadService : IEntityService<Dmsfileupload>
    {
        //Task<List<Damagepayeeregister>> GetAllDamagepayeeregisterTemp();
        //// Task<List<Damagepayeeregister>> GetDamagepayeeregisterUsingRepo();
        ////Task<bool> Update(int id, Damagepayeeregister damagepayeeregister);
        //Task<bool> Create(Damagepayeeregister damagepayeeregistertemp);
        //Task<bool> Update(Damagepayeeregister damagepayeeregistertemp);
        //Task<Damagepayeeregister> FetchSingleResult(int id);
        //Task<bool> Delete(int id);
        //Task<List<Locality>> GetLocalityList();
        //Task<List<District>> GetDistrictList();
        //Task<PagedResult<Damagepayeeregister>> GetPagedDamagepayeeregister(DamagepayeeregistertempSearchDto model);

        ////********* rpt 1 Persolnal info of damage assesse ***********
        //Task<bool> SavePayeePersonalInfoTemp(Damagepayeepersonelinfo damagepayeepersonelinfotemp);
        //Task<List<Damagepayeepersonelinfo>> GetPersonalInfoTemp(int id);
        //Task<bool> DeletePayeePersonalInfoTemp(int Id);
        //Task<Damagepayeepersonelinfo> GetPersonelInfoFilePath(int id);


        ////********* rpt 2 Allotte Type **********

        //Task<bool> SaveAllotteTypeTemp(List<Allottetype> allottetypetemp);
        //Task<List<Allottetype>> GetAllottetypeTemp(int id);
        //Task<bool> DeleteAllotteTypeTemp(int Id);
        //Task<Allottetype> GetAllotteTypeSingleResult(int id);


        ////********* rpt 3 Damage payment history ***********

        //Task<bool> SavePaymentHistoryTemp(List<Damagepaymenthistory> damagepaymenthistorytemp);
        //Task<List<Damagepaymenthistory>> GetPaymentHistoryTemp(int id);
        //Task<Damagepayeeregister> FetchSelfAssessmentUserId(int userId);
        //Task<bool> DeletePaymentHistoryTemp(int Id);
        //Task<Rebate> GetRebateValue();
        //Task<Damagepaymenthistory> GetPaymentHistorySingleResult(int id);
        //string GetLocalityName(int? localityId);
        //Task<bool> UpdateBeforeApproval(int id, Damagepayeeregister damagepayeeregister);
        Task<List<Department>> GetDepartmentList();
        Task<List<Propertyregistration>> GetKhasraNoList();
        Task<List<Locality>> GetLocalityList();
        Task<PagedResult<Dmsfileupload>> GetPagedDMSFileUploadList(DMSFileUploadSearchDto model);
        Task<bool> Create(Dmsfileupload dmsfileupload);
        Task<Dmsfileupload> FetchSingleResult(int id);
        Task<bool> Update(int id, Dmsfileupload dmsfileupload);
        Task<bool> Delete(int id, int userId);
        int GetLocalityByName(string name);
        int GetKhasraByName(string name);
        Task<bool> CheckUniqueName(string fileNo);
        Task<PagedResult<Dmsfileupload>> GetPagedDMSRetriveFileReport(DMSRetriveFileSearchDto model);
    }
}
