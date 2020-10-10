using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Repository.Common;

namespace Libraries.Service.IApplicationService
{
    public interface IOnlinecomplaintService
    {


        Task<List<Onlinecomplaint>> GetAllOnlinecomplaint();
        Task<List<ComplaintType>> GetAllComplaintType();

        Task<List<Location>> GetAllLocation();
        Task<PagedResult<Onlinecomplaint>> GetPagedOnlinecomplaint(OnlinecomplaintSearchDto model);
        Task<List<Onlinecomplaint>> GetOnlinecomplaintUsingRepo();
        Task<bool> Update(int id, Onlinecomplaint onlinecomplaint);
        Task<bool> Create(Onlinecomplaint onlinecomplaint);
        Task<Onlinecomplaint> FetchSingleResult(int id);
        Task<bool> Delete(int id);
        //  Task<bool> CheckUniqueLoginName(int id, string loginname);

      


    }
}
