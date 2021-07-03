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
    public class StatusofVacantLandRepository : GenericRepository<Vacantlandimage>, IStatusofVacantLandRepository
    {
        public StatusofVacantLandRepository(DataContext dbContext) : base(dbContext)
        {

        }




        //public async Task<PagedResult<Vacantlandimage>> GetPagedStatusOfVacantLandReportData(StatusOfVacantLandSearchDto model)
        //{
        //     var data = await _dbContext.Vacantlandimage
        //                                .Include(x => x.Department)
        //                                .GroupBy(x => x.Department)

        //                                .GetPaged<Vacantlandimage>(model.PageNumber, model.PageSize);

        //    int SortOrder = (int)model.SortOrder;
        //    if (SortOrder == 1)
        //    {
        //        switch (model.SortBy.ToUpper())
        //        {
        //            case ("DEPARTMENT"):
        //                data = null;
        //                data = await _dbContext.Vacantlandimage
        //                           .Include(x => x.Department)
        //                           .OrderBy(a => a.Department)
        //                           .GetPaged<Vacantlandimage>(model.PageNumber, model.PageSize);

        //                break;

        //        }
        //    }
        //    else if (SortOrder == 2)
        //    {
        //        switch (model.SortBy.ToUpper())
        //        {
        //            case ("DEPARTMENT"):
        //                data = null;
        //                data = await _dbContext.Vacantlandimage
        //                           .Include(x => x.Department)
        //                           .OrderByDescending(a => a.Department)
        //                           .GetPaged<Vacantlandimage>(model.PageNumber, model.PageSize);

        //                break;

        //        }
        //    }
        //    return data;
        //}
        public async Task<List<StatusOfVacantLandListDataDto>> GetStatusOfVacantLandReportData(StatusOfVacantLandSearchDto model)

        {

            int SortOrder = (int)model.SortOrder;
            var data = await _dbContext.LoadStoredProcedure("StatusOfVacantLand")
                                        .WithSqlParams(
                                         ("P_SortOrder", SortOrder),
                                         ("P_SortBy", model.SortBy))
                                        .ExecuteStoredProcedureAsync<StatusOfVacantLandListDataDto>();
            return (List<StatusOfVacantLandListDataDto>)data;

        }
    }
}
