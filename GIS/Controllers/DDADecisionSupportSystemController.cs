using Dto.Master;
using GIS.Helper;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.Record.CF;
using NPOI.SS.Formula.Functions;
using Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using OpenCvSharp;
using Dto.GIS;
using Microsoft.Extensions.Configuration;
using Accord.Imaging.Filters;
using Accord.Imaging;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.MachineLearning.VectorMachines;
using Accord.Statistics.Kernels;
using Org.BouncyCastle.Asn1.Tsp;
using Accord.MachineLearning;
using Accord.Math;
using Accord.IO;
using NPOI.POIFS.Crypt.Dsig;
using Utility.Helper;
using Microsoft.AspNetCore.Http;
using Point = OpenCvSharp.Point;
using AForge.Imaging.Filters;
using Notification.Constants;
using Notification.OptionEnums;
using Notification;
using Microsoft.AspNetCore.Hosting;
using System.Text;
using System.Xml.Linq;
using Dto.Search;
using Libraries.Service.ApplicationService;

namespace GIS.Controllers
{
    public class DDADecisionSupportSystemController : Controller
    {
        private readonly IGISService _GISService;
        private readonly ISiteContext _siteContext;
        private readonly IUserProfileService _userProfileService;
        public IConfiguration _Configuration;
        private readonly IHostingEnvironment _hostingEnvironment;
        string InputImages = string.Empty;
        string ChangeDetectionImage = string.Empty;
        public DDADecisionSupportSystemController(IGISService GISService, IUserProfileService userProfileService, ISiteContext siteContext, IConfiguration configuration, IHostingEnvironment en)
        {
            _siteContext = siteContext;
            _userProfileService = userProfileService;
            _GISService = GISService; 
            _Configuration = configuration;
            _hostingEnvironment = en;
            InputImages = _Configuration.GetSection("FilePaths:InputImages:FirstPhotoPath").Value.ToString();
            ChangeDetectionImage = _Configuration.GetSection("FilePaths:OutPutImages:ChangedImagePath").Value.ToString();
           
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] AIchangeDetectionSearchDto model)
        {
            var result = await _GISService.GetChangeDetectionData(model);
            return PartialView("_List", result);
        }
        public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> DownloadImageFirst(int Id) 
        { 
            FileHelper file = new FileHelper(); 
            var Data = await _GISService.GetAIchangedetectionImageDetails(Id);
            string targetPhotoPathLayout = InputImages + Data.FirstPhotoPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
            return File(FileBytes, file.GetContentType(targetPhotoPathLayout));
            // return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadImageSecond(int Id)
        {
            FileHelper file = new FileHelper();
            var Data = await _GISService.GetAIchangedetectionImageDetails(Id);
            string targetPhotoPathLayout = InputImages + Data.SecondPhotoPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
            return File(FileBytes, file.GetContentType(targetPhotoPathLayout));
            //return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
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
