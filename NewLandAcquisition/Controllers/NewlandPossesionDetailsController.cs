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
using NewLandAcquisition.Filters;
using Core.Enum;

namespace NewLandAcquisition.Controllers
{
    
   public class NewlandPossesionDetailsController : BaseController
    {
        private readonly INewlandpossessiondetailsService _Possessiondetailservice;

        public NewlandPossesionDetailsController(INewlandpossessiondetailsService possessiondetailsService)
        {
            _Possessiondetailservice = possessiondetailsService;
        }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {

            return View();
        }


        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] NewlandpossesiondetailsSearchDto model)
        {
            var result = await _Possessiondetailservice.GetPagedNoPossessiondetails(model);

            return PartialView("_List", result);
        }



        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Newlandpossessiondetails newlandpossessiondetails = new Newlandpossessiondetails();
            newlandpossessiondetails.IsActive = 1;

            newlandpossessiondetails.KhasraList = await _Possessiondetailservice.BindKhasra(newlandpossessiondetails.VillageId);
            newlandpossessiondetails.VillageList = await _Possessiondetailservice.GetAllVillage();
            newlandpossessiondetails.PossKhasraList = await _Possessiondetailservice.GetAllPossKhasra();
            newlandpossessiondetails.us17List = await _Possessiondetailservice.GetAllus17();
            newlandpossessiondetails.us4List = await _Possessiondetailservice.GetAllus4();
            newlandpossessiondetails.us6List = await _Possessiondetailservice.GetAllus6();
            return View(newlandpossessiondetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Newlandpossessiondetails newlandpossessiondetails)
        {
            try
            {

                newlandpossessiondetails.KhasraList = await _Possessiondetailservice.BindKhasra(newlandpossessiondetails.VillageId);
                newlandpossessiondetails.VillageList = await _Possessiondetailservice.GetAllVillage();
                newlandpossessiondetails.PossKhasraList = await _Possessiondetailservice.GetAllPossKhasra();
                newlandpossessiondetails.us17List = await _Possessiondetailservice.GetAllus17();
                newlandpossessiondetails.us4List = await _Possessiondetailservice.GetAllus4();
                newlandpossessiondetails.us6List = await _Possessiondetailservice.GetAllus6();

                if (ModelState.IsValid)
                {
                    var result = await _Possessiondetailservice.Create(newlandpossessiondetails);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _Possessiondetailservice.GetAllPossessiondetails();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(newlandpossessiondetails);
                    }
                }
                else
                {
                    return View(newlandpossessiondetails);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(newlandpossessiondetails);
            }
        }


        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _Possessiondetailservice.FetchSingleResult(id);



            Data.KhasraList = await _Possessiondetailservice.BindKhasra(Data.VillageId);
            Data.VillageList = await _Possessiondetailservice.GetAllVillage();
            Data.PossKhasraList = await _Possessiondetailservice.GetAllPossKhasra();
            Data.us17List = await _Possessiondetailservice.GetAllus17();
            Data.us4List = await _Possessiondetailservice.GetAllus4();
            Data.us6List = await _Possessiondetailservice.GetAllus6();

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Newlandpossessiondetails newlandpossessiondetails)
        {

            newlandpossessiondetails.KhasraList = await _Possessiondetailservice.BindKhasra(newlandpossessiondetails.VillageId);
            newlandpossessiondetails.VillageList = await _Possessiondetailservice.GetAllVillage();
            newlandpossessiondetails.PossKhasraList = await _Possessiondetailservice.GetAllPossKhasra();
            newlandpossessiondetails.us17List = await _Possessiondetailservice.GetAllus17();
            newlandpossessiondetails.us4List = await _Possessiondetailservice.GetAllus4();
            newlandpossessiondetails.us6List = await _Possessiondetailservice.GetAllus6();
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _Possessiondetailservice.Update(id, newlandpossessiondetails);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _Possessiondetailservice.GetAllPossessiondetails();
                        return View("Index", list);
                        // return View("Index");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(newlandpossessiondetails);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(newlandpossessiondetails);
                }
            }
            else
            {
                return View(newlandpossessiondetails);
            }
        }
        [AuthorizeContext(ViewAction.Delete)]
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
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _Possessiondetailservice.FetchSingleResult(id);

            Data.KhasraList = await _Possessiondetailservice.BindKhasra(Data.VillageId);
            Data.VillageList = await _Possessiondetailservice.GetAllVillage();
            Data.PossKhasraList = await _Possessiondetailservice.GetAllPossKhasra();
            Data.us17List = await _Possessiondetailservice.GetAllus17();
            Data.us4List = await _Possessiondetailservice.GetAllus4();
            Data.us6List = await _Possessiondetailservice.GetAllus6();

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
