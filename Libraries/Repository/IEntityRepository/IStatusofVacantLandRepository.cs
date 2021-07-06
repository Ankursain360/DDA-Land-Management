using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;

namespace Libraries.Repository.IEntityRepository
{
    public interface IStatusofVacantLandRepository : IGenericRepository<Vacantlandimage>
    {
        //Task<PagedResult<Vacantlandimage>> GetPagedStatusOfVacantLandReportData(StatusOfVacantLandSearchDto model);//added by ishu
        Task<List<StatusOfVacantLandListDataDto>> GetStatusOfVacantLandReportData(StatusOfVacantLandSearchDto model);
    }
}
