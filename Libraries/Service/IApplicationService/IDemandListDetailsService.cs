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
    public interface IDemandListDetailsService : IEntityService<Demandlistdetails>
    {
        Task<List<Acquiredlandvillage>> GetVillageList();
        Task<List<Khasra>> GetKhasraList(int id);
        Task<PagedResult<Demandlistdetails>> GetPagedDMSFileUploadList(DemandListDetailsSearchDto model);
        Task<bool> Create(Demandlistdetails demandlistdetails);
        Task<Demandlistdetails> FetchSingleResult(int id);
        Task<bool> Update(int id, Demandlistdetails demandlistdetails);
        Task<bool> Delete(int id, int userId);
        int GetLocalityByName(string name);
        int GetKhasraByName(string name);
        Task<bool> CheckUniqueName(int id, string fileNo);
        Task<List<Demandlistdetails>> GetAllDemandlistdetails();
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
        Task<Paymentdetail> GetPaymentProofDocument(int Id);

    }
}
