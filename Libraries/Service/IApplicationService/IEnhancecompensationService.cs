using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface IEnhancecompensationService
    {

        Task<List<Enhancecompensation>> GetAllEnhancecompensation();

        Task<List<Khasra>> BindKhasra();
        Task<List<Acquiredlandvillage>> GetAllVillage();
        Task<List<Enhancecompensation>> GetEnhancecompensationUsingRepo();
        Task<bool> Update(int id, Enhancecompensation enhancecompensation);
        Task<bool> Create(Enhancecompensation enhancecompensation);
        Task<Enhancecompensation> FetchSingleResult(int id);
        Task<bool> Delete(int id);
    }
}
