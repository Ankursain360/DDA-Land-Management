using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;


namespace Libraries.Service.IApplicationService
{
    public interface IDoor2DoorAPIService : IEntityService<Doortodoorsurvey>
    {
        Task<bool> Create(ApiSaveDoor2DoorSurveyDto dto); // To Create Particular data added by renu
        Task<bool> Update(ApiSaveDoor2DoorSurveyDto dto);
        Task<List<ApiSaveDoor2DoorSurveyDto>> GetSurveyDetails(ApiSaveDoor2DoorSurveyDto dto, string identityDocumentPath, string propertyDocumentPath);
        Task<List<ApiSaveDoor2DoorSurveyDto>> GetAllSurveyDetails(ApiGetAllDoor2DoorSurveyParamsDto dto, int adminroleid, string identityDocumentPath, string propertyDocumentPath);
        Task<List<ApiSurveyUserDetailsDto>> VerifySurveyUserDetailsLogin(ApiSurveyUserLoginDto dto);
        Task<List<ApiGetPresentUseDto>> GetPresentUseDetails();
        Task<bool> SaveDoorToDoorSurveyIdentityProofs(Doortodoorsurveyidentityproof item);
        Task<bool> SaveDoorToDoorSurveyPropertyProofs(Doortodoorsurveypropertyproof item);
        Task<bool> DeleteDoorToDoorSurveyIdentityProofs(int id);
        Task<bool> DeleteDoorToDoorSurveyPropertyProofs(int id);
    }
}
