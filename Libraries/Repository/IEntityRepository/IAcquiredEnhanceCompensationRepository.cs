using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;

namespace Libraries.Repository.IEntityRepository
{
    public interface IAcquiredEnhanceCompensationRepository : IGenericRepository<Acquiredenhancecompensation>
    {
        Task<PagedResult<Acquiredenhancecompensation>> GetPagedAcquiredEnhance(SakaniDetailsSearchDto model);
        Task<List<Acquiredenhancecompensation>> GetAllAcquiredEnhance();
        Task<List<Acquiredlandvillage>> GetVillageList();
        Task<List<Khasra>> GetKhasraList(int id);

       
        //*********  Appeal Details **********

        Task<bool> SaveAppeal(Appealdetail appealdetail);
        ////Task<List<Acquiredenhancecompensation>> GetAllAcquiredEnhance(int id);
        Task<bool> DeleteAppeal(int Id);
        Task<Appealdetail> FetchSingleAppeal(int id);
        Task<List<Appealdetail>> GetAllAppeal(int id);
        Task<bool> UpdateAppeal(int id, Appealdetail appealdetail);
        //*********  demand Details **********

        Task<bool> SaveDemand(Demandlistdetails demandlistdetails);
        Task<List<Demandlistdetails>> GetAllDemand(int id);
        Task<bool> DeleteDemand(int Id);
        Task<Demandlistdetails> FetchSingleDemand(int id);

        Task<bool> UpdateDemand(int id, Demandlistdetails demandlistdetails);
        //*********  payment Details **********
        Task<bool> SavePayment(Paymentdetail demandlistdetails);
        Task<List<Paymentdetail>> GetAllPayment(int id);
        Task<bool> Deletepayment(int Id);
        Task<Paymentdetail> FetchSinglePayment(int id);

        Task<bool> UpdatePayment(int id, Paymentdetail paymentdetail);
    }
}
