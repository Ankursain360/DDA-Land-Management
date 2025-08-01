﻿using Dto.Search;
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
   
      public class BooktransferlandRepository : GenericRepository<Booktransferland>, IBooktransferlandRepository
    {
        public BooktransferlandRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Booktransferland>> GetPagedBooktransferland(BooktransferlandSearchDto model)
        {
            var data = await _dbContext.Booktransferland
                                       .Include(x => x.Locality)
                                       .Include(x => x.Khasra)
                                       .Include(x => x.OtherLandNotification)
                                         .Where(x => string.IsNullOrEmpty(model.name) || x.Part.Contains(model.name))
                                       .GetPaged<Booktransferland>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    
                    case ("NO"):
                        data = null;
                        data = await _dbContext.Booktransferland
                                                .Include(x => x.Locality)
                                                .Include(x => x.Khasra)
                                                .Include(x => x.OtherLandNotification)
                                                .Where(x => string.IsNullOrEmpty(model.name) || x.Part.Contains(model.name))
                                                .OrderBy(x => x.OtherLandNotification.NotificationNumber)
                                                .GetPaged<Booktransferland>(model.PageNumber, model.PageSize);

                        break;
                    case ("DATE"):
                        data = null;
                        data = await _dbContext.Booktransferland
                                               .Include(x => x.Locality)
                                               .Include(x => x.Khasra)
                                               .Include(x => x.OtherLandNotification)
                                               .Where(x => string.IsNullOrEmpty(model.name) || x.Part.Contains(model.name))
                                               .OrderBy(x => x.NotificationDate)
                                               .GetPaged<Booktransferland>(model.PageNumber, model.PageSize);



                        break;
                    case ("POSESSION"):
                        data = null;
                        data = await _dbContext.Booktransferland
                                                .Include(x => x.Locality)
                                                .Include(x => x.Khasra)
                                                .Include(x => x.OtherLandNotification)
                                                .Where(x => string.IsNullOrEmpty(model.name) || x.Part.Contains(model.name))
                                                .OrderBy(s => s.DateofPossession)
                                                .GetPaged<Booktransferland>(model.PageNumber, model.PageSize);




                        break;
                    case ("PART"):
                        data = null;
                        data = await _dbContext.Booktransferland
                                                .Include(x => x.Locality)
                                                .Include(x => x.Khasra)
                                                .Include(x => x.OtherLandNotification)
                                                .Where(x => string.IsNullOrEmpty(model.name) || x.Part.Contains(model.name))
                                                .OrderBy(s => s.Part)
                                                .GetPaged<Booktransferland>(model.PageNumber, model.PageSize);

                       

                        break;
                   
                    
                    

                       
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Booktransferland
                                              .Include(x => x.Locality)
                                              .Include(x => x.Khasra)
                                              .Include(x => x.OtherLandNotification)
                                              .Where(x => string.IsNullOrEmpty(model.name) || x.Part.Contains(model.name))
                                              .OrderByDescending(s => s.IsActive)
                                              .GetPaged<Booktransferland>(model.PageNumber, model.PageSize);


                    

                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("NO"):
                        data = null;
                        data = await _dbContext.Booktransferland
                                                .Include(x => x.Locality)
                                                .Include(x => x.Khasra)
                                                .Include(x => x.OtherLandNotification)
                                                .Where(x => string.IsNullOrEmpty(model.name) || x.Part.Contains(model.name))
                                                .OrderByDescending(s => s.OtherLandNotification.NotificationNumber)
                                                .GetPaged<Booktransferland>(model.PageNumber, model.PageSize);

                        break;
                    case ("DATE"):
                        data = null;
                        data = await _dbContext.Booktransferland
                                               .Include(x => x.Locality)
                                               .Include(x => x.Khasra)
                                               .Include(x => x.OtherLandNotification)
                                               .Where(x => string.IsNullOrEmpty(model.name) || x.Part.Contains(model.name))
                                               .OrderByDescending(s => s.NotificationDate)
                                               .GetPaged<Booktransferland>(model.PageNumber, model.PageSize);



                        break;
                    case ("POSESSION"):
                        data = null;
                        data = await _dbContext.Booktransferland
                                                .Include(x => x.Locality)
                                                .Include(x => x.Khasra)
                                                .Include(x => x.OtherLandNotification)
                                                .Where(x => string.IsNullOrEmpty(model.name) || x.Part.Contains(model.name))
                                                .OrderByDescending(s => s.DateofPossession)
                                                .GetPaged<Booktransferland>(model.PageNumber, model.PageSize);




                        break;
                    case ("PART"):
                        data = null;
                        data = await _dbContext.Booktransferland
                                                .Include(x => x.Locality)
                                                .Include(x => x.Khasra)
                                                .Include(x => x.OtherLandNotification)
                                                .Where(x => string.IsNullOrEmpty(model.name) || x.Part.Contains(model.name))
                                                .OrderByDescending(s => s.Part)
                                                .GetPaged<Booktransferland>(model.PageNumber, model.PageSize);



                        break;
                    
                    



                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Booktransferland
                                              .Include(x => x.Locality)
                                              .Include(x => x.Khasra)
                                              .Include(x => x.OtherLandNotification)
                                              .Where(x => string.IsNullOrEmpty(model.name) || x.Part.Contains(model.name))
                                              .OrderBy(s => s.IsActive)
                                              .GetPaged<Booktransferland>(model.PageNumber, model.PageSize);




                        break;

                }
            }
          
            
            
            return data;
        }
     
        
        
        public async Task<List<Booktransferland>> GetBooktransferland()
        {
            return await _dbContext.Booktransferland.ToListAsync();
        }
        public async Task<List<Booktransferland>> GetAllBooktransferland()
        {
            return await _dbContext.Booktransferland
                .Include(x => x.OtherLandNotification)
                .Include(x => x.Locality)
                .Include(x => x.Khasra)
                
                .ToListAsync();


        }
        public async Task<List<Booktransferland>> GetALlBooktransferlandList(BooktransferlandSearchDto model)
        {
            var data = await _dbContext.Booktransferland
                                       .Include(x => x.Locality)
                                       .Include(x => x.Khasra)
                                       .Include(x => x.OtherLandNotification)
                                         .Where(x => string.IsNullOrEmpty(model.name) || x.Part.Contains(model.name)).ToListAsync();
            return data;
        }

        public async Task<List<LandNotification>> GetAllLandNotification()
        {
            List<LandNotification> landNotificationList = await _dbContext.LandNotification.Where(x => x.IsActive == 1).ToListAsync();
            return landNotificationList;
        }

        public async Task<List<Otherlandnotification>> GetAllOtherLandNotification()
        {
            List<Otherlandnotification> List = await _dbContext.Otherlandnotification.Where(x => (x.IsActive == 1) && (x.LandType == "BOK")).ToListAsync();
            return List;
        }
        public async Task<List<Khasra>> BindKhasra(int? villageId)
        {
            List<Khasra> khasraList = await _dbContext.Khasra.Where(x => x.AcquiredlandvillageId == villageId && x.IsActive == 1).ToListAsync();
            return khasraList;
        }

        public async Task<List<Acquiredlandvillage>> GetAllLocality()
        {
            List<Acquiredlandvillage> localityList = await _dbContext.Acquiredlandvillage.Where(x => x.IsActive == 1).ToListAsync();
            return localityList;
        }
        public async Task<List<Khasra>> GetAllKhasra()
        {
            List<Khasra> khasraList = await _dbContext.Khasra.Where(x => x.IsActive == 1).ToListAsync();
            return khasraList;
        }


    }
}
