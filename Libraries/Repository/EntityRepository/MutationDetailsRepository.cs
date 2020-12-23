using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class MutationDetailsRepository : GenericRepository<Mutationdetails>, IMutationDetailsRepository
    {
        public MutationDetailsRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public Task<bool> Any(int id, string name)
        {
            throw new NotImplementedException();
        }
              

        public async Task<List<Mutationdetails>> GetAllMutationDetails()
        {
            var data = await _dbContext.Mutationdetails
                .Include(x => x.Locality)
                .Include(x => x.Zone)
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            return data;
        }

        public async Task<List<Locality>> GetLocalityList()
        {
            var localityList = await _dbContext.Locality.Where(x => x.IsActive == 1).ToListAsync();
            return localityList;
        }
        public async Task<List<District>> GetDistrictList()
        {
            var districtList = await _dbContext.District.Where(x => x.IsActive == 1).ToListAsync();
            return districtList;
        }

        public async Task<Mutationdetails> GetPhotoPropFile(int id)
        {
            return await _dbContext.Mutationdetails.Where(x => x.Id == id && x.IsActive == 1).FirstOrDefaultAsync();

        }
        public async Task<Mutationdetails> SaveMutationAtsFilePath(int id)
        {
            return await _dbContext.Mutationdetails.Where(x => x.Id == id && x.IsActive == 1).FirstOrDefaultAsync();

        }
        public async Task<Mutationdetails> SaveMutationGPAFilePath(int id)
        {
            return await _dbContext.Mutationdetails.Where(x => x.Id == id && x.IsActive == 1).FirstOrDefaultAsync();

        }
        public async Task<Mutationdetails> SaveMutationMoneyReceiptFilePath(int id)
        {
            return await _dbContext.Mutationdetails.Where(x => x.Id == id && x.IsActive == 1).FirstOrDefaultAsync();

        }
        public async Task<Mutationdetails> SaveMutationSignSPCFilePath(int id)
        {
            return await _dbContext.Mutationdetails.Where(x => x.Id == id && x.IsActive == 1).FirstOrDefaultAsync();

        }
        public async Task<Mutationdetails> SaveMutationAddressProofFilePath(int id)
        {
            return await _dbContext.Mutationdetails.Where(x => x.Id == id && x.IsActive == 1).FirstOrDefaultAsync();

        }
        public async Task<Mutationdetails> SaveMutationAffitDevitFilePath(int id)
        {
            return await _dbContext.Mutationdetails.Where(x => x.Id == id && x.IsActive == 1).FirstOrDefaultAsync();

        }
        public async Task<Mutationdetails> SaveMutationIndemnityFilePath(int id)
        {
            return await _dbContext.Mutationdetails.Where(x => x.Id == id && x.IsActive == 1).FirstOrDefaultAsync();

        }

        public async Task<bool> SaveMutationOldDamage(Mutationolddamageassesse oldDamage)
        {
            _dbContext.Mutationolddamageassesse.Add(oldDamage);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<List<Damagepayeeregistertemp>> FetchSingleResult(int id)
        {
            var data = await _dbContext.Damagepayeeregistertemp
                .Include(x =>x.Damagepayeepersonelinfotemp)
                 .Include(x => x.Damagepaymenthistorytemp)
                  .Include(x => x.Allottetypetemp)
               .Include(x => x.Locality)
               .Include(x => x.District)
               .OrderByDescending(x => x.Id)
               .ToListAsync();

            return data;
        }

        public async Task<Damagepayeeregistertemp> FetchMutationDetailsUserId(int userId)
        {
            return await _dbContext.Damagepayeeregistertemp
                                    .Include(x => x.Damagepayeepersonelinfotemp)
                                    .Include(x => x.Damagepaymenthistorytemp)
                                    .Include(x => x.Allottetypetemp)
                                    .Where(x => x.UserId == userId)
                                    .FirstOrDefaultAsync();
        }
    }
}
