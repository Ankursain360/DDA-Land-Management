using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;

namespace AcquiredLandInformationManagement.Controllers
{
   
      public class BookTransferLandController : Controller
    {
        private readonly IBooktransferlandService _booktransferlandService;

        public BookTransferLandController(IBooktransferlandService booktransferlandService)
        {
            _booktransferlandService = booktransferlandService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _booktransferlandService.GetAllBooktransferland();
            return View(result);
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] BooktransferlandSearchDto model)
        {
            var result = await _booktransferlandService.GetPagedBooktransferland(model);
            return PartialView("_List", result);
        }

        public async Task<IActionResult> Create()
        {
            Booktransferland booktransferland = new Booktransferland();
            //proposalplotdetails.IsActive = 1;
            booktransferland.LandNotificationList = await _booktransferlandService.GetAllLandNotification();
            booktransferland.LocalityList = await _booktransferlandService.GetAllLocality();
            booktransferland.KhasraList = await _booktransferlandService.GetAllKhasra();

            return View(booktransferland);
        }


        [HttpPost]

        public async Task<IActionResult> Create(Booktransferland booktransferland)
        {
            try
            {
                booktransferland.LandNotificationList = await _booktransferlandService.GetAllLandNotification();
                booktransferland.LocalityList = await _booktransferlandService.GetAllLocality();
                booktransferland.KhasraList = await _booktransferlandService.GetAllKhasra();

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
        public async Task<IActionResult> Edit(int id)
        {

            var Data = await _booktransferlandService.FetchSingleResult(id);

            
            Data.LandNotificationList = await _booktransferlandService.GetAllLandNotification();
            Data.LocalityList = await _booktransferlandService.GetAllLocality();
            Data.KhasraList = await _booktransferlandService.GetAllKhasra();


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(int id, Booktransferland booktransferland)
        {
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

        public async Task<IActionResult> View(int id)
        {
            var Data = await _booktransferlandService.FetchSingleResult(id);

            Data.LandNotificationList = await _booktransferlandService.GetAllLandNotification();
            Data.LocalityList = await _booktransferlandService.GetAllLocality();
            Data.KhasraList = await _booktransferlandService.GetAllKhasra();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
    }
}
