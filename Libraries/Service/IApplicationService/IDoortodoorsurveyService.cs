using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface IDoortodoorsurveyService
    {
        Task<List<Doortodoorsurvey>> GetDoortodoorsurvey();
        Task<List<Presentuse>> GetAllPresentuse();
        Task<List<Areaunit>> GetAllAreaunit();
        Task<List<Floors>> GetAllFloor();
        Task<PagedResult<Doortodoorsurvey>> GetPagedDoortodoorsurvey(DoortodoorsurveySearchDto model);


        Task<List<Doortodoorsurvey>> GetDoortodoorsurveyUsingRepo();
        Task<bool> Update(int id, Doortodoorsurvey doortodoorsurvey);
        Task<bool> Create(Doortodoorsurvey doortodoorsurvey);
        Task<Doortodoorsurvey> FetchSingleResult(int id);
        Task<bool> Delete(int id);

        Task<PagedResult<Doortodoorsurvey>> GetPagedDoortodoorsurveyReport(DoorToDoorSurveyReportSearchDto model);

        Task<bool> SaveDoorToDoorSurveyIdentityProofs(Doortodoorsurveyidentityproof item);
        Task<bool> SaveDoorToDoorSurveyPropertyProofs(Doortodoorsurveypropertyproof item);
        Task<bool> DeleteDoorToDoorSurveyIdentityProofs(int id);
        Task<bool> DeleteDoorToDoorSurveyPropertyProofs(int id);
        Task<Doortodoorsurveyidentityproof> FetchSingleResultDoor2DoorSurveyIdentity(int id);
        Task<Doortodoorsurveypropertyproof> FetchSingleResultDoor2DoorSurveyProperty(int id);
    }
}
