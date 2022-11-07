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
            var data = await _dbContext.Landtransfer.Where(x => x.IsActive == 1)
                .Include(x => x.PropertyRegistration)
                .Include(x => x.PropertyRegistration.Department)
                .Include(x => x.PropertyRegistration.Zone)
                .Include(x => x.PropertyRegistration.Division)
                .Include(x => x.PropertyRegistration.Locality)
                .GetPaged<Landtransfer>(model.PageNumber, model.PageSize);





            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {

                    case ("DEPARTMENT"):
                        data = null;
                         data = await _dbContext.Landtransfer.Where(x => x.IsActive == 1)
                .Include(x => x.PropertyRegistration)
                .Include(x => x.PropertyRegistration.Department)
                .Include(x => x.PropertyRegistration.Zone)
                .Include(x => x.PropertyRegistration.Division)
                .Include(x => x.PropertyRegistration.Locality)
                .OrderBy(x => x.PropertyRegistration.Department.Name).GetPaged<Landtransfer>(model.PageNumber, model.PageSize);
                        break;

                    case ("ZONE"):
                        data = null;
                        data = await _dbContext.Landtransfer.Where(x => x.IsActive == 1)
               .Include(x => x.PropertyRegistration)
               .Include(x => x.PropertyRegistration.Department)
               .Include(x => x.PropertyRegistration.Zone)
               .Include(x => x.PropertyRegistration.Division)
               .Include(x => x.PropertyRegistration.Locality)
               .OrderBy(x => x.PropertyRegistration.Zone.Name).GetPaged<Landtransfer>(model.PageNumber, model.PageSize);
                        break;


                    case ("DIVISION"):
                        data = null;
                        data = await _dbContext.Landtransfer.Where(x => x.IsActive == 1)
               .Include(x => x.PropertyRegistration)
               .Include(x => x.PropertyRegistration.Department)
               .Include(x => x.PropertyRegistration.Zone)
               .Include(x => x.PropertyRegistration.Division)
               .Include(x => x.PropertyRegistration.Locality)
               .OrderBy(x => x.PropertyRegistration.Division.Name).GetPaged<Landtransfer>(model.PageNumber, model.PageSize);
                        break;





                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {

                    case ("DEPARTMENT"):
                        data = null;
                        data = await _dbContext.Landtransfer.Where(x => x.IsActive == 1)
               .Include(x => x.PropertyRegistration)
               .Include(x => x.PropertyRegistration.Department)
               .Include(x => x.PropertyRegistration.Zone)
               .Include(x => x.PropertyRegistration.Division)
               .Include(x => x.PropertyRegistration.Locality)
               .OrderByDescending(x => x.PropertyRegistration.Department.Name).GetPaged<Landtransfer>(model.PageNumber, model.PageSize);
                        break;

                    case ("ZONE"):
                        data = null;
                        data = await _dbContext.Landtransfer.Where(x => x.IsActive == 1)
               .Include(x => x.PropertyRegistration)
               .Include(x => x.PropertyRegistration.Department)
               .Include(x => x.PropertyRegistration.Zone)
               .Include(x => x.PropertyRegistration.Division)
               .Include(x => x.PropertyRegistration.Locality)
               .OrderByDescending(x => x.PropertyRegistration.Zone.Name).GetPaged<Landtransfer>(model.PageNumber, model.PageSize);
                        break;


                    case ("DIVISION"):
                        data = null;
                        data = await _dbContext.Landtransfer.Where(x => x.IsActive == 1)
               .Include(x => x.PropertyRegistration)
               .Include(x => x.PropertyRegistration.Department)
               .Include(x => x.PropertyRegistration.Zone)
               .Include(x => x.PropertyRegistration.Division)
               .Include(x => x.PropertyRegistration.Locality)
               .OrderByDescending(x => x.PropertyRegistration.Division.Name).GetPaged<Landtransfer>(model.PageNumber, model.PageSize);
                        break;

                }
            }



            return data;


















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
            return await _dbContext.Landtransfer.Include(x => x.PropertyRegistration).Where(x => x.PropertyRegistration.KhasraNo == (khasraNo).Trim() && x.IsActive == 1).ToListAsync();
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

        public async Task<PagedResult<Landtransfer>> GetPagedLandtransferReportData(LandTransferReportDivisionLocalitySearchDto model)
        {
            var data = await _dbContext.Landtransfer
                .Include(x => x.PropertyRegistration)
                .Include(x => x.PropertyRegistration.Locality)
                .Include(x => x.PropertyRegistration.Department)
                .Include(x => x.PropertyRegistration.Zone)
                .Include(x => x.PropertyRegistration.Division)
                   .Include(x => x.TakenOverDepartment)
                     .Include(x => x.HandedOverDepartment)                
                .Where(x => (x.PropertyRegistration.DepartmentId == ((model.departmentId??0) == 0 ? x.PropertyRegistration.DepartmentId : model.departmentId))
                && (x.PropertyRegistration.ZoneId == (model.zoneId == 0 ? x.PropertyRegistration.ZoneId : model.zoneId))
                && (x.PropertyRegistration.DivisionId == (model.divisionId == 0 ? x.PropertyRegistration.DivisionId : model.divisionId))
                && (x.PropertyRegistration.LocalityId == (model.localityId == 0 ? x.PropertyRegistration.LocalityId : model.localityId))
                && (x.HandedOverDate > (model.StartDate == null ? x.HandedOverDate : Convert.ToDateTime(model.StartDate)))
                && (x.HandedOverDate < (model.EndDate == null ? x.HandedOverDate : Convert.ToDateTime(model.EndDate)))
               
                )
                .OrderBy(x => x.PropertyRegistration.Department.Name)
                                                .OrderByDescending(x => x.IsActive)
                                                .ThenBy(x => x.PropertyRegistration.Zone.Name)
                                                .ThenBy(x => x.PropertyRegistration.Division.Name)
                                            .GetPaged<Landtransfer>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("DEPARTMENT"):
                        data.Results = data.Results.OrderBy(x => (x.PropertyRegistration != null ? x.PropertyRegistration.Department.Name : null)).ToList();
                        break;
                    case ("ZONE"):
                        data.Results = data.Results.OrderBy(x => (x.PropertyRegistration != null ? x.PropertyRegistration.Zone.Name : null)).ToList();
                        break;
                    case ("DIVISION"):
                        data.Results = data.Results.OrderBy(x => (x.PropertyRegistration != null ? x.PropertyRegistration.Division.Name : null)).ToList();
                        break;
                    case ("HANDEDOVERDATE"):
                        data.Results = data.Results.OrderBy(x => x.HandedOverDate).ToList();
                        break;
                    case ("TAKENOVERDATE"):
                        data.Results = data.Results.OrderBy(x => x.DateofTakenOver).ToList();
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("DEPARTMENT"):
                        data.Results = data.Results.OrderByDescending(x => (x.PropertyRegistration != null ? x.PropertyRegistration.Department.Name : null)).ToList();
                        break;
                    case ("ZONE"):
                        data.Results = data.Results.OrderByDescending(x => (x.PropertyRegistration != null ? x.PropertyRegistration.Zone.Name : null)).ToList();
                        break;
                    case ("DIVISION"):
                        data.Results = data.Results.OrderByDescending(x => (x.PropertyRegistration != null ? x.PropertyRegistration.Division.Name : null)).ToList();
                        break;
                    case ("HANDEDOVERDATE"):
                        data.Results = data.Results.OrderByDescending(x => x.HandedOverDate).ToList();
                        break;
                    case ("TAKENOVERDATE"):
                        data.Results = data.Results.OrderByDescending(x => x.DateofTakenOver).ToList();
                        break;
                }
            }
            return data;
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
            var data= await _dbContext.Landtransfer
                                   .Include(x => x.PropertyRegistration)
                                   .Include(x => x.PropertyRegistration.Department)
                                   .Include(x => x.PropertyRegistration.Zone)
                                   .Include(x => x.PropertyRegistration.Division)
                                   .Include(x => x.PropertyRegistration.Locality)
                                    .Include(x => x.HandedOverDepartment)
                                   .Where(x => (x.HandedOverDepartmentId == (model.reportType == 1 ? x.HandedOverDepartmentId : (model.departmentId == 0 ? x.HandedOverDepartmentId : model.departmentId)))
                                    && (x.TakenOverDepartmentId == (model.reportType == 0 ? x.TakenOverDepartmentId :
                                    (model.departmentId == 0 ? x.TakenOverDepartmentId : model.departmentId)))
                                    && (x.IsActive == 1))
                                    .OrderBy(x => x.PropertyRegistration.Department.Name)
                                                .OrderByDescending(x => x.IsActive)
                                                .ThenBy(x => x.PropertyRegistration.Zone.Name)
                                                .ThenBy(x => x.PropertyRegistration.Division.Name)
                                            .GetPaged<Landtransfer>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("DEPARTMENT"):
                        data.Results = data.Results.OrderBy(x => (x.PropertyRegistration != null ? x.PropertyRegistration.Department.Name : null)).ToList();
                        break;
                    case ("ZONE"):
                        data.Results = data.Results.OrderBy(x => (x.PropertyRegistration != null ? x.PropertyRegistration.Zone.Name : null)).ToList();
                        break;
                    case ("DIVISION"):
                        data.Results = data.Results.OrderBy(x => (x.PropertyRegistration != null ? x.PropertyRegistration.Division.Name : null)).ToList();
                        break;
                    case ("HANDEDOVERDATE"):
                        data.Results = data.Results.OrderBy(x => x.HandedOverDate).ToList();
                        break;
                    case ("TAKENOVERDATE"):
                        data.Results = data.Results.OrderBy(x => x.DateofTakenOver).ToList();
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("DEPARTMENT"):
                        data.Results = data.Results.OrderByDescending(x => (x.PropertyRegistration != null ? x.PropertyRegistration.Department.Name : null)).ToList();
                        break;
                    case ("ZONE"):
                        data.Results = data.Results.OrderByDescending(x => (x.PropertyRegistration != null ? x.PropertyRegistration.Zone.Name : null)).ToList();
                        break;
                    case ("DIVISION"):
                        data.Results = data.Results.OrderByDescending(x => (x.PropertyRegistration != null ? x.PropertyRegistration.Division.Name : null)).ToList();
                        break;
                    case ("HANDEDOVERDATE"):
                        data.Results = data.Results.OrderByDescending(x => x.HandedOverDate).ToList();
                        break;
                    case ("TAKENOVERDATE"):
                        data.Results = data.Results.OrderByDescending(x => x.DateofTakenOver).ToList();
                        break;
                }
            }
            return data;
        }

        public async Task<PagedResult<Landtransfer>> GetLandTransferReportDataKhasraNumberWise(LandtrasferreportkhasranowiseDto model)
        {
            var data = await _dbContext.Landtransfer
                 .Include(x => x.PropertyRegistration)
                 .Include(x => x.PropertyRegistration.Locality)
                 .Include(x => x.PropertyRegistration.Department)
                 .Include(x => x.PropertyRegistration.Zone)
                 .Include(x => x.PropertyRegistration.Division)
                  
                // .OrderByDescending(x => x.Id)
                 .Where(x => x.PropertyRegistration.Id == model.KhasraNo && x.IsActive == 1 ).GetPaged<Landtransfer>(model.PageNumber, model.PageSize);



            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("DEPARTMENT"):
                        data = null;
                        data = await _dbContext.Landtransfer
                 .Include(x => x.PropertyRegistration)
                 .Include(x => x.PropertyRegistration.Locality)
                 .Include(x => x.PropertyRegistration.Department)
                 .Include(x => x.PropertyRegistration.Zone)
                 .Include(x => x.PropertyRegistration.Division)
                             .Where(x => x.PropertyRegistration.Id == model.KhasraNo && x.IsActive == 1)
                             .OrderBy(x => x.PropertyRegistration.Department.Name)
                            .GetPaged<Landtransfer>(model.PageNumber, model.PageSize);
                        break;
                    case ("ZONE"):
                        data = null;
                        data = await _dbContext.Landtransfer
                 .Include(x => x.PropertyRegistration)
                 .Include(x => x.PropertyRegistration.Locality)
                 .Include(x => x.PropertyRegistration.Department)
                 .Include(x => x.PropertyRegistration.Zone)
                 .Include(x => x.PropertyRegistration.Division)
                             .Where(x => x.PropertyRegistration.Id == model.KhasraNo && x.IsActive == 1)
                             .OrderBy(x => x.PropertyRegistration.Zone.Name)
                            .GetPaged<Landtransfer>(model.PageNumber, model.PageSize);
                        break;
                    case ("DIVISION"):
                        data = null;
                        data = await _dbContext.Landtransfer
                 .Include(x => x.PropertyRegistration)
                 .Include(x => x.PropertyRegistration.Locality)
                 .Include(x => x.PropertyRegistration.Department)
                 .Include(x => x.PropertyRegistration.Zone)
                 .Include(x => x.PropertyRegistration.Division)
                             .Where(x => x.PropertyRegistration.Id == model.KhasraNo && x.IsActive == 1)
                             .OrderBy(x => x.PropertyRegistration.Division.Name)
                            .GetPaged<Landtransfer>(model.PageNumber, model.PageSize);
                        break;
                   
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("DEPARTMENT"):
                        data = null;
                        data = await _dbContext.Landtransfer
                 .Include(x => x.PropertyRegistration)
                 .Include(x => x.PropertyRegistration.Locality)
                 .Include(x => x.PropertyRegistration.Department)
                 .Include(x => x.PropertyRegistration.Zone)
                 .Include(x => x.PropertyRegistration.Division)
                             .Where(x => x.PropertyRegistration.Id == model.KhasraNo && x.IsActive == 1)
                             .OrderByDescending(x => x.PropertyRegistration.Department.Name)
                            .GetPaged<Landtransfer>(model.PageNumber, model.PageSize);
                        break;
                    case ("ZONE"):
                        data = null;
                        data = await _dbContext.Landtransfer
                 .Include(x => x.PropertyRegistration)
                 .Include(x => x.PropertyRegistration.Locality)
                 .Include(x => x.PropertyRegistration.Department)
                 .Include(x => x.PropertyRegistration.Zone)
                 .Include(x => x.PropertyRegistration.Division)
                             .Where(x => x.Id == model.KhasraNo && x.IsActive == 1)
                             .OrderByDescending(x => x.PropertyRegistration.Zone.Name)
                            .GetPaged<Landtransfer>(model.PageNumber, model.PageSize);
                        break;
                    case ("DIVISION"):
                        data = null;
                        data = await _dbContext.Landtransfer
                 .Include(x => x.PropertyRegistration)
                 .Include(x => x.PropertyRegistration.Locality)
                 .Include(x => x.PropertyRegistration.Department)
                 .Include(x => x.PropertyRegistration.Zone)
                 .Include(x => x.PropertyRegistration.Division)
                             .Where(x => x.PropertyRegistration.Id == model.KhasraNo && x.IsActive == 1)
                             .OrderByDescending(x => x.PropertyRegistration.Division.Name)
                            .GetPaged<Landtransfer>(model.PageNumber, model.PageSize);
                        break;

                }
            }



            return data;






        }

        public async Task<List<Landtransfer>> GetAllLandTransferList()
        {
            return await _dbContext.Landtransfer.Include(x => x.PropertyRegistration)
                 .Include(x => x.PropertyRegistration.Locality)
                 .Include(x => x.PropertyRegistration.Department)
                 .Include(x => x.PropertyRegistration.Zone)
                 .Include(x => x.PropertyRegistration.Division).Where(x => x.IsActive == 1).ToListAsync();
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
                var data =await _dbContext.Landtransfer.Where(y => y.IsValidate == 0).Select(y => y.PropertyRegistrationId).ToListAsync();
                var data1= await _dbContext.Propertyregistration.Include(x => x.ClassificationOfLand)
                                .Include(x => x.Department)
                                .Include(x => x.Zone)
                                .Include(x => x.Division)
                                .Include(x => x.Locality)
                                .Include(x => x.DisposalType)
                                .Include(x => x.MainLandUse)
                                    .Where(x => (x.IsDeleted == 1 && x.IsValidate == 1 && (x.IsDisposed == 1 || x.IsDisposed == null))
                                    && (x.ClassificationOfLandId == (model.classificationofland == 0 ? x.ClassificationOfLandId : model.classificationofland))
                                   && (x.DepartmentId == (model.department == 0 ? x.DepartmentId : model.department)) && (x.ZoneId == (model.zone == 0 ? x.ZoneId : model.zone))
                                   && (x.DivisionId == (model.division == 0 ? x.DivisionId : model.division))
                                   && (x.PlannedUnplannedLand == (model.plannedUnplannedLand == "0" ? x.PlannedUnplannedLand : model.plannedUnplannedLand)) && (!data.Contains(x.Id))

                                   )
                                    .OrderBy(x => x.InventoriedInId)
                                                .OrderByDescending(x => x.IsActive)
                                                .ThenBy(x => x.PlannedUnplannedLand)
                                                .ThenBy(x => x.ClassificationOfLand.Name)
                                                .ThenBy(x => x.Department.Name)
                                                .ThenBy(x => x.Zone.Name)
                                                .ThenBy(x => x.Division.Name)
                                            .GetPaged<Propertyregistration>(model.PageNumber, model.PageSize);

                int SortOrder = (int)model.SortOrder;
                if (SortOrder == 1)
                {
                    switch (model.SortBy.ToUpper())
                    {
                        case ("INVENTORIEDIN"):
                            data1.Results = data1.Results.OrderBy(x => x.InventoriedInId).ToList();
                            break;
                        case ("PLANNEDUNPLANNED"):
                            data1.Results = data1.Results.OrderBy(x => x.PlannedUnplannedLand).ToList();
                            break;
                        case ("CLASSIFICATION"):
                            data1.Results = data1.Results.OrderBy(x => (x.ClassificationOfLand != null ? x.ClassificationOfLand.Name : null)).ToList();
                            break;
                        case ("DEPARTMENT"):
                            data1.Results = data1.Results.OrderBy(x => (x.Department != null ? x.Department.Name : null)).ToList();
                            break;
                        case ("ZONE"):
                            data1.Results = data1.Results.OrderBy(x => (x.Zone != null ? x.Zone.Name : null)).ToList();
                            break;
                        case ("DIVISION"):
                            data1.Results = data1.Results.OrderBy(x => (x.Division != null ? x.Division.Name : null)).ToList();
                            break;
                    }
                }
                else if (SortOrder == 2)
                {
                    switch (model.SortBy.ToUpper())
                    {
                        case ("INVENTORIEDIN"):
                            data1.Results = data1.Results.OrderByDescending(x => x.InventoriedInId).ToList();
                            break;
                        case ("PLANNEDUNPLANNED"):
                            data1.Results = data1.Results.OrderByDescending(x => x.PlannedUnplannedLand).ToList();
                            break;
                        case ("CLASSIFICATION"):
                            data1.Results = data1.Results.OrderByDescending(x => (x.ClassificationOfLand != null ? x.ClassificationOfLand.Name : null)).ToList();
                            break;
                        case ("DEPARTMENT"):
                            data1.Results = data1.Results.OrderByDescending(x => (x.Department != null ? x.Department.Name : null)).ToList();
                            break;
                        case ("ZONE"):
                            data1.Results = data1.Results.OrderByDescending(x => (x.Zone != null ? x.Zone.Name : null)).ToList();
                            break;
                        case ("DIVISION"):
                            data1.Results = data1.Results.OrderByDescending(x => (x.Division != null ? x.Division.Name : null)).ToList();
                            break;
                    }
                }
                return data1;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<PagedResult<Propertyregistration>> GetPropertyRegisterationUnverifiedDataForLandTransfer(LandTransferSearchDto model)
        {
            try
            {
                var data = await _dbContext.Landtransfer.Where(y => y.IsValidate == 0).Select(y => y.PropertyRegistrationId).ToListAsync();
                var data1 = await _dbContext.Propertyregistration.Include(x => x.ClassificationOfLand)
                                .Include(x => x.Department)
                                .Include(x => x.Zone)
                                .Include(x => x.Division)
                                .Include(x => x.Locality)
                                .Include(x => x.DisposalType)
                                .Include(x => x.MainLandUse)
                                    .Where(x => (x.IsDeleted == 1 && x.IsValidate == 1)
                                                                       && (x.ClassificationOfLandId == (model.classificationofland == 0 ? x.ClassificationOfLandId : model.classificationofland))
                                   && (x.DepartmentId == (model.department == 0 ? x.DepartmentId : model.department)) && (x.ZoneId == (model.zone == 0 ? x.ZoneId : model.zone))
                                   && (x.DivisionId == (model.division == 0 ? x.DivisionId : model.division))
                                   && (x.PlannedUnplannedLand == (model.plannedUnplannedLand == "0" ? x.PlannedUnplannedLand : model.plannedUnplannedLand)) && (data.Contains(x.Id))

                                   )
                                   .OrderBy(x => x.InventoriedInId)
                                                .OrderByDescending(x => x.IsActive)
                                                .ThenBy(x => x.PlannedUnplannedLand)
                                                .ThenBy(x => x.ClassificationOfLand.Name)
                                                .ThenBy(x => x.Department.Name)
                                                .ThenBy(x => x.Zone.Name)
                                                .ThenBy(x => x.Division.Name)
                                            .GetPaged<Propertyregistration>(model.PageNumber, model.PageSize);

                int SortOrder = (int)model.SortOrder;
                if (SortOrder == 1)
                {
                    switch (model.SortBy.ToUpper())
                    {
                        case ("INVENTORIEDIN"):
                            data1.Results = data1.Results.OrderBy(x => x.InventoriedInId).ToList();
                            break;
                        case ("PLANNEDUNPLANNED"):
                            data1.Results = data1.Results.OrderBy(x => x.PlannedUnplannedLand).ToList();
                            break;
                        case ("CLASSIFICATION"):
                            data1.Results = data1.Results.OrderBy(x => (x.ClassificationOfLand != null ? x.ClassificationOfLand.Name : null)).ToList();
                            break;
                        case ("DEPARTMENT"):
                            data1.Results = data1.Results.OrderBy(x => (x.Department != null ? x.Department.Name : null)).ToList();
                            break;
                        case ("ZONE"):
                            data1.Results = data1.Results.OrderBy(x => (x.Zone != null ? x.Zone.Name : null)).ToList();
                            break;
                        case ("DIVISION"):
                            data1.Results = data1.Results.OrderBy(x => (x.Division != null ? x.Division.Name : null)).ToList();
                            break;
                    }
                }
                else if (SortOrder == 2)
                {
                    switch (model.SortBy.ToUpper())
                    {
                        case ("INVENTORIEDIN"):
                            data1.Results = data1.Results.OrderByDescending(x => x.InventoriedInId).ToList();
                            break;
                        case ("PLANNEDUNPLANNED"):
                            data1.Results = data1.Results.OrderByDescending(x => x.PlannedUnplannedLand).ToList();
                            break;
                        case ("CLASSIFICATION"):
                            data1.Results = data1.Results.OrderByDescending(x => (x.ClassificationOfLand != null ? x.ClassificationOfLand.Name : null)).ToList();
                            break;
                        case ("DEPARTMENT"):
                            data1.Results = data1.Results.OrderByDescending(x => (x.Department != null ? x.Department.Name : null)).ToList();
                            break;
                        case ("ZONE"):
                            data1.Results = data1.Results.OrderByDescending(x => (x.Zone != null ? x.Zone.Name : null)).ToList();
                            break;
                        case ("DIVISION"):
                            data1.Results = data1.Results.OrderByDescending(x => (x.Division != null ? x.Division.Name : null)).ToList();
                            break;
                    }
                }
                return data1;
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
            return await _dbContext.Landtransfer.Include(x => x.PropertyRegistration).Where(x => (x.IsActive == 1) && (x.PropertyRegistrationId == id) &&(x.IsValidate==0)).FirstOrDefaultAsync();
        }

        public async Task<bool> CreateHistory(PropertyRegistrationHistory propertyRegistrationHistory)
        {
            await _dbContext.Propertyregistrationhistory.AddAsync(propertyRegistrationHistory);
            await _dbContext.SaveChangesAsync();
            return propertyRegistrationHistory.Id > 0 ? true : false;
        }


        public async Task<List<Propertyregistration>> GetAllHandOverTakeOverList()
        {

            var data = await _dbContext.Landtransfer.Where(y => y.IsValidate == 0).Select(y => y.PropertyRegistrationId).ToListAsync();

            var data1 = await _dbContext.Propertyregistration.Include(x => x.ClassificationOfLand)
                                .Include(x => x.Department)
                                .Include(x => x.Zone)
                                .Include(x => x.Division)
                                .Include(x => x.Locality)
                                .Include(x => x.DisposalType)
                                .Include(x => x.MainLandUse)
                                        .Where(x => (x.IsDeleted == 1 && x.IsValidate == 1) && (!data.Contains(x.Id))


                                        )

                                        .ToListAsync();
            return data1;
        }

        public async Task<List<Propertyregistration>> GetAllPropertyRegisterationDataForLandTransferList(LandTransferSearchDto model)
        {
           
                var data = await _dbContext.Landtransfer.Where(y => y.IsValidate == 0).Select(y => y.PropertyRegistrationId).ToListAsync();
                var data1 = await _dbContext.Propertyregistration.Include(x => x.ClassificationOfLand)
                                .Include(x => x.Department)
                                .Include(x => x.Zone)
                                .Include(x => x.Division)
                                .Include(x => x.Locality)
                                .Include(x => x.DisposalType)
                                .Include(x => x.MainLandUse)
                                    .Where(x => (x.IsDeleted == 1 && x.IsValidate == 1 && (x.IsDisposed == 1 || x.IsDisposed == null))
                                    && (x.ClassificationOfLandId == (model.classificationofland == 0 ? x.ClassificationOfLandId : model.classificationofland))
                                   && (x.DepartmentId == (model.department == 0 ? x.DepartmentId : model.department)) && (x.ZoneId == (model.zone == 0 ? x.ZoneId : model.zone))
                                   && (x.DivisionId == (model.division == 0 ? x.DivisionId : model.division))
                                   && (x.PlannedUnplannedLand == (model.plannedUnplannedLand == "0" ? x.PlannedUnplannedLand : model.plannedUnplannedLand)) && (!data.Contains(x.Id))).ToListAsync();

            return data1;
        }
        public async Task<List<Propertyregistration>> GetAllUnverifiedTransferRecordList()
        {

            var data = await _dbContext.Landtransfer.Where(y => y.IsValidate == 0).Select(y => y.PropertyRegistrationId).ToListAsync();

            var data1 = await _dbContext.Propertyregistration.Include(x => x.ClassificationOfLand)
                                .Include(x => x.Department)
                                .Include(x => x.Zone)
                                .Include(x => x.Division)
                                .Include(x => x.Locality)
                                .Include(x => x.DisposalType)
                                .Include(x => x.MainLandUse)
                                        .Where(x => (x.IsDeleted == 1 && x.IsValidate == 1) && (data.Contains(x.Id))


                                        )

                                        .ToListAsync();
            return data1;
        }



    }
}