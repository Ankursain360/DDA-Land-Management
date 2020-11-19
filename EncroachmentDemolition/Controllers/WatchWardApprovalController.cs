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
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace EncroachmentDemolition.Controllers
{
    public class WatchWardApprovalController : BaseController
    {
        public readonly IWatchAndWardApprovalService _watchAndWardApprovalService;
        private readonly IWatchandwardService _watchandwardService;
        public IConfiguration _configuration;

        public WatchWardApprovalController(IWatchAndWardApprovalService watchAndWardApprovalService, IConfiguration configuration, IWatchandwardService watchandwardService)
        {
            _watchAndWardApprovalService = watchAndWardApprovalService;
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
            var result = await _watchAndWardApprovalService.GetPagedWatchandward(model);
            return PartialView("_List", result);
        }
        public async Task<IActionResult> Create(int id)
        {
            var Data = await _watchAndWardApprovalService.FetchSingleResult(id);
            //Data.VillageList = await _watchAndWardApprovalService.GetAllVillage();
            //Data.LocalityList = await _watchAndWardApprovalService.GetAllLocality();
            //Data.KhasraList = await _watchAndWardApprovalService.GetAllKhasra();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        public async Task<PartialViewResult> WatchWardView(int id)
        {
            var Data = await _watchandwardService.FetchSingleResult(id);
            Data.PrimaryListNoList = await _watchandwardService.GetAllPrimaryList();

            return PartialView("_WatchWardView", Data);
        }

        public async Task<IActionResult> DownloadPhotoFile(int Id)
        {
            FileHelper file = new FileHelper();
            Watchandwardphotofiledetails Data = await _watchandwardService.GetWatchandwardphotofiledetails(Id);
            string filename = Data.PhotoFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }

        public async Task<FileResult> ViewDocument(int Id)
        {
            FileHelper file = new FileHelper();
            Watchandwardphotofiledetails Data = await _watchandwardService.GetWatchandwardphotofiledetails(Id);
            string path = Data.PhotoFilePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
    }
}
