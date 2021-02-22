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


namespace NewLandAcquisition.Controllers
{
    public class NewLandSchemeController : Controller
    {
        private readonly INewlandSchemeService _schemeService;

        public NewLandSchemeController(INewlandSchemeService schemeService)
        {
            _schemeService = schemeService;
        }


        public async Task<IActionResult> Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] NewlandschemeSearchDto model)
        {
            var result = await _schemeService.GetPagedScheme(model);

            return PartialView("_List", result);
        }

        public async Task<IActionResult> Create()
        {
            Newlandscheme scheme = new Newlandscheme();
            scheme.IsActive = 1;
            return View(scheme);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Newlandscheme scheme)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = await _schemeService.Create(scheme);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _schemeService.GetAllScheme();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(scheme);
                    }
                }
                else
                {
                    return View(scheme);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(scheme);
            }
        }


        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _schemeService.FetchSingleResult(id);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Newlandscheme scheme)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _schemeService.Update(id, scheme);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _schemeService.GetAllScheme();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(scheme);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(scheme);
                }
            }
            else
            {
                return View(scheme);
            }
        }


        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _schemeService.Delete(id);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            var list = await _schemeService.GetAllScheme();
            return View("Index", list);
        }

        public async Task<IActionResult> View(int id)
        {
            var Data = await _schemeService.FetchSingleResult(id);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


    }
}
