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
    public class SelfAssessmentDamageRepository : GenericRepository<Damagepayeeregister>, ISelfAssessmentDamageRepository
    {
        public SelfAssessmentDamageRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Damagepayeeregister>> GetPagedDamagepayeeregister(DamagepayeeregistertempSearchDto model)
        {
            return await _dbContext.Damagepayeeregister
                .Where(x => x.IsActive == 1)
                .Include(x => x.Locality)
                .Include(x => x.District)
                .GetPaged<Damagepayeeregister>(model.PageNumber, model.PageSize);
        }

        public async Task<List<Damagepayeeregister>> GetAllDamagepayeeregisterTemp()
        {
            return await _dbContext.Damagepayeeregister
           .Where(x => x.IsActive == 1)
           .Include(x => x.Locality)
           .Include(x => x.District)
           .ToListAsync();
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
        //********* rpt 1 Persolnal info of damage assesse ***********
        public async Task<bool> SavePayeePersonalInfoTemp(Damagepayeepersonelinfo damagepayeepersonelinfotemp)
        {
            _dbContext.Damagepayeepersonelinfo.Add(damagepayeepersonelinfotemp);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<List<Damagepayeepersonelinfo>> GetPersonalInfoTemp(int id)
        {
            return await _dbContext.Damagepayeepersonelinfo.Where(x => x.DamagePayeeRegisterTempId == id && x.IsActive == 1).ToListAsync();
        }

        public async Task<bool> DeletePayeePersonalInfoTemp(int Id)
        {
            _dbContext.RemoveRange(_dbContext.Damagepayeepersonelinfo.Where(x => x.DamagePayeeRegisterTempId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<Damagepayeepersonelinfo> GetPersonelInfoFilePath(int Id)
        {
            return await _dbContext.Damagepayeepersonelinfo.Where(x => x.Id == Id && x.IsActive == 1).FirstOrDefaultAsync();
        }

        //********* rpt 2 Allotte Type **********

        public async Task<bool> SaveAllotteTypeTemp(List<Allottetype> allottetypetemp)
        {
            await _dbContext.Allottetype.AddRangeAsync(allottetypetemp);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<List<Allottetype>> GetAllottetypeTemp(int id)
        {
            return await _dbContext.Allottetype.Where(x => x.DamagePayeeRegisterTempId == id && x.IsActive == 1).ToListAsync();
        }
        public async Task<bool> DeleteAllotteTypeTemp(int Id)
        {
            _dbContext.RemoveRange(_dbContext.Allottetype.Where(x => x.DamagePayeeRegisterTempId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<Allottetype> GetAllotteTypeSingleResult(int id)
        {
            return await _dbContext.Allottetype.Where(x => x.Id == id && x.IsActive == 1).FirstOrDefaultAsync();
        }

        //********* rpt 3 Damage payment history ***********

        public async Task<bool> SavePaymentHistoryTemp(List<Damagepaymenthistory> damagepaymenthistorytemp)
        {
            await _dbContext.Damagepaymenthistory.AddRangeAsync(damagepaymenthistorytemp);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<List<Damagepaymenthistory>> GetPaymentHistoryTemp(int id)
        {
            return await _dbContext.Damagepaymenthistory.Where(x => x.DamagePayeeRegisterTempId == id && x.IsActive == 1).ToListAsync();
        }
        public async Task<bool> DeletePaymentHistoryTemp(int Id)
        {
            _dbContext.RemoveRange(_dbContext.Damagepaymenthistory.Where(x => x.DamagePayeeRegisterTempId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<Damagepayeeregister> FetchSelfAssessmentUserId(int userId)
        {
            return await _dbContext.Damagepayeeregister
                                    .Include(x => x.Damagepayeepersonelinfo)
                                    .Include(x => x.Damagepaymenthistory)
                                    .Include(x => x.Allottetype)
                                    .Where(x => x.UserId == userId)
                                    .FirstOrDefaultAsync();
        }

        public async Task<Rebate> GetRebateValue()
        {
            return await _dbContext.Rebate
                              .Where(x => x.IsActive == 1 && x.IsRebateOn == 1 
                              && x.FromDate <= DateTime.Now && x.ToDate>= DateTime.Now 
                              )
                              .FirstOrDefaultAsync();
        }
        public async Task<Damagepaymenthistory> GetPaymentHistorySingleResult(int id)
        {
            return await _dbContext.Damagepaymenthistory.Where(x => x.Id == id && x.IsActive == 1).FirstOrDefaultAsync();
        }
    }
}
