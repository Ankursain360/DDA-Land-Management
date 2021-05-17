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
        public async Task<PagedResult<Watchandward>> GetPagedWatchandward(WatchandwardApprovalSearchDto model, int userId, int zoneId)
        {
            var AllDataList = await _dbContext.Watchandward.ToListAsync();
            var UserWiseDataList = AllDataList.Where(x => x.PendingAt.Split(',').Contains(userId.ToString()));
            List<int> myIdList = new List<int>();
            foreach (Watchandward myLine in UserWiseDataList)
                myIdList.Add(myLine.Id);
            int[] myIdArray = myIdList.ToArray();

            var data = await _dbContext.Watchandward
                                        .Include(x => x.PrimaryListNoNavigation)
                                        .Include(x => x.PrimaryListNoNavigation.Locality)
                                        .Include(x => x.ApprovedStatusNavigation)
                                        .Where(x => x.IsActive == 1
                                            && (model.StatusId == 0 ? (x.PrimaryListNoNavigation.ZoneId == x.PrimaryListNoNavigation.ZoneId) : (x.PrimaryListNoNavigation.ZoneId == (zoneId == 0 ? x.PrimaryListNoNavigation.ZoneId : zoneId)))
                                            && (model.StatusId == 0 ? x.PendingAt != "0" : x.PendingAt == "0")
                                            && (model.StatusId == 0 ? (myIdArray).Contains(x.Id) : x.PendingAt == "0")
                                            )
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

        public async Task<bool> IsApplicationPendingAtUserEnd(int id, int userId)
        {
            var result = false;
            var AllDataList = await _dbContext.Watchandward
                                                .Where(x => x.IsActive == 1 && x.Id == id)
                                                .ToListAsync();
            var UserWiseDataList = AllDataList.Where(x => x.PendingAt.Split(',').Contains(userId.ToString())).ToList();

            if (UserWiseDataList.Count == 0)
                result = false;
            else
                result = true;

            return result;
        }

        public async Task<Watchandward> FetchSingleResult(int id)
        {
           return await _dbContext.Watchandward
                                        .Include(x => x.PrimaryListNoNavigation)
                                        .Include(x => x.PrimaryListNoNavigation.Locality)
                                        .Include(x => x.Locality)
                                        .Include(x => x.Khasra)
                                        .Where(x => x.IsActive == 1 && x.Id == id)
                                        .FirstOrDefaultAsync();
        }
    }
}
