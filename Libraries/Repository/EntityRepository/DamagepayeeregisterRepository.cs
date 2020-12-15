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
    public class DamagepayeeregisterRepository : GenericRepository<Damagepayeeregister>, IDamagepayeeregisterRepository
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

        public async Task<List<Damagepayeeregister>> GetAllDamagepayeeregister()
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
        public async Task<bool> SavePayeePersonalInfo(Damagepayeepersonelinfo damagepayeepersonelinfo)
        {
            _dbContext.Damagepayeepersonelinfo.Add(damagepayeepersonelinfo);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<List<Damagepayeepersonelinfo>> GetPersonalInfo(int id)
        {
            return await _dbContext.Damagepayeepersonelinfo.Where(x => x.DamagePayeeRegisterId == id && x.IsActive == 1).ToListAsync();
        }

        public async Task<bool> DeletePayeePersonalInfo(int Id)
         {
            _dbContext.RemoveRange(_dbContext.Damagepayeepersonelinfo.Where(x => x.DamagePayeeRegisterId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        //********* rpt 2 Allotte Type **********

        public async Task<bool> SaveAllotteType(List<Allottetype> allottetype)
        {
            await _dbContext.Allottetype.AddRangeAsync(allottetype);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<List<Allottetype>> GetAllottetype(int id)
        { 
             return await _dbContext.Allottetype.Where(x => x.DamagePayeeRegisterId == id && x.IsActive == 1).ToListAsync();
        }
        public async Task<bool> DeleteAllotteType(int Id)
          {
            _dbContext.RemoveRange(_dbContext.Allottetype.Where(x => x.DamagePayeeRegisterId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

    //********* rpt 3 Damage payment history ***********

    public async Task<bool> SavePaymentHistory(Damagepaymenthistory Damagepaymenthistory)
        {
            _dbContext.Damagepaymenthistory.Add(Damagepaymenthistory);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<List<Damagepaymenthistory>> GetPaymentHistory(int id)
        {
            return await _dbContext.Damagepaymenthistory.Where(x => x.DamagePayeeRegisterId == id && x.IsActive == 1).ToListAsync();
        }
        public async Task<bool> DeletePaymentHistory(int Id)
         {
            _dbContext.RemoveRange(_dbContext.Damagepaymenthistory.Where(x => x.DamagePayeeRegisterId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
}
}
