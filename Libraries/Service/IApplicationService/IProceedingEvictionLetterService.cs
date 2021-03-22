using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Libraries.Service.IApplicationService
{
    public interface IProceedingEvictionLetterService : IEntityService<Requestforproceeding>
    {
        Task<List<RefNoNameDto>> BindRefNoNameList();
        Task<string> GetLetterRefNo(int id);
        Task<bool> UpdateRequestProceeding(ProceedingEvictionLetterSearchDto model, int UserId);
        Task<Requestforproceeding> FetchProceedingConvictionLetterData(int id);
        Task<bool> UpdateRequestProceedingUpload(int id, Requestforproceeding requestforproceeding);
        Task<bool> UpdateRequestProceedingIsSend(Requestforproceeding data, int userId);
        Task<ProceedingEvictionLetterViewLetterDataDto> BindProceedingConvictionLetterData(int refNoNameId);
    }
}
