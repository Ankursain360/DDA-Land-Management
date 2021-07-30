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
    }
}
