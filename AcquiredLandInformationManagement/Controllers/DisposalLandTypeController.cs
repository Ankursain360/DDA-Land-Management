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

namespace DDAPropertyREG.Controllers
{
    
    public class DisposalLandTypeController : Controller
    {
        private readonly IDisposallandtypeService _disposallandtypeService;

        public DisposalLandTypeController(IDisposallandtypeService disposallandtypeService)
        {
            _disposallandtypeService = disposallandtypeService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _disposallandtypeService.GetAllDisposallandtype();
            return View(result);

        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        
        public async Task<IActionResult> Create(Disposallandtype disposallandtype)
        {
            try
            {

                if (ModelState.IsValid)
                {
                  
                    var result = await _disposallandtypeService.Create(disposallandtype);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        return View();
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(disposallandtype);

                    }
                }
                else
                {
                    return View(disposallandtype);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(disposallandtype);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _disposallandtypeService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        
        public async Task<IActionResult> Edit(int id, Disposallandtype disposallandtype)
        {
            if (ModelState.IsValid)
            {
                try
                {


                    var result = await _disposallandtypeService.Update(id, disposallandtype);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        return View();
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(disposallandtype);

                    }
                }
                catch (Exception ex)
                {

                }
            }
            return View(disposallandtype);
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> Exist(int Id, string Name)
        {
            var result = await _disposallandtypeService.CheckUniqueName(Id, Name);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Disposallandtype: {Name} already exist");
            }
        }


        public async Task<IActionResult> Delete(int id)  //Not in use
        {
            if (id == 0)
            {
                return NotFound();
            }

            var form = await _disposallandtypeService.Delete(id);
            if (form == false)
            {
                return NotFound();
            }
            var result = await _disposallandtypeService.GetAllDisposallandtype();
            ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            return View("Index", result);
        }


        public async Task<IActionResult> View(int id)
        {
            var Data = await _disposallandtypeService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


    }
}