using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
//using FileDataLoading.Models;

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
        //[HttpPost]
        //public JsonResult GetAutocmplete(string Prefix)
        //{

        //    //var Countries = (from c in _context.TblMasterDesignation
        //    //                 where c.DesignationName.StartsWith(Prefix)
        //    //                 select new { c.DesignationName, c.DesignationId });
        //    //return Json(Countries);
        //}
        //[HttpPost]
        //public IActionResult Index(int id)
        //{
        //    ViewBag.IsShowData = "Yes";
        //    return View();
        //}

        //public async Task<IActionResult> AutocompleteParameter(string term)
        //{
        //    return Json(_context.TblMasterDesignation.Where(x => x.DesignationName.Equals(term)).ToList());
        //}

        public async Task<IActionResult>  IssueFile()
        {
            return View();
        }

        //public IActionResult IssueFileData()
        //{
        //    return View();
        //}
        public async Task<IActionResult> IssueFileData( int id)
        {
            Issuereturnfile model = new Issuereturnfile();
            var Data = await _datastorageService.FetchSingleResult(id);
          
            model.DepartmentList = await _issueReturnFileService.GetAllDepartment();
            model.BranchList = await _issueReturnFileService.GetAllBranch();
            model.DesignationList = await _issueReturnFileService.GetAllDesignation();
            //   model.ZoneList = await _localityService.GetAllZone(model.DepartmentId);
            Data.AlmirahList = await _datastorageService.GetAlmirahs();
            Data.RowList = await _datastorageService.GetRows();
            Data.ColumnList = await _datastorageService.GetColumns();
            Data.BundleList = await _datastorageService.GetBundles();
            model.DataStorageDetails = Data;
            return View(model);
        }

        //[HttpPost]
        //public async Task<IActionResult> IssueFileData(int id = 0)
        //{
        //    if (id == 0)
        //        return View();
        //    else
        //    {
               
        //        return View();
        //    }
        //}
        public IActionResult IssueReceipt()
        {
            return PartialView("IssueReceipt");
        }
        public IActionResult ReturnReceipt()
        {
            return PartialView("ReturnReceipt");
        }

        [HttpPost]
        public async Task<IActionResult> IssueFileData(int id, Issuereturnfile issuereturnfile)
        {
            var Data = await _datastorageService.FetchSingleResult(id);
            issuereturnfile.CreatedBy = SiteContext.UserId;
            issuereturnfile.DataStorageDetails = Data;
            issuereturnfile.Id = 0;
            if (issuereturnfile.DataStorageDetailsId == 0)
            {
                return NotFound();
            }
            //var errors = ModelState.Values.SelectMany(x => x.Errors);
            //ModelState.Remove(null);
            //if (ModelState.IsValid)
            {

                issuereturnfile.DataStorageDetailsId = issuereturnfile.DataStorageDetails.Id;
                var result = await _issueReturnFileService.Create(issuereturnfile);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                    return View("Index");
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(issuereturnfile);
                }
            }
        }

    }
}
