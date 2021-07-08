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


        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            Gramsabhaland gramsabhaland = new Gramsabhaland();

            gramsabhaland.ZoneList = await _gramsabhalandService.GetAllZone();
            gramsabhaland.VillageList = await _gramsabhalandService.GetAllVillage(gramsabhaland.ZoneId);
            return View(gramsabhaland);
        }



        [HttpPost]
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
                        return View("Create", gramsabhaland);
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

    }
}
