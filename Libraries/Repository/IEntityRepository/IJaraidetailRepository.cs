using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IJaraidetailRepository : IGenericRepository<Jaraidetail>
    {
        Task<List<Jaraidetail>> GetJaraidetail();
        Task<List<Khewat>> GetAllKhewat();
        Task<List<Acquiredlandvillage>> GetAllVillage();
        Task<List<Khasra>> BindKhasra();
        Task<List<Taraf>> GetAllTaraf();
        Task<List<Khatauni>> GetAllKhatauni();
    }
}
