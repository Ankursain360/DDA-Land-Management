using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;


using Libraries.Model;
using Libraries.Model.Entity;
using Libraries.Repository.Common;
using Libraries.Repository.IEntityRepository;
using Microsoft.EntityFrameworkCore;


using System.Text;

using Repository.Common;

namespace Libraries.Repository.EntityRepository
{
    public class DataStorageRepository : GenericRepository<Datastoragedetails>, IDataStorageRepository
    {
        public DataStorageRepository(DataContext dbcontext) : base(dbcontext)
        { }

        public async Task<List<Almirah>> GetAlmirahs()
        {
            var almirahList = await _dbContext.Almirah.Where(x => x.IsActive == 1).ToListAsync();
            return almirahList;
        }

        public async Task<List<Row>> GetRows()
        {
            var rowList = await _dbContext.Row.Where(x => x.IsActive == 1).ToListAsync();
            return rowList;
        }

        public async Task<List<Column>> GetColumns()
        {
            var columnList = await _dbContext.Column.Where(x => x.IsActive == 1).ToListAsync();
            return columnList;
        }

        public async Task<List<Bundle>> GetBundles()
        {
            var bundleList = await _dbContext.Bundle.Where(x => x.IsActive == 1).ToListAsync();
            return bundleList;
        }

        public async Task<List<Locality>> GetLocalities()
        {
            var localityList = await _dbContext.Locality.Where(x => x.IsActive == 1).ToListAsync();
            return localityList;
        }
        public async Task<List<Department>> GetDepartment()
        {
            var departmentList = await _dbContext.Department.Where(x => x.IsActive == 1).ToListAsync();
            return departmentList;
        }
        public async Task<List<Branch>> GetBranch()
        {
            var branchList = await _dbContext.Branch.Where(x => x.IsActive == 1).ToListAsync();
            return branchList;
        }
        public async Task<List<Zone>> GetZones()
        {
            var zoneList = await _dbContext.Zone.Where(x => x.IsActive == 1).ToListAsync();
            return zoneList;
        }


        //public async Task<List<Scheme>> GetSchemes()
        //{
        //    var schemeList = await _dbContext.Scheme.Where(x => x.IsActive == 1).ToListAsync();
        //    return schemeList;
        //}


        public async Task<List<SchemeFileLoading>> GetSchemesFileLoading()
        {
            var schemeList = await _dbContext.SchemeFileLoading.Where(x => x.IsActive == 1).ToListAsync();
            return schemeList;
        }


        public async Task<List<Datastoragedetails>> GetDataStorageDetails()
        {
            return await _dbContext.Datastoragedetails.Where(x => x.IsActive == 1).ToListAsync();
        }

        public async Task<PagedResult<Datastoragedetails>> GetPagedDataStorageDetails(DataStorgaeDetailsSearchDto model)
        {
            var data = await _dbContext.Datastoragedetails
                                        .Include(x => x.Almirah)
                                        .Include(x => x.Row)
                                        .Where(x => x.FileNo == (model.FileNo =="" ? x.FileNo : model.FileNo)
                                        && (x.Name == (model.Name == "" ? x.Name : model.Name))                                       
                                        ).GetPaged<Datastoragedetails>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                data = null;
                data = await _dbContext.Datastoragedetails
                                        .Include(x => x.Almirah)
                                        .Include(x => x.Row)
                                        .Where(x => x.FileNo == (model.FileNo == "" ? x.FileNo : model.FileNo)
                                         && (x.Name == (model.Name == "" ? x.Name : model.Name))
                                        )
                                .OrderBy(s =>
                                (model.SortBy.ToUpper() == "FILENO" ? s.FileNo
                                : model.SortBy.ToUpper() == "NAME" ? (s.Name)
                                : model.SortBy.ToUpper() == "ALMIRAHNO" ? (s.Almirah != null ? s.Almirah.AlmirahNo : null)
                                : model.SortBy.ToUpper() == "ROWNO" ? (s.Row != null ? s.Row.RowNo : null) : s.FileNo)
                                )
                                .GetPaged<Datastoragedetails>(model.PageNumber, model.PageSize);
            }
            else if (SortOrder == 2)
            {
                  data = null;
                  data = await _dbContext.Datastoragedetails
                                     .Include(x => x.Almirah)
                                        .Include(x => x.Row)
                                        .Where(x => x.FileNo == (model.FileNo == "" ? x.FileNo : model.FileNo)
                                        && (x.Name == (model.Name == "" ? x.Name : model.Name))
                                        )
                                .OrderByDescending(s =>
                               (model.SortBy.ToUpper() == "FILENO" ? s.FileNo
                                : model.SortBy.ToUpper() == "NAME" ? (s.Name)
                                : model.SortBy.ToUpper() == "ALMIRAHNO" ? (s.Almirah != null ? s.Almirah.AlmirahNo : null)
                                : model.SortBy.ToUpper() == "ROWNO" ? (s.Row != null ? s.Row.RowNo : null) : s.FileNo)
                                )
                                .GetPaged<Datastoragedetails>(model.PageNumber, model.PageSize);
            }
       
            return data;
        }



        public async Task<List<Datastoragepartfilenodetails>> GetDetailsOfPartFileDetails(int DataStorageID)
        {
            return await _dbContext.Datastoragepartfilenodetails.Where(x => x.DataStorageDetailsId == DataStorageID).ToListAsync();
        }



        public async Task<bool> DeleteDataStoragePartFile(int Id)
        {
            _dbContext.RemoveRange(_dbContext.Datastoragepartfilenodetails.Where(x => x.DataStorageDetailsId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }
        public async Task<bool> Any(int id, string name)
        {
            return await _dbContext.Datastoragedetails.AnyAsync(t => t.Id != id && t.FileNo.ToLower() == name.ToLower());
        }

        public async Task<bool> SaveDetailsOfPartFile(List<Datastoragepartfilenodetails> datastoragepartfilenodetails)
        {
            await _dbContext.AddRangeAsync(datastoragepartfilenodetails);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<List<FileStatusReportListDataDto>> GetPagedFileStatusReportData(FileStatusReportSearchDto fileStatusReportSearchDto, int UserId)

        {
            try
            {

                var data = await _dbContext.LoadStoredProcedure("FileStatus")
                                            .WithSqlParams(("P_departmentId", fileStatusReportSearchDto.Department),
                                            ("UserId", UserId)
                                            , ("P_From_Date", fileStatusReportSearchDto.FromDate)
                                            , ("P_To_Date", fileStatusReportSearchDto.ToDate))


                                            .ExecuteStoredProcedureAsync<FileStatusReportListDataDto>();
                return (List<FileStatusReportListDataDto>)data;

            }
            catch (Exception ex)
            {
                throw;
            }
        }



        public async Task<List<ListofTotalFileReportListDataDto>> GetPagedListofReportFile(ListOfTotalFilesReportUserWiseSearchDto model, int UserId)
        {

            int SortOrder = (int)model.SortOrder;
            var data = await _dbContext.LoadStoredProcedure("BindListofTotalFilesUserWiseReport")
                                             .WithSqlParams(("UserId", UserId),
                                             ("FreeHoldStatus", model.name),
                                             ("P_SortOrder", SortOrder),
                                             ("P_SortBy", model.SortBy),
                                              ("Search_Value", model.searchText),
                                              ("Search_Column", model.searchCol))
                                             .ExecuteStoredProcedureAsync<ListofTotalFileReportListDataDto>();
            return (List<ListofTotalFileReportListDataDto>)data;


        }


        public async Task<PagedResult<Locality>> GetPagedLocality(LocalitySearchDto model)
        {
            var data = await _dbContext.Locality
                        .Include(x => x.Zone)
                        .Include(x => x.Department)
                        .Include(x => x.Division)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.localityCode) || x.LocalityCode.Contains(model.localityCode)))
                             .OrderByDescending(s => s.IsActive)
                            .ThenBy(s => s.Zone.Name)
                            .ThenBy(s => s.Division.Name)
                            .ThenBy(s => s.Name).GetPaged<Locality>(model.PageNumber, model.PageSize); ;

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {
                    case ("DEPARTMENT"):
                        data = null;
                        data = await _dbContext.Locality
                        .Include(x => x.Zone)
                        .Include(x => x.Department)
                        .Include(x => x.Division)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.localityCode) || x.LocalityCode.Contains(model.localityCode)))
                            .OrderBy(x => x.Department.Name)
                            .GetPaged<Locality>(model.PageNumber, model.PageSize); ;
                        break;
                    case ("ZONE"):
                        data = null;
                        data = await _dbContext.Locality
                        .Include(x => x.Zone)
                        .Include(x => x.Department)
                        .Include(x => x.Division)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.localityCode) || x.LocalityCode.Contains(model.localityCode)))
                            .OrderBy(x => x.Zone.Name)
                            .GetPaged<Locality>(model.PageNumber, model.PageSize); ;

                        break;
                    case ("DIVISION"):
                        data = null;
                        data = await _dbContext.Locality
                        .Include(x => x.Zone)
                        .Include(x => x.Department)
                        .Include(x => x.Division)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.localityCode) || x.LocalityCode.Contains(model.localityCode)))
                            .OrderBy(x => x.Division.Name)
                            .GetPaged<Locality>(model.PageNumber, model.PageSize); ;

                        break;
                    case ("LOCALITY"):
                        data = null;
                        data = await _dbContext.Locality
                        .Include(x => x.Zone)
                        .Include(x => x.Department)
                        .Include(x => x.Division)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.localityCode) || x.LocalityCode.Contains(model.localityCode)))
                            .OrderBy(x => x.Name)
                            .GetPaged<Locality>(model.PageNumber, model.PageSize); ;


                        break;
                    case ("LOCALITYCODE"):
                        data = null;
                        data = await _dbContext.Locality
                        .Include(x => x.Zone)
                        .Include(x => x.Department)
                        .Include(x => x.Division)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.localityCode) || x.LocalityCode.Contains(model.localityCode)))
                            .OrderBy(x => x.LocalityCode)
                            .GetPaged<Locality>(model.PageNumber, model.PageSize); ;
                        break;
                    case ("ISACTIVE"):
                        data = null;
                        data = await _dbContext.Locality
                        .Include(x => x.Zone)
                        .Include(x => x.Department)
                        .Include(x => x.Division)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.localityCode) || x.LocalityCode.Contains(model.localityCode)))
                            .OrderByDescending(x => x.IsActive)
                            .GetPaged<Locality>(model.PageNumber, model.PageSize); ;

                        break;
                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {

                    case ("DEPARTMENT"):
                        data = null;
                        data = await _dbContext.Locality
                        .Include(x => x.Zone)
                        .Include(x => x.Department)
                        .Include(x => x.Division)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.localityCode) || x.LocalityCode.Contains(model.localityCode)))
                            .OrderByDescending(x => x.Department.Name)
                            .GetPaged<Locality>(model.PageNumber, model.PageSize); ;
                        break;
                    case ("ZONE"):
                        data = null;
                        data = await _dbContext.Locality
                        .Include(x => x.Zone)
                        .Include(x => x.Department)
                        .Include(x => x.Division)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.localityCode) || x.LocalityCode.Contains(model.localityCode)))
                            .OrderByDescending(x => x.Zone.Name)
                            .GetPaged<Locality>(model.PageNumber, model.PageSize); ;

                        break;
                    case ("DIVISION"):
                        data = null;
                        data = await _dbContext.Locality
                        .Include(x => x.Zone)
                        .Include(x => x.Department)
                        .Include(x => x.Division)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.localityCode) || x.LocalityCode.Contains(model.localityCode)))
                            .OrderByDescending(x => x.Division.Name)
                            .GetPaged<Locality>(model.PageNumber, model.PageSize); ;

                        break;
                    case ("LOCALITY"):
                        data = null;
                        data = await _dbContext.Locality
                        .Include(x => x.Zone)
                        .Include(x => x.Department)
                        .Include(x => x.Division)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.localityCode) || x.LocalityCode.Contains(model.localityCode)))
                            .OrderByDescending(x => x.Name)
                            .GetPaged<Locality>(model.PageNumber, model.PageSize); ;


                        break;
                    case ("LOCALITYCODE"):
                        data = null;
                        data = await _dbContext.Locality
                        .Include(x => x.Zone)
                        .Include(x => x.Department)
                        .Include(x => x.Division)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.localityCode) || x.LocalityCode.Contains(model.localityCode)))
                            .OrderByDescending(x => x.LocalityCode)
                            .GetPaged<Locality>(model.PageNumber, model.PageSize); ;
                        break;
                    case ("ISACTIVE"):
                        data = null;
                        data = await _dbContext.Locality
                        .Include(x => x.Zone)
                        .Include(x => x.Department)
                        .Include(x => x.Division)
                            .Where(x => (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))
                             && (string.IsNullOrEmpty(model.localityCode) || x.LocalityCode.Contains(model.localityCode)))
                            .OrderBy(x => x.IsActive)
                            .GetPaged<Locality>(model.PageNumber, model.PageSize); ;

                        break;
                }
            }
            return data;
        }

    }
}
