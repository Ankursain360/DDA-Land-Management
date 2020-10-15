using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Libraries.Service.IApplicationService;
using Libraries.Model.Entity;
//using AutoMapper.Configuration;
using Utility.Helper;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.Extensions.Configuration;
using Dto.Search;

namespace EncroachmentDemolition.Controllers
{
    public class WatchWardController : BaseController
    {
        private readonly IWatchandwardService _watchandwardService;
        public IConfiguration _configuration;
        string targetPhotoPathLayout = string.Empty;
        string targetReportfilePathLayout = string.Empty;

        public WatchWardController(IWatchandwardService watchandwardService, IConfiguration configuration)
        {
            _watchandwardService = watchandwardService;
            _configuration = configuration;
        }
       
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] WatchandwardSearchDto model)
        {
            var result = await _watchandwardService.GetPagedWatchandward(model);
            return PartialView("_List", result);
        }

        public async Task<IActionResult> Create()
        {
            Watchandward watchandward = new Watchandward();
            watchandward.IsActive = 1;
            watchandward.VillageList = await _watchandwardService.GetAllVillage();
            watchandward.KhasraList = await _watchandwardService.GetAllKhasra();
            return View(watchandward);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Watchandward watchandward)
        {
            watchandward.VillageList = await _watchandwardService.GetAllVillage();
            watchandward.KhasraList = await _watchandwardService.GetAllKhasra();
            if (ModelState.IsValid)
            {
                targetPhotoPathLayout = _configuration.GetSection("FilePaths:WatchAndWard:Photo").Value.ToString();
                targetReportfilePathLayout = _configuration.GetSection("FilePaths:WatchAndWard:ReportFile").Value.ToString();
                FileHelper file = new FileHelper();

                if (watchandward.Photo != null)
                {
                    watchandward.PhotoPath = file.SaveFile(targetPhotoPathLayout, watchandward.Photo);
                }
                if (watchandward.ReportFile !=null)
                {
                    watchandward.ReportFiletPath = file.SaveFile(targetReportfilePathLayout, watchandward.ReportFile);
                }
                var result = await _watchandwardService.Create(watchandward);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                    var result1 = await _watchandwardService.GetAllWatchandward();
                    return View("Index", result1);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(watchandward);
                }
            }

            return View(watchandward);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _watchandwardService.FetchSingleResult(id);
            Data.VillageList = await _watchandwardService.GetAllVillage();
            Data.KhasraList = await _watchandwardService.GetAllKhasra();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Watchandward watchandward)
        {
            var Data = await _watchandwardService.FetchSingleResult(watchandward.Id);
            Data.VillageList = await _watchandwardService.GetAllVillage();
            Data.KhasraList = await _watchandwardService.GetAllKhasra();
            if (ModelState.IsValid)
            {
                targetPhotoPathLayout = _configuration.GetSection("FilePaths:WatchAndWard:Photo").Value.ToString();
                targetReportfilePathLayout = _configuration.GetSection("FilePaths:WatchAndWard:ReportFile").Value.ToString();

                if (watchandward.Photo != null)
                {
                    FileHelper file = new FileHelper();
                    watchandward.PhotoPath = file.SaveFile(targetPhotoPathLayout, watchandward.Photo);
                }
                if (watchandward.ReportFile != null)
                {
                    FileHelper file = new FileHelper();
                    watchandward.ReportFiletPath = file.SaveFile(targetReportfilePathLayout, watchandward.ReportFile);
                }
                var result = await _watchandwardService.Update(id, watchandward);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                    var result1 = await _watchandwardService.GetAllWatchandward();
                    return View("Index", result1);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(watchandward);
                }
            }
            else
            {
                return View(watchandward);
            }
        }

        public async Task<IActionResult> View(int id)
        {
            var Data = await _watchandwardService.FetchSingleResult(id);
            Data.VillageList = await _watchandwardService.GetAllVillage();
            Data.KhasraList = await _watchandwardService.GetAllKhasra();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _watchandwardService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            var result1 = await _watchandwardService.GetAllWatchandward();
            return View("Index", result1);
        }
        public IActionResult WatchWardApproval()
        {
            return View();
        }

        public IActionResult WatchWardApprovalCreate()
        {
            return View();
        }
    }
}
