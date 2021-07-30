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
    public class KycdemandpaymentdetailstablecRepository : GenericRepository<Kycdemandpaymentdetailstablec>, IKycdemandpaymentdetailstablecRepository
    {
        public KycdemandpaymentdetailstablecRepository(DataContext dbContext) : base(dbContext)
        {


        }
    }
}
