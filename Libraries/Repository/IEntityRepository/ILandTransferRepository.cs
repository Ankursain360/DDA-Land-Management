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
        Task<PagedResult<Landtransfer>> GetPagedCurrentStatusLandtransfer(LandTransferSearchDto model);

        Task<PagedResult<Landtransfer>> GetPagedLandtransferReportDeptWise(LandTransferSearchDto model);
        Task<List<Locality>> GetAllLocalityList(int divisionId);
        Task<List<Landtransfer>> GetHistoryDetails(string khasraNo);
        Task<List<Landtransfer>> GetAllLandTransfer(int propertyRegistrationId);
        Task<List<Landtransfer>> GetLandTransferReportData(int department, int zone, int division, int primaryListNo);


        //Task<List<Landtransfer>> GetLandTransferReportDepartmentwise(int handedover);

        Task<List<Landtransfer>> GetLandTransferReportDataDepartmentWise(int reportType, int departmentId);//added by ishu
        Task<List<Landtransfer>> GetLandTransferReportDataKhasraNumberWise(int id);
        Task<List<Landtransfer>> GetAllLandTransferList();
        Task<List<Landtransfer>> GetLandTransferReportdataHandover(int id); 
        Task<PagedResult<Landtransfer>> GetPagedLandtransferReportData(LandTransferSearchDto model);//added by shalini
        Task<Landtransfer> FetchSingleResultWithPropertyRegistration(int id);

        //Current status of land history methods:
        Task<bool> SaveCurrentstatusoflandhistory(Currentstatusoflandhistory model);

        Task<List<Currentstatusoflandhistory>> GetCurrentstatusoflandhistory(int landtransferId);
        Task<PagedResult<Propertyregistration>> GetPropertyRegisterationDataForLandTransfer(LandTransferSearchDto model);
        Task<PagedResult<Propertyregistration>> GetPropertyRegisterationUnverifiedDataForLandTransfer(LandTransferSearchDto model);
        Task<bool> CreateHistory(PropertyRegistrationHistory propertyRegistrationHistory);
        Task<List<Propertyregistration>> GetAllUnverifiedTransferRecordList();
        Task<List<Propertyregistration>> GetAllHandOverTakeOverList();

    }
}
