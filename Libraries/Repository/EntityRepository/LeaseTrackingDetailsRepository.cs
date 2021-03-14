using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;

using Repository.Common;

namespace Libraries.Repository.EntityRepository
{
  public  class LeaseTrackingDetailsRepository : GenericRepository<Leaseapplication>, ILeaseTrackingDetailsRepository
    {
        public LeaseTrackingDetailsRepository(DataContext dbContext) : base(dbContext)
        {

        }




        public async Task<List<LeaseTrackingListDataDto>> GetPagedLeaseTrackingList(LeaseTrackingListSearchDto model)

        {
            try
            {


                var data = await _dbContext.LoadStoredProcedure("BindLeaseApplicationTrackingDetails")
                                            .WithSqlParams(("P_ReferenceId", model.referenceNo))



                                            .ExecuteStoredProcedureAsync<LeaseTrackingListDataDto>();

                return (List<LeaseTrackingListDataDto>)data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }




    }
}
