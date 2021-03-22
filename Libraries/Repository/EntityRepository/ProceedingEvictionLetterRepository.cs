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
    public class ProceedingEvictionLetterRepository : GenericRepository<Requestforproceeding>, IProceedingEvictionLetterRepository
    {

        public ProceedingEvictionLetterRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<ProceedingEvictionLetterViewLetterDataDto> BindProceedingConvictionLetterData(int id)
        {
            try
            {
                var data = await _dbContext.LoadStoredProcedure("ProceedingLetterGenerate")
                                            .WithSqlParams(("P_Id", id))
                                            .ExecuteStoredProcedureAsync<ProceedingEvictionLetterViewLetterDataDto>();

                return (ProceedingEvictionLetterViewLetterDataDto)data.FirstOrDefault();
            }

            catch (Exception ex)
            {
                return null;
            }
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

        public async Task<Requestforproceeding> FetchProceedingConvictionLetterData(int id)
        {
            return await _dbContext.Requestforproceeding
                                    .Include(x => x.Allotment)
                                    .Include(x => x.Allotment.Application)
                                    .Where(x => x.Id == id)
                                    .FirstOrDefaultAsync();
        }

        public async Task<string> GetLetterRefNo(int id)
        {
            var Data =await (from f in _dbContext.Requestforproceeding
                        where f.Id == id
                        orderby f.Id descending
                        select f.LetterReferenceNo).FirstOrDefaultAsync();

            return Data;
        }
    }


}
