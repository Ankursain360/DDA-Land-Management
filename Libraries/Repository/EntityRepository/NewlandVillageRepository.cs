﻿using Libraries.Model;
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
    public class NewlandVillageRepository : GenericRepository<Newlandvillage>, INewlandvillageRepository
    {
        public NewlandVillageRepository(DataContext dbContext) : base(dbContext)
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




        public async Task<List<Newlandvillage>> GetNewlandvillage()
        {
            return await _dbContext.Newlandvillage.Include(x => x.District).Include(x => x.Tehsil).Include(x => x.Zone).ToListAsync();
        }


        public async Task<PagedResult<Newlandvillage>> GetPagedNewlandvillage(NewlandvillageSearchDto model)
        {
            var data = await _dbContext.Newlandvillage
                                   .Include(x => x.District)
                                   .Include(x => x.Tehsil)
                                   .Include(x => x.Zone)
                                   .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                    && (string.IsNullOrEmpty(model.tehsil) || x.Tehsil.Name.Contains(model.tehsil))
                                    && (string.IsNullOrEmpty(model.district) || x.District.Name.Contains(model.district)))
                                   .GetPaged<Newlandvillage>(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("CODE"):
                        data = null;
                        data = await _dbContext.Newlandvillage
                                   .Include(x => x.District)
                                   .Include(x => x.Tehsil)
                                   .Include(x => x.Zone)
                                   .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                    && (string.IsNullOrEmpty(model.tehsil) || x.Tehsil.Name.Contains(model.tehsil))
                                    && (string.IsNullOrEmpty(model.district) || x.District.Name.Contains(model.district)))
                                   .OrderBy(a => a.Code)
                                   .GetPaged<Newlandvillage>(model.PageNumber, model.PageSize);

                                   
                                   
                        break;
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Newlandvillage
                                   .Include(x => x.District)
                                   .Include(x => x.Tehsil)
                                   .Include(x => x.Zone)
                                   .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                    && (string.IsNullOrEmpty(model.tehsil) || x.Tehsil.Name.Contains(model.tehsil))
                                    && (string.IsNullOrEmpty(model.district) || x.District.Name.Contains(model.district)))
                                   .OrderBy(a => a.Name)
                                   .GetPaged<Newlandvillage>(model.PageNumber, model.PageSize);
                        break;

                    case ("TEHSIL"):
                        data = null;
                        data = await _dbContext.Newlandvillage
                                   .Include(x => x.District)
                                   .Include(x => x.Tehsil)
                                   .Include(x => x.Zone)
                                   .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                    && (string.IsNullOrEmpty(model.tehsil) || x.Tehsil.Name.Contains(model.tehsil))
                                    && (string.IsNullOrEmpty(model.district) || x.District.Name.Contains(model.district)))
                                   .OrderBy(a => a.Tehsil.Name)
                                   .GetPaged<Newlandvillage>(model.PageNumber, model.PageSize);
                        break;
                    case ("DISTRICT"):
                        data = null;
                        data = await _dbContext.Newlandvillage
                                   .Include(x => x.District)
                                   .Include(x => x.Tehsil)
                                   .Include(x => x.Zone)
                                   .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                    && (string.IsNullOrEmpty(model.tehsil) || x.Tehsil.Name.Contains(model.tehsil))
                                    && (string.IsNullOrEmpty(model.district) || x.District.Name.Contains(model.district)))
                                   .OrderBy(a => a.District.Name)
                                   .GetPaged<Newlandvillage>(model.PageNumber, model.PageSize);
                                   
                        break;


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Newlandvillage
                                   .Include(x => x.District)
                                   .Include(x => x.Tehsil)
                                   .Include(x => x.Zone)
                                   .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                    && (string.IsNullOrEmpty(model.tehsil) || x.Tehsil.Name.Contains(model.tehsil))
                                    && (string.IsNullOrEmpty(model.district) || x.District.Name.Contains(model.district)))
                                   .OrderByDescending(a => a.IsActive)
                                   .GetPaged<Newlandvillage>(model.PageNumber, model.PageSize);
                        break;


                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("CODE"):
                        data = null;
                        data = await _dbContext.Newlandvillage
                                   .Include(x => x.District)
                                   .Include(x => x.Tehsil)
                                   .Include(x => x.Zone)
                                   .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                    && (string.IsNullOrEmpty(model.tehsil) || x.Tehsil.Name.Contains(model.tehsil))
                                    && (string.IsNullOrEmpty(model.district) || x.District.Name.Contains(model.district)))
                                   .OrderByDescending(a => a.Code)
                                   .GetPaged<Newlandvillage>(model.PageNumber, model.PageSize);
                        break;
                    case ("NAME"):
                        data = null;
                        data = await _dbContext.Newlandvillage
                                   .Include(x => x.District)
                                   .Include(x => x.Tehsil)
                                   .Include(x => x.Zone)
                                   .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                    && (string.IsNullOrEmpty(model.tehsil) || x.Tehsil.Name.Contains(model.tehsil))
                                    && (string.IsNullOrEmpty(model.district) || x.District.Name.Contains(model.district)))
                                   .OrderByDescending(a => a.Name)
                                   .GetPaged<Newlandvillage>(model.PageNumber, model.PageSize);
                        break;

                    case ("TEHSIL"):
                        data = null;
                        data = await _dbContext.Newlandvillage
                                   .Include(x => x.District)
                                   .Include(x => x.Tehsil)
                                   .Include(x => x.Zone)
                                   .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                    && (string.IsNullOrEmpty(model.tehsil) || x.Tehsil.Name.Contains(model.tehsil))
                                    && (string.IsNullOrEmpty(model.district) || x.District.Name.Contains(model.district)))
                                   .OrderByDescending(a => a.Tehsil.Name)
                                   .GetPaged<Newlandvillage>(model.PageNumber, model.PageSize);
                        break;
                    case ("DISTRICT"):
                        data = null;
                        data = await _dbContext.Newlandvillage
                                   .Include(x => x.District)
                                   .Include(x => x.Tehsil)
                                   .Include(x => x.Zone)
                                   .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                    && (string.IsNullOrEmpty(model.tehsil) || x.Tehsil.Name.Contains(model.tehsil))
                                    && (string.IsNullOrEmpty(model.district) || x.District.Name.Contains(model.district)))
                                   .OrderByDescending(a => a.District.Name)
                                   .GetPaged<Newlandvillage>(model.PageNumber, model.PageSize);
                        break;


                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Newlandvillage
                                   .Include(x => x.District)
                                   .Include(x => x.Tehsil)
                                   .Include(x => x.Zone)
                                   .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                                    && (string.IsNullOrEmpty(model.tehsil) || x.Tehsil.Name.Contains(model.tehsil))
                                    && (string.IsNullOrEmpty(model.district) || x.District.Name.Contains(model.district)))
                                   .OrderBy(a => a.IsActive)
                                   .GetPaged<Newlandvillage>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            return data;
        }

        public async Task<PagedResult<Newlandvillage>> GetPagedNewLandVillageReport(NewlandVillageReportSearchDto model)
        {
            var data = await _dbContext.Newlandvillage


                  .Where(x => (x.IsActive == 1)
                                   && (x.Id == (model.Name == 0 ? x.Id : model.Name)))



                                   .OrderByDescending(x => x.Id)
                               .GetPaged<Newlandvillage>(model.PageNumber, model.PageSize);

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

        public async Task<PagedResult<Newlandvillage>> GetPagedNewlandAcquiredVillageReport(NewlandAcquiredVillageReportSearchDto model)
        {
            var data = await _dbContext.Newlandvillage

                 .Where(x => (x.IsActive == 1)
                   &&(x.Acquired == "yes")
                                    && (x.Id == (model.Name == 0 ? x.Id : model.Name)))



                                    .OrderByDescending(x => x.Id)
                                .GetPaged<Newlandvillage>(model.PageNumber, model.PageSize);

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

        public async Task<List<Newlandvillage>> GetAllVillageList()
        {
            List<Newlandvillage> villageList = await _dbContext.Newlandvillage.Where(x => x.IsActive == 1).ToListAsync();
            return villageList;
        }


    }
}

