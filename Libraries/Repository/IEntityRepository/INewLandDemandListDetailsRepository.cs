using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{

    public interface INewLandDemandListDetailsRepository : IGenericRepository<Newlanddemandlistdetails>
    {
        int GetLocalityByName(string name);
        int GetKhasraByName(string name);
        Task<bool> Any(int id, string fileNo);
        Task<PagedResult<Newlanddemandlistdetails>> GetPagedDMSFileUploadList(NewLandDemandListDetailsSearchDto model);
        Task<Newlanddemandlistdetails> FetchSingleResult(int id);
        Task<List<Newlandvillage>> GetVillageList();
        Task<List<Newlandkhasra>> GetKhasraList(int id);
        Task<List<Newlanddemandlistdetails>> GetAllDemandlistdetails();
        Task<List<Newlanddemandlistdetails>> GetAllDMSFileUploadListList(NewLandDemandListDetailsSearchDto model);
        //*********  Appeal Details **********

        Task<bool> SaveAppeal(Newlandappealdetail newlandappealdetail);
        ////Task<List<Acquiredenhancecompensation>> GetAllAcquiredEnhance(int id);
        Task<bool> DeleteAppeal(int Id);
        Task<Newlandappealdetail> FetchSingleAppeal(int id);
        Task<List<Newlandappealdetail>> GetAllAppeal(int id);
        Task<bool> UpdateAppeal(int id, Newlandappealdetail newlandappealdetail);

        //*********  payment Details **********
        Task<bool> SavePayment(Newlandpaymentdetail newlandpaymentdetail);
        Task<List<Newlandpaymentdetail>> GetAllPayment(int id);
        Task<bool> Deletepayment(int Id);
        Task<Newlandpaymentdetail> FetchSinglePayment(int id);

        Task<bool> UpdatePayment(int id, Newlandpaymentdetail newlandpaymentdetail);
        Task<Newlandpaymentdetail> GetPaymentProofDocument(int Id);
    }
}
