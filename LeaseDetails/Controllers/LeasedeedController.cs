
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enum;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using LeaseDetails.Filters;
using Utility.Helper;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace LeaseDetails.Controllers
{

    public class LeasedeedController : BaseController
    {
        private readonly ILeasedeedService _leasedeedService;
        public IConfiguration _configuration;
        public LeasedeedController(ILeasedeedService leasedeedService, IConfiguration configuration)
        {
            _leasedeedService = leasedeedService;
            _configuration = configuration;
        }


        [HttpGet]
        public async Task<IActionResult> ApplicationDetails(int? Id)
        {
            Allotmententry entry = await _leasedeedService.FetchSingleDetails(Id ?? 0);

            return PartialView("_ApplicationDetails", entry);
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] LeasedeedSearchDto model)
        {

            var result = await _leasedeedService.GetPagedLeasedeed(model);
            return PartialView("_List", result);
        }
        public async Task<IActionResult> Create()
        {
            Leasedeed deed = new Leasedeed();

            deed.ApplicationList = await _leasedeedService.GetAllApplications();
            return View(deed);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Leasedeed deed)
        {
            deed.ApplicationList = await _leasedeedService.GetAllApplications();
            string Document = _configuration.GetSection("FilePaths:Leasedeed:Doc").Value.ToString();
            FileHelper fileHelper = new FileHelper();
            if (ModelState.IsValid)
            {
                if (deed.File != null)
                {
                    deed.DocumentPath = fileHelper.SaveFile(Document, deed.File);
                }
                deed.CreatedBy = SiteContext.UserId;
                deed.IsActive = 1;
                var result = await _leasedeedService.Create(deed);

                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);

                    var list = await _leasedeedService.GetAllLeasedeed();
                    return View("Index", list);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(deed);

                }
            }
            else
            {
                return View(deed);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _leasedeedService.FetchSingleResult(id);
            Data.ApplicationList = await _leasedeedService.GetAllApplications();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        //  [ValidateAntiForgeryToken]
        //  [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Leasedeed deed)
        {
            deed.ApplicationList = await _leasedeedService.GetAllApplications();
            string Document = _configuration.GetSection("FilePaths:Leasedeed:Doc").Value.ToString();
            FileHelper fileHelper = new FileHelper();
            if (ModelState.IsValid)
            {
                if (deed.File != null)
                {
                    deed.DocumentPath = deed.File != null ? fileHelper.SaveFile1(Document, deed.File) :
                        deed.File != null || deed.DocumentPath != "" ? deed.DocumentPath : string.Empty;
                    //deed.DocumentPath = fileHelper.SaveFile(Document, deed.File);
                }


               
                deed.ModifiedBy = SiteContext.UserId;
                    var result = await _leasedeedService.Update(id, deed);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);

                        var list = await _leasedeedService.GetAllLeasedeed();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(deed);

                    }
                
               
            }
            return View(deed);
        }

        //  [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _leasedeedService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                var result1 = await _leasedeedService.GetAllLeasedeed();
                return View("Index", result1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                var result1 = await _leasedeedService.GetAllLeasedeed();
                return View("Index", result1);
            }
        }

        //  [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _leasedeedService.FetchSingleResult(id);
            Data.ApplicationList = await _leasedeedService.GetAllApplications();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        //  [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> Download()
        {
            List<Leasedeed> result = await _leasedeedService.GetAllLeasedeed();
            var memory = ExcelHelper.CreateExcel(result);
            string sFileName = @"Leasedeed.xlsx";
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

        }

        public async Task<IActionResult> DownloadFile(int Id)
        {
            FileHelper file = new FileHelper();
            Leasedeed Data = await _leasedeedService.FetchSingleResult(Id);
            string filename = Data.DocumentPath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
    }
}
