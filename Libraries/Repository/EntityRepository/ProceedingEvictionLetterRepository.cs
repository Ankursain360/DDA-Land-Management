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
    public class ProceedingEvictionLetterRepository : GenericRepository<Leaseapplication>, IProceedingEvictionLetterRepository
    {

        public ProceedingEvictionLetterRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<RefNoNameDto>> BindRefNoNameList()
        {
            try
            {
                var data = await _dbContext.LoadStoredProcedure("BindDropdownForProceedingEvictionLetterRefNoName")
                                            .WithOutParams()
                                            .ExecuteStoredProcedureAsync<RefNoNameDto>();

                return (List<RefNoNameDto>)data;
            }
            
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<string> GetLetterRefNo(int id)
        {
            var Data =await (from f in _dbContext.Requestforproceeding
                        where f.AllotmentId == id
                        orderby f.Id descending
                        select f.LetterReferenceNo).FirstOrDefaultAsync();

            return Data;
        }
    }


}
