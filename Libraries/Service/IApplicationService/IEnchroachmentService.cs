using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Repository.Common;

namespace Libraries.Service.IApplicationService
{
    public interface IEnchroachmentService
    {

        Task<List<Enchroachment>> GetAllEnchroachment();
        Task<List<Khasra>> BindKhasra(int? villageId);
        //Task<List<Khasra>> BindKhasra();
        Task<List<Acquiredlandvillage>> GetAllVillage();
        Task<List<Natureofencroachment>> GetAllNencroachment();
        Task<List<Reasons>> GetAllReasons();
        Task<List<Enchroachment>> GetEnchroachmentUsingRepo();
        Task<bool> Update(int id, Enchroachment enchroachment);
        Task<bool> Create(Enchroachment enchroachment);
        Task<Enchroachment> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<PagedResult<Enchroachment>> GetPagedEnchroachment(EnchroachmentSearchDto model);
        Task<List<EncrochpeopleListDataDto>> GetPagedEncrocherPeople(EncrocherNameSearchDto model, int UserId);
        //********* repeater ! Owner Details **********

        Task<bool> SaveEName(EncrocherPeople encrocherPeople);
        Task<List<EncrocherPeople>> GetAllOwner(int id);
        Task<bool> DeleteOwner(int Id);
        //********* repeater ! Payment Details **********

        Task<bool> SavePayment(Enchroachmentpayment enchroachmentpayment);
        Task<List<Enchroachmentpayment>> GetAllPayment(int id);
        Task<bool> DeletePayment(int Id);

    }
}
