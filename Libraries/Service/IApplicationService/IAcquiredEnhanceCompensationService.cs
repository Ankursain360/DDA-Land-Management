using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface IAcquiredEnhanceCompensationService : IEntityService<Acquiredenhancecompensation>
    {
        Task<bool> Update(int id, Acquiredenhancecompensation acquiredenhancecompensation);
        Task<bool> Create(Acquiredenhancecompensation acquiredenhancecompensation);
        Task<Acquiredenhancecompensation> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<PagedResult<Acquiredenhancecompensation>> GetPagedAcquiredEnhance(SakaniDetailsSearchDto model);
        Task<List<Acquiredenhancecompensation>> GetAllAcquiredEnhance();
        Task<List<Acquiredlandvillage>> GetVillageList();
        Task<List<Khasra>> GetKhasraList(int id);

       
        //*********  Appeali Khasra Details **********

        Task<bool> SaveAppeal(Appealdetail appealdetail);
        Task<List<Appealdetail>> GetAllAppeal(int id);
        Task<bool> DeleteAppeal(int Id);
        Task<Appealdetail> FetchSingleAppeal(int id);

        Task<bool> UpdateAppeal(int id, Appealdetail appealdetail);

        //*********  payment Details **********

        Task<bool> SavePayment(Paymentdetail paymentdetail);
        Task<List<Paymentdetail>> GetAllPayment(int id);
        Task<bool> Deletepayment(int Id);
        Task<Paymentdetail> FetchSinglePayment(int id);

        Task<bool> UpdatePayment(int id, Paymentdetail paymentdetail);
        //********* demand Details **********

        Task<bool> SaveDemand(Demandlistdetails demandlistdetails);
        Task<List<Demandlistdetails>> GetAllDemand(int id);
        Task<bool> DeleteDemand(int Id);
        Task<Demandlistdetails> FetchSingleDemand(int id);

        Task<bool> UpdateDemand(int id, Demandlistdetails demandlistdetails);
    }
}
