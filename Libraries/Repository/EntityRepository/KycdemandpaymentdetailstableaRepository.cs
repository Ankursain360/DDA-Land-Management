using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Repository.Common;

namespace Libraries.Repository.EntityRepository
{
    public class KycdemandpaymentdetailstableaRepository : GenericRepository<Kycdemandpaymentdetailstablea>, IKycdemandpaymentdetailstableaRespository
    {
        public KycdemandpaymentdetailstableaRepository(DataContext dbcontext) : base(dbcontext)
        { }

        public async Task<List<Kycdemandpaymentdetailstablea>> FetchResult(int id)
        {
            return await _dbContext.Kycdemandpaymentdetailstablea
                                     .Include(x => x.Kyc)
                                     .Include(x => x.DemandPayment)
                                     .Where(x => x.DemandPaymentId == id)
                                     .ToListAsync();
        }
        public async Task<bool> RollBackEntry(int Id)//added by ishu
        {
            _dbContext.RemoveRange(_dbContext.Kycdemandpaymentdetailstablea.Where(x => x.DemandPaymentId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
    }
}
