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

namespace AcquiredLandInformationManagement.Controllers
{
    public class JointSurveyController : Controller
    {
        private readonly IJointsurveyService _jointsurveyService;
        public JointSurveyController(IJointsurveyService jointsurveyService)
        {
            _jointsurveyService = jointsurveyService;
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()


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
            jointsurvey.KhasraList = await _jointsurveyService.BindKhasra();
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

                jointsurvey.KhasraList = await _jointsurveyService.BindKhasra();
                jointsurvey.VillageList = await _jointsurveyService.GetAllVillage();

                if (ModelState.IsValid)
                {
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
            Jointsurvey jointsurvey = new Jointsurvey();
            jointsurvey = await _jointsurveyService.FetchSingleResult(id);

            jointsurvey.KhasraList = await _jointsurveyService.BindKhasra();
            jointsurvey.VillageList = await _jointsurveyService.GetAllVillage();
           

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

            Jointsurvey jointsurvey = new Jointsurvey();
            var Data = await _jointsurveyService.FetchSingleResult(id);

            Data.KhasraList = await _jointsurveyService.BindKhasra();
            Data.VillageList = await _jointsurveyService.GetAllVillage();


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }



    }
}
