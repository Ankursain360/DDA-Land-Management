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
using Service.ApplicationService;

namespace LandInventory.Controllers
{
    public class FormN1Controller : Controller
    {
        private readonly INazullandService _nazullandService;

       
        public FormN1Controller(INazullandService nazullandService)
        {
            _nazullandService = nazullandService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _nazullandService.GetAllNazulland();
            return View(result);
        }


        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] NazullandSearchDto model)
        {
            var result = await _nazullandService.GetPagedNazulland(model);
            return PartialView("_List", result);
        }
        public async Task<IActionResult> Create()
        {
            Nazulland nazulland = new Nazulland();
            nazulland.IsActive = 1;
            nazulland.DivisionList = await _nazullandService.GetAllDivision();
            return View(nazulland);
        }


      


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Nazulland nazulland)
        {
            try
            {
                nazulland.DivisionList = await _nazullandService.GetAllDivision();
                if (ModelState.IsValid)
                {


                    var result = await _nazullandService.Create(nazulland);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _nazullandService.GetAllNazulland();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(nazulland);

                    }
                }
                else
                {
                    return View(nazulland);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(nazulland);
            }
        }
        public async Task<IActionResult> Edit(int id)
        {

            var Data = await _nazullandService.FetchSingleResult(id);
            Data.DivisionList = await _nazullandService.GetAllDivision();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Nazulland nazulland)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var result = await _nazullandService.Update(id, nazulland);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _nazullandService.GetAllNazulland();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(nazulland);

                    }
                }
                catch (Exception ex)
                {

                }
            }
            return View(nazulland);
        }

        //[AcceptVerbs("Get", "Post")]
        //[AllowAnonymous]
        //public async Task<IActionResult> Exist(int Id, string Name)
        //{
        //    var result = await _pageService.CheckUniqueName(Id, Name);
        //    if (result == false)
        //    {
        //        return Json(true);
        //    }
        //    else
        //    {
        //        return Json($"Page: {Name} already exist");
        //    }
        //}


        public async Task<IActionResult> Delete(int id)  //Not in use
        {
            if (id == 0)
            {
                return NotFound();
            }

            var form = await _nazullandService.Delete(id);
            if (form == false)
            {
                return NotFound();
            }
            var result = await _nazullandService.GetAllNazulland();
            ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            return View("Index", result);
        }

        public async Task<IActionResult> View(int id)
        {
            var Data = await _nazullandService.FetchSingleResult(id);
            Data.DivisionList = await _nazullandService.GetAllDivision();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


    }
}
