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

        public async Task<List<Propertyregistration>> GetAllPropertyregistration()
        {
            return await _dbContext.Propertyregistration.Include(x => x.ClassificationOfLand).Include(x => x.DisposalType).Include(x => x.LandUse).Include(x => x.ZoneDivision).Include(x => x.Locality ).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<List<Classificationofland>> GetClassificationOfLandDropDownList()
        {
            List<Classificationofland> ClassificationoflandList = await _dbContext.Classificationofland.Where(x => x.IsActive == 1).ToListAsync();
            return ClassificationoflandList;
        }

        public async Task<List<Disposaltype>> GetDisposalTypeDropDownList()
        {
            List<Disposaltype> DisposaltypeList = await _dbContext.Disposaltype.Where(x => x.IsActive == 1).ToListAsync();
            return DisposaltypeList;
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

        public async Task<List<Zone>> GetZoneDropDownList()
        {
            List<Zone> ZoneList = await _dbContext.Zone.Where(x => x.IsActive == 1).ToListAsync();
            return ZoneList;
        }
    }


}
