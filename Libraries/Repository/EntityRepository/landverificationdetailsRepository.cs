using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class landverificationdetailsRepository : GenericRepository<LandVerificationDetails>, ILandverificationdetailsRepository
    {
        public landverificationdetailsRepository(DataContext dbContext) : base(dbContext)
        {
        }

        public async Task<int> SaveLandVillagedetails(LandVerificationVillageDetails landVerificationVillage)
        {
            _dbContext.landverificationvillagedetails.Add(landVerificationVillage);
            var result = await _dbContext.SaveChangesAsync();
            int id = landVerificationVillage.Id;
            _dbContext.Entry(landVerificationVillage).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            return result > 0 ? id : 0;
        }

        public async Task<int> SaveSignatureData(LandVerificationSignatureData signatureData)
        {
            _dbContext.landverificationsignaturedata.Add(signatureData);
            var result = await _dbContext.SaveChangesAsync();
            int id = signatureData.Id;
            _dbContext.Entry(signatureData).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            return result > 0 ? id : 0;
        }
    }
}
