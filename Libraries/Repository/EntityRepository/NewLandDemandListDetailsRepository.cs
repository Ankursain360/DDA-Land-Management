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
    public class NewLandDemandListDetailsRepository : GenericRepository<Newlanddemandlistdetails>, INewLandDemandListDetailsRepository
    {
        public NewLandDemandListDetailsRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<PagedResult<Newlanddemandlistdetails>> GetPagedDMSFileUploadList(NewLandDemandListDetailsSearchDto model)
        {
            var data = await _dbContext.Newlanddemandlistdetails
                                        .Include(x => x.Village)
                                        .Include(x => x.KhasraNo)
                                        .Where(x => x.VillageId == (model.villageId == 0 ? x.VillageId : model.villageId)
                                        && (x.DemandListNo != null ? x.DemandListNo.Contains(model.demandlistno == "" ? x.DemandListNo : model.demandlistno) : true)
                                        && (x.KhasraNoId == (model.KhasraId == 0 ? x.KhasraNoId : model.KhasraId))
                                        )
                                        .GetPaged<Newlanddemandlistdetails>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                data = null;
                data = await _dbContext.Newlanddemandlistdetails
                                        .Include(x => x.Village)
                                        .Include(x => x.KhasraNo)
                                        .Where(x => x.VillageId == (model.villageId == 0 ? x.VillageId : model.villageId)
                                        && (x.DemandListNo != null ? x.DemandListNo.Contains(model.demandlistno == "" ? x.DemandListNo : model.demandlistno) : true)
                                        && (x.KhasraNoId == (model.KhasraId == 0 ? x.KhasraNoId : model.KhasraId))
                                        )
                                .OrderBy(s =>
                                (model.SortBy.ToUpper() == "DEMANDLIST" ? s.DemandListNo
                                : model.SortBy.ToUpper() == "VILLAGE" ? (s.Village == null ? null : s.Village.Name)
                                : model.SortBy.ToUpper() == "KHASRANO" ? (s.KhasraNo != null ? s.KhasraNo.Name : null) : s.DemandListNo)
                                )
                                .GetPaged<Newlanddemandlistdetails>(model.PageNumber, model.PageSize);
            }
            else if (SortOrder == 2)
            {
                data = null;
                data = await _dbContext.Newlanddemandlistdetails
                                        .Include(x => x.Village)
                                        .Include(x => x.KhasraNo)
                                        .Where(x => x.VillageId == (model.villageId == 0 ? x.VillageId : model.villageId)
                                        && (x.DemandListNo != null ? x.DemandListNo.Contains(model.demandlistno == "" ? x.DemandListNo : model.demandlistno) : true)
                                        && (x.KhasraNoId == (model.KhasraId == 0 ? x.KhasraNoId : model.KhasraId))
                                        )
                                .OrderByDescending(s =>
                                 (model.SortBy.ToUpper() == "DEMANDLIST" ? s.DemandListNo
                                : model.SortBy.ToUpper() == "VILLAGE" ? (s.Village == null ? null : s.Village.Name)
                                : model.SortBy.ToUpper() == "KHASRANO" ? (s.KhasraNo != null ? s.KhasraNo.Name : null) : s.DemandListNo)
                                )
                                .GetPaged<Newlanddemandlistdetails>(model.PageNumber, model.PageSize);
            }
            return data;
        }
        public async Task<List<Newlanddemandlistdetails>> GetAllDemandlistdetails()
        {
            return await _dbContext.Newlanddemandlistdetails
                                   .Include(x => x.Village)
                                   .Include(x => x.KhasraNo)
                                   .ToListAsync();
        }
        public async Task<Newlanddemandlistdetails> FetchSingleResult(int id)
        {
            return await _dbContext.Newlanddemandlistdetails
                                        .Include(x => x.Village)
                                        .Include(x => x.KhasraNo)
                                        .Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();


        }

        public int GetLocalityByName(string name)
        {
            var File = (from f in _dbContext.Locality
                        where f.Name.ToUpper().Trim() == name.ToUpper().Trim()
                        select f.Id).FirstOrDefault();

            return File;
        }

        public int GetKhasraByName(string name)
        {
            var File = (from f in _dbContext.Propertyregistration
                        where f.KhasraNo.ToUpper().Trim() == name.ToUpper().Trim()
                        select f.Id).FirstOrDefault();

            return File;
        }

        public async Task<bool> Any(int id, string fileNo)
        {
            return await _dbContext.Dmsfileupload.AnyAsync(t => t.Id != id && t.FileNo.ToLower() == fileNo.ToLower());
        }
        public async Task<List<Newlandvillage>> GetVillageList()
        {
            return await _dbContext.Newlandvillage
                                     .Where(x => x.IsActive == 1)
                                     .ToListAsync();
        }

        public async Task<List<Newlandkhasra>> GetKhasraList(int id)
        {
            return await _dbContext.Newlandkhasra
                                     .Where(x => x.IsActive == 1 && x.NewLandvillageId == id)
                                     .ToListAsync();
        }
    }
}
