using System.Linq;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Dto.Search;
using Microsoft.Extensions.Configuration;
using Utility.Helper;
using Microsoft.AspNetCore.Http;
using System.IO;
using LeaseDetails.Filters;
using Core.Enum;
using Dto.Master;
using System;

namespace LeaseDetails.Controllers
{
    public class RestorationEntryController : BaseController
    {
        private readonly ICancellationEntryService _cancellationEntryService;
        private readonly IRestorationEntryService _restorationEntryService;
        public IConfiguration _configuration;
        string DemandletterFilePath = string.Empty;
        string NOCFilePath = string.Empty;
        string CancellationOrderFilePath = string.Empty;

        string targetPathDemandLetter = "";
        string targetPathNOC = "";
        string targetPathCanellationOrder = "";

        public RestorationEntryController(ICancellationEntryService cancellationEntryService,
            IConfiguration configuration, IRestorationEntryService restorationEntryService)
        {
            _configuration = configuration;
            _cancellationEntryService = cancellationEntryService;
            _restorationEntryService = restorationEntryService;
            targetPathDemandLetter = _configuration.GetSection("FilePaths:CancellationEntry:DemandletterFilePath").Value.ToString();
            targetPathNOC = _configuration.GetSection("FilePaths:CancellationEntry:NOCFilePath").Value.ToString();
            targetPathCanellationOrder = _configuration.GetSection("FilePaths:CancellationEntry:CancellationOrderFilePath").Value.ToString();
        }
        //  [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();


        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] CancellationEntrySearchDto model)
        {
            var result = await _cancellationEntryService.GetPagedCancellationEntry(model);
            return PartialView("_List", result);
        }

        // [AuthorizeContext(ViewAction.View)]
        public async Task<PartialViewResult> GetCancellationdetails(int id)
        {
            var Data = await _cancellationEntryService.FetchSingleResult(id);
            Data.HonbleList = await _cancellationEntryService.GetAllHonble();
            Data.AllotmententryList = await _cancellationEntryService.GetAllAllotment();
            return PartialView("_cancellationdetails", Data);
            //return View(Data);
        }

        //  [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            Restorationentry _obj = new Restorationentry();
            var Data = await _cancellationEntryService.FetchSingleResult(id);
            _obj.AllotmentId = Data.AllotmentId;
            _obj.Cancellationid = Data.Id;
            return View(_obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
      //  [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Restorationentry restorationentry)
        {

            try
            {
                restorationentry.CreatedBy = SiteContext.UserId;
                var result = await _restorationEntryService.Create(restorationentry);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success); 
                    return View("Index");
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(restorationentry);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(restorationentry);
            } 
        }
    }
}
