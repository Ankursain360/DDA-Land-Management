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



    }
}
