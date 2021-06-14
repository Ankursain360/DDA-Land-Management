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
    public class EncroachmentRegisterationApprovalRepository : GenericRepository<EncroachmentRegisteration>, IEncroachmentRegisterationApprovalRepository
    {
        public EncroachmentRegisterationApprovalRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<EncroachmentRegisteration>> GetPagedEncroachmentRegisteration(EncroachmentRegisterApprovalSearchDto model, int userId, int zoneId)
        {
            var AllDataList = await _dbContext.EncroachmentRegisteration.ToListAsync();
            var UserWiseDataList = AllDataList.Where(x => x.PendingAt.Split(',').Contains(userId.ToString()));
            List<int> myIdList = new List<int>();
            foreach (EncroachmentRegisteration myLine in UserWiseDataList)
                myIdList.Add(myLine.Id);
            int[] myIdArray = myIdList.ToArray();

            var data = await _dbContext.EncroachmentRegisteration
                                        .Include(x => x.Locality)
                                        .Include(x => x.ApprovedStatusNavigation)
                                        .Where(x => x.IsActive == 1
                                            && (model.StatusId == 0 ? (x.ZoneId == x.ZoneId) : (x.ZoneId == (zoneId == 0 ? x.ZoneId : zoneId)))
                                            && (model.StatusId == 0 ? x.PendingAt != "0" : x.PendingAt == "0")
                                            && (model.StatusId == 0 ? (myIdArray).Contains(x.Id) : x.PendingAt == "0")
                                            && (model.approvalstatusId == 0 ? (x.ApprovedStatus == x.ApprovedStatus) : (x.ApprovedStatus == model.approvalstatusId))
                                            )
                                        .GetPaged<EncroachmentRegisteration>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {

                    case ("DATE"):
                        data.Results = data.Results.OrderBy(x => x.EncrochmentDate).ToList();
                        break;
                    case ("LOCALITY"):
                        data.Results = data.Results.OrderBy(x => x.Locality.Name).ToList();
                        break;
                    case ("KHASRANO"):
                        data.Results = data.Results.OrderBy(x => x.KhasraNo).ToList();
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {

                    case ("DATE"):
                        data.Results = data.Results.OrderByDescending(x => x.EncrochmentDate).ToList();
                        break;
                    case ("LOCALITY"):
                        data.Results = data.Results.OrderByDescending(x => x.Locality.Name).ToList();
                        break;
                    case ("KHASRANO"):
                        data.Results = data.Results.OrderByDescending(x => x.KhasraNo).ToList();
                        break;


                }
            }
            return data;
        }


        public async Task<List<EncroachmentRegisteration>> GetEncroachmentRegisteration()
        {
            return await _dbContext.EncroachmentRegisteration.ToListAsync();
        }
        //public async Task<List<EncroachmentRegisteration>> GetAllEncroachmentRegisteration()
        //{
        //    return await _dbContext.EncroachmentRegisteration.Include(x => x.Village)
        //        .Include(x => x.Khasra)
        //        .ToListAsync();
        //}
        public async Task<List<EncroachmentRegisteration>> GetAllEncroachmentRegisteration()
        {
            return await _dbContext.EncroachmentRegisteration.Include(x => x.Locality)
                .Include(x => x.ApprovedStatusNavigation)
                .ToListAsync();
        }
        public async Task<List<Khasra>> GetAllKhasra()
        {
            List<Khasra> khasraList = await _dbContext.Khasra.Where(x => x.IsActive == 1).ToListAsync();
            return khasraList;
        }
        public async Task<List<Locality>> GetAllLocality()
        {
            List<Locality> localitylist = await _dbContext.Locality.Where(x => x.IsActive == 1).ToListAsync();
            return localitylist;
        }

        public async Task<bool> IsApplicationPendingAtUserEnd(int id, int userId)
        {
            var result = false;
            var AllDataList = await _dbContext.EncroachmentRegisteration
                                                .Where(x => x.IsActive == 1 && x.Id == id)
                                                .ToListAsync();
            var UserWiseDataList = AllDataList.Where(x => x.PendingAt.Split(',').Contains(userId.ToString())).ToList();

            if (UserWiseDataList.Count == 0)
                result = false;
            else
                result = true;

            return result;
        }

    }
}
