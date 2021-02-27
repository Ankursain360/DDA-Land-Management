//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//using Libraries.Model.Entity;
//using Libraries.Service.IApplicationService;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Notification;
//using Notification.Constants;
//using Notification.OptionEnums;
//using Dto.Search;

//namespace NewLandAcquisition.Controllers
//{
//    public class NewLandJointSurveyController : Controller
//    {
//        private readonly INewlandjointsurveyService _newlandjointsurveyService;

//        public NewLandJointSurveyController(INewlandjointsurveyService newlandjointsurveyService)
//        {
//            _newlandjointsurveyService = newlandjointsurveyService;
//        }
//        public async Task<IActionResult> Index()
//        {

//            return View();
//        }
//        public async Task<IActionResult> Create()
//        {

//            Newlandjointsurvey jointsurvey = new Newlandjointsurvey();
//            jointsurvey.IsActive = 1;

//            jointsurvey.ZoneList = await _newlandjointsurveyService.GetAllZone();
//            jointsurvey.VillageList = await _newlandjointsurveyService.GetAllVillage(jointsurvey.ZoneId);
//            jointsurvey.KhasraList = await _newlandjointsurveyService.GetAllKhasra(jointsurvey.VillageId);
          

//            return View(jointsurvey);
//        }
//        [HttpPost]
//        //[AuthorizeContext(ViewAction.Add)]
//        public async Task<IActionResult> Create(Newlandjointsurvey jointsurvey)
//        {
//            try
//            {
//                jointsurvey.ZoneList = await _newlandjointsurveyService.GetAllZone();
//                jointsurvey.VillageList = await _newlandjointsurveyService.GetAllVillage(jointsurvey.ZoneId);
//                jointsurvey.KhasraList = await _newlandjointsurveyService.GetAllKhasra(jointsurvey.VillageId);

//                if (ModelState.IsValid)
//                {
//                    var result = await _newlandjointsurveyService.Create(jointsurvey);

//                    if (result == true)
//                    {
//                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
//                        var list = await _newlandjointsurveyService.GetAllJointSurvey();
//                        return View("Index", list);
//                    }
//                    else
//                    {
//                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
//                        return View(jointsurvey);
//                    }
//                }
//                else
//                {
//                    return View(jointsurvey);
//                }
//            }
//            catch (Exception ex)
//            {
//                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
//                return View(jointsurvey);
//            }
//        }

//        [HttpGet]
//        public async Task<JsonResult> GetVillageList(int? ZoneId)
//        {
//            ZoneId = ZoneId ?? 0;
//            return Json(await _newlandjointsurveyService.GetAllVillage(Convert.ToInt32(ZoneId)));
//        }

//        [HttpGet]
//        public async Task<JsonResult> GetKhasraList(int? VillageId)
//        {
//            VillageId = VillageId ?? 0;
//            return Json(await _newlandjointsurveyService.GetAllKhasra(Convert.ToInt32(VillageId)));
//        }


//        [HttpGet]
//        public async Task<JsonResult> GetAreaList(int? khasraid)
//        {
//            khasraid = khasraid ?? 0;

//            return Json(await _newlandjointsurveyService.FetchSingleKhasraResult(Convert.ToInt32(khasraid)));
//        }


//    }
//}
