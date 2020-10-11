using Libraries.Model;
using Libraries.Repository.Common;
using Model.Entity;
using Repository.IEntityRepository;
using System;
using System.Collections.Generic;

namespace Repository.EntityRepository
{
    public class UserProfileRepository : GenericRepository<Userprofile>, IUserProfileRepository
    {
        public UserProfileRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
