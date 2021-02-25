using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.Common;
using Dto.Search;
using Libraries.Repository.Common;

namespace Libraries.Service.IApplicationService
{
    public interface INewlandAppealdetailservice : IEntityService<Newlandappealdetail>
    {
        Task<List<Newlandappealdetail>> GetNewlandappealdetails();
        Task<List<Newlandappealdetail>> GetNewLandAppealdetailUsingRepo();

        Task<bool> Update(int id, Newlandappealdetail Newlandappealdetail);
        Task<bool> Create(Newlandappealdetail Newlandappealdetail);
        Task<Newlandappealdetail> FetchSingleResult(int id);
        Task<bool> Delete(int id);


        Task<PagedResult<Newlandappealdetail>> GetPagedNewlandAppealdetail(NewlandAppealdetailSearchDto model);



    }
}

