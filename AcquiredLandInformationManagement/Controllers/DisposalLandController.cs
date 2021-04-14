using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcquiredLandInformationManagement.Filters;
using Core.Enum;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Utility.Helper;

namespace AcquiredLandInformationManagement.Controllers
{
  
    public class DisposalLandController : Controller
    {
        private readonly IDisposallandService _disposallandService;

        public DisposalLandController(IDisposallandService disposallandService)
        {
            _disposallandService = disposallandService;
        }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DisposalLandSearchDto model)
        {
            var result = await _disposallandService.GetPagedDisposalLand(model);
            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Disposalland disposalland = new Disposalland();
           
            disposalland.UtilizationtypeList = await _disposallandService.GetAllUtilizationtype();
            disposalland.VillageList = await _disposallandService.GetAllVillage();
            disposalland.KhasraList = await _disposallandService.GetAllKhasra(disposalland.VillageId);

            return View(disposalland);
        }


        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Disposalland disposalland)
        {
            try
            {
                disposalland.UtilizationtypeList = await _disposallandService.GetAllUtilizationtype();
                disposalland.VillageList = await _disposallandService.GetAllVillage();
                disposalland.KhasraList = await _disposallandService.GetAllKhasra(disposalland.VillageId);
                if (ModelState.IsValid)
                {


                    var result = await _disposallandService.Create(disposalland);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _disposallandService.GetAllDisposalland();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(disposalland);

                    }
                }
                else
                {
                    return View(disposalland);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(disposalland);
            }
        }
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {

            var Data = await _disposallandService.FetchSingleResult(id);

            Data.UtilizationtypeList = await _disposallandService.GetAllUtilizationtype();
            Data.VillageList = await _disposallandService.GetAllVillage();
            Data.KhasraList = await _disposallandService.GetAllKhasra(Data.VillageId);


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Disposalland disposalland)
        {
            disposalland.UtilizationtypeList = await _disposallandService.GetAllUtilizationtype();
            disposalland.VillageList = await _disposallandService.GetAllVillage();
            disposalland.KhasraList = await _disposallandService.GetAllKhasra(disposalland.VillageId);
            if (ModelState.IsValid)
            {
                try
                {

                    var result = await _disposallandService.Update(id, disposalland);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _disposallandService.GetAllDisposalland();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(disposalland);

                    }
                }
                catch (Exception ex)
                {

                }
            }
            return View(disposalland);
        }
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var form = await _disposallandService.Delete(id);
            if (form == false)
            {
                return NotFound();
            }
            var result = await _disposallandService.GetAllDisposalland();
            ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            return View("Index", result);
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _disposallandService.FetchSingleResult(id);

            Data.UtilizationtypeList = await _disposallandService.GetAllUtilizationtype();
            Data.VillageList = await _disposallandService.GetAllVillage();
            Data.KhasraList = await _disposallandService.GetAllKhasra(Data.VillageId);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> Download()
        {
            List<Disposalland> result = await _disposallandService.GetAllDisposalland();
            var memory = ExcelHelper.CreateExcel(result);
            string sFileName = @"LandDisposal.xlsx";
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

        }

        [HttpGet]
        public async Task<JsonResult> GetKhasraList(int? villageId)
        {
            villageId = villageId ?? 0;
            return Json(await _disposallandService.GetAllKhasra(Convert.ToInt32(villageId)));
        }
    }
}