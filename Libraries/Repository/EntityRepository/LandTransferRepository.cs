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
    public class LandtransferRepository : GenericRepository<Landtransfer>, ILandTransferRepository
    {
        public LandtransferRepository(DataContext dbContext) : base(dbContext)
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
        public async Task<List<Locality>> GetAllLocalityList(int divisionId)
        {
            return await _dbContext.Locality.Where(x => x.DivisionId == divisionId && x.IsActive == 1).ToListAsync();
        }
        public async Task<List<Landtransfer>> GetAllLandtransfer()
        {
            return await _dbContext.Landtransfer.ToListAsync();
        }

        public async Task<List<Zone>> GetAllZone(int departmentId)
        {
            return await _dbContext.Zone.Where(x => x.DepartmentId == departmentId).ToListAsync();
        }

        public async Task<PagedResult<Landtransfer>> GetPagedLandtransfer(LandTransferSearchDto model)
        {
            return await _dbContext.Landtransfer.GetPaged<Landtransfer>(model.PageNumber, model.PageSize);
        }

        public  async Task<List<Landtransfer>> GetHistoryDetails(string khasraNo)
        {
            return await _dbContext.Landtransfer.Where(x => x.KhasraNo == (khasraNo).Trim()).ToListAsync();
        }
    }
}