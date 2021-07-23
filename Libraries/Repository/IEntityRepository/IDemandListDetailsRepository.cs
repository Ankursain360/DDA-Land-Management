using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{

    public interface IDemandListDetailsRepository : IGenericRepository<Demandlistdetails>
    {
        int GetLocalityByName(string name);
        int GetKhasraByName(string name);
        Task<bool> Any(int id, string fileNo);
        Task<PagedResult<Demandlistdetails>> GetPagedDMSFileUploadList(DemandListDetailsSearchDto model);
        Task<Demandlistdetails> FetchSingleResult(int id);
        Task<List<Acquiredlandvillage>> GetVillageList();
        Task<List<Khasra>> GetKhasraList(int id);
        Task<List<Demandlistdetails>> GetAllDemandlistdetails();
        //*********  Appeal Details **********

        Task<bool> SaveAppeal(Appealdetail appealdetail);
        ////Task<List<Acquiredenhancecompensation>> GetAllAcquiredEnhance(int id);
        Task<bool> DeleteAppeal(int Id);
        Task<Appealdetail> FetchSingleAppeal(int id);
        Task<List<Appealdetail>> GetAllAppeal(int id);
        Task<bool> UpdateAppeal(int id, Appealdetail appealdetail);
      
        //*********  payment Details **********
        Task<bool> SavePayment(Paymentdetail demandlistdetails);
        Task<List<Paymentdetail>> GetAllPayment(int id);
        Task<bool> Deletepayment(int Id);
        Task<Paymentdetail> FetchSinglePayment(int id);

        Task<bool> UpdatePayment(int id, Paymentdetail paymentdetail);
        Task<Paymentdetail> GetPaymentProofDocument(int Id);
    }
}
