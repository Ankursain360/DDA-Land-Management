using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IEntityRepository
{
    public interface IVillageRepository : IGenericRepository<Village>
    {
        Task<List<Village>> GetVillage();
    }
}
