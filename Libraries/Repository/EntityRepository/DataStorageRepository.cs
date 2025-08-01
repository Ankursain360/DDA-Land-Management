﻿using System;
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


        public async Task<List<Schemefileloading>> GetSchemesFileLoading()
        {
            var schemeList = await _dbContext.Schemefileloading.Where(x => x.IsActive == 1).ToListAsync();
            return schemeList;
        }


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
        public async Task<List<Department>> GetDepartment(int? roleId, int? userDepartmentId)
        {
            if (roleId == 1)
            {
                var departmentList = await _dbContext.Department.Where(x => x.IsActive == 1).ToListAsync();
                return departmentList;
            }
            else
            {
                var departmentList = await _dbContext.Department.Where(x => x.IsActive == 1 && x.Id == userDepartmentId).ToListAsync();
                return departmentList;
            }
        }
        public async Task<List<Department>> GetDepartments()
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


        public async Task<List<Scheme>> GetSchemes()
        {
            var schemeList = await _dbContext.Scheme.Where(x => x.IsActive == 1).ToListAsync();
            return schemeList;
        }

        public async Task<List<Datastoragedetails>> GetDataStorageDetails()
        {
            return await _dbContext.Datastoragedetails
                                   .Include(x => x.Almirah)
                                   .Include(x => x.Row)
                                   .Include(x => x.Column)
                                   .Include(x => x.Bundle)
                                   .ToListAsync();
        }

        public async Task<List<Datastoragedetails>> GetAllDisplayLabelList(DisplayLabelSearchDto model)
        { 
            var data = await _dbContext.Datastoragedetails
                                .Include(x => x.Almirah)
                                .Include(x => x.Row)
                                .Include(x => x.Column)
                                .Include(x => x.Bundle)
                                 .Where(x => (string.IsNullOrEmpty(model.fileNo) || x.FileNo.Contains(model.fileNo))
                                 && (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name))).ToListAsync();
            return data;
        }
        public async Task<List<Datastoragedetails>> GetAllDataStorageDetailsList(DataStorgaeDetailsSearchDto model) 
        {
            var data = await _dbContext.Datastoragedetails
                                        .Include(x => x.Almirah)
                                        .Include(x => x.Row)
                                        .Include(x => x.Column)
                                        .Include(x => x.Bundle)
                                        .Where(x => x.FileNo == (model.FileNo == "" ? x.FileNo : model.FileNo)
                                        && (x.Name == (model.Name == "" ? x.Name : model.Name))).ToListAsync();
            return data;
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
                int SortOrder = (int)fileStatusReportSearchDto.SortOrder;
                var data = await _dbContext.LoadStoredProcedure("FileStatus")
                                            .WithSqlParams(("P_departmentId", fileStatusReportSearchDto.Department),
                                            ("P_UserId", UserId)
                                            , ("P_From_Date", fileStatusReportSearchDto.FromDate)
                                            , ("P_To_Date", fileStatusReportSearchDto.ToDate)
                                            , ("P_SortOrder", SortOrder)
                                            , ("P_SortBy", fileStatusReportSearchDto.SortBy))


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
        public async Task<List<ListofTotalDocReportListDataDto>> GetPagedListofReportDoc(ListOfTotalDocReportUserWiseSearchDto model, int UserId)
        {

            int SortOrder = (int)model.SortOrder;
            var data = await _dbContext.LoadStoredProcedure("BindListofTotalDocUserWiseReport")
                                             .WithSqlParams(("UserId", UserId),
                                             ("FreeHoldStatus", model.name),
                                             ("P_SortOrder", SortOrder),
                                             ("P_SortBy", model.SortBy),
                                              ("Search_Value", model.searchText),
                                              ("Search_Column", model.searchCol))
                                             .ExecuteStoredProcedureAsync<ListofTotalDocReportListDataDto>();
            return (List<ListofTotalDocReportListDataDto>)data;


        }
        public async Task<List<SearchByParticularListDataDto>> GetPagedListofSearchByParticular(SearchByParticularSearchDto model, int UserId)
        {
            int SortOrder = (int)model.SortOrder;
            var data = await _dbContext.LoadStoredProcedure("BindFileListOfSearchByParameter")
                                             .WithSqlParams(("UserId", UserId),
                                             ("FreeHoldStatus", model.name),
                                             ("P_SortOrder", SortOrder),
                                             ("P_SortBy", model.SortBy),
                                              ("DeptId", model.DeptId),
                                              ("LocalityId", model.LocalityId),
                                              ("AlmirahId", model.AlmirahId),
                                              ("RowId", model.RowId),
                                              ("BundleId", model.BundleId),
                                              ("ColId", model.ColId),
                                              ("RecordRoomId", model.RRNo),
                                              ("FileNo", model.FileNo),
                                              ("FileName", model.FileName)
                                              )
                                             .ExecuteStoredProcedureAsync<SearchByParticularListDataDto>();
            return (List<SearchByParticularListDataDto>)data;

        }
        public async Task<List<SearchByParticularFileHistoryListDataDto>> GetPagedListofFileHistory(SearchByParticularFileHistorySearchDto model)
        {

            int SortOrder = (int)model.SortOrder;
            var data = await _dbContext.LoadStoredProcedure("BindFileIssueReturnHistory")
                                             .WithSqlParams(("FileNo", model.FileNo))
                                             .ExecuteStoredProcedureAsync<SearchByParticularFileHistoryListDataDto>();
            return (List<SearchByParticularFileHistoryListDataDto>)data;


        }
        public async Task<List<SearchByParticularDocListDataDto>> GetPagedListofSearchByParticularDoc(SearchByParticularDocSearchDto model, int UserId)
        {
            int SortOrder = (int)model.SortOrder;
            var data = await _dbContext.LoadStoredProcedure("BindSearchByParameterDoc")
                                             .WithSqlParams(("UserId", UserId),
                                             ("FreeHoldStatus", model.name),
                                             ("P_SortOrder", SortOrder),
                                             ("P_SortBy", model.SortBy),
                                              ("DeptId", model.DeptId),
                                              ("LocalityId", model.LocalityId),
                                              ("AlmirahId", model.AlmirahId),
                                              ("RowId", model.RowId),
                                              ("BundleId", model.BundleId),
                                              ("ColId", model.ColId),
                                              ("RecordRoomId", model.RRNo),
                                              ("FileNo", model.FileNo),
                                              ("FileName", model.FileName)
                                              )
                                             .ExecuteStoredProcedureAsync<SearchByParticularDocListDataDto>();
            return (List<SearchByParticularDocListDataDto>)data;

        }
        public async Task<List<SearchByParticularDocHistoryListDataDto>> GetPagedListofDocHistory(SearchByParticularDocHistorySearchDto model)
        {

            int SortOrder = (int)model.SortOrder;
            var data = await _dbContext.LoadStoredProcedure("BindDocIssueReturnHistory")
                                             .WithSqlParams(("FileNo", model.FileNo))
                                             .ExecuteStoredProcedureAsync<SearchByParticularDocHistoryListDataDto>();
            return (List<SearchByParticularDocHistoryListDataDto>)data;


        }
        public async Task<Datastoragedetails> GetDatastorageListName(int id)
            {
            return await _dbContext.Datastoragedetails
                                    .Include(x=>x.Locality)
                                    .Include(x=>x.Zone)
                                    .Where(x => x.Id == id)
                                    .SingleOrDefaultAsync();

            }
       

        public int? GetDepartmentIdFromProfile(int userId)
        {
            var File = (from a in _dbContext.Userprofile
                        where a.IsActive == 1 && a.User.Id == userId
                        select a.DepartmentId).FirstOrDefault();

            return File;
        }
        // **************DISPLAY LABEL *********


       

       

        public async Task<Datastoragedetails> FetchPrintLabel(int id)
        {
            return await _dbContext.Datastoragedetails
                                   .Include(x => x.Almirah)
                                   .Include(x => x.Row)
                                   .Include(x => x.Column)
                                   .Include(x => x.Bundle)
                                   .Where(x => x.Id == id)
                                   .FirstOrDefaultAsync();
        }

        public async Task<PagedResult<Datastoragedetails>> GetPagedDisplayLabel(DisplayLabelSearchDto model)
        {
            var data = await _dbContext.Datastoragedetails
                                .Include(x => x.Almirah)
                                .Include(x => x.Row)
                                .Include(x => x.Column)
                                .Include(x => x.Bundle)
                                 .Where(x => (string.IsNullOrEmpty(model.fileNo) || x.FileNo.Contains(model.fileNo))
                                 && (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))
                                 // && (string.IsNullOrEmpty(model.almirah) || x.Almirah.AlmirahNo.Contains(model.almirah)))
                                // && (Convert.ToString(model.almirah) || x.Almirah.AlmirahNo.Contains(model.almirah)))
                                //&& (string.IsNullOrEmpty(model.row) || x.Name.Contains(model.row))
                                //&& (string.IsNullOrEmpty(model.column) || x.Name.Contains(model.bundle))
                                //&& (string.IsNullOrEmpty(model.bundle) || x.Name.Contains(model.bundle)))
                                //.Where(x => (x.Id == (model.FileNo == 0 ? x.Id : model.FileNo)))
                                .GetPaged(model.PageNumber, model.PageSize);

            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                switch (model.SortBy.ToUpper())
                {

                    case ("FILENO"):
                        data = null;
                        data = await _dbContext.Datastoragedetails
                                     .Include(x => x.Almirah)
                                     .Include(x => x.Row)
                                     .Include(x => x.Column)
                                     .Include(x => x.Bundle)
                                     .Where(x => (string.IsNullOrEmpty(model.fileNo) || x.FileNo.Contains(model.fileNo))
                                      && (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))
                                     .OrderBy(x => x.FileNo)
                                     .GetPaged(model.PageNumber, model.PageSize);
                        break;
                    case ("FILENAME"):
                        data = null;
                        data = await _dbContext.Datastoragedetails
                                     .Include(x => x.Almirah)
                                     .Include(x => x.Row)
                                     .Include(x => x.Column)
                                     .Include(x => x.Bundle)
                                     .Where(x => (string.IsNullOrEmpty(model.fileNo) || x.FileNo.Contains(model.fileNo))
                                      && (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))
                                     .OrderBy(x => x.Name)
                                     .GetPaged(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Datastoragedetails
                                     .Include(x => x.Almirah)
                                     .Include(x => x.Row)
                                     .Include(x => x.Column)
                                     .Include(x => x.Bundle)
                                     .Where(x => (string.IsNullOrEmpty(model.fileNo) || x.FileNo.Contains(model.fileNo))
                                      && (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))
                                     .OrderByDescending(x => x.FileStatus)
                                     .GetPaged(model.PageNumber, model.PageSize);
                        break;


                }
            }
            else if (SortOrder == 2)
            {
                switch (model.SortBy.ToUpper())
                {

                    case ("FILENO"):
                        data = null;
                        data = await _dbContext.Datastoragedetails
                                     .Include(x => x.Almirah)
                                     .Include(x => x.Row)
                                     .Include(x => x.Column)
                                     .Include(x => x.Bundle)
                                     .Where(x => (string.IsNullOrEmpty(model.fileNo) || x.FileNo.Contains(model.fileNo))
                                      && (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))
                                     .OrderByDescending(x => x.FileNo)
                                     .GetPaged(model.PageNumber, model.PageSize);
                        break;
                    case ("FILENAME"):
                        data = null;
                        data = await _dbContext.Datastoragedetails
                                     .Include(x => x.Almirah)
                                     .Include(x => x.Row)
                                     .Include(x => x.Column)
                                     .Include(x => x.Bundle)
                                     .Where(x => (string.IsNullOrEmpty(model.fileNo) || x.FileNo.Contains(model.fileNo))
                                      && (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))
                                     .OrderByDescending(x => x.Name)
                                     .GetPaged(model.PageNumber, model.PageSize);
                        break;
                    case ("STATUS"):
                        data = null;
                        data = await _dbContext.Datastoragedetails
                                     .Include(x => x.Almirah)
                                     .Include(x => x.Row)
                                     .Include(x => x.Column)
                                     .Include(x => x.Bundle)
                                    .Where(x => (string.IsNullOrEmpty(model.fileNo) || x.FileNo.Contains(model.fileNo))
                                      && (string.IsNullOrEmpty(model.name) || x.Name.Contains(model.name)))
                                    .OrderBy(x => x.FileStatus)
                                    .GetPaged(model.PageNumber, model.PageSize);
                        break;


                }
            }
            return data;
        }

        public async Task<bool> DeleteDataStoragePartFile(int Id)
        {
            _dbContext.RemoveRange(_dbContext.Datastoragepartfilenodetails.Where(x => x.DataStorageDetailsId == Id));
            var Result = await _dbContext.SaveChangesAsync();
            return Result > 0 ? true : false;
        }

        public async Task<List<Datastoragepartfilenodetails>> GetDetailsOfPartFileDetails(int DataStorageID)
        {
            return await _dbContext.Datastoragepartfilenodetails.Where(x => x.DataStorageDetailsId == DataStorageID).ToListAsync();
        }




        public async Task<PagedResult<Datastoragedetails>> GetPagedDataStorageDetails(DataStorgaeDetailsSearchDto model)
        {
            var data = await _dbContext.Datastoragedetails
                                        .Include(x => x.Almirah)
                                        .Include(x => x.Row)
                                        .Include(x => x.Column)
                                        .Include(x => x.Bundle)
                                        .Where(x => x.FileNo == (model.FileNo == "" ? x.FileNo : model.FileNo)
                                        && (x.Name == (model.Name == "" ? x.Name : model.Name))
                                        ).GetPaged<Datastoragedetails>(model.PageNumber, model.PageSize);
            int SortOrder = (int)model.SortOrder;
            if (SortOrder == 1)
            {
                data = null;
                data = await _dbContext.Datastoragedetails
                                        .Include(x => x.Almirah)
                                        .Include(x => x.Row)
                                           .Include(x => x.Column)
                                        .Include(x => x.Bundle)
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
                                         .Include(x => x.Column)
                                        .Include(x => x.Bundle)
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


    }
}

