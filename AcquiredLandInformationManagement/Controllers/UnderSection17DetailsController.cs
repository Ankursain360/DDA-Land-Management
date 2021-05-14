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
using AcquiredLandInformationManagement.Filters;
using Core.Enum;
using Utility.Helper;
using Dto.Master;

namespace AcquiredLandInformationManagement.Controllers
{
    public class UnderSection17DetailsController : Controller
    {
        private readonly IUndersection17Service _undersection17Service;


        public UnderSection17DetailsController(IUndersection17Service undersection17Service)
        {
          _undersection17Service = undersection17Service;
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            var list = await _undersection17Service.GetAllUndersection17();
            return View(list);
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] UnderSection17SearchDto model)
        {
            var result = await _undersection17Service.GetPagedUndersection17(model);
            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()

        {
            Undersection17 undersection17 = new Undersection17();
            undersection17.IsActive = 1;


            undersection17.Undersection6List = await _undersection17Service.GetAllUndersection6List();
            return View(undersection17);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Undersection17 undersection17)
        {
            try
            {


                undersection17.Undersection6List = await _undersection17Service.GetAllUndersection6List();

                if (ModelState.IsValid)
                {
                    var result = await _undersection17Service.Create(undersection17);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _undersection17Service.GetAllUndersection17();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(undersection17);
                    }
                }
                else
                {
                    return View(undersection17);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(undersection17);
            }
        }



        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _undersection17Service.FetchSingleResult(id);
           

            Data.Undersection6List = await _undersection17Service.GetAllUndersection6List();

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Undersection17 undersection17)
        {
            undersection17.Undersection6List = await _undersection17Service.GetAllUndersection6List();
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _undersection17Service.Update(id, undersection17);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _undersection17Service.GetAllUndersection17();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(undersection17);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(undersection17);
                }
            }
            else
            {
                return View(undersection17);
            }
        }
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _undersection17Service.Delete(id);
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
            var list = await _undersection17Service.GetAllUndersection17();
            return View("Index", list);
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _undersection17Service.FetchSingleResult(id);

         
            Data.Undersection6List = await _undersection17Service.GetAllUndersection6List();


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> Undersection17List()
        {
            var result = await _undersection17Service.GetAllUndersection17();
            List<Undersection17ListDto> data = new List<Undersection17ListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new Undersection17ListDto()
                    {
                        Id = result[i].Id,
                        NotificationNo = result[i].Number,
                        NotificationDate = Convert.ToDateTime(result[i].NotificationDate).ToString("dd-MMM-yyyy"),
                        NotificationUS6 = result[i].UnderSection6 == null ? "" : result[i].UnderSection6.Number,
                        
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }

    }
}

