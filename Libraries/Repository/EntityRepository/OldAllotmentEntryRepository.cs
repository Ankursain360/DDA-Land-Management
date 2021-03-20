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
       


        //********* rpt ! Owner Details **********

        //public async Task<bool> SaveOwner(Jaraiowner Jaraiowner)
        //{
        //    _dbContext.Jaraiowner.Add(Jaraiowner);
        //    var Result = await _dbContext.SaveChangesAsync();
        //    return Result > 0 ? true : false;
        //}
        //public async Task<List<Jaraiowner>> GetAllOwner(int id)
        //{
        //    return await _dbContext.Jaraiowner.Where(x => x.JaraiDetailId == id && x.IsActive == 1).ToListAsync();
        //}

        //public async Task<bool> DeleteOwner(int Id)
        //{
        //    _dbContext.RemoveRange(_dbContext.Jaraiowner.Where(x => x.JaraiDetailId == Id));
        //    var Result = await _dbContext.SaveChangesAsync();
        //    return Result > 0 ? true : false;
        //}

    }
}
