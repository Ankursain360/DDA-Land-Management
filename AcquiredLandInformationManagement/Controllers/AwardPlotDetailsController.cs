using Core.Enum;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Dto.Master;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using AcquiredLandInformationManagement.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.Helper;
using Microsoft.AspNetCore.Http;

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
                        return View("Create", awardplotdetails);
                       
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
                       // return View("Index", list);
                        return View("Edit", awardplotdetails);
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

       

        public async Task<PartialViewResult> AwardView([FromBody] AwardViewSearchDto model)
        {
            var Data = await _awardplotDetailService.GetAllAwardViewList(model);

            return PartialView("_ListAward", Data);
        }


        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> AwardplotdetailsList([FromBody] AwardPlotDetailSearchDto model)
        {
            var result =  await _awardplotDetailService.GetAllAwardplotdetailsList(model);
            List<AwardplotdetailsListtDto> data = new List<AwardplotdetailsListtDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new AwardplotdetailsListtDto()
                    {
                        Id = result[i].Id,
                        AwardNumber = result[i].AwardMaster == null ? "" : result[i].AwardMaster.AwardNumber,
                        VillageName = result[i].Village == null ? "" : result[i].Village.Name,
                        KhasraNo = result[i].Khasra == null ? "" : result[i].Khasra.Name,

                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive"
                    }); 
                }
            }

            //var memory = ExcelHelper.CreateExcel(data);
            //TempData["file"] = memory;
            //return Ok();
            var memory = ExcelHelper.CreateExcel(data);
            HttpContext.Session.Set("file", memory);
            return Ok();

        }

        [HttpGet]
        public virtual ActionResult download()
        {
            //byte[] data = TempData["file"] as byte[];
            //return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            byte[] data = HttpContext.Session.Get("file") as byte[];
            HttpContext.Session.Remove("file");
            // var dem = Decompress(data);
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}
