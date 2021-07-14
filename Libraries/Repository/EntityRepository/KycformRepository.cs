using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Repository.IEntityRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
     
    public class KycformRepository : GenericRepository<Kycform>, IKycformRepository
    {
        public KycformRepository(DataContext dbContext) : base(dbContext)
        {

        }
       
        public async Task<List<Leasetype>> GetAllLeasetypeList()
        {
            List<Leasetype> List = await _dbContext.Leasetype.Where(x => x.IsActive == 1).ToListAsync();
            return List;
        }
        public async Task<List<Branch>> GetAllBranchList()
        {
            List<Branch> List = await _dbContext.Branch.Where(x => x.IsActive == 1&& x.DepartmentId == 50).ToListAsync();
            return List;
        }
        public async Task<List<PropertyType>> GetAllPropertyTypeList()
        {
            List<PropertyType> List = await _dbContext.PropertyType.Where(x => x.IsActive == 1).ToListAsync();
            return List;
        }
        public async Task<List<Zone>> GetAllZoneList()
        {
            List<Zone> List = await _dbContext.Zone.Where(x => x.IsActive == 1).ToListAsync();
            return List;
        }
        public async Task<List<Locality>> GetLocalityList()
        {
            List<Locality> List = await _dbContext.Locality.Where(x => x.IsActive == 1).ToListAsync();
            return List;
        }
        //********* rpt ! Kycleasepaymentrpt Details **********
        public async Task<bool> Saveleasepayment(Kycleasepaymentrpt payment)
        {
            _dbContext.Kycleasepaymentrpt.Add(payment);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        //********* rpt ! Kyclicensepaymentrpt Details **********
        public async Task<bool> Savelicensepayment(Kyclicensepaymentrpt payment)
        {
            _dbContext.Kyclicensepaymentrpt.Add(payment);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
    }
}
