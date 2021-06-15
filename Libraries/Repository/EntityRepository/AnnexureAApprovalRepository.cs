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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Libraries.Repository.EntityRepository
{
    public class AnnexureAApprovalRepository : GenericRepository<Fixingdemolition>, IAnnexureAApprovalRepository
    {
        public AnnexureAApprovalRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<List<Fixingdemolition>> GetAllFixingdemolition()
        {
            return await _dbContext.Fixingdemolition
                                        .Include(x => x.Encroachment.Locality)
                                        .Include(x => x.Encroachment)
                                        .Include(x => x.ApprovedStatusNavigation)
                .ToListAsync();
        }
        public async Task<PagedResult<Fixingdemolition>> GetPagedAnnexureA(AnnexureAApprovalSearchDto model, int userId, int zoneId)
        {
            var AllDataList = await _dbContext.Fixingdemolition.ToListAsync();
            var UserWiseDataList = AllDataList.Where(x => x.PendingAt.Split(',').Contains(userId.ToString()));
            List<int> myIdList = new List<int>();
            foreach (Fixingdemolition myLine in UserWiseDataList)
                myIdList.Add(myLine.Id);
            int[] myIdArray = myIdList.ToArray();

            var data = await _dbContext.Fixingdemolition
                                        .Include(x => x.Encroachment.Locality)
                                        .Include(x => x.Encroachment)
                                        .Include(x => x.ApprovedStatusNavigation)
                                        .Where(x => x.IsActive == 1
                                            && (model.StatusId == 0 ? (x.Encroachment.ZoneId == x.Encroachment.ZoneId) : (x.Encroachment.ZoneId == (zoneId == 0 ? x.Encroachment.ZoneId : zoneId)))
                                            && (model.StatusId == 0 ? x.PendingAt != "0" : x.PendingAt == "0")
                                            && (model.StatusId == 0 ? (myIdArray).Contains(x.Id) : x.PendingAt == "0")
                                            && (model.approvalstatusId == 0 ? (x.ApprovedStatus == x.ApprovedStatus) : (x.ApprovedStatus == model.approvalstatusId))
                                            )
                                        .GetPaged<Fixingdemolition>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("INSPECTIONDATE"):
                        data.Results = data.Results.OrderBy(x => (x.Encroachment.EncrochmentDate)).ToList();
                        break;
                    case ("LOCALITY"):
                        data.Results = data.Results.OrderBy(x => x.Encroachment == null ? null : x.Encroachment.Locality == null ? null : x.Encroachment.Locality.Name).ToList();
                        break;
                    case ("KHASRANO"):
                        data.Results = data.Results.OrderBy(x => x.Encroachment == null ? null : x.Encroachment.KhasraNo).ToList();
                        break;
                }

            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("INSPECTIONDATE"):
                        data.Results = data.Results.OrderByDescending(x => (x.Encroachment.EncrochmentDate)).ToList();
                        break;
                    case ("LOCALITY"):
                        data.Results = data.Results.OrderByDescending(x => x.Encroachment == null ? null : x.Encroachment.Locality == null ? null : x.Encroachment.Locality.Name).ToList();
                        break;
                    case ("KHASRANO"):
                        data.Results = data.Results.OrderByDescending(x => x.Encroachment == null ? null : x.Encroachment.KhasraNo).ToList();
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
            var AllDataList = await _dbContext.Fixingdemolition
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
