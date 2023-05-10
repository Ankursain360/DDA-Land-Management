using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface ILandverificationdetailsRepository : IGenericRepository<LandVerificationDetails>
    {
       Task<int> SaveSignatureData(LandVerificationSignatureData signatureData);
       Task<int> SaveLandVillagedetails(LandVerificationVillageDetails landVerificationVillage);
    }
}
