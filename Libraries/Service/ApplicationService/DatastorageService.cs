using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Service.Common;
using Libraries.Service.IApplicationService;
using Libraries.Repository.IEntityRepository;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using Libraries.Model;
using Dto.Search;

namespace Libraries.Service.ApplicationService
{
    public class DatastorageService : EntityService<Datastoragedetails>, IDataStorageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDataStorageRepository _datastoragedetailRepository;
        protected readonly DataContext _dbContext;


        public DatastorageService(IUnitOfWork unitOfWork, IDataStorageRepository datastoragedetailRepository, DataContext dbContext)
       : base(unitOfWork, datastoragedetailRepository)
        {
            _unitOfWork = unitOfWork;
            _datastoragedetailRepository = datastoragedetailRepository;
            _dbContext = dbContext;
        }

        public async Task<List<Datastoragedetails>> GetAllDataStorageDetail()
        {
            return await _datastoragedetailRepository.GetAll();
        }

        public async Task<List<Datastoragedetails>> GetDataStorageDetailsUsingReport()
        {
            return await _datastoragedetailRepository.GetDataStorageDetails();
        }

        public async Task<bool> Update(int id, Datastoragedetails dataStorageDetails)
        {
            var result = await _datastoragedetailRepository.FindBy(a => a.Id == id);
            Datastoragedetails model = result.FirstOrDefault();
            //model.AlmirahNo = almirah.AlmirahNo;
            //model.ModifiedDate = DateTime.Now;
            //model.IsActive = almirah.IsActive;
            model.ModifiedBy = 1;
            _datastoragedetailRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public bool CheckUniqueName(int id, Datastoragedetails dataStoragedetails)
        {
            var result = _dbContext.Datastoragedetails.Any(t => t.Id != id && t.AlmirahId == dataStoragedetails.AlmirahId);
            return result;
        }

        public async Task<Datastoragedetails> FetchSingleResult(int id)
        {
            var result = await _datastoragedetailRepository.FindBy(a => a.Id == id);
            Datastoragedetails model = result.FirstOrDefault();
            return model;
        }

        public async Task<bool> Create(Datastoragedetails dataStorageDetails)
        {

            dataStorageDetails.CreatedBy = 1;
            dataStorageDetails.CreatedDate = DateTime.Now;
            _datastoragedetailRepository.Add(dataStorageDetails);
            return await _unitOfWork.CommitAsync() > 0;
        }


        public async Task<bool> SaveDetailsOfPartFile(List<Datastoragepartfilenodetails> datastoragepartfilenodetails)
        {

            return await _datastoragedetailRepository.SaveDetailsOfPartFile(datastoragepartfilenodetails);
        }


        public async Task<bool> CheckUniqueName(int id, string almirah)
        {
            bool result = await _datastoragedetailRepository.Any(id, almirah);

            return result;
        }


        public async Task<bool> Delete(int id)
        {
            var form = await _datastoragedetailRepository.FindBy(a => a.Id == id);
            Datastoragedetails model = form.FirstOrDefault();
            model.IsActive = 0;
            _datastoragedetailRepository.Edit(model);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<PagedResult<Datastoragedetails>> GetPagedDataStorageDetails(DataStorgaeDetailsSearchDto model)
        {
            return await _datastoragedetailRepository.GetPagedDataStorageDetails(model);
        }
        //public async Task<PagedResult<Datastoragedetails>> GetFileStatusReportData(FileStatusReportSearchDto fileStatusReportSearchDto)
        //{
        //    return await _datastoragedetailRepository.GetFileStatusReportData(fileStatusReportSearchDto);
        //}
        public async Task<List<FileStatusReportListDataDto>> GetPagedFileStatusReportData(FileStatusReportSearchDto fileStatusReportSearchDto, int UserId)
        {
            return await _datastoragedetailRepository.GetPagedFileStatusReportData(fileStatusReportSearchDto, UserId);
        }

        public async Task<List<Almirah>> GetAlmirahs()
        {
            List<Almirah> almirhaList = await _datastoragedetailRepository.GetAlmirahs();
            return almirhaList;
        }

        public async Task<List<Row>> GetRows()
        {
            List<Row> rowList = await _datastoragedetailRepository.GetRows();
            return rowList;
        }

        public async Task<List<Column>> GetColumns()
        {
            List<Column> columnList = await _datastoragedetailRepository.GetColumns();
            return columnList;
        }

        public async Task<List<Department>> GetDepartments()
        {
            List<Department> departmentlist = await _datastoragedetailRepository.GetDepartments();
            return departmentlist;
        }
        public async Task<List<Bundle>> GetBundles()
        {
            List<Bundle> bundleList = await _datastoragedetailRepository.GetBundles();
            return bundleList;
        }

        public async Task<List<Locality>> GetLocalities()
        {
            List<Locality> localityList = await _datastoragedetailRepository.GetLocalities();
            return localityList;
        }
        public async Task<List<Department>> GetDepartment(int? roleId, int? userDepartmentId)
        {
            List<Department> departmentList = await _datastoragedetailRepository.GetDepartment(roleId, userDepartmentId);
            return departmentList;
        }
        public async Task<List<Branch>> GetBranch()
        {
            List<Branch> branchList = await _datastoragedetailRepository.GetBranch();
            return branchList;
        }

        public async Task<List<Zone>> GetZones()
        {
            List<Zone> zoneList = await _datastoragedetailRepository.GetZones();
            return zoneList;
        }
        public async Task<List<Schemefileloading>> GetSchemesFileLoading()
        {
            var schemeList = await _datastoragedetailRepository.GetSchemesFileLoading();
            return schemeList;
        }


        public async Task<List<Scheme>> GetSchemes()
        {
            List<Scheme> schemesList = await _datastoragedetailRepository.GetSchemes();
            return schemesList;
        }
        public async Task<List<ListofTotalFileReportListDataDto>> GetPagedListofReportFile(ListOfTotalFilesReportUserWiseSearchDto model, int UserId)
        {
            return await _datastoragedetailRepository.GetPagedListofReportFile(model, UserId);
        }

        public async Task<List<ListofTotalDocReportListDataDto>> GetPagedListofReportDoc(ListOfTotalDocReportUserWiseSearchDto model, int UserId)
        {
            return await _datastoragedetailRepository.GetPagedListofReportDoc(model, UserId);
        }

        public async Task<List<SearchByParticularListDataDto>> GetPagedListofSearchByParticular(SearchByParticularSearchDto model, int UserId)
        {
            return await _datastoragedetailRepository.GetPagedListofSearchByParticular(model, UserId);
        }

        public async Task<List<SearchByParticularFileHistoryListDataDto>> GetPagedListofFileHistory(SearchByParticularFileHistorySearchDto model)
        {
            return await _datastoragedetailRepository.GetPagedListofFileHistory(model);
        }
        public async Task<List<SearchByParticularDocListDataDto>> GetPagedListofSearchByParticularDoc(SearchByParticularDocSearchDto model, int UserId)
        {
            return await _datastoragedetailRepository.GetPagedListofSearchByParticularDoc(model, UserId);
        }
        public async Task<List<SearchByParticularDocHistoryListDataDto>> GetPagedListofDocHistory(SearchByParticularDocHistorySearchDto model)
        {
            return await _datastoragedetailRepository.GetPagedListofDocHistory(model);
        }
        public async Task<Datastoragedetails> GetDatastorageListName(int id)
        {
            Datastoragedetails model = await _datastoragedetailRepository.GetDatastorageListName(id);
            return model;
        }
        public int? GetDepartmentIdFromProfile(int userId)
        {
            return _datastoragedetailRepository.GetDepartmentIdFromProfile(userId);
        }
        public async Task<PagedResult<Datastoragedetails>> GetPagedDisplayLabel(DisplayLabelSearchDto model)
        {
            return await _datastoragedetailRepository.GetPagedDisplayLabel(model);

        }
        public async Task<Datastoragedetails> FetchPrintLabel(int id)
        {
            return await _datastoragedetailRepository.FetchPrintLabel(id);
        }

        public async Task<bool> DeleteDataStoragePartFile(int Id)
        {
            return await _datastoragedetailRepository.DeleteDataStoragePartFile(Id);
        }


        public async Task<List<Datastoragepartfilenodetails>> GetDetailsOfPartFileDetails(int Id)
        {
            return await _datastoragedetailRepository.GetDetailsOfPartFileDetails(Id);
        }
    }
}
