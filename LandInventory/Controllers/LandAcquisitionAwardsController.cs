using Dto.Master;
using Dto.Search;
using LandInventory.Filters;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Utility.Helper;

namespace LandInventory.Controllers
{
    public class LandAcquisitionAwardsController : Controller
    {
        private readonly ILandAcquisitionAwardsService _acquisitionAwardsService;
        private readonly IConfiguration _configuration;
        string FileDocumentPath = "";
        public LandAcquisitionAwardsController(ILandAcquisitionAwardsService acquisitionAwardsService, IConfiguration configuration)
        {
            _acquisitionAwardsService = acquisitionAwardsService;
            _configuration = configuration;
            FileDocumentPath = _configuration.GetSection("FilePaths:LandAcquisitionAwardsFiles:AwardsDocumentPath").Value.ToString();
        }

        public IActionResult Index()
        {
            //ViewBag.LocalityList = await _acquisitionAwardsService.GetLocalityList();
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List ([FromBody] LandAcquisitionAwardsDto model)
        {
            try
            {
                var result = await _acquisitionAwardsService.GetPagedLandAcquisitionAwards(model); 
                return PartialView("_List", result);
            }
            catch (Exception ex)
            {
                return PartialView(ex); 
            }
        }
        [AuthorizeContext(Core.Enum.ViewAction.Download)]
        public async Task<IActionResult> ViewDocument(int id)
        {
            FileHelper file = new FileHelper();
            LandAcquisitionAwards result = await _acquisitionAwardsService.FetchDocumentDetails(id);
            string fileName = FileDocumentPath + HttpUtility.UrlDecode(result.Documents);
            byte[] FileBytes = System.IO.File.ReadAllBytes(fileName);
            return File(FileBytes, file.GetContentType(fileName));
        }

        [HttpPost]
        [AuthorizeContext(Core.Enum.ViewAction.Download)]

        public async Task<IActionResult> DownloadLandAcquisitionAwards([FromBody] LandAcquisitionAwardsSearchDto model)
        {
            //if (model.title == "" && model.village == "" )
            //{
            //    var result1 = await _acquisitionAwardsService.GetAllAcquisitionAwards();
            //    List<LandAcquisitionAwards> data1  = new List<LandAcquisitionAwards>();
            //    if (result1 != null)
            //    {
            //        for (int i = 0; i < result1.Count; i++)
            //        {
            //            data1.Add(new LandAcquisitionAwards()
            //            {
            //                Id = result1[i].Id,
            //                Title = result1[i].Title,
            //                Village = result1[i].Village
            //            });

            //        }

            //    }
            //    var memory1 = ExcelHelper.CreateExcel(data1);
            //    //return File(memory1, "application/vnd.openxmlformats-officedocument.LandAcquisitionAwards.sheet");
            //    TempData["file1"] = memory1;
            //    return Ok();

            //}
            var result = await _acquisitionAwardsService.GetLandAcquisitionAwardsList(model);
            List<LandAcquisitionAwardsListDto> data = new List<LandAcquisitionAwardsListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new LandAcquisitionAwardsListDto()
                    {
                        Id = result[i].Id,
                        Title = result[i].Title,
                        Village = result[i].Village

                    });

                }

            }
            var memory = ExcelHelper.CreateExcel(data);
            HttpContext.Session.Set("file", memory);
            return Ok();
        }
        [HttpGet]
        [AuthorizeContext(Core.Enum.ViewAction.Download)]
        public virtual IActionResult Download()
        {
            byte[] data = HttpContext.Session.Get("file") as byte[];
            HttpContext.Session.Remove("file");
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "LandAcquisitionAwards.xlsx");
        }
        [AuthorizeContext(Core.Enum.ViewAction.Download)]
        public async Task<IActionResult> DownloadallLandAcquisitionAwards()
        { 
            var result1 = await _acquisitionAwardsService.GetAllAcquisitionAwards();
            List<LandAcquisitionAwards> data1 = new List<LandAcquisitionAwards>();
            if (result1 != null)
            {
                for (int i = 0; i < result1.Count; i++)
                {
                    data1.Add(new LandAcquisitionAwards()
                    {
                        Id = result1[i].Id,
                        Title = result1[i].Title,
                        Village = result1[i].Village
                    });

                }

            }
            var memory1 = ExcelHelper.CreateExcel(data1);
            return File(memory1, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "LandAcquisitionAwards.xlsx");

        }
        //[HttpGet]
        //public virtual IActionResult Download1() 
        //{
        //    byte[] data = TempData["file1"] as byte[];
        //    return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "LandAcquisitionAwards.xlsx");
        //}
    }
}
