using Dto.Master;
using GIS.Helper;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Utility.Helper;
using Microsoft.AspNetCore.Hosting;
using Dto.Search;
using Core.Enum;
using GIS.Filters;
using System.Linq;


namespace GIS.Controllers
{
    public class DDADecisionSupportSystemController : Controller
    {
        private readonly IGISService _GISService;
        private readonly ISiteContext _siteContext;
        private readonly IUserProfileService _userProfileService;
        private readonly IApplicationModificationDetailsService _modificationDetails;
        public IConfiguration _Configuration;
        private readonly IHostingEnvironment _hostingEnvironment;
        string InputImages = string.Empty;
        string ChangeDetectionImage = string.Empty;
        public DDADecisionSupportSystemController(IGISService GISService, IUserProfileService userProfileService, ISiteContext siteContext, IConfiguration configuration, IHostingEnvironment en, IApplicationModificationDetailsService modificationDetails)
        {
            _siteContext = siteContext;
            _userProfileService = userProfileService;
            _GISService = GISService; 
            _Configuration = configuration;
            _hostingEnvironment = en;
            _modificationDetails = modificationDetails;
            InputImages = _Configuration.GetSection("FilePaths:InputImages:FirstPhotoPath").Value.ToString();
            ChangeDetectionImage = _Configuration.GetSection("FilePaths:OutPutImages:ChangedImagePath").Value.ToString();
           
        }
        public void updateDateFun()
        {
            var updatedDate = _modificationDetails.GetApplicationModificationDetails();
            var dt = Convert.ToDateTime(updatedDate).ToString("dd/MMM/yyyy HH:MM:ss tt");
            if (updatedDate != null)
            {
                TempData["updatedDate"] = dt;

            }
            else
            {
                TempData["updatedDate"] = "No Data Available";

            }

        }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            updateDateFun();
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] AIchangeDetectionSearchDto model)
        {
            var result = await _GISService.GetChangeDetectionData(model);
            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {

            var data = await _GISService.GetAllChangeDetectionData();
            var result = data.Select(x => new { x.Village.Name, x.FirstPhotoPath, x.SecondPhotoPath, x.Village.Id }).ToList();
            List<AIchangeDetectionListSearchDto> obj = new List<AIchangeDetectionListSearchDto>();
            for (int i = 0; i < result.Count; i++)
            {
                obj.Add(new AIchangeDetectionListSearchDto
                {
                    id = result[i].Id,
                    FirstPhoto = result[i].Name + "_" + result[i].FirstPhotoPath.Split('.')[0],
                    SecondPhoto = result[i].Name + "_" + result[i].SecondPhotoPath.Split('.')[0],
                });

            }
         
            return View(obj);
        }
         
        public async Task<IActionResult> ViewComparingImage([FromBody] AIchangeDetectionListSearchDto modal)
        {
            var data = await _GISService.ResultAfterComparingImage(modal);
            return PartialView("_ViewComparingImage",data);
        }

        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> DownloadImageFirst(int Id) 
        { 
            FileHelper file = new FileHelper(); 
            var Data = await _GISService.GetAIchangedetectionImageDetails(Id);
            string targetPhotoPathLayout = InputImages + Data.FirstPhotoPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
            return File(FileBytes, file.GetContentType(targetPhotoPathLayout));
            // return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> DownloadImageSecond(int Id)
        {
            FileHelper file = new FileHelper();
            var Data = await _GISService.GetAIchangedetectionImageDetails(Id);
            string targetPhotoPathLayout = InputImages + Data.SecondPhotoPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
            return File(FileBytes, file.GetContentType(targetPhotoPathLayout));
            //return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> DownloadChangeDetectionImage(int Id)
        {
            FileHelper file = new FileHelper();
            var Data = await _GISService.GetAIchangedetectionImageDetails(Id);
            string targetPhotoPathLayout = ChangeDetectionImage + Data.ChangedImage;
            byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
            return File(FileBytes, file.GetContentType(targetPhotoPathLayout));
            //return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
    }
}
