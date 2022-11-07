using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Repository.Common;
using Libraries.Service.Common;

namespace Libraries.Service.IApplicationService
{
    public interface INewLandJointSurveyService : IEntityService<Newlandjointsurvey>
    {


       
        //Task<List<Newlandjointsurvey>> GetJointSurveyUsingRepo();
        Task<List<Zone>> GetAllZone(); 
        Task<List<Newlandvillage>> GetAllVillage(int zoneId); 
        Task<List<Newlandkhasra>> GetAllKhasra(int? villageId);
        //********* rpt ! Attendance Details **********

        Task<bool> SaveAttendance(Newjointsurveyattendancedetail attendance);
        Task<List<Newjointsurveyattendancedetail>> GetAllattendance(int id);
        Task<bool> DeleteAttendance(int Id);
        //********* rpt Survey Report ***********
        Task<bool> SaveSurveyReport(Newjointsurveyreportdetail newjointsurveyreportdetail);
        Task<List<Newjointsurveyreportdetail>> GetNewjointsurveyreportdetail(int id);
        Task<bool> DeleteSurveyReport(int Id);
        Task<Newjointsurveyreportdetail> GetNewjointsurveyreportdetailFilePath(int Id);
        Task<Newjointsurveyreportdetail> GetUploadDocumentFilePath(int Id);
        Task<bool> Update(int id, Newlandjointsurvey newlandjointsurvey);
        Task<bool> Create(Newlandjointsurvey newlandjointsurvey);
        Task<Newlandjointsurvey> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<PagedResult<Newlandjointsurvey>> GetPagedNewLandJointSurvey(NewLandJointSurveySearchDto model);
        Task<List<Newlandjointsurvey>> GetAllNewLandJointSurvey();
        Task<List<Newlandjointsurvey>> GetAllNewLandJointSurveyList(NewLandJointSurveySearchDto model);
        Task<Newlandkhasra> FetchSingleKhasraResult(int? khasraId);




    }
}
