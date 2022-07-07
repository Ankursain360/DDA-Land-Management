using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface INewDamageSelfAssessmentRepository : IGenericRepository<NewDamageSelfAssessment>
    {
         Task<List<NewDamageSelfAssessment>> GetAllDamageSelfAssessments();
        Task<List<District>> GetAllDistrict();
        Task<List<Acquiredlandvillage>> GetAllVillage(int districtId);  
        Task<List<New_Damage_Colony>> GetAllColony(int villageId);
        Task<List<Locality>> GetLocalityList();
        Task<bool> Any(int id, string name);



        //********* Ats Self Assessment Details **********

        

        Task<bool> SaveFloorDetails(NewdamageAddfloor attendance);

        Task<bool> SaveGPADetails(NewDamageSelfAssessmentGpaDetails gpaDetails);

        Task<bool> SaveATSDetails(NewDamageSelfAssessmentAtsDetails atsDetails);
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


        Task<NewDamageSelfAssessment> GetUploadDocumentFilePath(int Id);

        // Task<PagedResult<NewDamageSelfAssessment>> GetPagedDivision(NewDamageSelfAssessmentSearchDto model);
    }
}
