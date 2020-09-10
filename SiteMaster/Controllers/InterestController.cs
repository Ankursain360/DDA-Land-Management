using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using SiteMaster.Models;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.AspNetCore.Authorization;

namespace SiteMaster.Controllers
{
    public class InterestController : Controller
    {

        private readonly IInterestService _interestService;

        public InterestController(IInterestService interestService)
        {
            _interestService = interestService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _interestService.GetAllInterest();
            return View(result);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Interest interest)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var result = await _interestService.Create(interest);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        return View();
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(interest);

                    }
                }
                else
                {
                    return View(interest);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(interest);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _interestService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Interest interest)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _interestService.Update(id, interest);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var result1 = await _interestService.GetAllInterest();
                        return View("Index", result1);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(interest);

                    }
                }
                catch (Exception ex)
                {

                }
            }
            return View(interest);
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> Exist(int Id, string Name)
        {
            var result = await _interestService.CheckUniqueName(Id, Name);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Interest: {Name} already exist");
            }
        }


        public async Task<IActionResult> Delete(int id)  //Not in use
        {
            if (id == 0)
            {
                return NotFound();
            }

            var form = await _interestService.Delete(id);
            if (form == false)
            {
                return NotFound();
            }

            ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            return View(form);
        }

        public async Task<IActionResult> DeleteConfirmed(int id)  // Used to Perform Delete Functionality added by Renu
        {

            var result = await _interestService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                var result1 = await _interestService.GetAllInterest();
                return View("Index", result1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                var result1 = await _interestService.GetAllInterest();
                return View("Index", result1);
            }

        }

        public async Task<IActionResult> View(int id)
        {
            var Data = await _interestService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
    }

}
