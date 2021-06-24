using System;
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
using Repository.IEntityRepository;

namespace Repository.EntityRepository
{

    public class PossesionplanRepository : GenericRepository<Possesionplan>, IPossesionplanRepository
    {
        public PossesionplanRepository(DataContext dbContext) : base(dbContext)
        {

        }

      
        public async Task<List<Allotmententry>> GetAllAllotmententry()
        {
            List<Allotmententry> allotmententryList = await _dbContext.Allotmententry.Include(x => x.Application).Where(x => (x.ApplicationId== x.Application.Id && x.IsActive == 1)).ToListAsync();
            return allotmententryList;
        }
        public async Task<List<Possesionplan>> GetAllPossesionplan()
        {
            return await _dbContext.Possesionplan.Include(x => x.Allotment).Include(x => x.Allotment.Application)
                //.Where(x=> x.Id == Allotmententry.Id)
                .OrderByDescending(x => x.Id).ToListAsync();
        }


        public async Task<List<Leaseapplication>> GetAllLeaseApplication()
        {
            List<Leaseapplication> leaseapplicationList = await _dbContext.Leaseapplication.Include(x => x.Allotmententry)
                .Where(x => x.IsActive == 1)
                .ToListAsync();
            return leaseapplicationList;
            //List<Leaseapplication> leaseapplicationList = await _dbContext.Leaseapplication.Include(x => x.Allotmententry)
            //                                                .Where(x => (x.Id == x.Allotmententry.App && x.IsActive == 1)).ToListAsync();
            //return leaseapplicationList;
        }



        public async Task<List<Allotmententry>> BindAllotmentDetails(int? AllotmentId)
        {
            return await _dbContext.Allotmententry.Include(x=>x.Application).Where(x => x.Id == AllotmentId).ToListAsync();
        }
        public async Task<List<Leaseapplication>> BindLeaseApplicationDetails(int? AppId)
        {
            return await _dbContext.Leaseapplication.Where(x => x.Id == AppId).ToListAsync();
            
        }

        public async Task<PagedResult<Possesionplan>> GetPagedPossesionPlan(PossesionplanSearchDto model)
        {

            var data = await _dbContext.Possesionplan
                                      .Include(x => x.Allotment)
                                      .Include(x=>x.Allotment.Application)
                                          .OrderByDescending(x => x.Id)
                          .GetPaged<Possesionplan>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("ALLOTMENTNUMBER"):
                        data = null;
                        data = await _dbContext.Possesionplan
                                        .Include(x => x.Allotment)
                                       .Include(x => x.Allotment.Application)
                               //        .Where(x => string.IsNullOrEmpty(model.AllotmentId) || (x.Allotment.ApplicationId == Convert.ToInt32(model.AllotmentId)))
                               .Where(x => string.IsNullOrEmpty(model.AllotmentId) || x.Allotment.Application.RefNo.Contains(model.AllotmentId))
                                .OrderBy(x => x.Allotment.Application.RefNo)
                                .GetPaged<Possesionplan>(model.PageNumber, model.PageSize);
                        break;
                     case ("STATUS"):
                        data = null;
                        data = await _dbContext.Possesionplan
                                        .Include(x => x.Allotment)
                                        .Include(x => x.Allotment.Application)
                                  .Where(x => string.IsNullOrEmpty(model.AllotmentId) || x.Allotment.Application.RefNo.Contains(model.AllotmentId))

                                .OrderByDescending(x => x.IsActive)
                                .GetPaged<Possesionplan>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("ALLOTMENTNUMBER"):
                        data = null;
                        data = await _dbContext.Possesionplan
                                        .Include(x => x.Allotment)
                                         .Include(x => x.Allotment.Application)
                                //          .Where(x => string.IsNullOrEmpty(model.AllotmentId) || (x.Allotment.ApplicationId == Convert.ToInt32(model.AllotmentId)))
                                .Where(x => string.IsNullOrEmpty(model.AllotmentId) || x.Allotment.Application.RefNo.Contains(model.AllotmentId))
                                .OrderByDescending(x => x.Allotment.Application.RefNo)
                                .GetPaged<Possesionplan>(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Possesionplan
                                        .Include(x => x.Allotment)
                                         .Include(x => x.Allotment.Application)
                                         .Where(x => string.IsNullOrEmpty(model.AllotmentId) || (x.Allotment.ApplicationId == Convert.ToInt32(model.AllotmentId)))
                                 .Where(x => string.IsNullOrEmpty(model.AllotmentId) || x.Allotment.Application.RefNo.Contains(model.AllotmentId))

                                .OrderBy(x => x.IsActive)
                                .GetPaged<Possesionplan>(model.PageNumber, model.PageSize);
                        break;
                }
            }
            return data;
        }

        public Task<Khasra> FetchSingleKhasraResult(int? khasraId)
        {
            throw new NotImplementedException();
        }



        public string GetDownload(int id)
        {
            var File = (from f in _dbContext.Possesionplan
                        where f.Id == id
                        select f.SitePlanFilePath).First();

            return File;
        }

    }
}
