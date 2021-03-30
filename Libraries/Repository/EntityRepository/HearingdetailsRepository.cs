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

    public class HearingdetailsRepository : GenericRepository<Hearingdetails>, IHearingdetailsRepository
    {
        public HearingdetailsRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public async Task<bool> DeleteHphotofiledetails(int Id)
        {
            _dbContext.RemoveRange(_dbContext.Hearingdetailsphotofiledetails.Where(x => x.HearingDetailsId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<Hearingdetailsphotofiledetails> GetHphotofiledetails(int hid)
        {
            return await _dbContext.Hearingdetailsphotofiledetails.Where(x => x.Id == hid && x.IsActive == 1).FirstOrDefaultAsync();
        }
        public async Task<bool> SaveHearingphotofiledetails(Hearingdetailsphotofiledetails hearingdetailsphotofiledetails)
        {
            _dbContext.Hearingdetailsphotofiledetails.Add(hearingdetailsphotofiledetails);
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<List<Requestforproceeding>> GetAllRequestforproceeding()
        {
            return await _dbContext.Requestforproceeding.Include(x => x.Allotment)
                //.Where(x=> x.Id == Allotmententry.Id)
                .OrderByDescending(x => x.Id).ToListAsync();
        }


        //public async Task<List<Leasenoticegeneration>> BindAllotmentDetails(int? AllotmentId)
        //{
        //    return await _dbContext.Allotmententry.Where(x => x.Id == AllotmentId).ToListAsync();
        //}
        public async Task<List<Leasenoticegeneration>> GetAllLeasenoticegeneration(int? AppId)
        {
            return await _dbContext.Leasenoticegeneration.Where(x => x.RequestProceedingId == AppId).ToListAsync();

        }

        public async Task<PagedResult<Hearingdetails>> GetPagedHearingDetails(HearingdetailsSeachDto model)
        {

            var data = await _dbContext.Hearingdetails
                                      //.Include(x => x.ReqProc)
                                      //.Include(x=>x.NoticeGen)
                                      //.Include(x => x.EvidanceDoc)
                                          .OrderByDescending(x => x.Id)
                          .GetPaged<Hearingdetails>(model.PageNumber, model.PageSize);
            //int SortOrder = (int)model.SortOrder;
            //if (SortOrder == 1)
            //{
            //    switch (model.SortBy.ToUpper())
            //    {
            //        case ("ALLOTMENTNUMBER"):
            //            data = null;
            //            data = await _dbContext.Possesionplan
            //                            .Include(x => x.Allotment)
            //                           .Include(x => x.Allotment.Application)
            //                   //        .Where(x => string.IsNullOrEmpty(model.AllotmentId) || (x.Allotment.ApplicationId == Convert.ToInt32(model.AllotmentId)))
            //                   .Where(x => string.IsNullOrEmpty(model.AllotmentId) || x.Allotment.Application.RefNo.Contains(model.AllotmentId))
            //                    .OrderBy(x => x.Allotment.Application.RefNo)
            //                    .GetPaged<Possesionplan>(model.PageNumber, model.PageSize);
            //            break;
            //        case ("STATUS"):
            //            data = null;
            //            data = await _dbContext.Possesionplan
            //                            .Include(x => x.Allotment)
            //                            .Include(x => x.Allotment.Application)
            //                      .Where(x => string.IsNullOrEmpty(model.AllotmentId) || x.Allotment.Application.RefNo.Contains(model.AllotmentId))

            //                    .OrderByDescending(x => x.IsActive)
            //                    .GetPaged<Possesionplan>(model.PageNumber, model.PageSize);
            //            break;
            //    }
            //}
            //else if (SortOrder == 2)
            //{
            //    switch (model.SortBy.ToUpper())
            //    {
            //        case ("ALLOTMENTNUMBER"):
            //            data = null;
            //            data = await _dbContext.Possesionplan
            //                            .Include(x => x.Allotment)
            //                             .Include(x => x.Allotment.Application)
            //                    //          .Where(x => string.IsNullOrEmpty(model.AllotmentId) || (x.Allotment.ApplicationId == Convert.ToInt32(model.AllotmentId)))
            //                    .Where(x => string.IsNullOrEmpty(model.AllotmentId) || x.Allotment.Application.RefNo.Contains(model.AllotmentId))
            //                    .OrderByDescending(x => x.Allotment.Application.RefNo)
            //                    .GetPaged<Possesionplan>(model.PageNumber, model.PageSize);
            //            break;
            //        case ("STATUS"):
            //            data = null;
            //            data = await _dbContext.Possesionplan
            //                            .Include(x => x.Allotment)
            //                             .Include(x => x.Allotment.Application)
            //                             .Where(x => string.IsNullOrEmpty(model.AllotmentId) || (x.Allotment.ApplicationId == Convert.ToInt32(model.AllotmentId)))
            //                     .Where(x => string.IsNullOrEmpty(model.AllotmentId) || x.Allotment.Application.RefNo.Contains(model.AllotmentId))

            //                    .OrderBy(x => x.IsActive)
            //                    .GetPaged<Possesionplan>(model.PageNumber, model.PageSize);
            //            break;
            //    }
            //}
            return data;
        }



    }
}
