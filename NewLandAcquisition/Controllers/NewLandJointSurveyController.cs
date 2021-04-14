using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.Extensions.Configuration;
using Utility.Helper;
using NewLandAcquisition.Filters;
using Core.Enum;

namespace NewLandAcquisition.Controllers
{
    public class NewLandJointSurveyController : BaseController
    {
        private readonly INewLandJointSurveyService _newLandJointSurveyService;
        public IConfiguration _configuration;
        public NewLandJointSurveyController(INewLandJointSurveyService newLandJointSurveyService, IConfiguration configuration)
        {
            _newLandJointSurveyService = newLandJointSurveyService;
            _configuration = configuration;
        }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] NewLandJointSurveySearchDto model)
        {
            var result = await _newLandJointSurveyService.GetPagedNewLandJointSurvey(model);
            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Newlandjointsurvey newlandjointsurvey = new Newlandjointsurvey();
            newlandjointsurvey.IsActive = 1;
            newlandjointsurvey.ZoneList = await _newLandJointSurveyService.GetAllZone();
             newlandjointsurvey.VillageList = await _newLandJointSurveyService.GetAllVillage(newlandjointsurvey.ZoneId);
            newlandjointsurvey.KhasraList = await _newLandJointSurveyService.GetAllKhasra(newlandjointsurvey.VillageId);
            newlandjointsurvey.VillageId = 0;
            return View(newlandjointsurvey);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Newlandjointsurvey newlandjointsurvey)
        {
            newlandjointsurvey.ZoneList = await _newLandJointSurveyService.GetAllZone();
           
            newlandjointsurvey.VillageList = await _newLandJointSurveyService.GetAllVillage(newlandjointsurvey.ZoneId);
            newlandjointsurvey.KhasraList = await _newLandJointSurveyService.GetAllKhasra(newlandjointsurvey.VillageId);
            string SurveyNoDocument = _configuration.GetSection("FilePaths:NewJointSurvey:SurveyNoDocument").Value.ToString();
            if (ModelState.IsValid)
            {
                FileHelper fileHelper = new FileHelper();
                newlandjointsurvey.CreatedBy = SiteContext.UserId;
                var result = await _newLandJointSurveyService.Create(newlandjointsurvey);

           
                    //************ Save attendance  ************  

                    if (newlandjointsurvey.AName != null &&
                        newlandjointsurvey.ADesignation != null &&
                        newlandjointsurvey.AAttendance != null)

                    {
                        if (newlandjointsurvey.AName.Count > 0 &&
                            newlandjointsurvey.ADesignation.Count > 0 &&
                            newlandjointsurvey.AAttendance.Count > 0
                           )

                        {
                        List<Newjointsurveyattendancedetail> attendance = new List<Newjointsurveyattendancedetail>();
                        for (int i = 0; i < newlandjointsurvey.AName.Count; i++)
                        {
                            attendance.Add(new Newjointsurveyattendancedetail
                            {
                                Name = newlandjointsurvey.AName.Count <= i ? string.Empty : newlandjointsurvey.AName[i],
                                Designation = newlandjointsurvey.ADesignation.Count <= i ? string.Empty : newlandjointsurvey.ADesignation[i],
                                Attendance = newlandjointsurvey.AAttendance.Count <= i ? string.Empty : newlandjointsurvey.AAttendance[i],
                                JointSurveyId = newlandjointsurvey.Id,
                                CreatedBy = SiteContext.UserId
                            });
                        }
                        foreach (var item in attendance)
                            {
                                result = await _newLandJointSurveyService.SaveAttendance(item);
                            }
                        }
                    }
                //****** code for saving  survey report *****

                if (newlandjointsurvey.DocumentName != null)
                {
                    if (newlandjointsurvey.DocumentName.Count > 0)

                    {
                        List<Newjointsurveyreportdetail> newjointsurveyreportdetail = new List<Newjointsurveyreportdetail>();
                        for (int i = 0; i < newlandjointsurvey.DocumentName.Count; i++)
                        {
                            newjointsurveyreportdetail.Add(new Newjointsurveyreportdetail
                            {

                                DocumentName = newlandjointsurvey.DocumentName.Count <= i ? string.Empty : newlandjointsurvey.DocumentName[i],

                               
                                UploadFilePath = newlandjointsurvey.Document != null ?
                                                                    newlandjointsurvey.Document.Count <= i ? string.Empty :
                                                                    fileHelper.SaveFile(SurveyNoDocument, newlandjointsurvey.Document[i]) :
                                                                    newlandjointsurvey.UploadFilePath[i] != null || newlandjointsurvey.UploadFilePath[i] != "" ?
                                                                    newlandjointsurvey.UploadFilePath[i] : string.Empty,
                                JointSurveyId = newlandjointsurvey.Id,
                                CreatedBy = SiteContext.UserId



                            });
                        }
                        foreach (var item in newjointsurveyreportdetail)
                        {
                            result = await _newLandJointSurveyService.SaveSurveyReport(item);
                        }
                    }
                }

                ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                    var list = await _newLandJointSurveyService.GetAllNewLandJointSurvey();
                    return View("Index", list);

                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(newlandjointsurvey);
                }
             
            }
            

        public async Task<JsonResult> GetDetailsAttendance(int? Id)
        {
            Id = Id ?? 0;
            var data = await _newLandJointSurveyService.GetAllattendance(Convert.ToInt32(Id));
            return Json(data.Select(x => new
            {
                x.Id,
                x.Name,
                x.Designation,
                x.Attendance
            }));
        }
        public async Task<JsonResult> GetDetailssurveyreport(int? Id)
        {
            Id = Id ?? 0;
            var data = await _newLandJointSurveyService.GetNewjointsurveyreportdetail(Convert.ToInt32(Id));
            
            return Json(data.Select(x => new
            {
                x.Id,
                x.UploadFilePath,
                x.DocumentName
                
            }));
        }
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _newLandJointSurveyService.FetchSingleResult(id);
            Data.ZoneList = await _newLandJointSurveyService.GetAllZone();
            Data.VillageList = await _newLandJointSurveyService.GetAllVillage(Data.ZoneId);
            Data.KhasraList = await _newLandJointSurveyService.GetAllKhasra(Data.VillageId);
            Newlandjointsurvey newlandjointsurvey = new Newlandjointsurvey();
            newlandjointsurvey.IsActive = newlandjointsurvey.IsActive;
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Newlandjointsurvey newlandjointsurvey)
        {
            newlandjointsurvey.ZoneList = await _newLandJointSurveyService.GetAllZone();
            newlandjointsurvey.VillageList = await _newLandJointSurveyService.GetAllVillage(newlandjointsurvey.ZoneId);
            newlandjointsurvey.KhasraList = await _newLandJointSurveyService.GetAllKhasra(newlandjointsurvey.VillageId);
            string SurveyNoDocument = _configuration.GetSection("FilePaths:NewJointSurvey:SurveyNoDocument").Value.ToString(); ;
            if (ModelState.IsValid)
            {
                FileHelper fileHelper = new FileHelper();

                newlandjointsurvey.ModifiedBy = SiteContext.UserId;
                var result = await _newLandJointSurveyService.Update(id, newlandjointsurvey);

                if (result == true)
                {



                    //************ Save tenant  ************  

                    if (newlandjointsurvey.AName != null &&
                        newlandjointsurvey.ADesignation != null &&
                        newlandjointsurvey.AAttendance != null)

                    {
                        if (newlandjointsurvey.AName.Count > 0 &&
                            newlandjointsurvey.ADesignation.Count > 0 &&
                            newlandjointsurvey.AAttendance.Count > 0
                           )

                        {
                            List<Newjointsurveyattendancedetail> attendance = new List<Newjointsurveyattendancedetail>();
                            result = await _newLandJointSurveyService.DeleteAttendance(id);
                            for (int i = 0; i < newlandjointsurvey.AName.Count; i++)
                            {
                                attendance.Add(new Newjointsurveyattendancedetail
                                {
                                    Name = newlandjointsurvey.AName.Count <= i ? string.Empty : newlandjointsurvey.AName[i],
                                    Designation = newlandjointsurvey.ADesignation.Count <= i ? string.Empty : newlandjointsurvey.ADesignation[i],
                                    Attendance = newlandjointsurvey.AAttendance.Count <= i ? string.Empty : newlandjointsurvey.AAttendance[i],
                                    JointSurveyId = newlandjointsurvey.Id,
                                    CreatedBy = SiteContext.UserId
                                });
                            }
                            foreach (var item in attendance)
                            {
                                result = await _newLandJointSurveyService.SaveAttendance(item);
                            }
                        }
                    }

                    //****** code for saving  surveyreport *****

                    if (newlandjointsurvey.DocumentName != null &&

                 newlandjointsurvey.UploadFilePath != null)
                    {
                        if (newlandjointsurvey.DocumentName.Count > 0 &&

                    newlandjointsurvey.UploadFilePath.Count > 0)

                        {
                            List<Newjointsurveyreportdetail> newjointsurveyreportdetail = new List<Newjointsurveyreportdetail>();
                            result = await _newLandJointSurveyService.DeleteSurveyReport(id);
                            for (int i = 0; i < newlandjointsurvey.DocumentName.Count; i++)
                            {
                                newjointsurveyreportdetail.Add(new Newjointsurveyreportdetail
                                {

                                    DocumentName = newlandjointsurvey.DocumentName.Count <= i ? string.Empty : newlandjointsurvey.DocumentName[i],


                                    UploadFilePath = newlandjointsurvey.Document != null ?
                                                                newlandjointsurvey.Document.Count <= i ? string.Empty :
                                                                    fileHelper.SaveFile(SurveyNoDocument, newlandjointsurvey.Document[i]) :
                                                                    newlandjointsurvey.UploadFilePath[i] != null || newlandjointsurvey.UploadFilePath[i] != "" ?
                                                                    newlandjointsurvey.UploadFilePath[i] : string.Empty,
                                    JointSurveyId = newlandjointsurvey.Id,
                                    CreatedBy = SiteContext.UserId


                                });


                            }
                            foreach (var item in newjointsurveyreportdetail)
                            {
                                result = await _newLandJointSurveyService.SaveSurveyReport(item);
                            }
                        }
                    }

                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                    var list = await _newLandJointSurveyService.GetAllNewLandJointSurvey();
                    return View("Index", list);

                }



                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(newlandjointsurvey);
                }
            }
            else

            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(newlandjointsurvey);
            }
               
            }

        //**********************  download repeater files****************************
        public async Task<FileResult> ViewSurveyReportFile(int Id)
        {
            FileHelper file = new FileHelper();
            Newjointsurveyreportdetail Data = await _newLandJointSurveyService.GetNewjointsurveyreportdetailFilePath(Id);
            string path = Data.UploadFilePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }




        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {

            var result = await _newLandJointSurveyService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }

            var list = await _newLandJointSurveyService.GetAllNewLandJointSurvey();
            return View("Index", list);
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _newLandJointSurveyService.FetchSingleResult(id);

            Data.ZoneList = await _newLandJointSurveyService.GetAllZone();
            Data.VillageList = await _newLandJointSurveyService.GetAllVillage(Data.ZoneId);
            Data.KhasraList = await _newLandJointSurveyService.GetAllKhasra(Data.VillageId);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [HttpGet]
        public async Task<JsonResult> GetVillageList(int? ZoneId)
        {
            ZoneId = ZoneId ?? 0;
            return Json(await _newLandJointSurveyService.GetAllVillage(Convert.ToInt32(ZoneId)));
        }

        

        [HttpGet]
        public async Task<JsonResult> GetKhasraList(int? villageId)
        {
            villageId = villageId ?? 0;
            return Json(await _newLandJointSurveyService.GetAllKhasra(Convert.ToInt32(villageId)));
        }

        [HttpGet]
        public async Task<JsonResult> GetAreaList(int? khasraid)
        {
            khasraid = khasraid ?? 0;

            return Json(await _newLandJointSurveyService.FetchSingleKhasraResult(Convert.ToInt32(khasraid)));
        }
    }
}