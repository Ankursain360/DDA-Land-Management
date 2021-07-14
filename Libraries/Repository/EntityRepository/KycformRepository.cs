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
    }
}
