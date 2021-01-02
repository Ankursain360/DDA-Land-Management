using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Dto.Search;
using FileDataLoading.Filters;
using Core.Enum;


namespace FileDataLoading.Controllers
{
    public class AlmirahController : BaseController
    {
        private readonly IAlmirahService _almirahService;


        public AlmirahController(IAlmirahService almiraService)
        {
            _almirahService = almiraService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _almirahService.GetAllAlmirah();
            return View(result);
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] AlmirahSearchDto model)
        {
            var result = await _almirahService.GetPagedAlmirah(model);
            return PartialView("_List", result);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Create(Almirah almirha)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = await _almirahService.Create(almirha);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _almirahService.GetAllAlmirah();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(almirha);

                    }
                }
                else
                {
                    return View(almirha);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(almirha);
            }
        }


        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _almirahService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Almirah almirah)
        {
            if (ModelState.IsValid)
            {
                var result = await _almirahService.Update(id, almirah);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);

                    var list = await _almirahService.GetAllAlmirah();
                    return View("Index", list);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(almirah);
                }
            }
            return View(almirah);
        }



        public async Task<IActionResult> View(int id)
        {
            var Data = await _almirahService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        public async Task<IActionResult> DeleteConfirmed(int id)  // Used to Perform Delete Functionality added by Pankaj
        {
            var result = await _almirahService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            return RedirectToAction("Index", "Almirah");
        }

        public async Task<IActionResult> Delete(int id)  // Used to Perform Delete Functionality added by Pankaj
        {
            try
            {
                var result = await _almirahService.Delete(id);
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
            var list = await _almirahService.GetAllAlmirah();
            return View("Index", list);
        }
    }
}
