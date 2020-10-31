using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;

namespace SiteMaster.Controllers
{
    public class PageController : BaseController
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
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] PageSearchDto model)
        {
            var result = await _pageService.GetPagedPage(model);
            return PartialView("_List", result);
        }
        public async Task<IActionResult> Create()
        {
            Page page = new Page();
            
            page.MenuList = await _pageService.GetAllMenu();
            return View(page);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Page page)
        {
            try
            {
                page.MenuList = await _pageService.GetAllMenu();
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
           
            Data.MenuList = await _pageService.GetAllMenu();
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
        public async Task<IActionResult> Exist(int Id, string Name,int MenuId)
        {
            var result = await _pageService.CheckUniqueName(Id, Name,MenuId);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Page: {Name} already exist");
            }
        }



        public async Task<IActionResult> View(int id)
        {
            var Data = await _pageService.FetchSingleResult(id);
            Data.MenuList = await _pageService.GetAllMenu();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        public async Task<IActionResult> Delete(int id)  // Used to Perform Delete Functionality 
        {
            try
            {

                var result = await _pageService.Delete(id);
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
            var list = await _pageService.GetAllPage();
            return View("Index", list);
        }
    }
}