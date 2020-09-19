using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface IEnchroachmentService
    {

        Task<List<Enchroachment>> GetAllEnchroachment();

        Task<List<Khasra>> BindKhasra();
        Task<List<Acquiredlandvillage>> GetAllVillage();
        Task<List<Natureofencroachment>> GetAllNencroachment();
        Task<List<Reasons>> GetAllReasons();
        Task<List<Enchroachment>> GetEnchroachmentUsingRepo();
        Task<bool> Update(int id, Enchroachment enchroachment);
        Task<bool> Create(Enchroachment enchroachment);
        Task<Enchroachment> FetchSingleResult(int id);
        Task<bool> Delete(int id);
    }
}
