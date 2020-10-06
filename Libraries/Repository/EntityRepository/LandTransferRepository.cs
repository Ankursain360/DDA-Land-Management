using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.EntityRepository
{
    public class LandTransferRepository : GenericRepository<LandTransfer>, ILandTransferRepository
    {
        public LandTransferRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<List<Department>> GetAllDepartment()
        {
            return await _dbContext.Department.ToListAsync();
        }

        public async Task<List<Division>> GetAllDivision(int zoneId)
        {
            return await _dbContext.Division.Where(x => x.ZoneId == zoneId).ToListAsync();
        }

        public async Task<List<LandTransfer>> GetAllLandTransfer()
        {
            return await _dbContext.LandTransfer.ToListAsync();
        }

        public async Task<List<Zone>> GetAllZone(int departmentId)
        {
            return await _dbContext.Zone.Where(x => x.DepartmentId == departmentId).ToListAsync();
        }

        public async Task<PagedResult<LandTransfer>> GetPagedLandTransfer(LandTransferSearchDto model)
        {
            return await _dbContext.LandTransfer.GetPaged<LandTransfer>(model.PageNumber, model.PageSize);
        }
    }
}