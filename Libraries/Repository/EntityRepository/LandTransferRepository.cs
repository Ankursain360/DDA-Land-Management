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
            return await _dbContext.Department.Where(x => x.IsActive == 1).ToListAsync();
        }
        public async Task<List<Division>> GetAllDivision(int zoneId)
        {
            return await _dbContext.Division.Where(x => x.ZoneId == zoneId && x.IsActive == 1).ToListAsync();
        }
        public async Task<List<Locality>> GetAllLocalityList(int divisionId)
        {
            return await _dbContext.Locality.Where(x => x.DivisionId == divisionId && x.IsActive == 1).ToListAsync();
        }
        public async Task<List<Landtransfer>> GetAllLandtransfer()
        {
            return await _dbContext.Landtransfer.Where(x => x.IsActive == 1).ToListAsync();
        }
        public async Task<List<Zone>> GetAllZone(int departmentId)
        {
            return await _dbContext.Zone.Where(x => x.DepartmentId == departmentId && x.IsActive == 1).ToListAsync();
        }
        public async Task<PagedResult<Landtransfer>> GetPagedLandtransfer(LandTransferSearchDto model)
        {
            return await _dbContext.Landtransfer.Where(x => x.IsActive == 1)
                .Include(x => x.Department)
                .Include(x => x.Zone)
                .Include(x => x.Division)
                .Include(x => x.Locality)
                .GetPaged<Landtransfer>(model.PageNumber, model.PageSize);
        }





        public async Task<List<Landtransfer>> GetHistoryDetails(string khasraNo)
        {
            return await _dbContext.Landtransfer.Where(x => x.KhasraNo == (khasraNo).Trim() && x.IsActive == 1).ToListAsync();
        }
        public async Task<List<Landtransfer>> GetAllLandTransfer()
        {
            return await _dbContext.Landtransfer.Where(x => x.IsActive == 1).ToListAsync();
        }
        public async Task<List<Landtransfer>> GetLandTransferReportData(int department, int zone, int division, int locality)
        {
            var data = await _dbContext.Landtransfer
                .Include(x => x.Locality)
                .Include(x => x.Department)
                .Include(x => x.Zone)
                .Include(x => x.Division)
                .OrderByDescending(x => x.Id)
                .Where(x => (x.DepartmentId == (department == 0 ? x.DepartmentId : department))
                && (x.ZoneId == (zone == 0 ? x.ZoneId : zone))
                && (x.DivisionId == (division == 0 ? x.DivisionId : division))
                && (x.LocalityId == (locality == 0 ? x.LocalityId : locality))).ToListAsync();
            return data;
        }


       
        public async Task<List<Landtransfer>> GetLandTransferReportDataDepartmentWise(int reportType, int departmentId) //added by ishu
        {
            var data = await _dbContext.Landtransfer
                    .Include(x => x.Locality)
                    .Include(x => x.Department)
                    .Include(x => x.Zone)
                    .Include(x => x.Division)

                    .Where(x => (x.HandedOverDepartmentId == (reportType == 1 ? x.HandedOverDepartmentId :
                    (departmentId == 0 ? x.HandedOverDepartmentId : departmentId)))
                    && (x.TakenOverDepartmentId == (reportType == 0 ? x.TakenOverDepartmentId :
                    (departmentId == 0 ? x.TakenOverDepartmentId : departmentId)))
                    && (x.IsActive == 1))
                     .OrderByDescending(x => x.Id)
                    .ToListAsync();
            return data;
        }

        public async Task<PagedResult<Landtransfer>> GetPagedLandtransferReportDeptWise(LandTransferSearchDto model)//added by ishu
        {
            return await _dbContext.Landtransfer
                                   .Include(x => x.Department)
                                   .Include(x => x.Zone)
                                   .Include(x => x.Division)
                                   .Include(x => x.Locality)
                                   .Where(x => (x.HandedOverDepartmentId == (model.reportType == 1 ? x.HandedOverDepartmentId : (model.departmentId == 0 ? x.HandedOverDepartmentId : model.departmentId)))
                                    && (x.TakenOverDepartmentId == (model.reportType == 0 ? x.TakenOverDepartmentId :
                                    (model.departmentId == 0 ? x.TakenOverDepartmentId : model.departmentId)))
                                    && (x.IsActive == 1))
                                   .OrderByDescending(x => x.Id)
                                   .GetPaged<Landtransfer>(model.PageNumber, model.PageSize);
        }

        public async Task<List<Landtransfer>> GetLandTransferReportDataKhasraNumberWise(int id)
        {
            return await _dbContext.Landtransfer
                 .Include(x => x.Locality)
                 .Include(x => x.Department)
                 .Include(x => x.Zone)
                 .Include(x => x.Division)
                 .OrderByDescending(x => x.Id)
                 .Where(x => x.Id == id && x.IsActive == 1).ToListAsync();
        }

        public async Task<List<Landtransfer>> GetAllLandTransferList()
        {
            return await _dbContext.Landtransfer.Where(x => x.IsActive == 1).ToListAsync();
        }



        public async Task<List<Landtransfer>> GetLandTransferReportdataHandover(int id)
        {
            return await _dbContext.Landtransfer
                 .Include(x => x.Locality)
                 .Include(x => x.Department)
                 .Include(x => x.Zone)
                 .Include(x => x.Division)
                 .OrderByDescending(x => x.Id)
                 .Where(x => x.Id == id && x.IsActive == 1).ToListAsync();
        }


    }
}