using Libraries.Model.Entity;
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


        //********* Ats Self Assessment Details **********

       

        Task<bool> SaveFloorDetails(Newdamageselfassessmentfloordetail attendance);

        Task<bool> SaveOccupantDetails(Newdamagepayeeoccupantinfo occupantdetails);
        Task<bool> SaveGPADetails(Newdamageselfassessmentgpadetail gpaDetails);
        Task<bool> SaveATSDetails(Newdamageselfassessmentatsdetail gpaDetails);
        Task<bool> SaveHolderdetails(Newdamageselfassessmentholderdetail holderDetails);
        Task<bool> SavePaymentdetails(Newdamagepaymenthistory paymentDetails);
        
        Task<List<NewDamageSelfAssessmentAtsDetails>> GetAllAtsDetails(int id);
        Task<bool> DeleteAts(int Id);

        //********* Gpa Self Assessment Details **********

        
        Task<List<NewDamageSelfAssessmentGpaDetails>> GetAllGpaDetails(int id);
        Task<bool> DeleteGpa(int Id);

        //********* Add Floor ! Damage Details ***********
        Task<bool> SaveSurveyReport(NewdamageAddfloor addDloorDetails);
        Task<List<NewdamageAddfloor>> GetAddfloorsDetails(int id);
        Task<NewdamageAddfloor> GetAddFloorFilePath(int Id);
        Task<bool> DeleteAddFloor(int Id);


        Task<Newdamagepayeeregistration> GetUploadDocumentFilePath(int Id);
       
    }
}
