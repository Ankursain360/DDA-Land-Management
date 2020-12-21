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
    public class DamagepayeeregisterRepository : GenericRepository<Damagepayeeregistertemp>, IDamagepayeeregisterRepository
    {
        public DamagepayeeregisterRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Damagepayeeregister>> GetPagedDamagepayeeregister(DamagepayeeregisterSearchDto model)
        {
            return await _dbContext.Damagepayeeregister
                .Where(x => x.IsActive == 1)
                .Include(x => x.Locality)
                .Include(x => x.District)
                .GetPaged<Damagepayeeregister>(model.PageNumber, model.PageSize);
        }

        public async Task<List<Damagepayeeregistertemp>> GetAllDamagepayeeregisterTemp()
        {
                 return await _dbContext.Damagepayeeregistertemp
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
        public async Task<bool> SavePayeePersonalInfoTemp(Damagepayeepersonelinfotemp damagepayeepersonelinfotemp)
        {
            _dbContext.Damagepayeepersonelinfotemp.Add(damagepayeepersonelinfotemp);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<List<Damagepayeepersonelinfotemp>> GetPersonalInfoTemp(int id)
        {
            return await _dbContext.Damagepayeepersonelinfotemp.Where(x => x.DamagePayeeRegisterTempId == id && x.IsActive == 1).ToListAsync();
        }

        public async Task<bool> DeletePayeePersonalInfoTemp(int Id)
         {
            _dbContext.RemoveRange(_dbContext.Damagepayeepersonelinfotemp.Where(x => x.DamagePayeeRegisterTempId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<Damagepayeepersonelinfotemp> GetAadharFilePath(int Id)
        {
            return await _dbContext.Damagepayeepersonelinfotemp.Where(x => x.Id == Id && x.IsActive == 1).FirstOrDefaultAsync();
        }
        public async Task<Damagepayeepersonelinfotemp> GetPanFilePath(int Id)
        {
            return await _dbContext.Damagepayeepersonelinfotemp.Where(x => x.Id == Id && x.IsActive == 1).FirstOrDefaultAsync();
        }
        public async Task<Damagepayeepersonelinfotemp> GetPhotographPath(int Id)
        {
            return await _dbContext.Damagepayeepersonelinfotemp.Where(x => x.Id == Id && x.IsActive == 1).FirstOrDefaultAsync();
        }
        public async Task<Damagepayeepersonelinfotemp> GetSignaturePath(int Id)
        {
            return await _dbContext.Damagepayeepersonelinfotemp.Where(x => x.Id == Id && x.IsActive == 1).FirstOrDefaultAsync();
        }


        //********* rpt 2 Allotte Type **********

        public async Task<bool> SaveAllotteTypeTemp(List<Allottetypetemp> allottetypetemp)
        {
            await _dbContext.Allottetypetemp.AddRangeAsync(allottetypetemp);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<List<Allottetypetemp>> GetAllottetypeTemp(int id)
        { 
             return await _dbContext.Allottetypetemp.Where(x => x.DamagePayeeRegisterTempId == id && x.IsActive == 1).ToListAsync();
        }
        public async Task<bool> DeleteAllotteTypeTemp(int Id)
          {
            _dbContext.RemoveRange(_dbContext.Allottetypetemp.Where(x => x.DamagePayeeRegisterTempId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<Allottetypetemp> GetATSFilePath(int Id)
        {
            return await _dbContext.Allottetypetemp.Where(x => x.Id == Id && x.IsActive == 1).FirstOrDefaultAsync();
        }


        //********* rpt 3 Damage payment history ***********

        public async Task<bool> SavePaymentHistoryTemp(List<Damagepaymenthistorytemp> damagepaymenthistorytemp)
        {
            await _dbContext.Damagepaymenthistorytemp.AddRangeAsync(damagepaymenthistorytemp);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<List<Damagepaymenthistorytemp>> GetPaymentHistoryTemp(int id)
        {
            return await _dbContext.Damagepaymenthistorytemp.Where(x => x.DamagePayeeRegisterTempId == id && x.IsActive == 1).ToListAsync();
        }
        public async Task<bool> DeletePaymentHistoryTemp(int Id)
         {
            _dbContext.RemoveRange(_dbContext.Damagepaymenthistorytemp.Where(x => x.DamagePayeeRegisterTempId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
}
}
