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
using Dto.Search;


using Repository.Common;

namespace Libraries.Repository.EntityRepository
{
    public class LeasesignupRepository : GenericRepository<Leasesignup>, ILeasesignupRepository
    {
        public LeasesignupRepository(DataContext dbContext) : base(dbContext)
        {

        }

        //public async Task<Leasesignup> Sendotpf(int? khasraId)
        //{
        //    return await _dbContext.Khasra.Where(x => x.Id == khasraId).SingleOrDefaultAsync();
        //}


        public async Task<bool> ValidateMobileEmail(string mobile, string email)
        {
            return await _dbContext.Leasesignup.AnyAsync(t => t.MobileNo == mobile || t.EmailId.ToLower() == email.ToLower());
        }






    }
}
