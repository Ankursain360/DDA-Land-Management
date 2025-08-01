﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Utility.Helper;

namespace FileDataLoading.Controllers
{

    public class IssueReturnFileController : BaseController
    {
        private readonly IIssueReturnFileService _issueReturnFileService;
        private readonly IDataStorageService _datastorageService;
        public IssueReturnFileController(IIssueReturnFileService issueReturnFileService, IDataStorageService datastorageService)
        {
            _issueReturnFileService = issueReturnFileService;
            _datastorageService = datastorageService;
        }

        async Task BindDropDownView(Datastoragedetails model)
        {

            model.FileNoList = await _issueReturnFileService.GetFileNoList();
        }
        public async Task<IActionResult> Index()
        {
            Datastoragedetails model = new Datastoragedetails();
            await BindDropDownView(model);
            return View(model);
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] IssueReturnFileSearchDto model)
        {
            var result = await _issueReturnFileService.GetPagedIssueReturnFile(model);
            if (result != null)
            {
                return PartialView("_List", result);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return PartialView();
            }
        }


        public async Task<IActionResult> IssueFileData(int id)
        {
            Issuereturnfile model = new Issuereturnfile();
            var Data = await _datastorageService.FetchSingleResult(id);
            model.IssuedDate = DateTime.Now;
            model.DepartmentList = await _issueReturnFileService.GetAllDepartment();
            model.BranchList = await _issueReturnFileService.GetAllBranch();
            model.DesignationList = await _issueReturnFileService.GetAllDesignation();
            Data.AlmirahList = await _datastorageService.GetAlmirahs();
            Data.RowList = await _datastorageService.GetRows();
            Data.ColumnList = await _datastorageService.GetColumns();
            Data.BundleList = await _datastorageService.GetBundles();
            model.DataStorageDetails = Data;
            return View(model);
        }



        public async Task<IActionResult> IssueReceipt(int id)
        {
            
           var Data = await _issueReturnFileService.FetchSingleReceiptResult(id);
            var datastorageid = Data.DataStorageDetailsId;
            var data2 = await _datastorageService.FetchSingleResult(datastorageid);
            Data.DataStorageDetails = data2;
            if (Data == null)
            {
                return NotFound();
            }
            return PartialView(Data);

        }

        public async Task<IActionResult> ReturnReceipt(int id)
        {
            var Data = await _issueReturnFileService.FetchReturnReceiptResult(id);
            var Data2 = await _datastorageService.FetchSingleResult(id);

            Data.DataStorageDetails = Data2;
            if (Data == null)
            {
                return NotFound();
            }
            return PartialView(Data);

        }

        [HttpPost]
        public async Task<IActionResult> IssueFileData(int id, Issuereturnfile issuereturnfile)
        {
            var Data = await _datastorageService.FetchSingleResult(id);
            issuereturnfile.DepartmentList = await _issueReturnFileService.GetAllDepartment();
            issuereturnfile.BranchList = await _issueReturnFileService.GetAllBranch();
            issuereturnfile.DesignationList = await _issueReturnFileService.GetAllDesignation();
            issuereturnfile.Id = 0;
            Data.AlmirahList = await _datastorageService.GetAlmirahs();
            Data.RowList = await _datastorageService.GetRows();
            Data.ColumnList = await _datastorageService.GetColumns();
            Data.BundleList = await _datastorageService.GetBundles();
            
            issuereturnfile.CreatedBy = SiteContext.UserId;
            issuereturnfile.DataStorageDetails = Data;
           
            if (issuereturnfile.DataStorageDetailsId == 0)
            {
                return NotFound();
            }

            {

                issuereturnfile.DataStorageDetailsId = issuereturnfile.DataStorageDetails.Id;
                var result = await _issueReturnFileService.Create(issuereturnfile);
                if (result == true)
                {
                    var result1 = await _issueReturnFileService.UpdateIssueFileStatus(id);
                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                    return  RedirectToAction("IssueReceipt", "IssueReturnFile", new { id = issuereturnfile.Id });
                  

                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(issuereturnfile);
                }

            }

        }

        public async Task<IActionResult> ReturnFileData(int id)
        {
            Issuereturnfile model = await _issueReturnFileService.FetchfiletResult(id);
            var Data = await _datastorageService.FetchSingleResult(id);
            model.DepartmentList = await _issueReturnFileService.GetAllDepartment();
            model.BranchList = await _issueReturnFileService.GetAllBranch();
            model.DesignationList = await _issueReturnFileService.GetAllDesignation();
           
            Data.AlmirahList = await _datastorageService.GetAlmirahs();
            Data.RowList = await _datastorageService.GetRows();
            Data.ColumnList = await _datastorageService.GetColumns();
            Data.BundleList = await _datastorageService.GetBundles();
            model.DataStorageDetails = Data;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ReturnFileData(int id, Issuereturnfile issuereturnfile)
        {
            var Data = await _datastorageService.FetchSingleResult(id);
            Data.AlmirahList = await _datastorageService.GetAlmirahs();
            Data.RowList = await _datastorageService.GetRows();
            Data.ColumnList = await _datastorageService.GetColumns();
            Data.BundleList = await _datastorageService.GetBundles();
            issuereturnfile.DepartmentList = await _issueReturnFileService.GetAllDepartment();
            issuereturnfile.BranchList = await _issueReturnFileService.GetAllBranch();
            issuereturnfile.DesignationList = await _issueReturnFileService.GetAllDesignation();
            issuereturnfile.DataStorageDetails = Data;
            issuereturnfile.ModifiedBy = SiteContext.UserId;
           
            var result = await _issueReturnFileService.Update(id, issuereturnfile);
               
                if (result == true)
                {
                    var result1 = await _issueReturnFileService.UpdateReturnFileStatus(id);

                    ViewBag.Message =  Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                     return RedirectToAction("ReturnReceipt", "IssueReturnFile", new { id = issuereturnfile.Id });
               
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(issuereturnfile);
                }

           
        }

        // [AuthorizeContext(ViewAction.Download)]

        public async Task<IActionResult> IssuereturnfileList([FromBody] IssueReturnFileSearchDto model)
        {
            var result = await _issueReturnFileService.GetAllIssueReturnFileList(model);
            List<IssuereturnfileListDto> data = new List<IssuereturnfileListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new IssuereturnfileListDto()
                    {
                        Id = result[i].Id,
                        FileNo = result[i].FileNo,
                        FileName = result[i].Name,
                        RecordRoomNo = result[i].RecordRoomNo,
                        AlNoCompactorNo = result[i].Almirah == null ? "" : result[i].Almirah.AlmirahNo,
                        RowNo = result[i].Row == null ? "" : result[i].Row.RowNo,
                        ColNo = result[i].Column == null ? "" : result[i].Column.ColumnNo,
                        BnNo = result[i].Bundle == null ? "" : result[i].Bundle.BundleNo,
                        Status = result[i].FileStatus == "Issued" ? "Not Available" : "Available",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            TempData["file"] = memory;
            return Ok();

        }

        [HttpGet]
        public virtual ActionResult download()
        {
            byte[] data = TempData["file"] as byte[];
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}
