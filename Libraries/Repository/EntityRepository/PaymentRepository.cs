using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository.Common;

namespace Libraries.Repository.EntityRepository
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {

        public PaymentRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<Possesionplan> GetAllotteeDetails(int userId)
        {
            return await _dbContext.Possesionplan
                                   .Include(x => x.Allotment)
                                   .Include(x => x.Allotment.Application)
                                   .Include(x => x.Allotment.LeasePurposesType)
                                    .Include(x => x.Allotment.LeasesType)
                                   .Where(x => x.Allotment.Application.UserId == userId)
                                   .FirstOrDefaultAsync();
        }

        public async Task<List<PaymentPremiumListDataDto>> GetGroundRentDrDetails(int userId)
        {
            try
            {
                var data = await _dbContext.LoadStoredProcedure("PaymentGroundRentIndexListBind")
                                            .WithSqlParams(("P_UserId", userId))
                                            .ExecuteStoredProcedureAsync<PaymentPremiumListDataDto>();

                return (List<PaymentPremiumListDataDto>)data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<PaymentPremiumListDataDto>> GetPremiumDrDetails(int AllotmentId, int LeasePaymentTypeId, int userId)
        {
            try
            {
                var data = await _dbContext.LoadStoredProcedure("PaymentPremiumIndexListBind")
                                            .WithSqlParams(("P_AllotmentId", AllotmentId), ("P_LeasePaymentTypeId", LeasePaymentTypeId), ("P_UserId", userId))
                                            .ExecuteStoredProcedureAsync<PaymentPremiumListDataDto>();

                return (List<PaymentPremiumListDataDto>)data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
