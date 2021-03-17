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

namespace AcquiredLandInformationManagement.Controllers
{
    public class PossessionDetailController : Controller
    {
        private readonly IPossessiondetailsService _Possessiondetailservice;

        public PossessionDetailController(IPossessiondetailsService possessiondetailsService)
        {
            _Possessiondetailservice = possessiondetailsService;
        }

        public IActionResult Index()
        {

            return View();
        }


        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] PossessiondetailsSearchDto model)
        {
            var result = await _Possessiondetailservice.GetPagedNoPossessiondetails(model);

            return PartialView("_List", result);
        }




        public async Task<IActionResult> Create()
        {
            Possessiondetails undersection4plot = new Possessiondetails();
            undersection4plot.IsActive = 1;
         
            undersection4plot.KhasraList = await _Possessiondetailservice.BindKhasra(undersection4plot.VillageId);
            undersection4plot.VillageList = await _Possessiondetailservice.GetAllVillage();

            return View(undersection4plot);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Possessiondetails undersection4plot)
        {
            try
            {
             
                undersection4plot.KhasraList = await _Possessiondetailservice.BindKhasra(undersection4plot.VillageId);
                undersection4plot.VillageList = await _Possessiondetailservice.GetAllVillage();


                if (ModelState.IsValid)
                {
                    var result = await _Possessiondetailservice.Create(undersection4plot);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _Possessiondetailservice.GetAllPossessiondetails();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(undersection4plot);
                    }
                }
                else
                {
                    return View(undersection4plot);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(undersection4plot);
            }
        }



        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _Possessiondetailservice.FetchSingleResult(id);


         
            Data.KhasraList = await _Possessiondetailservice.BindKhasra(Data.VillageId);
            Data.VillageList = await _Possessiondetailservice.GetAllVillage();


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Possessiondetails undersection4plot)
        {

            undersection4plot.KhasraList = await _Possessiondetailservice.BindKhasra(undersection4plot.VillageId);
            undersection4plot.VillageList = await _Possessiondetailservice.GetAllVillage();

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _Possessiondetailservice.Update(id, undersection4plot);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _Possessiondetailservice.GetAllPossessiondetails();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(undersection4plot);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(undersection4plot);
                }
            }
            else
            {
                return View(undersection4plot);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _Possessiondetailservice.Delete(id);
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
            var list = await _Possessiondetailservice.GetAllPossessiondetails();
            return View("Index", list);
        }

        public async Task<IActionResult> View(int id)
        {
            var Data = await _Possessiondetailservice.FetchSingleResult(id);
         
            Data.KhasraList = await _Possessiondetailservice.BindKhasra(Data.VillageId);
            Data.VillageList = await _Possessiondetailservice.GetAllVillage();


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


        [HttpGet]
        public async Task<JsonResult> GetKhasraList(int? villageId)
        {
            villageId = villageId ?? 0;
            return Json(await _Possessiondetailservice.BindKhasra(Convert.ToInt32(villageId)));
        }




        [HttpGet]
        public async Task<JsonResult> GetAreaList(int? khasraid)
        {
            khasraid = khasraid ?? 0;

            return Json(await _Possessiondetailservice.FetchSingleKhasraResult(Convert.ToInt32(khasraid)));
        }



    }
}
