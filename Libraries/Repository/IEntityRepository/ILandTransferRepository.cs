using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libraries.Repository.IEntityRepository
{
    public interface ILandTransferRepository : IGenericRepository<Landtransfer>
    {
        Task<List<Landtransfer>> GetAllLandtransfer();
        Task<List<Zone>> GetAllZone(int departmentId);
        Task<List<Division>> GetAllDivision(int zoneId);
        Task<List<Department>> GetAllDepartment();
        Task<PagedResult<Landtransfer>> GetPagedLandtransfer(LandTransferSearchDto model);
        Task<List<Locality>> GetAllLocalityList(int divisionId);
        Task<List<Landtransfer>> GetHistoryDetails(string khasraNo);
        Task<List<Landtransfer>> GetAllLandTransfer();
        Task<List<Landtransfer>> GetLandTransferReportData(int department, int zone, int division, int primaryListNo);
        //Task<List<Landtransfer>> GetLandTransferReportDepartmentwise(int handedover);
        Task<List<Landtransfer>> GetLandTransferReportDataDepartmentWise(int reportType, int departmentId);//added by ishu
        Task<List<Landtransfer>> GetLandTransferReportDataKhasraNumberWise(int id);
        Task<List<Landtransfer>> GetAllLandTransferList();
        Task<List<Landtransfer>> GetLandTransferReportdataHandover(int id);
    }
}
