using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DamagePayeePublicInterface.Controllers
{
    public class NewSelfAssessmentFormController : Controller
    {
        private readonly INewDamageSelfAssessmentService _selfAssessmentService;


        public NewSelfAssessmentFormController(INewDamageSelfAssessmentService selfAssessmentService)
        {
            _selfAssessmentService = selfAssessmentService;
        }
        public IActionResult Index()
        {
            return View(); 
        }
        public async Task<IActionResult> Create()
        
        {
            NewDamageSelfAssessment model = new NewDamageSelfAssessment();
            model.DistrictList = await _selfAssessmentService.GetAllDistrict();
            model.LocalitieList = await _selfAssessmentService.GetLocalityList();
            model.AcquiredlandvillageList = await _selfAssessmentService.GetAllVillage(model.Districtid);
            model.New_DamageColonyList = await _selfAssessmentService.GetAllColony(model.VillageId);
            return View(model);
           
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewDamageSelfAssessment selfAssessment)
        {
            try
            {
                selfAssessment.DistrictList = await _selfAssessmentService.GetAllDistrict();
                selfAssessment.LocalitieList = await _selfAssessmentService.GetLocalityList();
                selfAssessment.AcquiredlandvillageList = await _selfAssessmentService.GetAllVillage(selfAssessment.Districtid);
                selfAssessment.New_DamageColonyList = await _selfAssessmentService.GetAllColony(selfAssessment.VillageId);
               // string SurveyNoDocument = _selfAssessmentService.GetSection("FilePaths:NewJointSurvey:SurveyNoDocument").Value.ToString();
                if (ModelState.IsValid)
                {

                    var result = await _selfAssessmentService.Create(selfAssessment);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _selfAssessmentService.GetAllDistrict();
                        return View("Index", list);

                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(selfAssessment);

                    }
                }
                else
                {
                    return View(selfAssessment);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(selfAssessment);
            }
        }

        //if (ModelState.IsValid)
        //{
        //    StringBuilder str = new StringBuilder();
        //    if (selfAssessment.IsBuiltup == true)
        //    {
        //        str.Append("Built Up");
        //    }
        //    if (selfAssessment.IsPartialBuiltup == true)
        //    {
        //        if (str.Length != 0)
        //            str.Append("|");
        //        str.Append("Partial Built Up");
        //    }
        //    if (selfAssessment.IsVacant == true)
        //    {
        //        if (str.Length != 0)
        //            str.Append("|");
        //        str.Append("Vacant");
        //    }
        //    if (selfAssessment.IsBoundary == true)
        //    {
        //        if (str.Length != 0)
        //            str.Append("|");
        //        str.Append("Boundry Wall");
        //    }

        //    selfAssessment.SitePosition = str.ToString();

        //    FileHelper fileHelper = new FileHelper();
        //    selfAssessment.CreatedBy = SiteContext.UserId;
        //    var result = await _selfAssessmentService.Create(selfAssessment);


        //    //************ Save attendance  ************  

        //    if (selfAssessment.AName != null &&
        //        selfAssessment.ADesignation != null &&
        //        selfAssessment.AAttendance != null)

        //    {
        //        if (selfAssessment.AName.Count > 0 &&
        //            selfAssessment.ADesignation.Count > 0 &&
        //            selfAssessment.AAttendance.Count > 0
        //           )

        //        {
        //            List<Newjointsurveyattendancedetail> attendance = new List<Newjointsurveyattendancedetail>();
        //            for (int i = 0; i < selfAssessment.AName.Count; i++)
        //            {
        //                attendance.Add(new Newjointsurveyattendancedetail
        //                {
        //                    Name = selfAssessment.AName.Count <= i ? string.Empty : selfAssessment.AName[i],
        //                    Designation = selfAssessment.ADesignation.Count <= i ? string.Empty : selfAssessment.ADesignation[i],
        //                    Attendance = selfAssessment.AAttendance.Count <= i ? string.Empty : selfAssessment.AAttendance[i],
        //                    JointSurveyId = selfAssessment.Id,
        //                    CreatedBy = SiteContext.UserId
        //                });
        //            }
        //            foreach (var item in attendance)
        //            {
        //                result = await _selfAssessmentService.SaveAttendance(item);
        //            }
        //        }
        //    }
        //    //****** code for saving  survey report *****

        //    if (selfAssessment.DocumentName != null)
        //    {
        //        if (selfAssessment.DocumentName.Count > 0)

        //        {
        //            List<Newjointsurveyreportdetail> newjointsurveyreportdetail = new List<Newjointsurveyreportdetail>();
        //            for (int i = 0; i < selfAssessment.DocumentName.Count; i++)
        //            {
        //                newjointsurveyreportdetail.Add(new Newjointsurveyreportdetail
        //                {

        //                    DocumentName = zy.DocumentName.Count <= i ? string.Empty : newlandjointsurvey.DocumentName[i],


        //                    UploadFilePath = newlandjointsurvey.Document != null ?
        //                                                        newlandjointsurvey.Document.Count <= i ? string.Empty :
        //                                                        fileHelper.SaveFile(SurveyNoDocument, newlandjointsurvey.Document[i]) :
        //                                                        newlandjointsurvey.UploadFilePath[i] != null || newlandjointsurvey.UploadFilePath[i] != "" ?
        //                                                        newlandjointsurvey.UploadFilePath[i] : string.Empty,
        //                    JointSurveyId = newlandjointsurvey.Id,
        //                    CreatedBy = SiteContext.UserId



        //                });
        //            }
        //            foreach (var item in newjointsurveyreportdetail)
        //            {
        //                result = await _newLandJointSurveyService.SaveSurveyReport(item);
        //            }
        //        }
        //    }

        //    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
        //    var list = await _newLandJointSurveyService.GetAllNewLandJointSurvey();
        //    return View("Index", list);

        //}
        //else
        //{
        //    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
        //    return View(newlandjointsurvey);
        //}

        [HttpGet]
        public async Task<JsonResult> GetNewVillageList(int? DistrictId)
        {
            
           DistrictId = DistrictId ?? 0;
            return Json(await _selfAssessmentService.GetAllVillage(Convert.ToInt32(DistrictId)));
        }
        [HttpGet]
        public async Task<JsonResult> GetColonyList(int? VillageId)
        {
            VillageId = VillageId ?? 0;
            return Json(await _selfAssessmentService.GetAllColony(Convert.ToInt32(VillageId)));
        }

        //[HttpGet]
        //public async Task<JsonResult> GetVillageList(int? ZoneId)
        //{
        //    ZoneId = ZoneId ?? 0;
        //    return Json(await _newLandJointSurveyService.GetAllVillage(Convert.ToInt32(ZoneId)));
        //}
    }
}
