using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{

    public interface ICalculationSheetService : IEntityService<Allotmententry>
    {
   
        Task<List<Allotmententry>> GetAllApplications();
        Task<Allotmententry> FetchSingleAppAreaDetails(int? ApplicationId);
    }
}
