using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface ISakanidetailService
    {

        Task<List<Sakanidetail>> GetSakanidetail();
        Task<List<Khewat>> GetAllKhewat();
        Task<List<Acquiredlandvillage>> GetAllVillage();
        Task<List<Khasra>> BindKhasra();
        Task<List<Sakanidetail>> GetSakanidetailUsingRepo();
        Task<bool> Update(int id, Sakanidetail sakanidetail);
        Task<bool> Create(Sakanidetail sakanidetail);
        Task<Sakanidetail> FetchSingleResult(int id);
        Task<bool> Delete(int id);


    }
}
