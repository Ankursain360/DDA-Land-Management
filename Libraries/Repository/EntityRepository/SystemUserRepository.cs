using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;

namespace Libraries.Repository.EntityRepository
{
    public class SystemUserRepository : GenericRepository<SystemUser>, ISystemUserRepository
    {
        public SystemUserRepository(DataContext context) : base(context)
        {
            
        }
    }
}