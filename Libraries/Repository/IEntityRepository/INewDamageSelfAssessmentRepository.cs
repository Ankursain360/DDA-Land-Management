using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface INewDamageSelfAssessmentRepository : IGenericRepository<Newdamagepayeeregistration>
    {
         Task<List<Newdamagepayeeregistration>> GetAllDamageSelfAssessments();
        Task<List<District>> GetAllDistrict();
        Task<List<Floors>> GetFloors();
        Task<List<Acquiredlandvillage>> GetAllVillage(int districtId);  
        Task<List<New_Damage_Colony>> GetAllColony(int villageId);
        Task<List<Locality>> GetLocalityList();
        Task<PagedResult<Newdamagepayeeregistration>> GetPagedDamagePayee(DamagePayeeSearchDto model , int id);
        Task<bool> Any(int id, string name);

        Task<Newdamagepayeeregistration> GetUploadDocumentFilePath(int Id);

        //********* Ats Self Assessment Details **********

        //  Task<bool> SaveHolderdetails(Newdamageselfassessmentholderdetail holderDetails);
        Task<bool> SaveATSDetails(Newdamageselfassessmentatsdetail atsDetails);
        Task<List<Newdamageselfassessmentatsdetail>> GetAllAtsDetails(int id);
        Task<bool> DeleteAts(int Id);
        Task<Newdamageselfassessmentatsdetail> GetAtsFilePath(int id);

        //********* Gpa Self Assessment Details **********

        //Task<bool> SaveAttendance(NewDamageSelfAssessmentGpaDetails gpaDetails); 
        Task<bool> SaveGPADetails(Newdamageselfassessmentgpadetail gpaDetails);
        Task<List<Newdamageselfassessmentgpadetail>> GetAllGpaDetails(int id); 
        Task<bool> DeleteGpa(int Id); 
        Task<Newdamageselfassessmentgpadetail> GetGpaFilePath(int Id);

        //********* Add Floor ! Damage Details ***********

        Task<bool> SaveFloorDetails(Newdamageselfassessmentfloordetail Floordetails);
        Task<bool> SaveSurveyReport(Newdamageselfassessmentfloordetail addDloorDetails); 
        Task<List<Newdamageselfassessmentfloordetail>> GetAddfloorsDetails(int id);  
        Task<Newdamageselfassessmentfloordetail> GetAddFloorFilePath(int Id); 
        Task<bool> DeleteAddFloor(int Id);


        //******* damage payment details ******
        Task<bool> SavePaymentdetails(Newdamagepaymenthistory PaymentDetails);
        Task<List<Newdamagepaymenthistory>> Getpaymentdetail(int id);
        Task<Newdamagepaymenthistory>GetpaymentFile(int id); 

        //********occupant details********
        Task<bool> SaveOccupantDetails(Newdamagepayeeoccupantinfo OccupantDetails);
        Task<List<Newdamagepayeeoccupantinfo>> GetOccupantDetails(int id);
        Task<Newdamagepayeeoccupantinfo> GetOccupantFile(int id);

        // Task<PagedResult<NewDamageSelfAssessment>> GetPagedDivision(NewDamageSelfAssessmentSearchDto model);
    }
}
