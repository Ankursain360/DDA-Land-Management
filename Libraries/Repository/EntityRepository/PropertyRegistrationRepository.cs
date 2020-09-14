using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;

namespace Libraries.Repository.EntityRepository
{
    public class PropertyRegistrationRepository : GenericRepository<Propertyregistration>, IPropertyRegistrationRepository
    {

        public PropertyRegistrationRepository(DataContext dbContext) : base(dbContext)
        {

        }

       
    }


}
