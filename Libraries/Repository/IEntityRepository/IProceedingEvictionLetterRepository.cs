using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Libraries.Repository.IEntityRepository
{
    public interface IProceedingEvictionLetterRepository : IGenericRepository<Requestforproceeding>
    {
        Task<List<RefNoNameDto>> BindRefNoNameList();
        Task<string> GetLetterRefNo(int id);
        Task<Requestforproceeding> FetchProceedingConvictionLetterData(ProceedingEvictionLetterSearchDto model);
    }
}