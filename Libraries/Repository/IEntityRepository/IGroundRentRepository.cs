using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;

namespace Libraries.Repository.IEntityRepository
{

    public interface IGroundRentRepository : IGenericRepository<Groundrent>
    {
        Task<List<Groundrent>> GetAllGroundRent();
        Task<List<Groundrent>> GetAllGroundRentList();
        Task<PagedResult<Groundrent>> GetPagedGroundRent(GroundrentSearchDto model);
        Task<List<Leasepurpose>> GetAllLeasepurpose();
        Task<List<Leasesubpurpose>> GetAllLeaseSubpurpose(int purposeId);
    }
}
