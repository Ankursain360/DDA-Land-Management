//using System;
//using System.Collections.Generic;
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
    public interface INewLandDemandListDetailsService : IEntityService<Newlanddemandlistdetails>
    {
        Task<List<Newlandvillage>> GetVillageList();
        Task<List<Newlandkhasra>> GetKhasraList(int id);
        Task<PagedResult<Newlanddemandlistdetails>> GetPagedDMSFileUploadList(NewLandDemandListDetailsSearchDto model);
        Task<bool> Create(Newlanddemandlistdetails newlanddemandlistdetails);
        Task<Newlanddemandlistdetails> FetchSingleResult(int id);
        Task<bool> Update(int id, Newlanddemandlistdetails newlanddemandlistdetails);
        Task<bool> Delete(int id, int userId);
        int GetLocalityByName(string name);
        int GetKhasraByName(string name);
        Task<bool> CheckUniqueName(int id, string fileNo);
        Task<List<Newlanddemandlistdetails>> GetAllDemandlistdetails();
        //*********  Appeali Khasra Details **********

        Task<bool> SaveAppeal(Newlandappealdetail newlandappealdetail);
        Task<List<Newlandappealdetail>> GetAllAppeal(int id);
        Task<bool> DeleteAppeal(int Id);
        Task<Newlandappealdetail> FetchSingleAppeal(int id);

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


