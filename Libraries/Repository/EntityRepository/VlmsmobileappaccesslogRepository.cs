using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Repository.EntityRepository
{
    public class VlmsmobileappaccesslogRepository : GenericRepository<Vlmsmobileappaccesslog>, IVlmsmobileappaccesslogRepository
    {
        public VlmsmobileappaccesslogRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
