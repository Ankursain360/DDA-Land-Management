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
    public class WatchAndWardApprovalRepository : GenericRepository<Watchandward>, IWatchAndWardApprovalRepository
    {
        public WatchAndWardApprovalRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Watchandward>> GetPagedWatchandward(WatchandwardApprovalSearchDto model, int userId)
        {

            var data = await _dbContext.Watchandward
                                    .Include(x => x.PrimaryListNoNavigation)
                                    .Include(x => x.PrimaryListNoNavigation.Locality)
                                    .Include(x => x.Locality)
                                    .Include(x => x.Khasra)
                                    .GetPaged<Watchandward>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {

                    case ("DATE"):
                        data.Results = data.Results.OrderBy(x => x.Date).ToList();
                        break;
                    case ("LOCALITY"):
                        data.Results = data.Results.OrderBy(x => x.PrimaryListNoNavigation.Locality.Name).ToList();
                        break;
                    case ("KHASRANO"):
                        data.Results = data.Results.OrderBy(x => x.PrimaryListNoNavigation.KhasraNo).ToList();
                        break;
                    case ("PRIMARYLISTNO"):
                        data.Results = data.Results.OrderBy(x => x.PrimaryListNo).ToList();
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {

                    case ("DATE"):
                        data.Results = data.Results.OrderByDescending(x => x.Date).ToList();
                        break;
                    case ("LOCALITY"):
                        data.Results = data.Results.OrderByDescending(x => x.PrimaryListNoNavigation.Locality.Name).ToList();
                        break;
                    case ("KHASRANO"):
                        data.Results = data.Results.OrderByDescending(x => x.PrimaryListNoNavigation.KhasraNo).ToList();
                        break;
                    case ("PRIMARYLISTNO"):
                        data.Results = data.Results.OrderByDescending(x => x.PrimaryListNo).ToList();
                        break;

                }
            }
            return data;
        }


        public async Task<List<Watchandward>> GetWatchandward()
        {
            return await _dbContext.Watchandward.ToListAsync();
        }
        public async Task<List<Watchandward>> GetAllWatchandward()
        {
            return await _dbContext.Watchandward.Include(x => x.Locality)
                .Include(x => x.Khasra)
                .ToListAsync();
        }
        public async Task<List<Khasra>> GetAllKhasra()
        {
            List<Khasra> khasraList = await _dbContext.Khasra.Where(x => x.IsActive == 1).ToListAsync();
            return khasraList;
        }
        public async Task<List<Village>> GetAllVillage()
        {
            List<Village> villagelist = await _dbContext.Village.Where(x => x.IsActive == 1).ToListAsync();
            return villagelist;
        }
        public async Task<List<Locality>> GetAllLocality()
        {
            List<Locality> localitylist = await _dbContext.Locality.Where(x => x.IsActive == 1).ToListAsync();
            return localitylist;
        }

    }
}
