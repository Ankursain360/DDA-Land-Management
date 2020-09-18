using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IEnhancecompensationRepository : IGenericRepository<Enhancecompensation>
    {
        Task<List<Enhancecompensation>> GetAllEnhancecompensation();

        Task<List<Acquiredlandvillage>> GetAllVillage();
        Task<List<Khasra>> BindKhasra();
    }
}
