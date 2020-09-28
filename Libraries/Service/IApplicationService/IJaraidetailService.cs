using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface IJaraidetailService
    {

        Task<List<Jaraidetail>> GetJaraidetail();
        Task<List<Khewat>> GetAllKhewat();
        Task<List<Acquiredlandvillage>> GetAllVillage();
        Task<List<Khasra>> BindKhasra();
        Task<List<Taraf>> GetAllTaraf();
        Task<List<Khatauni>> GetAllKhatauni();
        Task<List<Jaraidetail>> GetJaraidetailUsingRepo();
        Task<bool> Update(int id, Jaraidetail jaraidetail);
        Task<bool> Create(Jaraidetail jaraidetail);
        Task<Jaraidetail> FetchSingleResult(int id);
        Task<bool> Delete(int id);

    }
}
