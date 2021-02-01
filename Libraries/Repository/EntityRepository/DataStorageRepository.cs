using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Repository.Common;

namespace Libraries.Repository.EntityRepository
{
    public class DataStorageRepository : GenericRepository<Datastoragedetails>, IDataStorageRepository
    {
        public DataStorageRepository(DataContext dbcontext) : base(dbcontext)
        { }

        public async Task<List<Almirah>> GetAlmirahs()
        {
            var almirahList = await _dbContext.Almirah.Where(x => x.IsActive == 1).ToListAsync();
            return almirahList;
        }

        public async Task<List<Row>> GetRows()
        {
            var rowList = await _dbContext.Row.Where(x => x.IsActive == 1).ToListAsync();
            return rowList;
        }

        public async Task<List<Column>> GetColumns()
        {
            var columnList = await _dbContext.Column.Where(x => x.IsActive == 1).ToListAsync();
            return columnList;
        }

        public async Task<List<Bundle>> GetBundles()
        {
            var bundleList = await _dbContext.Bundle.Where(x => x.IsActive == 1).ToListAsync();
            return bundleList;
        }

        public async Task<List<Locality>> GetLocalities()
        {
            var localityList = await _dbContext.Locality.Where(x => x.IsActive == 1).ToListAsync();
            return localityList;
        }
        public async Task<List<Department>> GetDepartment()
        {
            var departmentList = await _dbContext.Department.Where(x => x.IsActive == 1).ToListAsync();
            return departmentList;
        }
        public async Task<List<Branch>> GetBranch()
        {
            var branchList = await _dbContext.Branch.Where(x => x.IsActive == 1).ToListAsync();
            return branchList;
        }
        public async Task<List<Zone>> GetZones()
        {
            var zoneList = await _dbContext.Zone.Where(x => x.IsActive == 1).ToListAsync();
            return zoneList;
        }


        public async Task<List<Scheme>> GetSchemes()
        {
            var schemeList = await _dbContext.Scheme.Where(x => x.IsActive == 1).ToListAsync();
            return schemeList;
        }

        public async Task<List<Datastoragedetails>> GetDataStorageDetails()
        {
            return await _dbContext.Datastoragedetails.Where(x => x.IsActive == 1).ToListAsync();
        }

        public async Task<PagedResult<Datastoragedetails>> GetPagedDataStorageDetails(DataStorgaeDetailsSearchDto model)
        {
            return await _dbContext.Datastoragedetails.Where(x => x.IsActive == 1).GetPaged(model.PageNumber, model.PageSize);
        }

        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Datastoragedetails.AnyAsync(t => t.Id != id && t.FileNo.ToLower() == name.ToLower());
        }

        public async Task<bool> SaveDetailsOfPartFile(List<Datastoragepartfilenodetails> datastoragepartfilenodetails)
        {
            await _dbContext.AddRangeAsync(datastoragepartfilenodetails);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }
        public async Task<List<FileStatusReportListDataDto>> GetPagedFileStatusReportData(FileStatusReportSearchDto fileStatusReportSearchDto)
       
        {
            try
            {
               
                var data = await _dbContext.LoadStoredProcedure("FileStatus")
                                            .WithSqlParams(("P_departmentId", fileStatusReportSearchDto.Department),
                                            ("UserId", fileStatusReportSearchDto.Userprofile)
                                            , ("P_From_Date", fileStatusReportSearchDto.FromDate)
                                            , ("P_To_Date", fileStatusReportSearchDto.ToDate))


                                            .ExecuteStoredProcedureAsync<FileStatusReportListDataDto>();
                return (List<FileStatusReportListDataDto>)data;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
       
    }
}
