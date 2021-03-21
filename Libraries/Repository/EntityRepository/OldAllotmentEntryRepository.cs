using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Libraries.Repository.EntityRepository
{
    public class OldAllotmentEntryRepository : GenericRepository<Leaseapplication>, IOldAllotmentEntryRepository
    {
        public OldAllotmentEntryRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<PropertyType>> GetAllPropertyType()
        {
            List<PropertyType> list = await _dbContext.PropertyType.Where(x => x.IsActive == 1).ToListAsync();
            return list;
        }
        public async Task<List<Leasetype>> GetAllLeaseType()
        {
            List<Leasetype> list = await _dbContext.Leasetype.Where(x => x.IsActive == 1).ToListAsync();
            return list;
        }
        public async Task<List<Leasepurpose>> GetAllLeasepurpose()
        {
            List<Leasepurpose> list = await _dbContext.Leasepurpose.Where(x => x.IsActive == 1).ToListAsync();
            return list;
        }
        public async Task<List<Leasesubpurpose>> GetAllLeaseSubpurpose(int purposeId)
        {
            List<Leasesubpurpose> list = await _dbContext.Leasesubpurpose.Where(x => x.PurposeUseId == purposeId && x.IsActive == 1).ToListAsync();
            return list;
        }

        //********* save in table  Allotmententry  **********


        public async Task<int> SaveAllotmentDetails(Allotmententry entry)
        {
            _dbContext.Allotmententry.Add(entry);
            var Result = await _dbContext.SaveChangesAsync();
            //return Result > 0 ? true : false;
            return Result;
        }
        public async Task<List<Allotmententry>> GetAllAllotmententry(int id)
        {
            return await _dbContext.Allotmententry.Where(x => x.ApplicationId == id && x.IsActive == 1).ToListAsync();
        }
        public async Task<bool> DeleteEntry(int Id)
        {
            _dbContext.Remove(_dbContext.Allotmententry.Where(x => x.ApplicationId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        //********* save in table  possesionplan  **********
        public async Task<bool> SavepossessionDetails(Possesionplan entry)
        {
            _dbContext.Possesionplan.Add(entry);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        //public async Task<List<Possesionplan>> GetAllPossesionplan(int id)
        //{
        //    return await _dbContext.Possesionplan.Where(x => x.AllotmentId == id && x.IsActive == 1).ToListAsync();
        //}
        //public async Task<bool> DeletePlan(int Id)
        //{

        //}

       

    }
}
