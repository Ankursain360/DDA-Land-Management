using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;


namespace Libraries.Repository.IEntityRepository
{
    public interface IOnlinecomplaintRepository : IGenericRepository<Onlinecomplaint>
    {
        Task<List<Onlinecomplaint>> GetAllOnlinecomplaint();
        Task<List<ComplaintType>> GetAllComplaintType();
     
        Task<List<Location>> GetAllLocation();
        Task<PagedResult<Onlinecomplaint>> GetPagedOnlinecomplaint(OnlinecomplaintSearchDto model);

    }
}
