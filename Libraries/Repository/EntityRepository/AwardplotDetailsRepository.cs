using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Dto.Master;
using Repository.Common;

namespace Libraries.Repository.EntityRepository
{
  public  class AwardplotDetailsRepository: GenericRepository<Awardplotdetails>, IAwardplotDetailsRepository
    {
        public AwardplotDetailsRepository(DataContext dbContext) : base(dbContext)
        {

        }


        public async Task<List<Awardmasterdetail>> GetAllAWardmaster()
        {
            List<Awardmasterdetail> awardList = await _dbContext.Awardmasterdetail.Where(x => x.IsActive == 1).ToListAsync();
            return awardList;
        }
        public async Task<List<Awardplotdetails>> GetAwardplotdetails()
        {
            return await _dbContext.Awardplotdetails.Include(x => x.AwardMaster).Include(x => x.Village).Include(x => x.Khasra).OrderByDescending(x => x.Id).ToListAsync();
        }
        public async Task<List<Awardplotdetails>> GetAllAwardplotdetailsList(AwardPlotDetailSearchDto model)
        {
            var data = await _dbContext.Awardplotdetails
                                    .Include(x => x.AwardMaster)
                                    .Include(x => x.Village)
                                    .Include(x => x.Khasra)
                                    .Where(x => string.IsNullOrEmpty(model.name) || x.AwardMaster.AwardNumber.Contains(model.name))
                                      .OrderByDescending(x => x.Id).ToListAsync();
            return data;
        }

        public async Task<List<Acquiredlandvillage>> GetAllVillage()
        {
            List<Acquiredlandvillage> villageList = await _dbContext.Acquiredlandvillage.Where(x => x.IsActive == 1).ToListAsync();
            return villageList;
        }



        public async Task<List<Khasra>> BindKhasra(int? villageId)
        {
            List<Khasra> khasraList = await _dbContext.Khasra.Where(x => x.AcquiredlandvillageId == villageId && x.IsActive == 1).ToListAsync();
            return khasraList;
        }


        public async Task<PagedResult<Awardplotdetails>> GetPagedAwardplotdetails(AwardPlotDetailSearchDto model)
        {
            
                var data = await _dbContext.Awardplotdetails
                                        .Include(x => x.AwardMaster)
                                        .Include(x => x.Village)
                                        .Include(x => x.Khasra)
                                          .OrderByDescending(x => x.Id)
                              .GetPaged<Awardplotdetails>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("AWARD"):
                        data = null;
                        data = await _dbContext.Awardplotdetails
                                        .Include(x => x.AwardMaster)
                                        .Include(x => x.Village)
                                        .Include(x => x.Khasra)
                                .Where(x => string.IsNullOrEmpty(model.name) || x.AwardMaster.AwardNumber.Contains(model.name))
                                .OrderBy(x => x.AwardMaster.AwardNumber)
                                .GetPaged<Awardplotdetails>(model.PageNumber, model.PageSize);
                        break;
                    case ("VILLAGE"):
                        data = null;
                        data = await _dbContext.Awardplotdetails
                                        .Include(x => x.AwardMaster)
                                        .Include(x => x.Village)
                                        .Include(x => x.Khasra)
                                 .Where(x => string.IsNullOrEmpty(model.name) || x.AwardMaster.AwardNumber.Contains(model.name))
                                .OrderBy(x => x.Village.Name)
                                .GetPaged<Awardplotdetails>(model.PageNumber, model.PageSize);
                       
                        break;
                    case ("KHASRA"):
                        data = null;
                        data = await _dbContext.Awardplotdetails
                                        .Include(x => x.AwardMaster)
                                        .Include(x => x.Village)
                                        .Include(x => x.Khasra)
                                 .Where(x => string.IsNullOrEmpty(model.name) || x.AwardMaster.AwardNumber.Contains(model.name))
                                .OrderBy(x => x.Khasra.Name)
                                .GetPaged<Awardplotdetails>(model.PageNumber, model.PageSize);
                        break;
                     case ("STATUS"):
                        data = null;
                        data = await _dbContext.Awardplotdetails
                                        .Include(x => x.AwardMaster)
                                        .Include(x => x.Village)
                                        .Include(x => x.Khasra)
                                 .Where(x => string.IsNullOrEmpty(model.name) || x.AwardMaster.AwardNumber.Contains(model.name))
                                .OrderByDescending(x => x.IsActive)
                                .GetPaged<Awardplotdetails>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("AWARD"):
                        data = null;
                        data = await _dbContext.Awardplotdetails
                                        .Include(x => x.AwardMaster)
                                        .Include(x => x.Village)
                                        .Include(x => x.Khasra)
                                .Where(x => string.IsNullOrEmpty(model.name) || x.AwardMaster.AwardNumber.Contains(model.name))
                                .OrderByDescending(x => x.AwardMaster.AwardNumber)
                                .GetPaged<Awardplotdetails>(model.PageNumber, model.PageSize);
                        break;
                    case ("VILLAGE"):
                        data = null;
                        data = await _dbContext.Awardplotdetails
                                        .Include(x => x.AwardMaster)
                                        .Include(x => x.Village)
                                        .Include(x => x.Khasra)
                                 .Where(x => string.IsNullOrEmpty(model.name) || x.AwardMaster.AwardNumber.Contains(model.name))
                                .OrderByDescending(x => x.Village.Name)
                                .GetPaged<Awardplotdetails>(model.PageNumber, model.PageSize);

                        break;
                    case ("KHASRA"):
                        data = null;
                        data = await _dbContext.Awardplotdetails
                                        .Include(x => x.AwardMaster)
                                        .Include(x => x.Village)
                                        .Include(x => x.Khasra)
                                 .Where(x => string.IsNullOrEmpty(model.name) || x.AwardMaster.AwardNumber.Contains(model.name))
                                .OrderByDescending(x => x.Khasra.Name)
                                .GetPaged<Awardplotdetails>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Awardplotdetails
                                        .Include(x => x.AwardMaster)
                                        .Include(x => x.Village)
                                        .Include(x => x.Khasra)
                                 .Where(x => string.IsNullOrEmpty(model.name) || x.AwardMaster.AwardNumber.Contains(model.name))
                                .OrderBy(x => x.IsActive)
                                .GetPaged<Awardplotdetails>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            return data;
        }

        public async Task<List<AwardReportDtoProfile>> BindAwardNoDateList()
        {
            try
            {
                var data = await _dbContext.LoadStoredProcedure("BindDropdownForAwardReport")
                                            .WithOutParams()
                                            .ExecuteStoredProcedureAsync<AwardReportDtoProfile>();

                return (List<AwardReportDtoProfile>)data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PagedResult<Awardplotdetails>> GetPagedAwardReport(AwardReportSearchDto model)
        {
            var data = await _dbContext.Awardplotdetails
                                        .Include(x => x.AwardMaster)
                                        .Include(x => x.Village)
                                        .Include(x => x.Khasra)
                                        .Where(x => x.AwardMasterId == (model.Id == 0 ? x.AwardMasterId : model.Id)
                                        && x.IsActive == 1
                                        )
                                        .GetPaged<Awardplotdetails>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                data = null;
                data = await _dbContext.Awardplotdetails
                                        .Include(x => x.AwardMaster)
                                        .Include(x => x.Village)
                                        .Include(x => x.Khasra)
                                        .Where(x => x.AwardMasterId == (model.Id == 0 ? x.AwardMasterId : model.Id)
                                        && x.IsActive == 1
                                        )
                                .OrderBy(s =>
                                (model.SortBy.ToUpper() == "VILLAGE" ? (s.Village == null ? null : s.Village.Name)
                                : model.SortBy.ToUpper() == "KHASRA" ? (s.Khasra != null ? s.Khasra.Name : null) : (s.Village == null ? null : s.Village.Name))
                                )
                                .GetPaged<Awardplotdetails>(model.PageNumber, model.PageSize);
            }
            else if (SortOrder == 2)
            {
                data = null;
                data = await _dbContext.Awardplotdetails
                                        .Include(x => x.AwardMaster)
                                        .Include(x => x.Village)
                                        .Include(x => x.Khasra)
                                        .Where(x => x.AwardMasterId == (model.Id == 0 ? x.AwardMasterId : model.Id)
                                        && x.IsActive == 1
                                        )
                                .OrderByDescending(s =>
                                 (model.SortBy.ToUpper() == "VILLAGE" ? (s.Village == null ? null : s.Village.Name)
                                : model.SortBy.ToUpper() == "KHASRA" ? (s.Khasra != null ? s.Khasra.Name : null) : (s.Village == null ? null : s.Village.Name))
                                )
                                .GetPaged<Awardplotdetails>(model.PageNumber, model.PageSize);
            }
            return data;
        }

        public async Task<Khasra> FetchSingleKhasraResult(int? khasraId)
        {
            return await _dbContext.Khasra.Where(x => x.Id == khasraId).SingleOrDefaultAsync();
        }


        public async Task<PagedResult<Awardplotdetails>> GetAllAwardViewList(AwardViewSearchDto model)
        {
            var data = await _dbContext.Awardplotdetails
                                        .Include(x => x.Village)
                                        .Include(x => x.AwardMaster)
                                        .Include(x => x.Khasra)
                                        .Where(x => x.AwardMasterId == model.AwardId)
                                        .OrderByDescending(x => x.Id)
                                        .GetPaged<Awardplotdetails>(model.PageNumber, model.PageSize);
            return data;
        }
        public async Task<List<Awardplotdetails>> GetAllAwardplotdetails(AwardReportSearchDto model)
        {
            var data = await _dbContext.Awardplotdetails
                                        .Include(x => x.AwardMaster)
                                        .Include(x => x.Village)
                                        .Include(x => x.Khasra)
                                         .Where(x => x.AwardMasterId == (model.Id == 0 ? x.AwardMasterId : model.Id)
                                        && x.IsActive == 1
                                        ).ToListAsync();
            return data;
        }
    }
}
