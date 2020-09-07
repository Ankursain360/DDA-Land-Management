using System.Collections.Generic;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.Common;


namespace Libraries.Service.IApplicationService
{
    public interface IDistrictService : IEntityService<District>
    {
        Task<List<District>> GetAllDistrict();
        Task<List<District>> GetDistrictUsingRepo();

        Task<bool> Update(int id, District district);
        Task<bool> Create(District district);
        Task<District> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        bool CheckUniqueName(int id, District district);   



        //Task<bool> Create();

    }
}
