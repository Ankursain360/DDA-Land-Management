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
    public class DamagePayeeRegistrationRepository : GenericRepository<Payeeregistration>, IDamagePayeeRegistrationRepository
    {
        public DamagePayeeRegistrationRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Payeeregistration>> GetPagedDamagePayeeRegistration(DamagePayeeRegistrationSearchDto model)
        {
            return await _dbContext.Payeeregistration.GetPaged<Payeeregistration>(model.PageNumber, model.PageSize);
        }

        public async Task<bool> Anyemail(int id, string emailid)
        {
            return await _dbContext.Payeeregistration.AnyAsync(t => t.Id != id && t.EmailId.ToLower() == emailid.ToLower());
        }
        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Payeeregistration.AnyAsync(t => t.Id != id && t.Name.ToLower() == name.ToLower());
        }

        public async Task<List<Payeeregistration>> GetAllPayeeregistration()
        {
            return await _dbContext.Payeeregistration.ToListAsync();
        }
        //public async Task<List<Payeeregistration>> GetAllEncryptPayeeregistration()
        //{
        //    return await _dbContext.Payeeregistration.Where(x => x.IsActive == 1).ToListAsync();
        //}

    }
}
