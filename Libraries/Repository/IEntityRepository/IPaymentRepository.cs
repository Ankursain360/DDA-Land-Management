using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Libraries.Repository.IEntityRepository
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
        Task<Possesionplan> GetAllotteeDetails(int userId);
        Task<List<PaymentPremiumListDataDto>> GetPremiumDrDetails(int AllotmentId, int LeasePaymentTypeId, int userId);
        Task<List<PaymentPremiumListDataDto>> GetGroundRentDrDetails(int userId);
    }
}