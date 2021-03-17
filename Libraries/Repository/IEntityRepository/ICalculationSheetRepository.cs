using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface ICalculationSheetRepository : IGenericRepository<Allotmententry>
   
    {
        Task<List<Allotmententry>> GetAllApplications();
        
        Task<Allotmententry> FetchSingleAppAreaDetails(int? ApplicationId);
    }
}
