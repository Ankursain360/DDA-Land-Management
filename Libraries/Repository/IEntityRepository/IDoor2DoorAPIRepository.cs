using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;


namespace Libraries.Repository.IEntityRepository
{
    public interface IDoor2DoorAPIRepository : IGenericRepository<Doortodoorsurvey>
    {
        Task<List<ApiSaveDoor2DoorSurveyDto>> GetSurveyDetails(ApiSaveDoor2DoorSurveyDto dto);
        Task<List<ApiSaveDoor2DoorSurveyDto>> GetAllSurveyDetails(ApiSaveDoor2DoorSurveyDto dto);
    }
}