using Dto.Search;
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
    public class MutationDetailsRepository : GenericRepository<Mutationdetailstemp>, IMutationDetailsRepository
    {
        public MutationDetailsRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public Task<bool> Any(int id, string name)
        {
            throw new NotImplementedException();
        }
              
        //public async Task AddMutationDetails(Mutationdetailstemp mutationdetailstemp)
        //{
        //    Mutationdetailstemp obj = new Mutationdetailstemp();
        //    obj.CreatedBy = 1;
        //    obj.CreatedDate = DateTime.Now;
        //    await  _dbContext.Mutationdetailstemp.AddAsync(obj);
        //  await  _dbContext.SaveChangesAsync(); 
            
        //}
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

        //public async Task<Mutationdetails> GetPhotoPropFile(int id)
        //{
        //    return await _dbContext.Mutationdetails.Where(x => x.Id == id && x.IsActive == 1).FirstOrDefaultAsync();

        //}
        //public async Task<Mutationdetails> SaveMutationAtsFilePath(int id)
        //{
        //    return await _dbContext.Mutationdetails.Where(x => x.Id == id && x.IsActive == 1).FirstOrDefaultAsync();

        //}
        //public async Task<Mutationdetails> SaveMutationGPAFilePath(int id)
        //{
        //    return await _dbContext.Mutationdetails.Where(x => x.Id == id && x.IsActive == 1).FirstOrDefaultAsync();

        //}
        //public async Task<Mutationdetails> SaveMutationMoneyReceiptFilePath(int id)
        //{
        //    return await _dbContext.Mutationdetails.Where(x => x.Id == id && x.IsActive == 1).FirstOrDefaultAsync();

        //}
        //public async Task<Mutationdetails> SaveMutationSignSPCFilePath(int id)
        //{
        //    return await _dbContext.Mutationdetails.Where(x => x.Id == id && x.IsActive == 1).FirstOrDefaultAsync();

        //}
        //public async Task<Mutationdetails> SaveMutationAddressProofFilePath(int id)
        //{
        //    return await _dbContext.Mutationdetails.Where(x => x.Id == id && x.IsActive == 1).FirstOrDefaultAsync();

        //}
        //public async Task<Mutationdetails> SaveMutationAffitDevitFilePath(int id)
        //{
        //    return await _dbContext.Mutationdetails.Where(x => x.Id == id && x.IsActive == 1).FirstOrDefaultAsync();

        //}
        //public async Task<Mutationdetails> SaveMutationIndemnityFilePath(int id)
        //{
        //    return await _dbContext.Mutationdetails.Where(x => x.Id == id && x.IsActive == 1).FirstOrDefaultAsync();

        //}

        //public async Task<bool> SaveMutationOldDamage(Mutationolddamageassesse oldDamage)
        //{
        //    _dbContext.Mutationolddamageassesse.Add(oldDamage);
        //    var Result = await _dbContext.SaveChangesAsync();
        //    return Result > 0 ? true : false;
        //}

        public async Task<List<Damagepayeeregister>> FetchSingleResult(int id)
        {
            var data = await _dbContext.Damagepayeeregister
                .Include(x =>x.Damagepayeepersonelinfo)
                 .Include(x => x.Damagepaymenthistory)
                  .Include(x => x.Allottetype)
               .Include(x => x.Locality)
               .Include(x => x.District)
               .OrderByDescending(x => x.Id)
               .ToListAsync();

            return data;
        }

        public async Task<Damagepayeeregister> FetchDamageResult(int Id)
        {
            return await _dbContext.Damagepayeeregister
                                    .Include(x => x.Damagepayeepersonelinfo)
                                    .Include(x => x.Damagepaymenthistory)
                                    .Include(x => x.Allottetype)
                                    .Where(x => x.Id == Id)
                                    .FirstOrDefaultAsync();
        }

        public async Task<PagedResult<Damagepayeeregister>> GetPagedSubsitutionMutationDetails(SubstitutionMutationDetailsDto model)
        {
            return await _dbContext.Damagepayeeregister
                                   .Include(x => x.Locality)
                                   .Include(x => x.District)
                                   .Where(x => x.IsActive == 1  && x.ApprovedStatus == 1
                                   //&& (model.StatusId == 0 ? x.PendingAt == userId : x.PendingAt == 0)
                                   )
                                   .GetPaged<Damagepayeeregister>(model.PageNumber, model.PageSize);
        }
        public async Task<List<Damagepayeepersonelinfo>> GetPersonalInfo(int id)
        {
            return await _dbContext.Damagepayeepersonelinfo.Where(x => x.DamagePayeeRegisterTempId == id && x.IsActive == 1).ToListAsync();
        }
        public async Task<List<Allottetype>> GetAllottetype(int id)
        {
            return await _dbContext.Allottetype.Where(x => x.DamagePayeeRegisterTempId == id && x.IsActive == 1).ToListAsync();
        }

        public async Task<Mutationdetailstemp> FetchMutationSingleResult(int id)
        {
            return await _dbContext.Mutationdetailstemp
                                    .Include(x => x.DamagePayeeRegister)
                                    .Where(x => x.DamagePayeeRegisterId == id)
                                    .FirstOrDefaultAsync();
        }
        public async Task<Mutationdetailstemp> FetchSingleResultMutationId(int id)
        {
            return await _dbContext.Mutationdetailstemp
                                    .Include(x => x.DamagePayeeRegister)
                                    .Where(x => x.Id == id)
                                    .FirstOrDefaultAsync();
        }

        public async Task<Damagepayeepersonelinfo> GetPersonelInfoFile(int id)
        {
            return await _dbContext.Damagepayeepersonelinfo
                                    .Where(x => x.Id == id)
                                    .FirstOrDefaultAsync();
        }
        public async Task<Allottetype> GetAlloteeTypeFile(int id)
        {
            return await _dbContext.Allottetype
                                    .Where(x => x.Id == id)
                                    .FirstOrDefaultAsync();
        }
    }
}
