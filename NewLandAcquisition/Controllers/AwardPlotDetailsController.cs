using Core.Enum;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using NewLandAcquisition.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.Helper;
using NewLandAcquisition.Controllers;

namespace AcquiredLandInformationManagement.Controllers
{
    public class AwardPlotDetailsController : BaseController
    {

        private readonly INewlandawardplotdetailsService _awardplotDetailService;
        public AwardPlotDetailsController(INewlandawardplotdetailsService awardplotDetailService)
        {
            _awardplotDetailService = awardplotDetailService;
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] NewlandawardplotdetailsSearchDto model)
        {
            var result = await _awardplotDetailService.GetPagedAwardplotdetails(model);

            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Newlandawardplotdetails awardplotdetails = new Newlandawardplotdetails();
            awardplotdetails.IsActive = 1;
            awardplotdetails.NewlandAwardmasterList = await _awardplotDetailService.GetAllAWardmaster();
            awardplotdetails.NewlandKhasraList = await _awardplotDetailService.GetAllKhasra(awardplotdetails.VillageId);
            awardplotdetails.NewlandVillageList = await _awardplotDetailService.GetAllVillage();

            return View(awardplotdetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Newlandawardplotdetails awardplotdetails)
        {
            try
            {
                awardplotdetails.NewlandAwardmasterList = await _awardplotDetailService.GetAllAWardmaster();
                awardplotdetails.NewlandKhasraList = await _awardplotDetailService.GetAllKhasra(awardplotdetails.VillageId);
                awardplotdetails.NewlandVillageList = await _awardplotDetailService.GetAllVillage();

                if (ModelState.IsValid)
                {
                    var result = await _awardplotDetailService.Create(awardplotdetails);
                      if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _awardplotDetailService.GetAwardplotdetails();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(awardplotdetails);
                    }
                }
                else
                {
                    return View(awardplotdetails);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(awardplotdetails);
            }
        }


        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _awardplotDetailService.FetchSingleResult(id);


            Data.NewlandAwardmasterList = await _awardplotDetailService.GetAllAWardmaster();
            Data.NewlandKhasraList = await _awardplotDetailService.GetAllKhasra(Data.VillageId);
            Data.NewlandVillageList = await _awardplotDetailService.GetAllVillage();


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Newlandawardplotdetails awardplotdetails)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _awardplotDetailService.Update(id, awardplotdetails);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _awardplotDetailService.GetAwardplotdetails();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(awardplotdetails);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(awardplotdetails);
                }
            }
            else
            {
                return View(awardplotdetails);
            }
        }
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _awardplotDetailService.Delete(id);
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
            var list = await _awardplotDetailService.GetAwardplotdetails();
            return View("Index", list);
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _awardplotDetailService.FetchSingleResult(id);
            Data.NewlandAwardmasterList = await _awardplotDetailService.GetAllAWardmaster();
            Data.NewlandKhasraList = await _awardplotDetailService.GetAllKhasra(Data.VillageId);
            Data.NewlandVillageList = await _awardplotDetailService.GetAllVillage();


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
            return Json(await _awardplotDetailService.GetAllKhasra(Convert.ToInt32(villageId)));
        }

        [HttpGet]
        public async Task<JsonResult> GetKhasraAreaList(int? khasraid)
        {
            khasraid = khasraid ?? 0;

            return Json(await _awardplotDetailService.FetchSingleKhasraResult(Convert.ToInt32(khasraid)));
        }



    }
}
