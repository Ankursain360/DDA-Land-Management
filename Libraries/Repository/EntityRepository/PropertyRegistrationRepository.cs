using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                var data = await _dbContext.Propertyregistration.Include(x => x.ClassificationOfLand).Include(x => x.Department).Include(x => x.Division).Include(x => x.DisposalType).Include(x => x.MainLandUse).Include(x => x.Zone).Include(x => x.Locality).OrderByDescending(x => x.Id).Where(x => x.IsDelated == 1).ToListAsync();
                return data;

            }
            else
            {
                var data = await _dbContext.Propertyregistration.Include(x => x.ClassificationOfLand).Include(x => x.Department).Include(x => x.Division).Include(x => x.DisposalType).Include(x => x.MainLandUse).Include(x => x.Zone).Include(x => x.Locality).OrderByDescending(x => x.Id).Where(x => x.IsDelated == 1 && x.IsValidate == 1).ToListAsync();
                return data;

            }

        }

        public async Task<List<Classificationofland>> GetClassificationOfLandDropDownList()
        {
            List<Classificationofland> ClassificationoflandList = await _dbContext.Classificationofland.Where(x => x.IsActive == 1).ToListAsync();
            return ClassificationoflandList;
        }

        public async Task<List<Department>> GetDepartmentDropDownList()
        {
            List<Department> DepartmentList = await _dbContext.Department.Where(x => x.IsActive == 1).ToListAsync();
            return DepartmentList;
        }

        public async Task<List<Disposaltype>> GetDisposalTypeDropDownList()
        {
            List<Disposaltype> DisposaltypeList = await _dbContext.Disposaltype.Where(x => x.IsActive == 1).ToListAsync();
            return DisposaltypeList;
        }

        public async Task<List<Division>> GetDivisionDropDownList()
        {
            List<Division> DivisionList = await _dbContext.Division.Where(x => x.IsActive == 1).ToListAsync();
            return DivisionList;
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

        public async Task<List<Landuse>> GetLandUseDropDownList()
        {
            List<Landuse> LanduseList = await _dbContext.Landuse.Where(x => x.IsActive == 1).ToListAsync();
            return LanduseList;
        }

        public async Task<List<Locality>> GetLocalityDropDownList()
        {
            List<Locality> LocalityList = await _dbContext.Locality.Where(x => x.IsActive == 1).ToListAsync();
            return LocalityList;
        }

        public async Task<List<Propertyregistration>> GetPropertyRegisterationReportData(int department, int landUse, int litigation, int encroached)
        {
          //  var Iscreated = _dbContext.Propertyregistration.Where(x => x.CreatedBy == UserId).Count();
            var data = await _dbContext.Propertyregistration.Include(x => x.ClassificationOfLand).Include(x => x.Department).Include(x => x.Division).Include(x => x.DisposalType).Include(x => x.MainLandUse).Include(x => x.Zone).Include(x => x.Locality).OrderByDescending(x => x.Id).Where(x => x.IsDelated == 1 && x.DepartmentId== department && x.MainLandUseId == landUse && x.LitigationStatus == litigation).ToListAsync();
            return data;

        }

        public async Task<List<Zone>> GetZoneDropDownList()
        {
            List<Zone> ZoneList = await _dbContext.Zone.Where(x => x.IsActive == 1).ToListAsync();
            return ZoneList;
        }
    }


}
