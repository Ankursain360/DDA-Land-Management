using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Service.IApplicationService
{
    public interface ILandTransferService
    {
        Task<List<Zone>> GetAllZone(int departmentId); // To Get all data added by Praveen
        Task<List<Department>> GetAllDepartment(); // To Get all data added by Praveen
        Task<List<LandTransfer>> GetLandTransferUsingRepo();
        Task<bool> Update(int id, LandTransfer landTransfer); // To Upadte Particular data added by Praveen
        Task<bool> Create(LandTransfer landTransfer);
        Task<LandTransfer> FetchSingleResult(int id);  // To fetch Particular data added by Praveen
        Task<bool> Delete(int id);    // To Delete Data  added by Praveen
        Task<PagedResult<LandTransfer>> GetPagedLandTransfer(LandTransferSearchDto model);
        Task<List<Division>> GetAllDivisionList(int zone);
    }
}
