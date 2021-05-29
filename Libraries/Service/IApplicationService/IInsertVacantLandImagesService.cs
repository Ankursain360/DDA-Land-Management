using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;


namespace Libraries.Service.IApplicationService
{
    public interface IInsertVacantLandImagesService : IEntityService<Vacantlandimage>
    {
        Task<bool> Create(ApiInsertVacantLandImageDto dto); // To Create Particular data added by renu
    }
}
