using System;
using System.Collections.Generic;
using Dto.Master;
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
   
      public class BookTransferLandController : Controller
    {
        private readonly IBooktransferlandService _booktransferlandService;

        public BookTransferLandController(IBooktransferlandService booktransferlandService)
        {
            _booktransferlandService = booktransferlandService;
        }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
           
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] BooktransferlandSearchDto model)
        {
            var result = await _booktransferlandService.GetPagedBooktransferland(model);
            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Booktransferland booktransferland = new Booktransferland();
            
            booktransferland.OtherlandnotificationList = await _booktransferlandService.GetAllOtherLandNotification();
            booktransferland.LocalityList = await _booktransferlandService.GetAllLocality();
           
            booktransferland.KhasraList = await _booktransferlandService.BindKhasra(booktransferland.LocalityId);
           
            return View(booktransferland);
        }
        
        
        [HttpGet]
        public async Task<JsonResult> GetKhasraList(int? LocalityId)
        {
            LocalityId = LocalityId ?? 0;
            return Json(await _booktransferlandService.BindKhasra(Convert.ToInt32(LocalityId)));
        }
       

        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Booktransferland booktransferland)
        {
            try
            {
                booktransferland.OtherlandnotificationList = await _booktransferlandService.GetAllOtherLandNotification();
                //booktransferland.LandNotificationList = await _booktransferlandService.GetAllLandNotification();
                booktransferland.LocalityList = await _booktransferlandService.GetAllLocality();
                booktransferland.KhasraList = await _booktransferlandService.BindKhasra(booktransferland.LocalityId);
                if (ModelState.IsValid)
                {


                    var result = await _booktransferlandService.Create(booktransferland);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _booktransferlandService.GetAllBooktransferland();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(booktransferland);

                    }
                }
                else
                {
                    return View(booktransferland);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(booktransferland);
            }
        }
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {

            var Data = await _booktransferlandService.FetchSingleResult(id);
                       
            Data.OtherlandnotificationList = await _booktransferlandService.GetAllOtherLandNotification();
            Data.LocalityList = await _booktransferlandService.GetAllLocality();
            Data.KhasraList = await _booktransferlandService.BindKhasra(Data.LocalityId);


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Booktransferland booktransferland)
        {
            booktransferland.OtherlandnotificationList = await _booktransferlandService.GetAllOtherLandNotification();
            booktransferland.LocalityList = await _booktransferlandService.GetAllLocality();
            booktransferland.KhasraList = await _booktransferlandService.BindKhasra(booktransferland.LocalityId);
            if (ModelState.IsValid)
            {
                try
                {

                    var result = await _booktransferlandService.Update(id, booktransferland);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _booktransferlandService.GetAllBooktransferland();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(booktransferland);

                    }
                }
                catch (Exception ex)
                {

                }
            }
            return View(booktransferland);
        }


        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _booktransferlandService.Delete(id);
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
            var list = await _booktransferlandService.GetAllBooktransferland();
            return View("Index", list);
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _booktransferlandService.FetchSingleResult(id);

            Data.OtherlandnotificationList = await _booktransferlandService.GetAllOtherLandNotification();
            Data.LocalityList = await _booktransferlandService.GetAllLocality();
            Data.KhasraList = await _booktransferlandService.BindKhasra(Data.LocalityId);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [AuthorizeContext(ViewAction.Download)]

        public async Task<IActionResult> BooktransferlandList([FromBody] BooktransferlandSearchDto model)
        {
            var result = await _booktransferlandService.GetALlBooktransferlandList(model);
            List<BooktransferlandListDto> data = new List<BooktransferlandListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new BooktransferlandListDto()
                    {
                        Id = result[i].Id,
                        NotificationNo = result[i].OtherLandNotification == null ? "" : result[i].OtherLandNotification.NotificationNumber,
                        NotificationDate = Convert.ToDateTime(result[i].NotificationDate).ToString("dd-MMM-yyyy"),
                        Part = result[i].Part,
                        Area = result[i].Area.ToString(),
                        StatusOfLand = result[i].StatusOfLand,
                        DateofPossession = Convert.ToDateTime(result[i].DateofPossession).ToString("dd-MMM-yyyy"),
                        Remarks = result[i].Remarks,
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }
            var memory = ExcelHelper.CreateExcel(data);
            TempData["file"] = memory;
            return Ok();

        }

        [HttpGet]
        [AuthorizeContext(ViewAction.Download)]
        public virtual ActionResult download()
        {
            byte[] data = TempData["file"] as byte[];
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}
