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

        public async Task<List<Zone>> GetAllZone(int departmentId)
        {
            return await _dbContext.Zone.Where(x => x.DepartmentId == departmentId && x.IsActive == 1).ToListAsync();
        }
        public async Task<PagedResult<Landtransfer>> GetPagedLandtransfer(LandTransferSearchDto model)
        {
            return await _dbContext.Landtransfer.Where(x => x.IsActive == 1)
                .Include(x => x.PropertyRegistration)
                .Include(x => x.PropertyRegistration.Department)
                .Include(x => x.PropertyRegistration.Zone)
                .Include(x => x.PropertyRegistration.Division)
                .Include(x => x.PropertyRegistration.Locality)
                .GetPaged<Landtransfer>(model.PageNumber, model.PageSize);
        }

        public async Task<PagedResult<Landtransfer>> GetPagedCurrentStatusLandtransfer(LandTransferSearchDto model) //added by ishu
        {
            return await _dbContext.Landtransfer
                .Include(x => x.PropertyRegistration)
                .Include(x => x.PropertyRegistration.Department)
                .Include(x => x.PropertyRegistration.Zone)
                .Include(x => x.PropertyRegistration.Division)
                .Include(x => x.PropertyRegistration.Locality)
                .Where(x => (x.IsActive == 1)
                && (x.PropertyRegistration.DepartmentId == (model.departmentId == 0 ? x.PropertyRegistration.DepartmentId : model.departmentId))
                && (x.PropertyRegistration.ZoneId == (model.zoneId == 0 ? x.PropertyRegistration.ZoneId : model.zoneId))
                && (x.PropertyRegistration.DivisionId == (model.divisionId == 0 ? x.PropertyRegistration.DivisionId : model.divisionId))
                && (x.PropertyRegistration.LocalityId == (model.localityId == 0 ? x.PropertyRegistration.LocalityId : model.localityId)))
               
               .OrderByDescending(x => x.Id)
                .GetPaged<Landtransfer>(model.PageNumber, model.PageSize);

        }




        public async Task<List<Landtransfer>> GetHistoryDetails(string khasraNo)
        {
            return await _dbContext.Landtransfer.Include(x=>x.PropertyRegistration).Where(x => x.PropertyRegistration.KhasraNo == (khasraNo).Trim() && x.IsActive == 1).ToListAsync();
        }
        public async Task<List<Landtransfer>> GetLandTransferReportData(int department, int zone, int division, int locality)
        {
            var data = await _dbContext.Landtransfer
                .Include(x => x.PropertyRegistration.Locality)
                .Include(x => x.PropertyRegistration.Department)
                .Include(x => x.PropertyRegistration.Zone)
                .Include(x => x.PropertyRegistration.Division)
                .OrderByDescending(x => x.Id)
                .Where(x => (x.PropertyRegistration.DepartmentId == (department == 0 ? x.PropertyRegistration.DepartmentId : department))
                && (x.PropertyRegistration.ZoneId == (zone == 0 ? x.PropertyRegistration.ZoneId : zone))
                && (x.PropertyRegistration.DivisionId == (division == 0 ? x.PropertyRegistration.DivisionId : division))
                && (x.PropertyRegistration.LocalityId == (locality == 0 ? x.PropertyRegistration.LocalityId : locality))).ToListAsync();

            return data;
        }

        public async Task<PagedResult<Landtransfer>> GetPagedLandtransferReportData(LandTransferSearchDto model)
        {
            return await _dbContext.Landtransfer
                .Include(x => x.PropertyRegistration)
                .Include(x => x.PropertyRegistration.Locality)
                .Include(x => x.PropertyRegistration.Department)
                .Include(x => x.PropertyRegistration.Zone)
                .Include(x => x.PropertyRegistration.Division)
                .Where(x=>(x.PropertyRegistration.DepartmentId == (model.departmentId == 0 ? x.PropertyRegistration.DepartmentId : model.departmentId))
                && (x.PropertyRegistration.ZoneId == (model.zoneId == 0 ? x.PropertyRegistration.ZoneId : model.zoneId))
                && (x.PropertyRegistration.DivisionId == (model.divisionId == 0 ? x.PropertyRegistration.DivisionId : model.divisionId))
                && (x.PropertyRegistration.LocalityId == (model.localityId == 0 ? x.PropertyRegistration.LocalityId : model.localityId)))
                .OrderByDescending(x => x.Id)
                .GetPaged<Landtransfer>(model.PageNumber, model.PageSize);
        }

        public async Task<List<Landtransfer>> GetLandTransferReportDataDepartmentWise(int reportType, int departmentId) //added by ishu
        {
            var data = await _dbContext.Landtransfer
                    .Include(x => x.PropertyRegistration)
                    .Include(x => x.PropertyRegistration.Locality)
                    .Include(x => x.PropertyRegistration.Department)
                    .Include(x => x.PropertyRegistration.Zone)
                    .Include(x => x.PropertyRegistration.Division)

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
                                   .Include(x => x.PropertyRegistration)
                                   .Include(x => x.PropertyRegistration.Department)
                                   .Include(x => x.PropertyRegistration.Zone)
                                   .Include(x => x.PropertyRegistration.Division)
                                   .Include(x => x.PropertyRegistration.Locality)
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
                 .Include(x => x.PropertyRegistration)
                 .Include(x => x.PropertyRegistration.Locality)
                 .Include(x => x.PropertyRegistration.Department)
                 .Include(x => x.PropertyRegistration.Zone)
                 .Include(x => x.PropertyRegistration.Division)
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
                 .Include(x => x.PropertyRegistration)
                 .Include(x => x.PropertyRegistration.Locality)
                 .Include(x => x.PropertyRegistration.Department)
                 .Include(x => x.PropertyRegistration.Zone)
                 .Include(x => x.PropertyRegistration.Division)
                 .OrderByDescending(x => x.Id)
                 .Where(x => x.Id == id && x.IsActive == 1).ToListAsync();
        }
        //*****************current status of land *********
        public async Task<bool> SaveCurrentstatusoflandhistory(Currentstatusoflandhistory model)
        {
            _dbContext.Currentstatusoflandhistory.Add(model);

            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<List<Currentstatusoflandhistory>> GetCurrentstatusoflandhistory(int landtransferId)
        {
            return await _dbContext.Currentstatusoflandhistory.Where(x => x.Id == landtransferId && x.IsActive == 1).ToListAsync();
        }

        public async Task<PagedResult<Propertyregistration>> GetPropertyRegisterationDataForLandTransfer(LandTransferSearchDto model)
        {
            try
            {
                return await _dbContext.Propertyregistration.Include(x => x.ClassificationOfLand)
                                .Include(x => x.Department)
                                .Include(x => x.Zone)
                                .Include(x => x.Division)
                                .Include(x => x.Locality)
                                .Include(x => x.DisposalType)
                                .Include(x => x.MainLandUse)
                                    .Where(x => (x.IsDeleted == 1 && x.IsValidate == 1))
                                    .OrderByDescending(x => x.Id)
                                .GetPaged<Propertyregistration>(model.PageNumber, model.PageSize);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Landtransfer>> GetAllLandtransfer()
        {
            return await _dbContext.Landtransfer.
                Include(x => x.PropertyRegistration).
                Include(x => x.PropertyRegistration.Department).
                Include(x => x.PropertyRegistration.Zone).
                Include(x => x.PropertyRegistration.Division).
                Include(x => x.PropertyRegistration.Locality).
                Where(x => x.IsActive == 1).ToListAsync();
        }

        public async Task<List<Landtransfer>> GetAllLandTransfer(int propertyRegistrationId)
        {
            return await _dbContext.Landtransfer.
                                Include(x => x.HandedOverDepartment).
                                Include(x => x.HandedOverZone).
                                Include(x => x.HandedOverDivision).
                                Include(x => x.TakenOverDepartment).
                                Include(x => x.TakenOverZone).
                                Include(x => x.TakenOverDivision).
                                Include(x => x.PropertyRegistration).
                                Include(x => x.PropertyRegistration.Department).
                                Include(x => x.PropertyRegistration.Zone).
                                Include(x => x.PropertyRegistration.Division).
                                Include(x => x.PropertyRegistration.Locality).Where(x => (x.IsActive == 1) && (x.PropertyRegistrationId == propertyRegistrationId)).ToListAsync();
        }

        public async Task<Landtransfer> FetchSingleResultWithPropertyRegistration(int id)
        {
            return await _dbContext.Landtransfer.Include(x => x.PropertyRegistration).Where(x => (x.IsActive == 1) && (x.Id == id)).FirstOrDefaultAsync();
        }
    }
}