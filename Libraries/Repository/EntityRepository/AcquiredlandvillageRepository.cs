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

using Repository.Common;

namespace Libraries.Repository.EntityRepository
{
    public class AcquiredlandvillageRepository : GenericRepository<Acquiredlandvillage>, IAcquiredlandvillageRepository
    {
        public AcquiredlandvillageRepository(DataContext dbContext) : base(dbContext)
        {

        }



        public async Task<List<District>> GetAllDistrict()
        {
            List<District> districtList = await _dbContext.District.Where(x => x.IsActive == 1).ToListAsync();
            return districtList;
        }
        public async Task<List<Tehsil>> GetAllTehsil()
        {
            List<Tehsil> tehsilList = await _dbContext.Tehsil.Where(x => x.IsActive == 1).ToListAsync();
            return tehsilList;
        }

        public async Task<List<Zone>> GetAllZone()
        {
            List<Zone> zonelist = await _dbContext.Zone.Where(x => x.IsActive == 1).ToListAsync();
            return zonelist;
        }




        public async Task<List<Acquiredlandvillage>> GetAcquiredlandvillage()
        {
            return await _dbContext.Acquiredlandvillage.Include(x => x.District).Include(x => x.Tehsil).Include(x => x.Zone).ToListAsync();
        }


        public async Task<PagedResult<Acquiredlandvillage>> GetPagedAcquiredlandvillage(AcquiredLandVillageSearchDto model)
        {
            var data = await _dbContext.Acquiredlandvillage
                                   .Include(x => x.District)
                                   .Include(x => x.Tehsil)
                                   .Include(x => x.Zone)
                                   .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                    && (string.IsNullOrEmpty(model.tehsil) || x.Tehsil.Name.Contains(model.tehsil))
                                    && (string.IsNullOrEmpty(model.district) || x.District.Name.Contains(model.district)))
                                   .GetPaged<Acquiredlandvillage>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("CODE"):
                        data = null;
                        data = await _dbContext.Acquiredlandvillage
                                   .Include(x => x.District)
                                   .Include(x => x.Tehsil)
                                   .Include(x => x.Zone)
                                   .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                    && (string.IsNullOrEmpty(model.tehsil) || x.Tehsil.Name.Contains(model.tehsil))
                                    && (string.IsNullOrEmpty(model.district) || x.District.Name.Contains(model.district)))
                                   .OrderBy(a => a.Code)
                                   .GetPaged<Acquiredlandvillage>(model.PageNumber, model.PageSize);
                        break;
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Acquiredlandvillage
                                   .Include(x => x.District)
                                   .Include(x => x.Tehsil)
                                   .Include(x => x.Zone)
                                   .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                    && (string.IsNullOrEmpty(model.tehsil) || x.Tehsil.Name.Contains(model.tehsil))
                                    && (string.IsNullOrEmpty(model.district) || x.District.Name.Contains(model.district)))
                                   .OrderBy(a => a.Name)
                                   .GetPaged<Acquiredlandvillage>(model.PageNumber, model.PageSize);
                        break;

                    case ("TEHSIL"):
                        data = null;
                        data = await _dbContext.Acquiredlandvillage
                                   .Include(x => x.District)
                                   .Include(x => x.Tehsil)
                                   .Include(x => x.Zone)
                                   .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                    && (string.IsNullOrEmpty(model.tehsil) || x.Tehsil.Name.Contains(model.tehsil))
                                    && (string.IsNullOrEmpty(model.district) || x.District.Name.Contains(model.district)))
                                   .OrderBy(a => a.Tehsil.Name)
                                   .GetPaged<Acquiredlandvillage>(model.PageNumber, model.PageSize);
                        break;
                    case ("DISTRICT"):
                        data = null;
                        data = await _dbContext.Acquiredlandvillage
                                   .Include(x => x.District)
                                   .Include(x => x.Tehsil)
                                   .Include(x => x.Zone)
                                   .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                    && (string.IsNullOrEmpty(model.tehsil) || x.Tehsil.Name.Contains(model.tehsil))
                                    && (string.IsNullOrEmpty(model.district) || x.District.Name.Contains(model.district)))
                                   .OrderBy(a => a.District.Name)
                                   .GetPaged<Acquiredlandvillage>(model.PageNumber, model.PageSize);
                        break;


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Acquiredlandvillage
                                   .Include(x => x.District)
                                   .Include(x => x.Tehsil)
                                   .Include(x => x.Zone)
                                   .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                    && (string.IsNullOrEmpty(model.tehsil) || x.Tehsil.Name.Contains(model.tehsil))
                                    && (string.IsNullOrEmpty(model.district) || x.District.Name.Contains(model.district)))
                                   .OrderByDescending(a => a.IsActive)
                                   .GetPaged<Acquiredlandvillage>(model.PageNumber, model.PageSize);
                        break;


                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("CODE"):
                        data = null;
                        data = await _dbContext.Acquiredlandvillage
                                   .Include(x => x.District)
                                   .Include(x => x.Tehsil)
                                   .Include(x => x.Zone)
                                   .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                    && (string.IsNullOrEmpty(model.tehsil) || x.Tehsil.Name.Contains(model.tehsil))
                                    && (string.IsNullOrEmpty(model.district) || x.District.Name.Contains(model.district)))
                                   .OrderByDescending(a => a.Code)
                                   .GetPaged<Acquiredlandvillage>(model.PageNumber, model.PageSize);
                        break;
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Acquiredlandvillage
                                   .Include(x => x.District)
                                   .Include(x => x.Tehsil)
                                   .Include(x => x.Zone)
                                   .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                    && (string.IsNullOrEmpty(model.tehsil) || x.Tehsil.Name.Contains(model.tehsil))
                                    && (string.IsNullOrEmpty(model.district) || x.District.Name.Contains(model.district)))
                                   .OrderByDescending(a => a.Name)
                                   .GetPaged<Acquiredlandvillage>(model.PageNumber, model.PageSize);
                        break;

                    case ("TEHSIL"):
                        data = null;
                        data = await _dbContext.Acquiredlandvillage
                                   .Include(x => x.District)
                                   .Include(x => x.Tehsil)
                                   .Include(x => x.Zone)
                                   .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                    && (string.IsNullOrEmpty(model.tehsil) || x.Tehsil.Name.Contains(model.tehsil))
                                    && (string.IsNullOrEmpty(model.district) || x.District.Name.Contains(model.district)))
                                   .OrderByDescending(a => a.Tehsil.Name)
                                   .GetPaged<Acquiredlandvillage>(model.PageNumber, model.PageSize);
                        break;
                    case ("DISTRICT"):
                        data = null;
                        data = await _dbContext.Acquiredlandvillage
                                   .Include(x => x.District)
                                   .Include(x => x.Tehsil)
                                   .Include(x => x.Zone)
                                   .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                    && (string.IsNullOrEmpty(model.tehsil) || x.Tehsil.Name.Contains(model.tehsil))
                                    && (string.IsNullOrEmpty(model.district) || x.District.Name.Contains(model.district)))
                                   .OrderByDescending(a => a.District.Name)
                                   .GetPaged<Acquiredlandvillage>(model.PageNumber, model.PageSize);
                        break;


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Acquiredlandvillage
                                   .Include(x => x.District)
                                   .Include(x => x.Tehsil)
                                   .Include(x => x.Zone)
                                   .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                    && (string.IsNullOrEmpty(model.tehsil) || x.Tehsil.Name.Contains(model.tehsil))
                                    && (string.IsNullOrEmpty(model.district) || x.District.Name.Contains(model.district)))
                                   .OrderBy(a => a.IsActive)
                                   .GetPaged<Acquiredlandvillage>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            return data;
        }

        public async Task<PagedResult<Acquiredlandvillage>> GetPagedVillageReport(VillageReportSearchDto model)
        {
            var data = await _dbContext.Acquiredlandvillage


                  .Where(x => (x.IsActive == 1)
                                   && (x.Id == (model.Name == 0 ? x.Id : model.Name)))



                                   .OrderByDescending(x => x.Id)
                               .GetPaged<Acquiredlandvillage>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {

                    case ("VILLAGE"):
                        data.Results = data.Results.OrderBy(x => x.Name).ToList();
                        break;

                    case ("CONSOLIDATION"):
                        data.Results = data.Results.OrderBy(x => x.YearofConsolidation).ToList();
                        break;
                    case ("SHEET"):
                        data.Results = data.Results.OrderBy(x => x.TotalNoOfSheet).ToList();
                        break;
                    case ("ACQUIRED"):
                        data.Results = data.Results.OrderBy(x => x.Acquired).ToList();
                        break;

                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {

                    case ("VILLAGE"):
                        data.Results = data.Results.OrderByDescending(x => x.Name).ToList();
                        break;

                    case ("CONSOLIDATION"):
                        data.Results = data.Results.OrderByDescending(x => x.YearofConsolidation).ToList();
                        break;
                    case ("SHEET"):
                        data.Results = data.Results.OrderByDescending(x => x.TotalNoOfSheet).ToList();
                        break;
                    case ("ACQUIRED"):
                        data.Results = data.Results.OrderByDescending(x => x.Acquired).ToList();
                        break;
                }
            }
            return data;
        }

        public async Task<List<Acquiredlandvillage>> GetAllVillageList()
        {
            List<Acquiredlandvillage> villageList = await _dbContext.Acquiredlandvillage.Where(x => x.IsActive == 1).ToListAsync();
            return villageList;
        }

        public async Task<PagedResult<Acquiredlandvillage>> GetPagedAcquiredVillageReport(AcquiredVillageReportSearchDto model)
        {
            var data = await _dbContext.Acquiredlandvillage


                   .Where(x => (x.Acquired == "yes")
                                    && (x.Id == (model.Name == 0 ? x.Id : model.Name)))



                                    .OrderByDescending(x => x.Id)
                                .GetPaged<Acquiredlandvillage>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {

                    case ("VILLAGE"):
                        data.Results = data.Results.OrderBy(x => x.Name).ToList();
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {

                    case ("VILLAGE"):
                        data.Results = data.Results.OrderByDescending(x => x.Name).ToList();
                        break;

                }
            }
            return data;
        }



        public async Task<List<VillageDetailsLitDataDto>> GetPagedvillagedetailsList(VillagedetailsSearchDto model)

        {
            try
            {


                var data = await _dbContext.LoadStoredProcedure("villagedetails")
                                            .WithSqlParams(("S_villageId", model.village))



                                            .ExecuteStoredProcedureAsync<VillageDetailsLitDataDto>();

                return (List<VillageDetailsLitDataDto>)data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


    }
}
