using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Repository.Common;

namespace Libraries.Service.IApplicationService
{
    public interface INewlandvillageService
    {
        Task<List<Newlandvillage>> GetNewlandvillage();
        Task<List<District>> GetAllDistrict();
        Task<List<Tehsil>> GetAllTehsil();
        Task<List<Zone>> GetAllZone();
        Task<List<Newlandvillage>> GetAllVillageList();
        Task<List<Newlandvillage>> GetACquiredlandvillageUsingRepo();
        Task<bool> Update(int id, Newlandvillage newlandvillage);
        Task<bool> Create(Newlandvillage newlandvillage);
        Task<Newlandvillage> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        //  Task<bool> CheckUniqueLoginName(int id, string loginname);
        Task<PagedResult<Newlandvillage>> GetPagedNewlandvillage(NewlandvillageSearchDto model);

        Task<PagedResult<Newlandvillage>> GetPagedNewLandVillageReport(NewlandVillageReportSearchDto model);


    }
}
