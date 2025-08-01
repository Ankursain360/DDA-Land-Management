﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Libraries.Service.IApplicationService
{
    public interface IPaymentService : IEntityService<Payment>
    {
        Task<List<PaymentPremiumListDataDto>> GetPremiumDrDetails(int AllotmentId, int LeasePaymentTypeId, int userId);
        Task<List<PaymentPremiumListDataDto>> GetGroundRentDrDetails(int userId);
        Task<Possesionplan> GetAllotteeDetails(int userId);
        Task<List<Leasepaymenttype>> LeasePaymentTypeListBind(int allotmentId);
        Task<List<ViewPaymentHistoryListDataDto>> GetAlloteeLeasePaymentDetails(int allotmentId, int leasePaymentTyeId);
        Task<Payment> FetchResultPayment(int id);
    }
}
