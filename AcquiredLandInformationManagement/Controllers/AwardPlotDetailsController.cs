using Core.Enum;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using AcquiredLandInformationManagement.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.Helper;
namespace AcquiredLandInformationManagement.Controllers
{
    public class AwardPlotDetailsController : BaseController
    {

        private readonly IAwardplotDetailService _awardplotDetailService;
        public AwardPlotDetailsController(IAwardplotDetailService awardplotDetailService)
        {
            _awardplotDetailService = awardplotDetailService;
        }

        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] AwardPlotDetailSearchDto model)
        {
            var result = await _awardplotDetailService.GetPagedAwardplotdetails(model);

            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Awardplotdetails awardplotdetails = new Awardplotdetails();
            awardplotdetails.IsActive = 1;
            awardplotdetails.AwardmasterList = await _awardplotDetailService.GetAllAWardmaster();
            awardplotdetails.KhasraList = await _awardplotDetailService.BindKhasra(awardplotdetails.VillageId); 
            awardplotdetails.VillageList = await _awardplotDetailService.GetAllVillage();

            return View(awardplotdetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Awardplotdetails awardplotdetails)
        {
            try
            {
                awardplotdetails.AwardmasterList = await _awardplotDetailService.GetAllAWardmaster();
                awardplotdetails.KhasraList = await _awardplotDetailService.BindKhasra(awardplotdetails.VillageId);
                awardplotdetails.VillageList = await _awardplotDetailService.GetAllVillage();

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


            Data.AwardmasterList = await _awardplotDetailService.GetAllAWardmaster();
            Data.KhasraList = await _awardplotDetailService.BindKhasra(Data.VillageId);
            Data.VillageList = await _awardplotDetailService.GetAllVillage();


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Awardplotdetails awardplotdetails)
        {


            awardplotdetails.AwardmasterList = await _awardplotDetailService.GetAllAWardmaster();
            awardplotdetails.KhasraList = await _awardplotDetailService.BindKhasra(awardplotdetails.VillageId);
            awardplotdetails.VillageList = await _awardplotDetailService.GetAllVillage();

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
            Data.AwardmasterList = await _awardplotDetailService.GetAllAWardmaster();
            Data.KhasraList = await _awardplotDetailService.BindKhasra(Data.VillageId);
            Data.VillageList = await _awardplotDetailService.GetAllVillage();


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


        [HttpGet]
        public async Task<JsonResult> GetKhasraList(int? VillageId)
        {
            VillageId = VillageId ?? 0;
            return Json(await _awardplotDetailService.BindKhasra(Convert.ToInt32(VillageId)));
        }

      
        [HttpGet]
        public async Task<JsonResult> GetAreaList(int? khasraid)
        {
            khasraid = khasraid ?? 0;

            return Json(await _awardplotDetailService.FetchSingleKhasraResult(Convert.ToInt32(khasraid)));
        }





    }
}
