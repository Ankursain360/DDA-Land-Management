using Core.Enum;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using AcquiredLandInformationManagement.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.Helper;
using Dto.Master;
using Microsoft.Extensions.Configuration;

namespace AcquiredLandInformationManagement.Controllers
{
    public class GramSabhaLandController : BaseController
    {

        private readonly IGramsabhalandService _gramsabhalandService;
        public IConfiguration _configuration;
        string GztNoUs507documentlayout = string.Empty;
        string Us22NoDocumentlayout = string.Empty;
        string Us22otherNoDocumentlayout = string.Empty;
        string UpTssSurveyReportlayout = string.Empty;
        string KabzaProceedingDocumentlayout = string.Empty;

        public GramSabhaLandController(IGramsabhalandService gramsabhalandService, IConfiguration configuration)
        {
            _gramsabhalandService = gramsabhalandService;
            _configuration = configuration;
            GztNoUs507documentlayout = _configuration.GetSection("FilePaths:Gramsabhaland:GazzetteNoUs507documentpath").Value.ToString();
            Us22NoDocumentlayout = _configuration.GetSection("FilePaths:Gramsabhaland:Us22NoDocumentpath").Value.ToString();
            Us22otherNoDocumentlayout = _configuration.GetSection("FilePaths:Gramsabhaland:Us22otherNoDocumentpath").Value.ToString();
            UpTssSurveyReportlayout = _configuration.GetSection("FilePaths:Gramsabhaland:UpTssSurveyReportPath").Value.ToString();
            KabzaProceedingDocumentlayout = _configuration.GetSection("FilePaths:Gramsabhaland:KabzaProceedingPath").Value.ToString();



        }

        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Gramsabhaland gramsabhaland = new Gramsabhaland();

            gramsabhaland.ZoneList = await _gramsabhalandService.GetAllZone();
            gramsabhaland.VillageList = await _gramsabhalandService.GetAllVillage(gramsabhaland.ZoneId);
            return View(gramsabhaland);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Gramsabhaland gramsabhaland)
        {
            try
            {
                gramsabhaland.ZoneList = await _gramsabhalandService.GetAllZone();
                gramsabhaland.VillageList = await _gramsabhalandService.GetAllVillage(gramsabhaland.ZoneId);
               


                if (ModelState.IsValid)
                {


                    FileHelper file = new FileHelper();
                    if (gramsabhaland.GNus507Document1 != null)
                    {
                        gramsabhaland.GazzetteNotificationUs507document = file.SaveFile1(GztNoUs507documentlayout, gramsabhaland.GNus507Document1);

                    }
                    if (gramsabhaland.Us22Document2 != null)
                    {
                        gramsabhaland.Us22notificationDocument = file.SaveFile1(Us22NoDocumentlayout, gramsabhaland.Us22Document2);

                    }
                    if (gramsabhaland.Us22OtherDocument3 != null)
                    {
                        gramsabhaland.Us22otherNotificationDocument = file.SaveFile1(Us22otherNoDocumentlayout, gramsabhaland.Us22OtherDocument3);

                    }
                    if (gramsabhaland.TssSurveyDocument4 != null)
                    {
                        gramsabhaland.UploadTssSurveyReport = file.SaveFile1(UpTssSurveyReportlayout, gramsabhaland.TssSurveyDocument4);

                    }
                    if (gramsabhaland.KabzaDocument5 != null)
                    {
                        gramsabhaland.KabzaProceeding = file.SaveFile1(KabzaProceedingDocumentlayout, gramsabhaland.KabzaDocument5);

                    }





                    var result = await _gramsabhalandService.Create(gramsabhaland);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _gramsabhalandService.GetAllGramsabhaland();
                        return View("Index");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(gramsabhaland);
                    }
                }
                else
                {
                    return View(gramsabhaland);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(gramsabhaland);
            }
        }

        [HttpGet]
        public async Task<JsonResult> AllVillagedataList(int? zoneid)
        {
            zoneid = zoneid ?? 0;
            return Json(await _gramsabhalandService.GetAllVillage(Convert.ToInt32(zoneid)));
        }



        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] GramsabhalandSearchDto model)
        {
            var result = await _gramsabhalandService.GetPagedGramsabhaland(model);
            
            return PartialView("_List", result);
        }




        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _gramsabhalandService.FetchSingleResult(id);
            Data.ZoneList = await _gramsabhalandService.GetAllZone();
            Data.VillageList = await _gramsabhalandService.GetAllVillage(Data.ZoneId);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Gramsabhaland gramsabhaland)
        {

            if (ModelState.IsValid)
            {





                FileHelper file = new FileHelper();
                if (gramsabhaland.GNus507Document1 != null)
                {
                    gramsabhaland.GazzetteNotificationUs507document = file.SaveFile1(GztNoUs507documentlayout, gramsabhaland.GNus507Document1);

                }
                if (gramsabhaland.Us22Document2 != null)
                {
                    gramsabhaland.Us22notificationDocument = file.SaveFile1(Us22NoDocumentlayout, gramsabhaland.Us22Document2);

                }
                if (gramsabhaland.Us22OtherDocument3 != null)
                {
                    gramsabhaland.Us22otherNotificationDocument = file.SaveFile1(Us22otherNoDocumentlayout, gramsabhaland.Us22OtherDocument3);

                }
                if (gramsabhaland.TssSurveyDocument4 != null)
                {
                    gramsabhaland.UploadTssSurveyReport = file.SaveFile1(UpTssSurveyReportlayout, gramsabhaland.TssSurveyDocument4);

                }
                if (gramsabhaland.KabzaDocument5 != null)
                {
                    gramsabhaland.KabzaProceeding = file.SaveFile1(KabzaProceedingDocumentlayout, gramsabhaland.KabzaDocument5);

                }
















                try
                {

                    gramsabhaland.ModifiedBy = SiteContext.UserId;
                    var result = await _gramsabhalandService.Update(id, gramsabhaland);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);

                        return View("Index");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(gramsabhaland);

                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(gramsabhaland);
                }
            }
            return View(gramsabhaland);
        }






        public async Task<IActionResult> View(int id)
        {
            var Data = await _gramsabhalandService.FetchSingleResult(id);
            Data.ZoneList = await _gramsabhalandService.GetAllZone();
            Data.VillageList = await _gramsabhalandService.GetAllVillage(Data.ZoneId);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> DeleteConfirmed(int id)  // Used to Perform Delete Functionality 
        {
            var result = await _gramsabhalandService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }

            return View("Index");

        }


        public async Task<IActionResult> ViewfDocument1(int Id)
        {
            FileHelper file = new FileHelper();
            Gramsabhaland Data = await _gramsabhalandService.FetchSingleResult(Id);
            string filename = GztNoUs507documentlayout + Data.GazzetteNotificationUs507document;
            byte[] FileBytes = System.IO.File.ReadAllBytes(filename);
            return File(FileBytes, file.GetContentType(filename));
        }


        public async Task<IActionResult> ViewfDocument2(int Id)
        {
            FileHelper file = new FileHelper();
            Gramsabhaland Data = await _gramsabhalandService.FetchSingleResult(Id);
            string filename = Us22NoDocumentlayout + Data.Us22notificationDocument;
            byte[] FileBytes = System.IO.File.ReadAllBytes(filename);
            return File(FileBytes, file.GetContentType(filename));
        }

        public async Task<IActionResult> ViewfDocument3(int Id)
        {
            FileHelper file = new FileHelper();
            Gramsabhaland Data = await _gramsabhalandService.FetchSingleResult(Id);
            string filename = Us22otherNoDocumentlayout + Data.Us22otherNotificationDocument;
            byte[] FileBytes = System.IO.File.ReadAllBytes(filename);
            return File(FileBytes, file.GetContentType(filename));
        }


        public async Task<IActionResult> ViewfDocument4(int Id)
        {
            FileHelper file = new FileHelper();
            Gramsabhaland Data = await _gramsabhalandService.FetchSingleResult(Id);
            string filename = UpTssSurveyReportlayout + Data.UploadTssSurveyReport;
            byte[] FileBytes = System.IO.File.ReadAllBytes(filename);
            return File(FileBytes, file.GetContentType(filename));
        }

        public async Task<IActionResult> ViewfDocument5(int Id)
        {
            FileHelper file = new FileHelper();
            Gramsabhaland Data = await _gramsabhalandService.FetchSingleResult(Id);
            string filename = KabzaProceedingDocumentlayout + Data.KabzaProceeding;
            byte[] FileBytes = System.IO.File.ReadAllBytes(filename);
            return File(FileBytes, file.GetContentType(filename));
        }



        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> GramSabhaLanddetailsList()
        {
            var result = await _gramsabhalandService.GetAllGramsabhaland();
            List<GramSabhaLandListDto> data = new List<GramSabhaLandListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new GramSabhaLandListDto()
                    {
                        Id = result[i].Id,
                       
                        Village = result[i].Village == null ? "" : result[i].Village.Name,
                        Zone = result[i].Zone == null ? "" : result[i].Zone.Name,
                        KhasraNo = result[i].KhasraNo == null ? "" : result[i].KhasraNo,
                        US507NotificationNo = result[i].Us507notificationNo == null ? "" : result[i].Us507notificationNo,

                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive"
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }



    }
}
