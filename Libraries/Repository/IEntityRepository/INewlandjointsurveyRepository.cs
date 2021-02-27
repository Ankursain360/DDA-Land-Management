using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface INewLandJointSurveyRepository : IGenericRepository<Newlandjointsurvey>
    {
        Task<PagedResult<Newlandjointsurvey>> GetPagedNewLandJointSurvey(NewLandJointSurveySearchDto model);
        Task<List<Newlandjointsurvey>> GetAllNewLandJointSurvey();
       
       
        Task<List<Zone>> GetAllZone();
        Task<List<Newlandvillage>> GetAllVillage(int zoneId);
        Task<List<Newlandkhasra>> GetAllKhasra(int? villageId);

        //********* rpt ! Attendance Details **********

        Task<bool> SaveAttendance(Newjointsurveyattendancedetail attendance);
        Task<List<Newjointsurveyattendancedetail>> GetAllattendance(int id);
        Task<bool> DeleteAttendance(int Id);

        //********* rpt survey report ***********
        Task<bool> SaveSurveyReport(Newjointsurveyreportdetail newjointsurveyreportdetail);
        Task<List<Newjointsurveyreportdetail>> GetNewjointsurveyreportdetail(int id);
        Task<Newjointsurveyreportdetail> GetNewjointsurveyreportdetailFilePath(int Id);
        Task<bool> DeleteSurveyReport(int Id);

       
        Task<Newjointsurveyreportdetail> GetUploadDocumentFilePath(int Id);
      

    }
}
