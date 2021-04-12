using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
namespace Libraries.Repository.EntityRepository
{
    public class MorlandRepository : GenericRepository<Morland>, IMorlandRepository
    {
        public MorlandRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<List<LandNotification>> GetAllLandNotification()
        {
            List<LandNotification> notificationList = await _dbContext.LandNotification.Where(x => x.IsActive == 1)
                .ToListAsync();
            return notificationList;
        }

      


        public async Task<bool> Any(int id, string Name)
        {
            return await _dbContext.Morland.AnyAsync(t => t.Id != id && t.Name.ToLower() == Name.ToLower());
        }

        public async Task<List<Morland>> GetAllMorland()
        {
            return await _dbContext.Morland.Include(x => x.LandNotification)
               
                .OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<PagedResult<Morland>> GetPagedMorland(MorLandsSearchDto model)
        {
           
            var data = await _dbContext.Morland
                                        .Include(x => x.LandNotification)

                                           .Where(x => (string.IsNullOrEmpty(model.name) || x.LandNotification.Name.Contains(model.name))
                               && (string.IsNullOrEmpty(model.propertyname) || x.PropertySiteNo.Contains(model.propertyname))
                               && (string.IsNullOrEmpty(model.sitedesc) || x.SiteDescription.Contains(model.sitedesc)))
                                .OrderBy(x => x.LandNotification.Name)
                                .GetPaged<Morland>(model.PageNumber, model.PageSize);
            
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("LOCALITY"):
                        data = null;
                        data = await _dbContext.Morland
                                        .Include(x => x.LandNotification)
                                     
                                           .Where(x => (string.IsNullOrEmpty(model.name) || x.LandNotification.Name.Contains(model.name))
                               && (string.IsNullOrEmpty(model.propertyname) || x.PropertySiteNo.Contains(model.propertyname))
                               && (string.IsNullOrEmpty(model.sitedesc) || x.SiteDescription.Contains(model.sitedesc)))
                                .OrderBy(x => x.LandNotification.Name)
                                .GetPaged<Morland>(model.PageNumber, model.PageSize);
                        break;
                    case ("PROPERTY"):
                        data = null;
                        data = await _dbContext.Morland
                                         .Include(x => x.LandNotification)
                                    
                                            .Where(x => (string.IsNullOrEmpty(model.name) || x.LandNotification.Name.Contains(model.name))
                                && (string.IsNullOrEmpty(model.propertyname) || x.PropertySiteNo.Contains(model.propertyname))
                                && (string.IsNullOrEmpty(model.sitedesc) || x.SiteDescription.Contains(model.sitedesc)))
                                 .OrderBy(x => x.PropertySiteNo)
                                 .GetPaged<Morland>(model.PageNumber, model.PageSize);

                        break;
                    case ("SITE"):
                        data = null;
                        data = await _dbContext.Morland
                                        .Include(x => x.LandNotification)
                                   .Where(x => (string.IsNullOrEmpty(model.name) || x.LandNotification.Name.Contains(model.name))
                               && (string.IsNullOrEmpty(model.propertyname) || x.PropertySiteNo.Contains(model.propertyname))
                               && (string.IsNullOrEmpty(model.sitedesc) || x.SiteDescription.Contains(model.sitedesc)))
                                .OrderBy(x => x.SiteDescription)
                                .GetPaged<Morland>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Morland
                               .Include(x => x.LandNotification)
                               .Where(x => (string.IsNullOrEmpty(model.name) || x.LandNotification.Name.Contains(model.name))
                               && (string.IsNullOrEmpty(model.propertyname) || x.PropertySiteNo.Contains(model.propertyname))
                               && (string.IsNullOrEmpty(model.sitedesc) || x.SiteDescription.Contains(model.sitedesc)))
                                .OrderByDescending(x => x.IsActive==1)
                                .GetPaged<Morland>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("LOCALITY"):
                        data = null;
                        data = await _dbContext.Morland
                                        .Include(x => x.LandNotification)
                                        
                                           .Where(x => (string.IsNullOrEmpty(model.name) || x.LandNotification.Name.Contains(model.name))
                               && (string.IsNullOrEmpty(model.propertyname) || x.PropertySiteNo.Contains(model.propertyname))
                               && (string.IsNullOrEmpty(model.sitedesc) || x.SiteDescription.Contains(model.sitedesc)))
                                .OrderByDescending(x => x.LandNotification.Name)
                                .GetPaged<Morland>(model.PageNumber, model.PageSize);
                        break;
                    case ("PROPERTY"):
                        data = null;
                        data = await _dbContext.Morland
                                         .Include(x => x.LandNotification)
                                         
                                            .Where(x => (string.IsNullOrEmpty(model.name) || x.LandNotification.Name.Contains(model.name))
                                && (string.IsNullOrEmpty(model.propertyname) || x.PropertySiteNo.Contains(model.propertyname))
                                && (string.IsNullOrEmpty(model.sitedesc) || x.SiteDescription.Contains(model.sitedesc)))
                                 .OrderByDescending(x => x.PropertySiteNo)
                                 .GetPaged<Morland>(model.PageNumber, model.PageSize);

                        break;
                    case ("SITE"):
                        data = null;
                        data = await _dbContext.Morland
                                        .Include(x => x.LandNotification)
                                       
                                           .Where(x => (string.IsNullOrEmpty(model.name) || x.LandNotification.Name.Contains(model.name))
                               && (string.IsNullOrEmpty(model.propertyname) || x.PropertySiteNo.Contains(model.propertyname))
                               && (string.IsNullOrEmpty(model.sitedesc) || x.SiteDescription.Contains(model.sitedesc)))
                                .OrderByDescending(x => x.SiteDescription)
                                .GetPaged<Morland>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Morland
                                        .Include(x => x.LandNotification)
                                       
                                           .Where(x => (string.IsNullOrEmpty(model.name) || x.LandNotification.Name.Contains(model.name))
                               && (string.IsNullOrEmpty(model.propertyname) || x.PropertySiteNo.Contains(model.propertyname))
                               && (string.IsNullOrEmpty(model.sitedesc) || x.SiteDescription.Contains(model.sitedesc)))
                                .OrderBy(x => x.IsActive == 1)
                                .GetPaged<Morland>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            return data;
        }





    }
}
