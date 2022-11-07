using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Repository.Common;
using Dto.Master;

namespace Libraries.Service.IApplicationService
{
    public interface INewlandawardplotdetailsService
    {
        Task<List<Newlandawardplotdetails>> GetAwardplotdetails();
        Task<List<Newlandawardplotdetails>> GetAllAwardplotdetailsList(NewlandawardplotdetailsSearchDto model);
        Task<List<Newlandawardmasterdetail>> GetAllAWardmaster();
     
        Task<List<Newlandvillage>> GetAllVillage();
        Task<List<Newlandkhasra>> GetAllKhasra(int? villageId);
        Task<List<Newlandawardplotdetails>> GetAwardplotdetailsUsingRepo();
        Task<bool> Update(int id, Newlandawardplotdetails awardplotdetails);
        Task<bool> Create(Newlandawardplotdetails awardplotdetails);
        Task<Newlandawardplotdetails> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        Task<PagedResult<Newlandawardplotdetails>> GetPagedAwardplotdetails(NewlandawardplotdetailsSearchDto model);

        Task<Newlandkhasra> FetchSingleKhasraResult(int? khasraId);
        Task<PagedResult<Newlandawardplotdetails>> GetAllFetchNotificationDetails(NewLandAwardPlotDetailsListSearchDto model);
    }
}
