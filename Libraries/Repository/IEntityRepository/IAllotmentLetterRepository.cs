using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;


namespace Libraries.Repository.IEntityRepository
{
  
    public interface IAllotmentLetterRepository : IGenericRepository<Allotmentletter>
    {
        Task<List<Allotmententry>> GetRefNoListforAllotmentLetter();
        Task<PagedResult<Allotmentletter>> GetPagedAllotmentLetter(AllotmentLetterSeearchDto model);
        Task<Allotmentletter> FetchSingleAllotmentLetterDetails(int id);
        string GetDownload(int id);

     }
}
