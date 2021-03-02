using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface INewlandannexure1Repository : IGenericRepository<Newlandannexure1>
    {
        Task<PagedResult<Newlandannexure1>> GetPagedNewlandannexure1(Newlandannexure1SearchDto model);
        Task<List<Newlandannexure1>> GetAllNewlandannexure1();
        Task<List<Muncipality>> GetAllMunicipality();
        Task<List<District>> GetAllDistrict();
      

        //********* rpt ! Khasra Details **********

        Task<bool> SaveKhasraRpt(Newlandannexure1khasrarpt Khasra);
        Task<List<Newlandannexure1khasrarpt>> GetAllKhasraRpt(int id);
        Task<bool> DeleteKhasraRpt(int Id);
    }
}
