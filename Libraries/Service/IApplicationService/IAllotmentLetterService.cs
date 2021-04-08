using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;


namespace Libraries.Service.IApplicationService
{
    public interface IAllotmentLetterService : IEntityService<Allotmentletter>
    {
        

        Task<bool> Create(Allotmentletter allotmentletter);
        Task<PagedResult<Allotmentletter>> GetPagedAllotmentLetter(AllotmentLetterSeearchDto model);
        Task<Allotmentletter> FetchSingleAllotmentLetterDetails(int id);
        Task<Allotmentletter> FetchAllotmentLetterDetails(int id);
        Task<List<Allotmententry>> GetRefNoListforAllotmentLetter();
        Task<bool> Update(int id, Allotmentletter allotmentletter);
        string GetDownload(int id);
    }
}
