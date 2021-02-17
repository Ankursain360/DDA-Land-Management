using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;

namespace Libraries.Repository.IEntityRepository
{
    public interface IEnchroachmentRepository : IGenericRepository<Enchroachment>
    {
        Task<List<Enchroachment>> GetAllEnchroachment();

        Task<List<Acquiredlandvillage>> GetAllVillage();
        Task<List<Natureofencroachment>> GetAllNencroachment();
        Task<List<Reasons>> GetAllReasons();
        Task<PagedResult<Enchroachment>> GetPagedEnchroachment(EnchroachmentSearchDto model);
        Task<List<Khasra>> BindKhasra();
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
