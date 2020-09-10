using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;

namespace SiteMaster.Controllers
{
    public class PageController : Controller
    {
        private readonly IPageService _pageService;

        public PageController(IPageService pageService)
        {
            _pageService = pageService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _pageService.GetAllPage();
            return View(result);
        }

        public async Task<IActionResult> Create()
        {
            Page page = new Page();
            page.IsActive = 1;
            page.ModuleList = await _pageService.GetAllModule();
            return View(page);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Page page)
        {
            try
            {
                page.ModuleList = await _pageService.GetAllModule();
                if (ModelState.IsValid)
                {
                   

                    var result = await _pageService.Create(page);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _pageService.GetAllPage();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(page);

                    }
                }
                else
                {
                    return View(page);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(page);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {

            var Data = await _pageService.FetchSingleResult(id);
            Data.ModuleList = await _pageService.GetAllModule();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Page page)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var result = await _pageService.Update(id, page);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _pageService.GetAllPage();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(page);

                    }
                }
                catch (Exception ex)
                {

                }
            }
            return View(page);
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> Exist(int Id, string Name)
        {
            var result = await _pageService.CheckUniqueName(Id, Name);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Page: {Name} already exist");
            }
        }


        public async Task<IActionResult> Delete(int id)  //Not in use
        {
            if (id == 0)
            {
                return NotFound();
            }

            var form = await _pageService.Delete(id);
            if (form == false)
            {
                return NotFound();
            }
            var result = await _pageService.GetAllPage();
            ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            return View("Index",result);
        }

        public async Task<IActionResult> View(int id)
        {
            var Data = await _pageService.FetchSingleResult(id);
            Data.ModuleList = await _pageService.GetAllModule();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


    }
}