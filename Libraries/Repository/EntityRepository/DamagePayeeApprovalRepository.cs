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

    public class DamagePayeeApprovalRepository : GenericRepository<Damagepayeeregister>, IDamagePayeeApprovalRepository
    {
        public DamagePayeeApprovalRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<PagedResult<Damagepayeeregister>> GetPagedDamageForApproval(DamagepayeeRegisterApprovalDto model, int userId)
        {
            var AllDataList = await _dbContext.Damagepayeeregister.ToListAsync();
            var UserWiseDataList = AllDataList.Where(x => x.PendingAt.Split(',').Contains(userId.ToString()));
            List<int> myIdList = new List<int>();
            foreach (Damagepayeeregister myLine in UserWiseDataList)
                myIdList.Add(myLine.Id);
            int[] myIdArray = myIdList.ToArray();

            var data = await _dbContext.Damagepayeeregister
                                        .Include(x => x.Locality)
                                        .Include(x => x.District)
                                        .Include(x => x.ApprovedStatusNavigation)
                                        .Where(x => x.IsActive == 1
                                        && (model.StatusId == 0 ? x.PendingAt != "0" : x.PendingAt == "0")
                                        && (model.StatusId == 0 ? (myIdArray).Contains(x.Id) : x.PendingAt == "0")
                                        )
                                        .GetPaged<Damagepayeeregister>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                data = null;
                data = await _dbContext.Damagepayeeregister
                                .Include(x => x.Locality)
                                .Include(x => x.District)
                                        .Include(x => x.ApprovedStatusNavigation)
                                .Where(x => x.IsActive == 1
                                        && (model.StatusId == 0 ? x.PendingAt != "0" : x.PendingAt == "0")
                                        && (model.StatusId == 0 ? (myIdArray).Contains(x.Id) : x.PendingAt == "0")
                                        )
                                .OrderBy(s =>
                                (model.SortBy.ToUpper() == "FILENO" ? s.FileNo : model.SortBy.ToUpper() == "DISTRICT" ? s.District.Name : model.SortBy.ToUpper() == "LOCALITY" ? s.Locality.Name : s.FileNo)
                                )
                                .GetPaged<Damagepayeeregister>(model.PageNumber, model.PageSize);
            }
            else if (SortOrder == 2)
            {
                data = null;
                data = await _dbContext.Damagepayeeregister
                                .Include(x => x.Locality)
                                .Include(x => x.District)
                                        .Include(x => x.ApprovedStatusNavigation)
                                .Where(x => x.IsActive == 1
                                        && (model.StatusId == 0 ? x.PendingAt != "0" : x.PendingAt == "0")
                                        && (model.StatusId == 0 ? (myIdArray).Contains(x.Id) : x.PendingAt == "0")
                                        )
                                .OrderByDescending(s =>
                                (model.SortBy.ToUpper() == "FILENO" ? s.FileNo : model.SortBy.ToUpper() == "DISTRICT" ? (s.District == null ? null : s.District.Name) : model.SortBy.ToUpper() == "LOCALITY" ? (s.Locality != null ? s.Locality.Name : null) : s.FileNo)
                                )
                                .GetPaged<Damagepayeeregister>(model.PageNumber, model.PageSize);
            }
            return data;
        }

        public async Task<bool> IsApplicationPendingAtUserEnd(int id, int userId)
        {
            var result = false;
            var AllDataList = await _dbContext.Damagepayeeregister
                                                .Where(x => x.IsActive == 1 && x.Id == id)
                                                .ToListAsync();
            var UserWiseDataList = AllDataList.Where(x => x.PendingAt.Split(',').Contains(userId.ToString())).ToList();

            if (UserWiseDataList.Count == 0)
                result = false;
            else
                result = true;

            return result;
        }
        public async Task<Damagepayeeregister> FetchSingleResult(int id)
        {
            return await _dbContext.Damagepayeeregister
                                .Include(x => x.Locality)
                                .Include(x => x.District)
                                .Where(x => x.IsActive == 1 && x.Id == id)
                                .FirstOrDefaultAsync();
        }
    }
}
