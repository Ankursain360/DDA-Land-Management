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
        Task<List<ApiSaveDoor2DoorSurveyDto>> GetSurveyDetails(ApiSaveDoor2DoorSurveyDto dto);
        Task<List<ApiSaveDoor2DoorSurveyDto>> GetAllSurveyDetails(ApiSaveDoor2DoorSurveyDto dto, int adminroleid);
        Task<List<ApiSurveyUserDetailsDto>> VerifySurveyUserDetailsLogin(ApiSurveyUserLoginDto dto);
        Task<List<ApiGetPresentUseDto>> GetPresentUseDetails();
    }
}
