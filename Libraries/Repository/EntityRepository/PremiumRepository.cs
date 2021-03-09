using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;

namespace Libraries.Repository.EntityRepository
{
    public class PremiumRepository : GenericRepository<Premiumrate>, IPremiumRepository
    {
        public PremiumRepository(DataContext dbcontext) : base(dbcontext)
        { }
    }
}
