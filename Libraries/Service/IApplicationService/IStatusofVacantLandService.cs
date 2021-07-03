using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Repository.Common;
namespace Libraries.Service.IApplicationService
{
    public interface IStatusofVacantLandService
    {
       // Task<PagedResult<Vacantlandimage>> GetPagedStatusOfVacantLandReportData(StatusOfVacantLandSearchDto model);//added by ishu
         Task<List<StatusOfVacantLandListDataDto>> GetStatusOfVacantLandReportData(StatusOfVacantLandSearchDto model);
    }
}
