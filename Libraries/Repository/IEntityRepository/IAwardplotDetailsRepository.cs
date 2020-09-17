using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface IAwardplotDetailsRepository : IGenericRepository<Awardplotdetails>
    {
        Task<List<Awardplotdetails>> GetAwardplotdetails();
        Task<List<Awardmasterdetail>> GetAllAWardmaster();
        Task<List<Acquiredlandvillage>> GetAllVillage();
        Task<List<Khasra>> BindKhasra();

    }
}
