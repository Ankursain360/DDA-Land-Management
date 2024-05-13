using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IApplicationModificationDetailsRepository : IGenericRepository<ApplicationModificationDetails>
    {
        DateTime? GetApplicationModificationDetails(); 
    }
}
