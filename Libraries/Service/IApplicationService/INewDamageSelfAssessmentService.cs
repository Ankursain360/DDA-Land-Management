using Libraries.Model.Entity;
using Libraries.Service.Common;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface INewDamageSelfAssessmentService : IEntityService<NewDamageSelfAssessment>
    {
        Task<List<NewDamageSelfAssessment>> GetDamageSelfAssessments();
        Task<List<District>> GetAllDistrict();
        Task<List<Locality>> GetLocalityList();
        Task<List<Acquiredlandvillage>> GetAllVillage(int districtId);
        Task<List<New_Damage_Colony>> GetAllColony(int villageId);
        Task<bool> Update(int id, NewDamageSelfAssessment selfAssessment); 
        Task<bool> Create(NewDamageSelfAssessment selfAssessment);
        Task<NewDamageSelfAssessment> FetchSingleResult(int id);
        Task<bool> Delete(int id);


        //********* Ats Self Assessment Details **********

        Task<bool> SaveAttendance(NewDamageSelfAssessmentAtsDetails atsDetails);
        Task<List<NewDamageSelfAssessmentAtsDetails>> GetAllAtsDetails(int id);
        Task<bool> DeleteAts(int Id);

        //********* Gpa Self Assessment Details **********

        Task<bool> SaveAttendance(NewDamageSelfAssessmentGpaDetails gpaDetails);
        Task<List<NewDamageSelfAssessmentGpaDetails>> GetAllGpaDetails(int id);
        Task<bool> DeleteGpa(int Id);

        //********* Add Floor ! Damage Details ***********
        Task<bool> SaveSurveyReport(NewdamageAddfloor addDloorDetails);
        Task<List<NewdamageAddfloor>> GetAddfloorsDetails(int id);
        Task<NewdamageAddfloor> GetAddFloorFilePath(int Id);
        Task<bool> DeleteAddFloor(int Id);


        Task<NewDamageSelfAssessment> GetUploadDocumentFilePath(int Id);
       
    }
}
