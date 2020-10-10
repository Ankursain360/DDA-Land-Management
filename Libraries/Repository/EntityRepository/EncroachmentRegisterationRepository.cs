using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class EncroachmentRegisterationRepository : GenericRepository<EncroachmentRegisteration>, IEncroachmentRegisterationRepository
    {
        public EncroachmentRegisterationRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<List<Department>> GetAllDepartment()
        {
            return await _dbContext.Department.Where(x => x.IsActive == 1).ToListAsync();
        }

        public async Task<List<Division>> GetAllDivision(int zoneId)
        {
            return await _dbContext.Division.Where(x =>x.ZoneId==zoneId && x.IsActive == 1).ToListAsync();
        }

        public async Task<List<EncroachmentRegisteration>> GetAllEncroachmentRegisteration()
        {
            return await _dbContext.EncroachmentRegisteration.Where(x => x.IsActive == 1).ToListAsync();
        }

        public async Task<List<Locality>> GetAllLocalityList(int divisionId)
        {
            return await _dbContext.Locality.Where(x => x.DivisionId == divisionId && x.IsActive == 1).ToListAsync();
        }

        public async Task<List<Zone>> GetAllZone(int departmentId)
        {
            return await _dbContext.Zone.Where(x => x.DepartmentId == departmentId && x.IsActive == 1).ToListAsync();
        }

        public async Task<PagedResult<EncroachmentRegisteration>> GetPagedEncroachmentRegisteration(EncroachmentRegisterationDto model)
        {
            return await _dbContext.EncroachmentRegisteration.Where(x => x.IsActive == 1).GetPaged(model.PageNumber, model.PageSize);
        }
    }
}
