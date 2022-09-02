using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Dto.Search;
using AcquiredLandInformationManagement.Filters;
using Core.Enum;
using Dto.Master;
using Utility.Helper;
using System.Text;

namespace AcquiredLandInformationManagement.Controllers
{
    public class JointSurveyController : BaseController
    {
        private readonly IJointsurveyService _jointsurveyService;
        public JointSurveyController(IJointsurveyService jointsurveyService)
        {
            _jointsurveyService = jointsurveyService;
        }

        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] JointSurveySearchDto model)
        {
            var result = await _jointsurveyService.GetPagedJointsurvey(model);

            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {

            Jointsurvey jointsurvey = new Jointsurvey();
            jointsurvey.IsActive = 1;
            jointsurvey.KhasraList = await _jointsurveyService.BindKhasra(jointsurvey.VillageId);
            jointsurvey.VillageList = await _jointsurveyService.GetAllVillage();
            return View(jointsurvey);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Jointsurvey jointsurvey)
        {
            try
            {

                jointsurvey.KhasraList = await _jointsurveyService.BindKhasra(jointsurvey.VillageId);
                jointsurvey.VillageList = await _jointsurveyService.GetAllVillage();

                if (ModelState.IsValid)
                {
                    StringBuilder str = new StringBuilder();
                    if (jointsurvey.IsBuiltup == true)
                    {
                        str.Append("Built Up");
                    }
                    if (jointsurvey.IsPartialBuiltup == true)
                    {
                        if (str.Length != 0)
                            str.Append("|");
                        str.Append("Partial Built Up");
                    }
                    if (jointsurvey.IsVacant == true)
                    {
                        if (str.Length != 0)
                            str.Append("|");
                        str.Append("Vacant");
                    }
                    if (jointsurvey.IsBoundary == true)
                    {
                        if (str.Length != 0)
                            str.Append("|");
                        str.Append("Boundry Wall");
                    }

                    jointsurvey.SitePosition = str.ToString();
                    var result = await _jointsurveyService.Create(jointsurvey);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _jointsurveyService.GetAllJointSurvey();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(jointsurvey);
                    }
                }
                else
                {
                    return View(jointsurvey);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(jointsurvey);
            }
        }

        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
           
           var jointsurvey = await _jointsurveyService.FetchSingleResult(id);

            jointsurvey.KhasraList = await _jointsurveyService.BindKhasra(jointsurvey.VillageId);
            jointsurvey.VillageList = await _jointsurveyService.GetAllVillage();
            if (jointsurvey.SitePosition != "")
            {
                string[] multiTo = jointsurvey.SitePosition.Split('|');
                foreach (string Multi in multiTo)
                {
                    if (Multi == "Vacant")
                        jointsurvey.IsVacant = true;
                    if (Multi == "Built Up")
                        jointsurvey.IsBuiltup = true;
                    if (Multi == "Partial Built Up")
                        jointsurvey.IsPartialBuiltup = true;
                    if (Multi == "Boundry Wall")
                        jointsurvey.IsBoundary = true;
                }
            }
            if (jointsurvey == null)
            {
                return NotFound();
            }
            return View(jointsurvey);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Jointsurvey jointsurvey)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        StringBuilder str = new StringBuilder();
                        if (jointsurvey.IsBuiltup == true)
                        {
                            str.Append("Built Up");
                        }
                        if (jointsurvey.IsPartialBuiltup == true)
                        {
                            if (str.Length != 0)
                                str.Append("|");
                            str.Append("Partial Built Up");
                        }
                        if (jointsurvey.IsVacant == true)
                        {
                            if (str.Length != 0)
                                str.Append("|");
                            str.Append("Vacant");
                        }
                        if (jointsurvey.IsBoundary == true)
                        {
                            if (str.Length != 0)
                                str.Append("|");
                            str.Append("Boundry Wall");
                        }

                        jointsurvey.SitePosition = str.ToString();
                        var result = await _jointsurveyService.Update(id, jointsurvey);

                        if (result == true)
                        {
                            ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                            var list = await _jointsurveyService.GetAllJointSurvey();
                            return View("Index", list);
                        }
                        else
                        {
                            ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                            return View(jointsurvey);
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(jointsurvey);
                    }
                }
                else
                {
                    return View(jointsurvey);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(jointsurvey);

            }
        }
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> DeleteConfirmed(int id)  // Used to Perform Delete Functionality added by Pankaj
        {
            var result = await _jointsurveyService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            var list = await _jointsurveyService.GetAllJointSurvey();
            return View("Index", list);
        }




        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {

          
            var jointsurvey = await _jointsurveyService.FetchSingleResult(id);

            jointsurvey.KhasraList = await _jointsurveyService.BindKhasra(jointsurvey.VillageId);
            jointsurvey.VillageList = await _jointsurveyService.GetAllVillage();
            if (jointsurvey.SitePosition != "")
            {
                string[] multiTo = jointsurvey.SitePosition.Split('|');
                foreach (string Multi in multiTo)
                {
                    if (Multi == "Vacant")
                        jointsurvey.IsVacant = true;
                    if (Multi == "Built Up")
                        jointsurvey.IsBuiltup = true;
                    if (Multi == "Partial Built Up")
                        jointsurvey.IsPartialBuiltup = true;
                    if (Multi == "Boundry Wall")
                        jointsurvey.IsBoundary = true;
                }
            }
            if (jointsurvey == null)
            {
                return NotFound();
            }
           
            return View(jointsurvey);
        }


        [AuthorizeContext(ViewAction.Download)]


        public async Task<IActionResult> JointSurveyList()
        {
            var result = await _jointsurveyService.GetAllJointSurvey();
            List<JointSurveyListDto> data = new List<JointSurveyListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new JointSurveyListDto()
                    {
                        Id = result[i].Id,
                        VillageName = result[i].Village == null ? "" : result[i].Village.Name,
                        KhasraNo = result[i].Khasra == null ? "" : result[i].Khasra.Name,
                        SitePosition = result[i].SitePosition,
                      
                        JointSurveyDate = Convert.ToDateTime(result[i].JointSurveyDate).ToString("dd-MMM-yyyy"),
                     
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }




        [HttpGet]
        public async Task<JsonResult> AllKhasraList(int? villageid)
        {
            villageid = villageid ?? 0;
            return Json(await _jointsurveyService.BindKhasra(Convert.ToInt32(villageid)));
        }


    }
}
