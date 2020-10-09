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
    public interface ILandTransferService
    {
        Task<List<Zone>> GetAllZone(int departmentId); // To Get all data added by Praveen
        Task<List<Department>> GetAllDepartment(); // To Get all data added by Praveen
        Task<List<Landtransfer>> GetLandTransferUsingRepo();
        Task<bool> Update(int id, Landtransfer Landtransfer); // To Upadte Particular data added by Praveen
        Task<bool> Create(Landtransfer Landtransfer);
        Task<Landtransfer> FetchSingleResult(int id);  // To fetch Particular data added by Praveen
        Task<bool> Delete(int id);    // To Delete Data  added by Praveen
        Task<PagedResult<Landtransfer>> GetPagedLandTransfer(LandTransferSearchDto model);
        Task<List<Division>> GetAllDivisionList(int zone);
        Task<List<Landtransfer>> GetAllLandTransfer();
        Task<List<Locality>> GetAllLocalityList(int divisionId);
        Task<List<Landtransfer>> GetHistoryDetails(string khasraNo);
        Task<List<Landtransfer>> GetLandTransferReportData(int department, int zone, int division, int primaryListNo);// added by shalini

        Task<List<Landtransfer>> GetLandTransferReportDepartmentwise(int handedover);
    }
}
