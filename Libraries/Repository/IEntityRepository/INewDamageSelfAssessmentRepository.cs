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
        Task<bool> Any(int id, string name);



        //********* Ats Self Assessment Details **********

        

        Task<bool> SaveFloorDetails(Newdamageselfassessmentfloordetail Floordetails);
        Task<bool> SaveOccupantDetails(Newdamagepayeeoccupantinfo OccupantDetails);
        
       Task<bool> SaveGPADetails(Newdamageselfassessmentgpadetail gpaDetails);

        Task<bool> SaveATSDetails(Newdamageselfassessmentatsdetail atsDetails);
        Task<bool> SaveHolderdetails(Newdamageselfassessmentholderdetail holderDetails);
        Task<bool> SavePaymentdetails(Newdamagepaymenthistory PaymentDetails);
        
        Task<List<NewDamageSelfAssessmentAtsDetails>> GetAllAtsDetails(int id);
        Task<bool> DeleteAts(int Id); 

        //********* Gpa Self Assessment Details **********

        //Task<bool> SaveAttendance(NewDamageSelfAssessmentGpaDetails gpaDetails); 
        Task<List<NewDamageSelfAssessmentGpaDetails>> GetAllGpaDetails(int id); 
        Task<bool> DeleteGpa(int Id);

        //********* Add Floor ! Damage Details ***********
        Task<bool> SaveSurveyReport(NewdamageAddfloor addDloorDetails); 
        Task<List<NewdamageAddfloor>> GetAddfloorsDetails(int id);  
        Task<NewdamageAddfloor> GetAddFloorFilePath(int Id); 
        Task<bool> DeleteAddFloor(int Id); 


        Task<Newdamagepayeeregistration> GetUploadDocumentFilePath(int Id);

        // Task<PagedResult<NewDamageSelfAssessment>> GetPagedDivision(NewDamageSelfAssessmentSearchDto model);
    }
}
