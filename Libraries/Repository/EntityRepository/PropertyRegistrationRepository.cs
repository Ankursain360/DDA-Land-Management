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

        public async Task<bool> CheckDeleteAuthority(int id)
        {
            var result = true;
            return result;
        }

        public async Task<List<Propertyregistration>> GetAllPropertyregistration(int UserId)
        {
            var Iscreated = _dbContext.Propertyregistration.Where(x => x.CreatedBy == UserId).Count();
            if (UserId == 2 || Iscreated > 0)
            {
                var data = await _dbContext.Propertyregistration.Include(x => x.ClassificationOfLand).Include(x => x.Department).Include(x => x.Division).Include(x => x.DisposalType).Include(x => x.MainLandUse).Include(x => x.Zone).Include(x => x.Locality).OrderByDescending(x => x.Id).Where(x => x.IsDeleted == 1).ToListAsync();
                return data;

            }
            else
            {
                var data = await _dbContext.Propertyregistration.Include(x => x.ClassificationOfLand).Include(x => x.Department).Include(x => x.Division).Include(x => x.DisposalType).Include(x => x.MainLandUse).Include(x => x.Zone).Include(x => x.Locality).OrderByDescending(x => x.Id).Where(x => x.IsDeleted == 1 && x.IsValidate == 1).ToListAsync();
                return data;

            }

        }

        public async Task<List<Classificationofland>> GetClassificationOfLandDropDownList()
        {
            var badCodes = new[] { 3,5 };
            List<Classificationofland> ClassificationoflandList = await _dbContext.Classificationofland.Where(x => x.IsActive == 1 && !badCodes.Contains(x.Id)).ToListAsync();
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
            List<Division> DivisionList = await _dbContext.Division.Where(x =>x.ZoneId == zoneId && x.IsActive == 1).ToListAsync();
            return DivisionList;
        }
      
        public async Task<List<Propertyregistration>> GetPrimaryListNoList(int divisionId)
        {
            List<Propertyregistration> PrimaryListNoList = await _dbContext.Propertyregistration.Where(x => x.DivisionId == divisionId && x.IsActive == 1).ToListAsync();
            return PrimaryListNoList;
        }
        public async Task<List<Locality>> GetLocalityDropDownList2(int divisionId)
        {
            List<Locality> localityList = await _dbContext.Locality.Where(x => x.DivisionId == divisionId ).Where(x=> x.IsActive==1).ToListAsync();
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
            List<Locality> LocalityList = await _dbContext.Locality.Where(x => x.ZoneId == zoneId && x.IsActive == 1).ToListAsync();
            return LocalityList;
        }

        public async Task<PagedResult<Propertyregistration>> GetPropertyRegisterationReportData(PropertyRegisterationReportSearchDto model)
        {
            return await _dbContext.Propertyregistration.Include(x => x.ClassificationOfLand)
                            .Include(x => x.Department)
                            .Include(x => x.Zone)
                            .Include(x => x.Division)
                            .Include(x => x.Locality)
                            .Include(x => x.DisposalType)
                            .Include(x => x.MainLandUse)
                                .Where(x => (x.IsDeleted == 1) && 
                                (x.ClassificationOfLandId == (model.classificationofland== 0 ? x.ClassificationOfLandId : model.classificationofland))
                                && (x.DepartmentId==(model.department == 0 ? x.DepartmentId : model.department)) && (x.ZoneId== (model.zone == 0 ? x.ZoneId : model.zone))
                                && (x.DivisionId == (model.division == 0 ? x.DivisionId : model.division))
                                && ( x.LocalityId == (model.locality == 0 ? x.LocalityId : model.locality))
                                && (x.PlannedUnplannedLand == (model.plannedUnplannedLand == "0" ? x.PlannedUnplannedLand : model.plannedUnplannedLand))
                                && (x.MainLandUseId == (model.mainLandUse == 0 ? x.MainLandUseId : model.mainLandUse)) 
                                && (x.LitigationStatus == (model.litigation == 2 ? x.LitigationStatus : model.litigation))
                                && (x.EncroachmentStatusId==(model.encroached == 2 ? x.EncroachmentStatusId : model.encroached)))
                                .OrderByDescending(x => x.Id)
                            .GetPaged<Propertyregistration>(model.PageNumber, model.PageSize); 

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
           
            List<Zone> ZoneList = await _dbContext.Zone.Where(x =>x.DepartmentId == DepartmentId && x.IsActive == 1).ToListAsync();
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

        public async Task<PagedResult<Propertyregistration>> GetPagedPropertyRegisteration(PropertyRegisterationSearchDto model, int UserId)
        {
            var Iscreated = _dbContext.Propertyregistration.Where(x => x.CreatedBy == UserId).Count();
            if (UserId == 2 || Iscreated > 0)
            {
                var data = await _dbContext.Propertyregistration
                                .Include(x => x.ClassificationOfLand)
                                .Include(x => x.Department)
                                .Include(x => x.Division)
                                .Include(x => x.DisposalType)
                                .Include(x => x.MainLandUse)
                                .Include(x => x.Zone)
                                .Include(x => x.Locality)
                                    .Where(x => x.IsDeleted == 1)
                                    .OrderByDescending(x => x.Id)
                                .GetPaged<Propertyregistration>(model.PageNumber, model.PageSize); 
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
                                    .OrderByDescending(x => x.Id)
                                .GetPaged<Propertyregistration>(model.PageNumber, model.PageSize); 
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

        public async Task<PagedResult<Propertyregistration>> GetRestoreLandReportData(PropertyRegisterationSearchDto model)
        {
            var data = await _dbContext.Propertyregistration
                .Include(x => x.Locality)
                .Include(x => x.Department)
                .Include(x => x.Zone)
                .Include(x => x.Division)
                .Include(x => x.Deletedproperty)

                .Where(x => (x.IsDeleted == 0)
                && (x.DepartmentId == (model.departmentId == 0 ? x.DepartmentId : model.departmentId))
                && (x.ZoneId == (model.zoneId == 0 ? x.ZoneId : model.zoneId))
                && (x.DivisionId == (model.divisionId == 0 ? x.DivisionId : model.divisionId))
                && (x.LocalityId == (model.Id == 0 ? x.LocalityId : model.Id)))
                .OrderByDescending(x => x.Id)
                .GetPaged(model.PageNumber, model.PageSize);
            return data;
        }
        public async Task<PagedResult<Propertyregistration>> GetRestorePropertyReportData(PropertyRegisterationSearchDto model)
        {
            var data = await _dbContext.Propertyregistration.
                Include(x => x.Department)
                .Include(x => x.Zone)
                .Include(x => x.Division)
                .Include(x => x.Locality)
                .Include(x => x.Restoreproperty)

                .Where(x => (x.IsDeleted == 1)
                && (x.DepartmentId == (model.departmentId == 0 ? x.DepartmentId : model.departmentId))
                && (x.ZoneId == (model.zoneId == 0 ? x.ZoneId : model.zoneId))
                && (x.DivisionId == (model.divisionId == 0 ? x.DivisionId : model.divisionId))
                && (x.LocalityId == (model.Id == 0 ? x.LocalityId : model.Id)))
                  .OrderByDescending(x => x.Id)
                .GetPaged(model.PageNumber, model.PageSize);
            return data;
        }
    }
}
