using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;

namespace Libraries.Repository.EntityRepository
{
    public class MonthlyRosterRepository : GenericRepository<MonthlyRoaster>, IMonthlyRosterRepository
    {
        public MonthlyRosterRepository(DataContext dbcontext) : base(dbcontext)
        { }
    }
}
