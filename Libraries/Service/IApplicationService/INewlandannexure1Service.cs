using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;

namespace Libraries.Service.IApplicationService
{
    public interface INewlandannexure1Service
    {
        Task<PagedResult<Newlandannexure1>> GetPagedNewlandannexure1(Newlandannexure1SearchDto model);
        Task<List<Newlandannexure1>> GetAllNewlandannexure1();
        Task<List<Muncipality>> GetAllMunicipality();
        Task<List<District>> GetAllDistrict();
        Task<bool> Update(int id, Newlandannexure1 Annexure1);
        Task<bool> Create(Newlandannexure1 Annexure1);
        Task<Newlandannexure1> FetchSingleResult(int id);
        Task<bool> Delete(int id);

        //********* rpt ! Khasra Details **********

        Task<bool> SaveKhasraRpt(Newlandannexure1khasrarpt Khasra);
        Task<List<Newlandannexure1khasrarpt>> GetAllKhasraRpt(int id);
        Task<bool> DeleteKhasraRpt(int Id);
    }
}
