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
using Dto.Master;
using Utility.Helper;

namespace NewLandAcquisition.Controllers
{
    public class NewLandEnhanceCompensationController : BaseController
    {
        private readonly INewLandEnhanceCompensationService _newLandEnhanceCompensationService;


        public NewLandEnhanceCompensationController(INewLandEnhanceCompensationService newLandEnhanceCompensationService)
        {
            _newLandEnhanceCompensationService = newLandEnhanceCompensationService;
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            var list = await _newLandEnhanceCompensationService.GetAllNewlandenhancecompensation();
            return View(list);
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] NewlandenhancecompensationSearchDto model)
        {
            var result = await _newLandEnhanceCompensationService.GetPagedNewlandenhancecompensation(model);
            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()

        {
            Newlandenhancecompensation newlandenhancecompensation = new Newlandenhancecompensation();
            newlandenhancecompensation.IsActive = 1;


            newlandenhancecompensation.VillageList = await _newLandEnhanceCompensationService.GetAllVillage();
            newlandenhancecompensation.KhasraList = await _newLandEnhanceCompensationService.GetAllKhasra(newlandenhancecompensation.VillageId);

            return View(newlandenhancecompensation);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Newlandenhancecompensation newlandenhancecompensation)
        {
            try
            {


                newlandenhancecompensation.VillageList = await _newLandEnhanceCompensationService.GetAllVillage();
                newlandenhancecompensation.KhasraList = await _newLandEnhanceCompensationService.GetAllKhasra(newlandenhancecompensation.VillageId);

                if (ModelState.IsValid)
                {
                    var result = await _newLandEnhanceCompensationService.Create(newlandenhancecompensation);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _newLandEnhanceCompensationService.GetAllNewlandenhancecompensation();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(newlandenhancecompensation);
                    }
                }
                else
                {
                    return View(newlandenhancecompensation);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(newlandenhancecompensation);
            }
        }



        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _newLandEnhanceCompensationService.FetchSingleResult(id);
            Data.VillageList = await _newLandEnhanceCompensationService.GetAllVillage();
            Data.KhasraList = await _newLandEnhanceCompensationService.GetAllKhasra(Data.VillageId);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Newlandenhancecompensation newlandenhancecompensation)
        {

          newlandenhancecompensation.VillageList = await _newLandEnhanceCompensationService.GetAllVillage();
            newlandenhancecompensation.KhasraList = await _newLandEnhanceCompensationService.GetAllKhasra(newlandenhancecompensation.VillageId);
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _newLandEnhanceCompensationService.Update(id, newlandenhancecompensation);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _newLandEnhanceCompensationService.GetAllNewlandenhancecompensation();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(newlandenhancecompensation);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(newlandenhancecompensation);
                }
            }
            else
            {
                return View(newlandenhancecompensation);
            }
        }
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _newLandEnhanceCompensationService.Delete(id);
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
            var list = await _newLandEnhanceCompensationService.GetAllNewlandenhancecompensation();
            return View("Index", list);
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _newLandEnhanceCompensationService.FetchSingleResult(id);

            Data.VillageList = await _newLandEnhanceCompensationService.GetAllVillage();
            Data.KhasraList = await _newLandEnhanceCompensationService.GetAllKhasra(Data.VillageId);


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
            return Json(await _newLandEnhanceCompensationService.GetAllKhasra(Convert.ToInt32(villageId)));
        }

        [HttpGet]
        public async Task<JsonResult> GetKhasraAreaList(int? khasraid)
        {
            khasraid = khasraid ?? 0;

            return Json(await _newLandEnhanceCompensationService.FetchSingleKhasraResult(Convert.ToInt32(khasraid)));
        }


        public async Task<IActionResult> NewLandEnhanceCompensationList()
        {
            var result = await _newLandEnhanceCompensationService.GetAllNewlandenhancecompensation();
            List<NewLandEnhanceCompensationListDto> data = new List<NewLandEnhanceCompensationListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new NewLandEnhanceCompensationListDto()
                    {
                        Id = result[i].Id,
                        DemandListNo = result[i].DemandListNo,
                        VillageName = result[i].Village == null ? "" : result[i].Village.Name.ToString(),
                        KhasraName = result[i].Khasra == null ? "" : result[i].Khasra.Name.ToString(),
                        Area = result[i].Bigha.ToString() + '-' + result[i].Biswa.ToString() + '-' + result[i].Biswanshi.ToString(),
                        PayableAppealable = result[i].PayableAppealable,
                        CaseInvolesWhichCourt=result[i].CaseInvolesWhichCourt,
                        IsActive = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }






    }
}

