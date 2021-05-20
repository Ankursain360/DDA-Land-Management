using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Libraries.Repository.EntityRepository
{
    public class DamageRateListRepository : GenericRepository<Resratelisttypea>, IDamageRateListRepository
    {

        public DamageRateListRepository(DataContext dbContext) : base(dbContext)
        {

        }


    }
}
