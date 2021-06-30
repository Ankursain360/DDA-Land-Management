using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Libraries.Repository.EntityRepository
{
    public class AllotmentLetterRepository : GenericRepository<Allotmentletter>, IAllotmentLetterRepository
    {

        public AllotmentLetterRepository(DataContext dbContext) : base(dbContext)
        {

        }
        public string GetDownload(int id)
        {
            var File = (from f in _dbContext.Allotmentletter
                        where f.Id == id
                        select f.FilePath).First();

            return File;
        }
        public async Task<List<Allotmententry>> GetRefNoListforAllotmentLetter()
        {
            return await _dbContext.Allotmententry
                        .Include(x => x.Application)
                       .Where(x => (x.IsActive == 1))
                .OrderByDescending(x => x.Application.RefNo).ToListAsync();
        }


        public async Task<PagedResult<Allotmentletter>> GetPagedAllotmentLetter(AllotmentLetterSeearchDto model)
        {
            var data = await _dbContext.Allotmentletter
                        .GetPaged<Allotmentletter>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("APPNO"):
                        data = null;
                        data = await _dbContext.Allotmentletter
                            .Include(s => s.Allotment)
                            .Include(s => s.Allotment.LeasesType)
                            .Include(s => s.Allotment.Application)
                            .Where(x => (string.IsNullOrEmpty(model.AppRefNo) || x.Allotment.Application.RefNo.Contains(model.AppRefNo))
                            // && (model.GenerateDate==null || x.DemandDate==(model.GenerateDate))
                            )
                            .OrderBy(x => x.Allotment.Application.RefNo)
                            .GetPaged<Allotmentletter>(model.PageNumber, model.PageSize);
                        break;


                    case ("DATE"):
                        data = null;
                        data = await _dbContext.Allotmentletter
                            .Include(s => s.Allotment)
                            .Include(s => s.Allotment.LeasesType)
                            .Include(s => s.Allotment.Application)
                            .Where(x => (string.IsNullOrEmpty(model.AppRefNo) || x.Allotment.Application.RefNo.Contains(model.AppRefNo))
                            // && (model.GenerateDate == null || x.DemandDate == (model.GenerateDate)))
                           ) .OrderBy(x => x.DemandDate)
                            .GetPaged<Allotmentletter>(model.PageNumber, model.PageSize);
                        break;
                    case ("UPLOAD"):
                        data = null;
                        data = await _dbContext.Allotmentletter
                            .Include(s => s.Allotment)
                            .Include(s => s.Allotment.LeasesType)
                            .Include(s => s.Allotment.Application)
                            .Where(x => (string.IsNullOrEmpty(model.AppRefNo) || x.Allotment.Application.RefNo.Contains(model.AppRefNo))
                          ) //)  && (model.GenerateDate == null || x.DemandDate == (model.GenerateDate)))
                            .OrderBy(x => x.FilePath == null)
                            .GetPaged<Allotmentletter>(model.PageNumber, model.PageSize);
                        break;
                    case ("GENERATE"):
                        data = null;
                        data = await _dbContext.Allotmentletter
                            .Include(s => s.Allotment)
                            .Include(s => s.Allotment.LeasesType)
                            .Include(s => s.Allotment.Application)
                            .Where(x => (string.IsNullOrEmpty(model.AppRefNo) || x.Allotment.Application.RefNo.Contains(model.AppRefNo))
                           )//  && (model.GenerateDate == null || x.DemandDate == (model.GenerateDate)))
                            .OrderBy(x => x.FilePath != null)
                            .GetPaged<Allotmentletter>(model.PageNumber, model.PageSize);
                        break;


                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("APPNO"):
                        data = null;
                        data = await _dbContext.Allotmentletter
                            .Include(s => s.Allotment)
                            .Include(s => s.Allotment.LeasesType)
                            .Include(s => s.Allotment.Application)
                            .Where(x => (string.IsNullOrEmpty(model.AppRefNo) || x.Allotment.Application.RefNo.Contains(model.AppRefNo))
                           //  && (model.GenerateDate == null || x.DemandDate == (model.GenerateDate)))
                            ).OrderByDescending(x => x.Allotment.Application.RefNo)
                            .GetPaged<Allotmentletter>(model.PageNumber, model.PageSize);
                        break;


                    case ("DATE"):
                        data = null;
                        data = await _dbContext.Allotmentletter
                            .Include(s => s.Allotment)
                            .Include(s => s.Allotment.LeasesType)
                            .Include(s => s.Allotment.Application)
                            .Where(x => (string.IsNullOrEmpty(model.AppRefNo) || x.Allotment.Application.RefNo.Contains(model.AppRefNo))
                            )// && (model.GenerateDate == null || x.DemandDate == (model.GenerateDate)))
                            .OrderByDescending(x => x.DemandDate)
                            .GetPaged<Allotmentletter>(model.PageNumber, model.PageSize);
                        break;
                    case ("UPLOAD"):
                        data = null;
                        data = await _dbContext.Allotmentletter
                            .Include(s => s.Allotment)
                            .Include(s => s.Allotment.LeasesType)
                            .Include(s => s.Allotment.Application)
                            .Where(x => (string.IsNullOrEmpty(model.AppRefNo) || x.Allotment.Application.RefNo.Contains(model.AppRefNo))
                           )//  && (model.GenerateDate == null || x.DemandDate == (model.GenerateDate)))
                            .OrderByDescending(x => x.FilePath == null)
                            .GetPaged<Allotmentletter>(model.PageNumber, model.PageSize);
                        break;
                    case ("GENERATE"):
                        data = null;
                        data = await _dbContext.Allotmentletter
                            .Include(s => s.Allotment)
                            .Include(s => s.Allotment.LeasesType)
                            .Include(s => s.Allotment.Application)
                            .Where(x => (string.IsNullOrEmpty(model.AppRefNo) || x.Allotment.Application.RefNo.Contains(model.AppRefNo))
                           )//  && (model.GenerateDate == null || x.DemandDate == (model.GenerateDate)))
                            .OrderByDescending(x => x.FilePath !=null)
                            .GetPaged<Allotmentletter>(model.PageNumber, model.PageSize);
                        break;

                }
            }
            return data;
        }

        public async Task<Allotmentletter> FetchSingleAllotmentLetterDetails(int id)
        {
            var data = await _dbContext.Allotmentletter
                                        .Include(x => x.Allotment)
                                        .Where(x => x.Id == id)// && x.AllotmentId==x.Allotment.Id)
                                        .FirstOrDefaultAsync();
            return data;
        }
        public async Task<Allotmentletter> FetchAllotmentLetterDetails(int id)
        {
            var data = await _dbContext.Allotmentletter
                                        .Include(x => x.Allotment)
                                        .Where(x => x.AllotmentId == id)// && x.AllotmentId==x.Allotment.Id)
                                        .FirstOrDefaultAsync();
            return data;
        }
    }


}

