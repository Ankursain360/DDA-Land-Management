using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;

namespace Libraries.Repository.EntityRepository
{
    public class PropertyRegistrationRepository : GenericRepository<Propertyregistration>, IPropertyRegistrationRepository
    {
        public PropertyRegistrationRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<List<Propertyregistration>> GetAllPropertyregistration(int UserId)
        {
            var Iscreated = _dbContext.Propertyregistration.Where(x => x.CreatedBy == UserId).Count();
            if (UserId == 2 || Iscreated > 0)
            {
                var data = await _dbContext.Propertyregistration.Include(x => x.ClassificationOfLand)
                    .Include(x => x.Department)
                    .Include(x => x.Division)
                    .Include(x => x.DisposalType)
                    .Include(x => x.MainLandUse)
                    .Include(x => x.Zone)
                    .Include(x => x.Locality)
                        .Where(x => x.IsDeleted == 1)
                        .OrderByDescending(x => x.Id).ToListAsync();
                return data;

            }
            else
            {
                var data = await _dbContext.Propertyregistration
                                .Include(x => x.ClassificationOfLand)
                                .Include(x => x.Department)
                                .Include(x => x.Division)
                                .Include(x => x.DisposalType)
                                .Include(x => x.MainLandUse)
                                .Include(x => x.Zone)
                                .Include(x => x.Locality)
                                    .Where(x => x.IsDeleted == 1 && x.IsValidate == 1)
                                    .OrderByDescending(x => x.Id).ToListAsync();
                return data;

            }

        }

        public async Task<List<Classificationofland>> GetClassificationOfLandDropDownList()
        {
            var badCodes = new[] { 3, 5 };
            List<Classificationofland> ClassificationoflandList = await _dbContext.Classificationofland
                                                                        .Where(x => x.IsActive == 1 && !badCodes.Contains(x.Id))
                                                                        .ToListAsync();
            return ClassificationoflandList;
        }

        public async Task<List<Classificationofland>> GetClassificationOfLandDropDownListMOR()
        {
            var badCodes = new[] { 3, 5 };
            List<Classificationofland> ClassificationoflandList = await _dbContext.Classificationofland
                                                                        .Where(x => x.IsActive == 1 && badCodes.Contains(x.Id))
                                                                        .ToListAsync();
            return ClassificationoflandList;
        }

        public async Task<List<Department>> GetDepartmentDropDownList()
        {
            List<Department> DepartmentList = await _dbContext.Department.Where(x => x.IsActive == 1).ToListAsync();
            return DepartmentList;
        }

        public string GetDisposalFile(int id)
        {
            var File = (from f in _dbContext.Propertyregistration
                        where f.Id == id
                        select f.DisposalTypeFilePath).First();

            return File;
        }

        public async Task<List<Disposaltype>> GetDisposalTypeDropDownList()
        {
            List<Disposaltype> DisposaltypeList = await _dbContext.Disposaltype.Where(x => x.IsActive == 1).ToListAsync();
            return DisposaltypeList;
        }

        public async Task<List<Division>> GetDivisionDropDownList(int zoneId)
        {
            List<Division> DivisionList = await _dbContext.Division.Where(x => x.ZoneId == zoneId && x.IsActive == 1)
                                                .ToListAsync();
            return DivisionList;
        }

        public async Task<List<Propertyregistration>> GetPrimaryListNoList(int divisionId)
        {
            List<Propertyregistration> PrimaryListNoList = await _dbContext.Propertyregistration
                                                                .Where(x => x.DivisionId == divisionId && x.IsActive == 1)
                                                                .ToListAsync();
            return PrimaryListNoList;
        }
        public async Task<List<Locality>> GetLocalityDropDownList2(int divisionId)
        {
            List<Locality> localityList = await _dbContext.Locality.Where(x => x.DivisionId == divisionId)
                                                .Where(x => x.IsActive == 1).ToListAsync();
            return localityList;
        }
        public string GetFile(int id)
        {
            var File = (from f in _dbContext.Propertyregistration
                        where f.Id == id
                        select f.LayoutFilePath).First();

            return File;
        }

        public string GetGeoFile(int id)
        {
            var File = (from f in _dbContext.Propertyregistration
                        where f.Id == id
                        select f.GeoFilePath).First();

            return File;
        }

        public string GetHandedOverFile(int id)
        {
            var File = (from f in _dbContext.Propertyregistration
                        where f.Id == id
                        select f.HandedOverFilePath).First();

            return File;
        }

        public async Task<List<Landuse>> GetLandUseDropDownList()
        {
            List<Landuse> LanduseList = await _dbContext.Landuse.Where(x => x.IsActive == 1).ToListAsync();
            return LanduseList;
        }

        public async Task<List<Locality>> GetLocalityDropDownList(int zoneId)
        {
            List<Locality> LocalityList = await _dbContext.Locality.Where(x => x.ZoneId == zoneId && x.IsActive == 1)
                                                .ToListAsync();
            return LocalityList;
        }

        public async Task<List<Propertyregistration>> GetAllPropertyRegistrationReportList()
        {
            

            return await _dbContext.Propertyregistration.
                Include(x => x.Locality)
                                     .Include(x => x.ClassificationOfLand)
                               .Include(x => x.Department)
                               .Include(x => x.Zone)
                               .Include(x => x.Division)
                               .Include(x => x.Locality)
                               .Include(x => x.DisposalType)
                               .Include(x => x.MainLandUse)
                               .Include(x => x.HandedOverDepartment)
                               .Include(x => x.HandedOverZone)
                               .Include(x => x.HandedOverDivision)
                               .Include(x => x.TakenOverDepartment)
                               .Include(x => x.TakenOverZone)
                               .Include(x => x.TakenOverDivision)
                                        .Where(x => (x.IsDeleted == 1 && x.IsActive==1  && x.IsDisposed != 0 && x.IsValidate==1 )).ToListAsync();
        }






        public async Task<PagedResult<Propertyregistration>> GetPropertyRegisterationReportData(PropertyRegisterationReportSearchDto model)
        {
            try
            {
                if (model.plannedUnplannedLand == "Planned Land")
                {
                    var data = await _dbContext.Propertyregistration.Include(x => x.ClassificationOfLand)
                               .Include(x => x.Department)
                               .Include(x => x.Zone)
                               .Include(x => x.Division)
                               .Include(x => x.Locality)
                               .Include(x => x.DisposalType)
                               .Include(x => x.MainLandUse)
                               .Include(x => x.HandedOverDepartment)
                               .Include(x => x.HandedOverZone)
                               .Include(x => x.HandedOverDivision)
                               .Include(x => x.TakenOverDepartment)
                               .Include(x => x.TakenOverZone)
                               .Include(x => x.TakenOverDivision)
                                   .Where(x => (x.IsDeleted == 1 && x.IsActive == 1 && x.IsDisposed != 0 && x.IsValidate == 1)
                                   && (x.ClassificationOfLandId == (model.classificationofland == 0 ? x.ClassificationOfLandId : model.classificationofland))
                                   && (x.DepartmentId == (model.department == 0 ? x.DepartmentId : model.department)) && (x.ZoneId == (model.zone == 0 ? x.ZoneId : model.zone))
                                   && (x.DivisionId == (model.division == 0 ? x.DivisionId : model.division))
                                   && (x.LocalityId == (model.locality == 0 ? x.LocalityId : model.locality))
                                   && (x.PlannedUnplannedLand == (model.plannedUnplannedLand == "0" ? x.PlannedUnplannedLand : model.plannedUnplannedLand))
                                   && (x.MainLandUseId == (model.mainLandUse == 0 ? x.MainLandUseId : model.mainLandUse))
                                   && (x.LitigationStatus == (model.litigation == 2 ? x.LitigationStatus : model.litigation))
                                   && (x.InventoriedInId == (model.inventoriedIn == 0 ? x.InventoriedInId : model.inventoriedIn))
                                   && (x.EncroachmentStatusId == (model.encroached == 2 ? x.EncroachmentStatusId : model.encroached))
                                   //&& (x.KhasraNo == (model.khasraNo == "0" ? x.KhasraNo : model.khasraNo))
                                   // && (x.KhasraNo.Contains(model.khasraNo == "" ? x.KhasraNo : model.khasraNo))
                                   && (x.Colony != null ? x.Colony.Contains(model.colony == "" ? x.Colony : model.colony) : true)
                                   && (x.Sector != null ? x.Sector.Contains(model.sector == "" ? x.Sector : model.sector) : true)
                                   && (x.Block != null ? x.Block.Contains(model.block == "" ? x.Block : model.block) : true)
                                   && (x.Pocket != null ? x.Pocket.Contains(model.pocket == "" ? x.Pocket : model.pocket) : true)
                                   && (x.PlotNo != null ? x.PlotNo.Contains(model.plotNo == "" ? x.PlotNo : model.plotNo) : true)
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
                                data.Results = data.Results.OrderBy(x => x.InventoriedInId).ToList();
                                break;
                            case ("PLANNEDUNPLANNED"):
                                data.Results = data.Results.OrderBy(x => x.PlannedUnplannedLand).ToList();
                                break;
                            case ("CLASSIFICATION"):
                                data.Results = data.Results.OrderBy(x => (x.ClassificationOfLand != null ? x.ClassificationOfLand.Name : null)).ToList();
                                break;
                            case ("DEPARTMENT"):
                                data.Results = data.Results.OrderBy(x => (x.Department != null ? x.Department.Name : null)).ToList();
                                break;
                            case ("ZONE"):
                                data.Results = data.Results.OrderBy(x => (x.Zone != null ? x.Zone.Name : null)).ToList();
                                break;
                            case ("DIVISION"):
                                data.Results = data.Results.OrderBy(x => (x.Division != null ? x.Division.Name : null)).ToList();
                                break;
                        }
                    }
                    else if (SortOrder == 2)
                    {
                        switch (model.SortBy.ToUpper())
                        {
                            case ("INVENTORIEDIN"):
                                data.Results = data.Results.OrderByDescending(x => x.InventoriedInId).ToList();
                                break;
                            case ("PLANNEDUNPLANNED"):
                                data.Results = data.Results.OrderByDescending(x => x.PlannedUnplannedLand).ToList();
                                break;
                            case ("CLASSIFICATION"):
                                data.Results = data.Results.OrderByDescending(x => (x.ClassificationOfLand != null ? x.ClassificationOfLand.Name : null)).ToList();
                                break;
                            case ("DEPARTMENT"):
                                data.Results = data.Results.OrderByDescending(x => (x.Department != null ? x.Department.Name : null)).ToList();
                                break;
                            case ("ZONE"):
                                data.Results = data.Results.OrderByDescending(x => (x.Zone != null ? x.Zone.Name : null)).ToList();
                                break;
                            case ("DIVISION"):
                                data.Results = data.Results.OrderByDescending(x => (x.Division != null ? x.Division.Name : null)).ToList();
                                break;
                        }
                    }
                    return data;
                }
                else
                {
                    var data = await _dbContext.Propertyregistration.Include(x => x.ClassificationOfLand)
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
                                   && (x.LocalityId == (model.locality == 0 ? x.LocalityId : model.locality))
                                   && (x.PlannedUnplannedLand == (model.plannedUnplannedLand == "0" ? x.PlannedUnplannedLand : model.plannedUnplannedLand))
                                   && (x.MainLandUseId == (model.mainLandUse == 0 ? x.MainLandUseId : model.mainLandUse))
                                   && (x.LitigationStatus == (model.litigation == 2 ? x.LitigationStatus : model.litigation))
                                   && (x.InventoriedInId == (model.inventoriedIn == 0 ? x.InventoriedInId : model.inventoriedIn))
                                   && (x.EncroachmentStatusId == (model.encroached == 2 ? x.EncroachmentStatusId : model.encroached))
                                   //&& (x.KhasraNo == (model.khasraNo == "0" ? x.KhasraNo : model.khasraNo))
                                   && (x.KhasraNo.Contains(model.khasraNo == "" ? x.KhasraNo : model.khasraNo))
                                   //&& (x.Colony.Contains(model.colony == "" ? x.Colony : model.colony))
                                   //&& (x.Sector.Contains(model.sector == "" ? x.Sector : model.sector))
                                   //&& (x.Block.Contains(model.block == "" ? x.Block : model.block))
                                   //&& (x.Pocket.Contains(model.pocket == "" ? x.Pocket : model.pocket))
                                   //&& (x.PlotNo.Contains(model.plotNo == "" ? x.PlotNo : model.plotNo))
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
                                data.Results = data.Results.OrderBy(x => x.InventoriedInId).ToList();
                                break;
                            case ("PLANNEDUNPLANNED"):
                                data.Results = data.Results.OrderBy(x => x.PlannedUnplannedLand).ToList();
                                break;
                            case ("CLASSIFICATION"):
                                data.Results = data.Results.OrderBy(x => (x.ClassificationOfLand != null ? x.ClassificationOfLand.Name : null)).ToList();
                                break;
                            case ("DEPARTMENT"):
                                data.Results = data.Results.OrderBy(x => (x.Department != null ? x.Department.Name : null)).ToList();
                                break;
                            case ("ZONE"):
                                data.Results = data.Results.OrderBy(x => (x.Zone != null ? x.Zone.Name : null)).ToList();
                                break;
                            case ("DIVISION"):
                                data.Results = data.Results.OrderBy(x => (x.Division != null ? x.Division.Name : null)).ToList();
                                break;
                        }
                    }
                    else if (SortOrder == 2)
                    {
                        switch (model.SortBy.ToUpper())
                        {
                            case ("INVENTORIEDIN"):
                                data.Results = data.Results.OrderByDescending(x => x.InventoriedInId).ToList();
                                break;
                            case ("PLANNEDUNPLANNED"):
                                data.Results = data.Results.OrderByDescending(x => x.PlannedUnplannedLand).ToList();
                                break;
                            case ("CLASSIFICATION"):
                                data.Results = data.Results.OrderByDescending(x => (x.ClassificationOfLand != null ? x.ClassificationOfLand.Name : null)).ToList();
                                break;
                            case ("DEPARTMENT"):
                                data.Results = data.Results.OrderByDescending(x => (x.Department != null ? x.Department.Name : null)).ToList();
                                break;
                            case ("ZONE"):
                                data.Results = data.Results.OrderByDescending(x => (x.Zone != null ? x.Zone.Name : null)).ToList();
                                break;
                            case ("DIVISION"):
                                data.Results = data.Results.OrderByDescending(x => (x.Division != null ? x.Division.Name : null)).ToList();
                                break;
                        }
                    }
                    return data;
                }


            }
            catch (Exception ex)
            {
                throw;
            }
        }



        public async Task<List<Propertyregistration>> GetRestoreLandReportData(int department, int zone, int division, int locality)
        {
            var data = await _dbContext.Propertyregistration
                .Include(x => x.Locality)
                .Include(x => x.Department)
                .Include(x => x.Zone)
                .Include(x => x.Division)
                .Include(x => x.Deletedproperty)

                .Where(x => (x.IsDeleted == 0)
                && (x.DepartmentId == (department == 0 ? x.DepartmentId : department))
                && (x.ZoneId == (zone == 0 ? x.ZoneId : zone))
                && (x.DivisionId == (division == 0 ? x.DivisionId : division))
                && (x.LocalityId == (locality == 0 ? x.LocalityId : locality))
                )
                .OrderByDescending(x => x.Id)
                .ToListAsync();
            return data;
        }
        public async Task<List<Propertyregistration>> GetRestorePropertyReportData(int department, int zone, int division, int locality)
        {
            var data = await _dbContext.Propertyregistration.
                Include(x => x.Department)
                .Include(x => x.Zone)
                .Include(x => x.Division)
                .Include(x => x.Locality)
                .Include(x => x.Restoreproperty)
              .Where(x => (x.RestoreReason != null)
                //.Where(x => (x.IsDeleted == 1)
                && (x.DepartmentId == (department == 0 ? x.DepartmentId : department))
                && (x.ZoneId == (zone == 0 ? x.ZoneId : zone))
                && (x.DivisionId == (division == 0 ? x.DivisionId : division))
                && (x.LocalityId == (locality == 0 ? x.LocalityId : locality)))
                  .OrderByDescending(x => x.Id)
                .ToListAsync();
            return data;
        }

        public string GetTakenOverFile(int id)
        {
            var File = (from f in _dbContext.Propertyregistration
                        where f.Id == id
                        select f.TakenOverFilePath).First();

            return File;
        }

        public async Task<List<Zone>> GetZoneDropDownList(int DepartmentId)
        {

            List<Zone> ZoneList = await _dbContext.Zone.Where(x => x.DepartmentId == DepartmentId && x.IsActive == 1)
                                        .ToListAsync();
            return ZoneList;
        }

        public async Task<bool> InsertInDeletedProperty(Deletedproperty model)
        {
            var result = _dbContext.Deletedproperty.Add(model);
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<bool> InsertInRestoreProperty(Restoreproperty model)
        {
            var result = _dbContext.Restoreproperty.Add(model);
            return await _dbContext.SaveChangesAsync() > 0;
        }


        public async Task<List<Propertyregistration>> GetAllPropertInventorylist(int UserId)
        {
            var badCodes = new[] { 3, 5 };
            return await _dbContext.Propertyregistration
       .Include(x => x.ClassificationOfLand)
                            .Include(x => x.Department)
                            .Include(x => x.Division)
                            .Include(x => x.DisposalType)
                            .Include(x => x.MainLandUse)
                            .Include(x => x.Zone)
                            .Include(x => x.Locality)
                                    .Where(x => x.IsDeleted == 1 && !badCodes.Contains(x.ClassificationOfLand.Id) && x.IsValidate == 1 && x.IsDisposed != 0)
                 .ToListAsync();
        }


        public async Task<PagedResult<Propertyregistration>> GetPagedPropertyRegisteration(PropertyRegisterationSearchDto model, int UserId)
        {
            var badCodes = new[] { 3, 5 };
            var data = await _dbContext.Propertyregistration
                            .Include(x => x.ClassificationOfLand)
                            .Include(x => x.Department)
                            .Include(x => x.Division)
                            .Include(x => x.DisposalType)
                            .Include(x => x.MainLandUse)
                            .Include(x => x.Zone)
                            .Include(x => x.Locality)
                                .Where(x => x.IsDeleted == 1 && !badCodes.Contains(x.ClassificationOfLand.Id) && x.IsValidate == 1 && x.IsDisposed != 0
                                && (x.ClassificationOfLandId == (model.classificationOfLandId == 0 ? x.ClassificationOfLandId : model.classificationOfLandId))
                                && (x.DepartmentId == (model.departmentId == 0 ? x.DepartmentId : model.departmentId))
                                && (x.ZoneId == (model.zoneId == 0 ? x.ZoneId : model.zoneId))
                                && (x.DivisionId == (model.divisionId == 0 ? x.DivisionId : model.divisionId))
                                && (x.InventoriedInId == (model.inventoriedId == 0 ? x.InventoriedInId : model.inventoriedId))
                                && (x.PlannedUnplannedLand == (model.plannedUnplannedLand == "0" ? x.PlannedUnplannedLand : model.plannedUnplannedLand)))
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
                        data.Results = data.Results.OrderBy(x => x.InventoriedInId).ToList();
                        break;
                    case ("PLANNEDUNPLANNED"):
                        data.Results = data.Results.OrderBy(x => x.PlannedUnplannedLand).ToList();
                        break;
                    case ("CLASSIFICATION"):
                        data.Results = data.Results.OrderBy(x => (x.ClassificationOfLand != null ? x.ClassificationOfLand.Name : null)).ToList();
                        break;
                    case ("DEPARTMENT"):
                        data.Results = data.Results.OrderBy(x => (x.Department != null ? x.Department.Name : null)).ToList();
                        break;
                    case ("ZONE"):
                        data.Results = data.Results.OrderBy(x => (x.Zone != null ? x.Zone.Name : null)).ToList();
                        break;
                    case ("DIVISION"):
                        data.Results = data.Results.OrderBy(x => (x.Division != null ? x.Division.Name : null)).ToList();
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("INVENTORIEDIN"):
                        data.Results = data.Results.OrderByDescending(x => x.InventoriedInId).ToList();
                        break;
                    case ("PLANNEDUNPLANNED"):
                        data.Results = data.Results.OrderByDescending(x => x.PlannedUnplannedLand).ToList();
                        break;
                    case ("CLASSIFICATION"):
                        data.Results = data.Results.OrderByDescending(x => (x.ClassificationOfLand != null ? x.ClassificationOfLand.Name : null)).ToList();
                        break;
                    case ("DEPARTMENT"):
                        data.Results = data.Results.OrderByDescending(x => (x.Department != null ? x.Department.Name : null)).ToList();
                        break;
                    case ("ZONE"):
                        data.Results = data.Results.OrderByDescending(x => (x.Zone != null ? x.Zone.Name : null)).ToList();
                        break;
                    case ("DIVISION"):
                        data.Results = data.Results.OrderByDescending(x => (x.Division != null ? x.Division.Name : null)).ToList();
                        break;
                }
            }
            return data;

        }

        public async Task<List<Propertyregistration>> GetAllPropertyRegistrationMORlist(int UserId)
        {
            var badCodes = new[] { 3, 5 };
            var Iscreated = _dbContext.Propertyregistration.Where(x => x.CreatedBy == UserId).Count();
            if (UserId == 14 || Iscreated > 0)
            {

                var data1= await _dbContext.Propertyregistration
       .Include(x => x.ClassificationOfLand)
                                .Include(x => x.Department)
                                .Include(x => x.Division)
                                .Include(x => x.DisposalType)
                                .Include(x => x.MainLandUse)
                                .Include(x => x.Zone)
                                .Include(x => x.Locality)
                                    .Where(x => x.IsDeleted == 1 && x.IsActive == 1 && badCodes.Contains(x.ClassificationOfLand.Id))
                 .ToListAsync();
                  return data1;
            }
            else
            {

                var data1 = await _dbContext.Propertyregistration
       .Include(x => x.ClassificationOfLand)
                                .Include(x => x.Department)
                                .Include(x => x.Division)
                                .Include(x => x.DisposalType)
                                .Include(x => x.MainLandUse)
                                .Include(x => x.Zone)
                                .Include(x => x.Locality)
                                    .Where(x => x.IsDeleted == 1 && badCodes.Contains(x.ClassificationOfLand.Id) && x.IsValidate == 1 && x.IsDisposed != 0)
                 .ToListAsync();
                return data1;

            }
          
        }


        public async Task<PagedResult<Propertyregistration>> GetPagedPropertyRegisterationMOR(PropertyRegisterationSearchDto model, int UserId)
        {
            var badCodes = new[] { 3, 5 };
            var Iscreated = _dbContext.Propertyregistration.Where(x => x.CreatedBy == UserId).Count();
            if (UserId == 14 || Iscreated > 0)
            {
                var data = await _dbContext.Propertyregistration
                                .Include(x => x.ClassificationOfLand)
                                .Include(x => x.Department)
                                .Include(x => x.Division)
                                .Include(x => x.DisposalType)
                                .Include(x => x.MainLandUse)
                                .Include(x => x.Zone)
                                .Include(x => x.Locality)
                                    .Where(x => x.IsDeleted == 1 && x.IsActive == 1 && badCodes.Contains(x.ClassificationOfLand.Id)
                                    && (x.ClassificationOfLandId == (model.classificationOfLandId == 0 ? x.ClassificationOfLandId : model.classificationOfLandId))
                                    && (x.DepartmentId == (model.departmentId == 0 ? x.DepartmentId : model.departmentId))
                                    && (x.ZoneId == (model.zoneId == 0 ? x.ZoneId : model.zoneId))
                                    && (x.DivisionId == (model.divisionId == 0 ? x.DivisionId : model.divisionId))
                                    && (x.InventoriedInId == (model.inventoriedId == 0 ? x.InventoriedInId : model.inventoriedId))
                                    && (x.PlannedUnplannedLand == (model.plannedUnplannedLand == "0" ? x.PlannedUnplannedLand : model.plannedUnplannedLand)))
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
                            data.Results = data.Results.OrderBy(x => x.InventoriedInId).ToList();
                            break;
                        case ("PLANNEDUNPLANNED"):
                            data.Results = data.Results.OrderBy(x => x.PlannedUnplannedLand).ToList();
                            break;
                        case ("CLASSIFICATION"):
                            data.Results = data.Results.OrderBy(x => (x.ClassificationOfLand != null ? x.ClassificationOfLand.Name : null)).ToList();
                            break;
                        case ("DEPARTMENT"):
                            data.Results = data.Results.OrderBy(x => (x.Department != null ? x.Department.Name : null)).ToList();
                            break;
                        case ("ZONE"):
                            data.Results = data.Results.OrderBy(x => (x.Zone != null ? x.Zone.Name : null)).ToList();
                            break;
                        case ("DIVISION"):
                            data.Results = data.Results.OrderBy(x => (x.Division != null ? x.Division.Name : null)).ToList();
                            break;
                    }
                }
                else if (SortOrder == 2)
                {
                    switch (model.SortBy.ToUpper())
                    {
                        case ("INVENTORIEDIN"):
                            data.Results = data.Results.OrderByDescending(x => x.InventoriedInId).ToList();
                            break;
                        case ("PLANNEDUNPLANNED"):
                            data.Results = data.Results.OrderByDescending(x => x.PlannedUnplannedLand).ToList();
                            break;
                        case ("CLASSIFICATION"):
                            data.Results = data.Results.OrderByDescending(x => (x.ClassificationOfLand != null ? x.ClassificationOfLand.Name : null)).ToList();
                            break;
                        case ("DEPARTMENT"):
                            data.Results = data.Results.OrderByDescending(x => (x.Department != null ? x.Department.Name : null)).ToList();
                            break;
                        case ("ZONE"):
                            data.Results = data.Results.OrderByDescending(x => (x.Zone != null ? x.Zone.Name : null)).ToList();
                            break;
                        case ("DIVISION"):
                            data.Results = data.Results.OrderByDescending(x => (x.Division != null ? x.Division.Name : null)).ToList();
                            break;
                    }
                }
                return data;

            }
            else
            {
                var data = await _dbContext.Propertyregistration
                                .Include(x => x.ClassificationOfLand)
                                .Include(x => x.Department)
                                .Include(x => x.Division)
                                .Include(x => x.DisposalType)
                                .Include(x => x.MainLandUse)
                                .Include(x => x.Zone)
                                .Include(x => x.Locality)
                                    .Where(x => x.IsDeleted == 1 && badCodes.Contains(x.ClassificationOfLand.Id) && x.IsValidate == 1 && x.IsDisposed != 0
                                    && (x.ClassificationOfLandId == (model.classificationOfLandId == 0 ? x.ClassificationOfLandId : model.classificationOfLandId))
                                    && (x.DepartmentId == (model.departmentId == 0 ? x.DepartmentId : model.departmentId))
                                    && (x.ZoneId == (model.zoneId == 0 ? x.ZoneId : model.zoneId))
                                    && (x.DivisionId == (model.divisionId == 0 ? x.DivisionId : model.divisionId))
                                    && (x.InventoriedInId == (model.inventoriedId == 0 ? x.InventoriedInId : model.inventoriedId))
                                    && (x.PlannedUnplannedLand == (model.plannedUnplannedLand == "0" ? x.PlannedUnplannedLand : model.plannedUnplannedLand)))
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
                            data.Results = data.Results.OrderBy(x => x.InventoriedInId).ToList();
                            break;
                        case ("PLANNEDUNPLANNED"):
                            data.Results = data.Results.OrderBy(x => x.PlannedUnplannedLand).ToList();
                            break;
                        case ("CLASSIFICATION"):
                            data.Results = data.Results.OrderBy(x => (x.ClassificationOfLand != null ? x.ClassificationOfLand.Name : null)).ToList();
                            break;
                        case ("DEPARTMENT"):
                            data.Results = data.Results.OrderBy(x => (x.Department != null ? x.Department.Name : null)).ToList();
                            break;
                        case ("ZONE"):
                            data.Results = data.Results.OrderBy(x => (x.Zone != null ? x.Zone.Name : null)).ToList();
                            break;
                        case ("DIVISION"):
                            data.Results = data.Results.OrderBy(x => (x.Division != null ? x.Division.Name : null)).ToList();
                            break;
                    }
                }
                else if (SortOrder == 2)
                {
                    switch (model.SortBy.ToUpper())
                    {
                        case ("INVENTORIEDIN"):
                            data.Results = data.Results.OrderByDescending(x => x.InventoriedInId).ToList();
                            break;
                        case ("PLANNEDUNPLANNED"):
                            data.Results = data.Results.OrderByDescending(x => x.PlannedUnplannedLand).ToList();
                            break;
                        case ("CLASSIFICATION"):
                            data.Results = data.Results.OrderByDescending(x => (x.ClassificationOfLand != null ? x.ClassificationOfLand.Name : null)).ToList();
                            break;
                        case ("DEPARTMENT"):
                            data.Results = data.Results.OrderByDescending(x => (x.Department != null ? x.Department.Name : null)).ToList();
                            break;
                        case ("ZONE"):
                            data.Results = data.Results.OrderByDescending(x => (x.Zone != null ? x.Zone.Name : null)).ToList();
                            break;
                        case ("DIVISION"):
                            data.Results = data.Results.OrderByDescending(x => (x.Division != null ? x.Division.Name : null)).ToList();
                            break;
                    }
                }
                return data;

            }
        }
        public async Task<List<Department>> GetTakenDepartmentDropDownList()
        {
            return await _dbContext.Department.Where(x => x.IsActive == 1).ToListAsync();
        }

        public async Task<List<Department>> GetHandedDepartmentDropDownList()
        {
            return await _dbContext.Department.Where(x => x.IsActive == 1).ToListAsync();
        }

        public async Task<List<Propertyregistration>> GetAllRestoreLandReportData()
        {
            var NonDeletedId = (from x in _dbContext.Propertyregistration
                                where x.IsActive == 1 && x.IsDeleted != 0 || x.IsDisposed != 0
                                select x.Id).ToArray();
            return await _dbContext.Propertyregistration.Include(x => x.Locality)
                                        .Include(x => x.Department)
                                        .Include(x => x.Zone)
                                        .Include(x => x.Division)
                                         .Include(x => x.Deletedproperty)
                                        .Include(x => x.ClassificationOfLand)
                                        .Where(x => (x.IsDeleted == 0 || x.IsDisposed == 0) && !(NonDeletedId).Contains(x.Id)).ToListAsync();
        }



        public async Task<PagedResult<Propertyregistration>> GetRestoreLandReportData(PropertyRegisterationSearchDto model)
        {
            var NonDeletedId = (from x in _dbContext.Propertyregistration
                                where x.IsActive == 1 && x.IsDeleted !=0 || x.IsDisposed !=0
                                  select x.Id).ToArray();

            var data = await _dbContext.Propertyregistration
                                        .Include(x => x.Locality)
                                        .Include(x => x.Department)
                                        .Include(x => x.Zone)
                                        .Include(x => x.Division)
                                        .Include(x => x.Deletedproperty)
                                        .Include(x => x.ClassificationOfLand)
                                        .Where(x => (x.IsDeleted == 0 || x.IsDisposed == 0)
                                         && (x.InventoriedInId == (model.inventoriedId == 0 ? x.InventoriedInId : model.inventoriedId))
                                         && (x.PlannedUnplannedLand == (model.plannedUnplannedLand == "0" ? x.PlannedUnplannedLand : model.plannedUnplannedLand))
                                        && (x.ClassificationOfLandId == (model.classificationOfLandId == 0 ? x.ClassificationOfLandId : model.classificationOfLandId))
                                        && (x.DepartmentId == (model.departmentId == 0 ? x.DepartmentId : model.departmentId))
                                        && (x.ZoneId == (model.zoneId == 0 ? x.ZoneId : model.zoneId))
                                        && (x.DivisionId == (model.divisionId == 0 ? x.DivisionId : model.divisionId))
                                        && (x.LocalityId == (model.Id == 0 ? x.LocalityId : model.Id))
                                        && !(NonDeletedId).Contains(x.Id)
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
                        data.Results = data.Results.OrderBy(x => x.InventoriedInId).ToList();
                        break;
                    case ("PLANNEDUNPLANNED"):
                        data.Results = data.Results.OrderBy(x => x.PlannedUnplannedLand).ToList();
                        break;
                    case ("CLASSIFICATION"):
                        data.Results = data.Results.OrderBy(x => (x.ClassificationOfLand != null ? x.ClassificationOfLand.Name : null)).ToList();
                        break;
                    case ("DEPARTMENT"):
                        data.Results = data.Results.OrderBy(x => (x.Department != null ? x.Department.Name : null)).ToList();
                        break;
                    case ("ZONE"):
                        data.Results = data.Results.OrderBy(x => (x.Zone != null ? x.Zone.Name : null)).ToList();
                        break;
                    case ("DIVISION"):
                        data.Results = data.Results.OrderBy(x => (x.Division != null ? x.Division.Name : null)).ToList();
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("INVENTORIEDIN"):
                        data.Results = data.Results.OrderByDescending(x => x.InventoriedInId).ToList();
                        break;
                    case ("PLANNEDUNPLANNED"):
                        data.Results = data.Results.OrderByDescending(x => x.PlannedUnplannedLand).ToList();
                        break;
                    case ("CLASSIFICATION"):
                        data.Results = data.Results.OrderByDescending(x => (x.ClassificationOfLand != null ? x.ClassificationOfLand.Name : null)).ToList();
                        break;
                    case ("DEPARTMENT"):
                        data.Results = data.Results.OrderByDescending(x => (x.Department != null ? x.Department.Name : null)).ToList();
                        break;
                    case ("ZONE"):
                        data.Results = data.Results.OrderByDescending(x => (x.Zone != null ? x.Zone.Name : null)).ToList();
                        break;
                    case ("DIVISION"):
                        data.Results = data.Results.OrderByDescending(x => (x.Division != null ? x.Division.Name : null)).ToList();
                        break;
                }
            }
            return data;
        }


        public async Task<List<Propertyregistration>> GetAllRestorePropertyReportList()
        {
            return await _dbContext.Propertyregistration.
                                        Include(x => x.Department)
                                        .Include(x => x.Zone)
                                        .Include(x => x.Division)
                                        .Include(x => x.Locality)
                                        .Include(x => x.Restoreproperty)
                                        .Include(x => x.ClassificationOfLand)
                                        .Where(x => (x.Restoreproperty.RestoreReason != null)).ToListAsync();
        }

        public async Task<PagedResult<Propertyregistration>> GetRestorePropertyReportData(PropertyRegisterationSearchDto model)
        {
            var data = await _dbContext.Propertyregistration.
                Include(x => x.Department)
                .Include(x => x.Zone)
                .Include(x => x.Division)
                .Include(x => x.Locality)
                .Include(x => x.Restoreproperty)
                .Include(x => x.ClassificationOfLand)
                .Where(x => (x.Restoreproperty.RestoreReason != null)
                //.Where(x => (x.IsDeleted == 1)
                 && (x.InventoriedInId == (model.inventoriedId == 0 ? x.InventoriedInId : model.inventoriedId))
                 && (x.PlannedUnplannedLand == (model.plannedUnplannedLand == "0" ? x.PlannedUnplannedLand : model.plannedUnplannedLand))
                && (x.ClassificationOfLandId == (model.classificationOfLandId == 0 ? x.ClassificationOfLandId : model.classificationOfLandId))

                && (x.DepartmentId == (model.departmentId == 0 ? x.DepartmentId : model.departmentId))
                && (x.ZoneId == (model.zoneId == 0 ? x.ZoneId : model.zoneId))
                && (x.DivisionId == (model.divisionId == 0 ? x.DivisionId : model.divisionId))
                && (x.LocalityId == (model.Id == 0 ? x.LocalityId : model.Id)))
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
                        data.Results = data.Results.OrderBy(x => x.InventoriedInId).ToList();
                        break;
                    case ("PLANNEDUNPLANNED"):
                        data.Results = data.Results.OrderBy(x => x.PlannedUnplannedLand).ToList();
                        break;
                    case ("CLASSIFICATION"):
                        data.Results = data.Results.OrderBy(x => (x.ClassificationOfLand != null ? x.ClassificationOfLand.Name : null)).ToList();
                        break;
                    case ("DEPARTMENT"):
                        data.Results = data.Results.OrderBy(x => (x.Department != null ? x.Department.Name : null)).ToList();
                        break;
                    case ("ZONE"):
                        data.Results = data.Results.OrderBy(x => (x.Zone != null ? x.Zone.Name : null)).ToList();
                        break;
                    case ("DIVISION"):
                        data.Results = data.Results.OrderBy(x => (x.Division != null ? x.Division.Name : null)).ToList();
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("INVENTORIEDIN"):
                        data.Results = data.Results.OrderByDescending(x => x.InventoriedInId).ToList();
                        break;
                    case ("PLANNEDUNPLANNED"):
                        data.Results = data.Results.OrderByDescending(x => x.PlannedUnplannedLand).ToList();
                        break;
                    case ("CLASSIFICATION"):
                        data.Results = data.Results.OrderByDescending(x => (x.ClassificationOfLand != null ? x.ClassificationOfLand.Name : null)).ToList();
                        break;
                    case ("DEPARTMENT"):
                        data.Results = data.Results.OrderByDescending(x => (x.Department != null ? x.Department.Name : null)).ToList();
                        break;
                    case ("ZONE"):
                        data.Results = data.Results.OrderByDescending(x => (x.Zone != null ? x.Zone.Name : null)).ToList();
                        break;
                    case ("DIVISION"):
                        data.Results = data.Results.OrderByDescending(x => (x.Division != null ? x.Division.Name : null)).ToList();
                        break;
                }
            }
            return data;
        }

        public async Task<List<Classificationofland>> GetClassificationOfLandDropDownListReport()
        {
            List<Classificationofland> ClassificationoflandList = await _dbContext.Classificationofland
                                                                        .Where(x => x.IsActive == 1)
                                                                        .ToListAsync();
            return ClassificationoflandList;
        }

        public string GetEncroachAtr(int id)
        {
            var File = (from f in _dbContext.Propertyregistration
                        where f.Id == id
                        select f.EncroachAtrfilepath).First();

            return File;
        }

        public string GetHandedOverCopyofOrderFile(int id)
        {
            var File = (from f in _dbContext.Propertyregistration
                        where f.Id == id
                        select f.HandedOverCopyofOrderFilepath).First();

            return File;
        }

        public async Task<bool> InsertInDisposedProperty(Disposedproperty model)
        {
            var result = _dbContext.Disposedproperty.Add(model);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<Propertyregistration>> GetKhasraReportList()
        {
            return await _dbContext.Propertyregistration.Where(x => x.IsActive == 1 && x.KhasraNo != null).Distinct().ToListAsync();
        }

        public async Task<List<Propertyregistration>> GetUnverifiedList(int UserId)
        {
           
            return await _dbContext.Propertyregistration
       .Include(x => x.ClassificationOfLand)
                            .Include(x => x.Department)
                            .Include(x => x.Division)
                            .Include(x => x.DisposalType)
                            .Include(x => x.MainLandUse)
                            .Include(x => x.Zone)
                            .Include(x => x.Locality)
                                    .Where(x => x.IsDeleted == 1 && x.IsActive==1  && x.IsDisposed != 0 && x.IsValidate == 0)
                 .ToListAsync();
        }



        public async Task<PagedResult<Propertyregistration>> GetInventoryUnverifiedVerified(InvnentoryUnverifiedVerifiedSearchDto model, int userId,int? RoleId)
        {
            int UserId = userId;
            var Iscreated = _dbContext.Propertyregistration.Where(x => x.CreatedBy == UserId).Count();
            //if (UserId == 14 || Iscreated > 0)
            // Role 9 For DD LMC Changes on 18Nov2021 By Sachin
            if (RoleId == 9 || Iscreated > 0)
            {
                var data = await _dbContext.Propertyregistration
                                 .Include(x => x.Locality)
                                 .Include(x => x.Department)
                                 .Include(x => x.Zone)
                                 .Include(x => x.Division)
                                 .Include(x => x.ClassificationOfLand)
                                 .Where ( x => (x.IsDeleted == 1 && x.IsActive == 1 && x.IsDisposed != 0 && x.IsValidate == 0)
                                 && (x.InventoriedInId == (model.inventoriedId == 0 ? x.InventoriedInId : model.inventoriedId))
                                 && (x.PlannedUnplannedLand == (model.plannedUnplannedLand == "0" ? x.PlannedUnplannedLand : model.plannedUnplannedLand))
                                 && (x.ClassificationOfLandId == (model.classificationOfLandId == 0 ? x.ClassificationOfLandId : model.classificationOfLandId))
                                 && (x.DepartmentId == (model.departmentId == 0 ? x.DepartmentId : model.departmentId))
                                 && (x.ZoneId == (model.zoneId == 0 ? x.ZoneId : model.zoneId))
                                 && (x.DivisionId == (model.divisionId == 0 ? x.DivisionId : model.divisionId))
                                 && (x.LocalityId == (model.Id == 0 ? x.LocalityId : model.Id)))
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
                            data.Results = data.Results.OrderBy(x => x.InventoriedInId).ToList();
                            break;
                        case ("PLANNEDUNPLANNED"):
                            data.Results = data.Results.OrderBy(x => x.PlannedUnplannedLand).ToList();
                            break;
                        case ("CLASSIFICATION"):
                            data.Results = data.Results.OrderBy(x => (x.ClassificationOfLand != null ? x.ClassificationOfLand.Name : null)).ToList();
                            break;
                        case ("DEPARTMENT"):
                            data.Results = data.Results.OrderBy(x => (x.Department != null ? x.Department.Name : null)).ToList();
                            break;
                        case ("ZONE"):
                            data.Results = data.Results.OrderBy(x => (x.Zone != null ? x.Zone.Name : null)).ToList();
                            break;
                        case ("DIVISION"):
                            data.Results = data.Results.OrderBy(x => (x.Division != null ? x.Division.Name : null)).ToList();
                            break;
                    }
                }
                else if (SortOrder == 2)
                {
                    switch (model.SortBy.ToUpper())
                    {
                        case ("INVENTORIEDIN"):
                            data.Results = data.Results.OrderByDescending(x => x.InventoriedInId).ToList();
                            break;
                        case ("PLANNEDUNPLANNED"):
                            data.Results = data.Results.OrderByDescending(x => x.PlannedUnplannedLand).ToList();
                            break;
                        case ("CLASSIFICATION"):
                            data.Results = data.Results.OrderByDescending(x => (x.ClassificationOfLand != null ? x.ClassificationOfLand.Name : null)).ToList();
                            break;
                        case ("DEPARTMENT"):
                            data.Results = data.Results.OrderByDescending(x => (x.Department != null ? x.Department.Name : null)).ToList();
                            break;
                        case ("ZONE"):
                            data.Results = data.Results.OrderByDescending(x => (x.Zone != null ? x.Zone.Name : null)).ToList();
                            break;
                        case ("DIVISION"):
                            data.Results = data.Results.OrderByDescending(x => (x.Division != null ? x.Division.Name : null)).ToList();
                            break;
                    }
                }
                return data;
            }
            else
            {
                var data = await _dbContext.Propertyregistration
                                            .Include(x => x.Locality)
                                            .Include(x => x.Department)
                                            .Include(x => x.Zone)
                                            .Include(x => x.Division)
                                            .Include(x => x.ClassificationOfLand)
                                            .Where(x => (x.IsDeleted == null))
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
                            data.Results = data.Results.OrderBy(x => x.InventoriedInId).ToList();
                            break;
                        case ("PLANNEDUNPLANNED"):
                            data.Results = data.Results.OrderBy(x => x.PlannedUnplannedLand).ToList();
                            break;
                        case ("CLASSIFICATION"):
                            data.Results = data.Results.OrderBy(x => (x.ClassificationOfLand != null ? x.ClassificationOfLand.Name : null)).ToList();
                            break;
                        case ("DEPARTMENT"):
                            data.Results = data.Results.OrderBy(x => (x.Department != null ? x.Department.Name : null)).ToList();
                            break;
                        case ("ZONE"):
                            data.Results = data.Results.OrderBy(x => (x.Zone != null ? x.Zone.Name : null)).ToList();
                            break;
                        case ("DIVISION"):
                            data.Results = data.Results.OrderBy(x => (x.Division != null ? x.Division.Name : null)).ToList();
                            break;
                    }
                }
                else if (SortOrder == 2)
                {
                    switch (model.SortBy.ToUpper())
                    {
                        case ("INVENTORIEDIN"):
                            data.Results = data.Results.OrderByDescending(x => x.InventoriedInId).ToList();
                            break;
                        case ("PLANNEDUNPLANNED"):
                            data.Results = data.Results.OrderByDescending(x => x.PlannedUnplannedLand).ToList();
                            break;
                        case ("CLASSIFICATION"):
                            data.Results = data.Results.OrderByDescending(x => (x.ClassificationOfLand != null ? x.ClassificationOfLand.Name : null)).ToList();
                            break;
                        case ("DEPARTMENT"):
                            data.Results = data.Results.OrderByDescending(x => (x.Department != null ? x.Department.Name : null)).ToList();
                            break;
                        case ("ZONE"):
                            data.Results = data.Results.OrderByDescending(x => (x.Zone != null ? x.Zone.Name : null)).ToList();
                            break;
                        case ("DIVISION"):
                            data.Results = data.Results.OrderByDescending(x => (x.Division != null ? x.Division.Name : null)).ToList();
                            break;
                    }
                }
                return data;
            }

        }

        public async Task<PagedResult<Propertyregistration>> GetDeletedLandReportData(PropertyRegisterationSearchDto model)
        {
            var data = await _dbContext.Propertyregistration
                                        .Include(x => x.Locality)
                                        .Include(x => x.Department)
                                        .Include(x => x.Zone)
                                        .Include(x => x.Division)
                                        .Include(x => x.Deletedproperty)
                                        .Include(x => x.ClassificationOfLand)
                                        .Where(x => (x.IsDeleted == 0 || x.IsDisposed == 0)
                                         && (x.InventoriedInId == (model.inventoriedId == 0 ? x.InventoriedInId : model.inventoriedId))
                                         && (x.PlannedUnplannedLand == (model.plannedUnplannedLand == "0" ? x.PlannedUnplannedLand : model.plannedUnplannedLand))
                                        && (x.ClassificationOfLandId == (model.classificationOfLandId == 0 ? x.ClassificationOfLandId : model.classificationOfLandId))
                                        && (x.DepartmentId == (model.departmentId == 0 ? x.DepartmentId : model.departmentId))
                                        && (x.ZoneId == (model.zoneId == 0 ? x.ZoneId : model.zoneId))
                                        && (x.DivisionId == (model.divisionId == 0 ? x.DivisionId : model.divisionId))
                                        && (x.LocalityId == (model.Id == 0 ? x.LocalityId : model.Id))
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
                        data.Results = data.Results.OrderBy(x => x.InventoriedInId).ToList();
                        break;
                    case ("PLANNEDUNPLANNED"):
                        data.Results = data.Results.OrderBy(x => x.PlannedUnplannedLand).ToList();
                        break;
                    case ("CLASSIFICATION"):
                        data.Results = data.Results.OrderBy(x => (x.ClassificationOfLand != null ? x.ClassificationOfLand.Name : null)).ToList();
                        break;
                    case ("DEPARTMENT"):
                        data.Results = data.Results.OrderBy(x => (x.Department != null ? x.Department.Name : null)).ToList();
                        break;
                    case ("ZONE"):
                        data.Results = data.Results.OrderBy(x => (x.Zone != null ? x.Zone.Name : null)).ToList();
                        break;
                    case ("DIVISION"):
                        data.Results = data.Results.OrderBy(x => (x.Division != null ? x.Division.Name : null)).ToList();
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("INVENTORIEDIN"):
                        data.Results = data.Results.OrderByDescending(x => x.InventoriedInId).ToList();
                        break;
                    case ("PLANNEDUNPLANNED"):
                        data.Results = data.Results.OrderByDescending(x => x.PlannedUnplannedLand).ToList();
                        break;
                    case ("CLASSIFICATION"):
                        data.Results = data.Results.OrderByDescending(x => (x.ClassificationOfLand != null ? x.ClassificationOfLand.Name : null)).ToList();
                        break;
                    case ("DEPARTMENT"):
                        data.Results = data.Results.OrderByDescending(x => (x.Department != null ? x.Department.Name : null)).ToList();
                        break;
                    case ("ZONE"):
                        data.Results = data.Results.OrderByDescending(x => (x.Zone != null ? x.Zone.Name : null)).ToList();
                        break;
                    case ("DIVISION"):
                        data.Results = data.Results.OrderByDescending(x => (x.Division != null ? x.Division.Name : null)).ToList();
                        break;
                }
            }
            return data;
        }

        public async Task<List<Propertyregistration>> GetAllDeletedPropertyList()
        {
            return await _dbContext.Propertyregistration.Include(x => x.Locality)
                                        .Include(x => x.Department)
                                        .Include(x => x.Zone)
                                        .Include(x => x.Division)
                                        .Include(x => x.Deletedproperty)
                                        .Include(x => x.ClassificationOfLand)
                                        .Where(x => (x.IsDeleted == 0 || x.IsDisposed == 0)).ToListAsync();
        }

        public async Task<List<Propertyregistration>> GetPrimaryListForAPI(int deptid, int zoneid, int divisionid)// for api added by renu
        {
            return await _dbContext.Propertyregistration
                                  .Where(x => x.ZoneId == zoneid 
                                  && x.DepartmentId == deptid
                                  && x.DivisionId == divisionid
                                  && x.IsActive == 1
                                  )
                                  .ToListAsync();
        }
    }


}
