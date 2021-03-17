using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Microsoft.EntityFrameworkCore;
using Model.Entity;
using Repository.IEntityRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using System;
using Repository.Common;

namespace Repository.EntityRepository
{
   
    public class ApprovalCompleteRepository : GenericRepository<Approvalproccess>, IApprovalCompleteRepository
    {
        public ApprovalCompleteRepository(DataContext dbContext) : base(dbContext)
        {

        }


        public async Task<List<ApprovalCompleteListDataDto>> GetApprovalCompleteModule(ApprovalCompleteSearchDto model)

        {
            try
            {
                

                var data = await _dbContext.LoadStoredProcedure("BindPendingAtUserEnd")
                                            .WithSqlParams(("User_Id", model.userid))



                                            .ExecuteStoredProcedureAsync<ApprovalCompleteListDataDto>();

                return (List<ApprovalCompleteListDataDto>)data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }



        public async Task<List<ApprovalCompleteListDataDto>> BindModuleName()
        {
            try
            {
                //int SortOrder = (int)model.SortOrder;
                var data = await _dbContext.LoadStoredProcedure("BindPendingAtUserEnd")
                                            .WithOutParams()
                                            .ExecuteStoredProcedureAsync<ApprovalCompleteListDataDto>();

                return (List<ApprovalCompleteListDataDto>)data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }






    }
}
