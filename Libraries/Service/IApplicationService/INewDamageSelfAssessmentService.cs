using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface INewDamageSelfAssessmentService : IEntityService<Newdamagepayeeregistration>
    {
        Task<List<Newdamagepayeeregistration>> GetDamageSelfAssessments();
        Task<List<District>> GetAllDistrict();
        Task<List<Locality>> GetLocalityList();
        Task<List<Acquiredlandvillage>> GetAllVillage(int districtId);
        Task<List<New_Damage_Colony>> GetAllColony(int villageId);
        Task<List<Floors>> GetFloors();
        Task<bool> Update(int id, Newdamagepayeeregistration selfAssessment); 
        Task<bool> Create(Newdamagepayeeregistration selfAssessment);
        Task<Newdamagepayeeregistration> FetchSingleResult(int id);
        Task<bool> Delete(int id);

        Task<PagedResult<Newdamagepayeeregistration>> GetPagedDamagePayee(DamagePayeeSearchDto model , int id );
        Task<Newdamagepayeeregistration> GetUploadDocumentFilePath(int Id);


        ////********* fetch occupant details **********
         
        //Task<Newdamagepayeeoccupantinfo> FetchSingleResultOccupant(int id);
        Task<bool> SaveOccupantDetails(Newdamagepayeeoccupantinfo occupantdetails);
        Task<List<Newdamagepayeeoccupantinfo>> GetOccupantDetails(int id);
        Task<Newdamagepayeeoccupantinfo> GetOccupantFile(int id);

        //********* Ats Self Assessment Details **********


        Task<bool> SaveATSDetails(Newdamageselfassessmentatsdetail gpaDetails);
       
        // Task<bool> SaveHolderdetails(Newdamageselfassessmentholderdetail holderDetails); 
        Task<List<Newdamageselfassessmentatsdetail>> GetAllAtsDetails(int id);
        Task<bool> DeleteAts(int Id);
        Task<Newdamageselfassessmentatsdetail> GetAtsFilePath(int id);

        //********* Gpa Self Assessment Details **********
        Task<bool> SaveGPADetails(Newdamageselfassessmentgpadetail gpaDetails);
        Task<List<Newdamageselfassessmentgpadetail>> GetAllGpaDetails(int id);
        Task<bool> DeleteGpa(int Id);
        Task<Newdamageselfassessmentgpadetail> GetGpaFilePath(int Id);

        //********* Add Floor ! Damage Details ***********
        Task<bool> SaveFloorDetails(Newdamageselfassessmentfloordetail attendance);
        Task<bool> SaveSurveyReport(Newdamageselfassessmentfloordetail addDloorDetails);
        Task<List<Newdamageselfassessmentfloordetail>> GetAddfloorsDetails(int id);
        Task<Newdamageselfassessmentfloordetail> GetAddFloorFilePath(int Id);
        Task<bool> DeleteAddFloor(int Id);


        //******* Payement details *******
        Task<bool> SavePaymentdetails(Newdamagepaymenthistory paymentDetails);
        Task<List<Newdamagepaymenthistory>> Getpaymentdetail(int id);
        Task<Newdamagepaymenthistory> GetpaymentFile(int id);

    }
}
