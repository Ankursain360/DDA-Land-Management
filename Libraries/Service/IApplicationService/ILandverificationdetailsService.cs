using Dto.Master;
using Libraries.Model.Entity;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.IApplicationService
{
    public interface ILandverificationdetailsService : IEntityService<LandVerificationDetails>
    {
        Task<bool> Create(landverificationdetailsDto dto);
        Task<int> SaveSignatureData(LandVerificationSignatureData signatureData);
        Task<int> SaveLandVillagedetails(LandVerificationVillageDetails landVerificationVillage);
    }
}
