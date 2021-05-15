using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcquiredLandInformationManagement.Filters;
using Core.Enum;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Utility.Helper;

namespace AcquiredLandInformationManagement.Controllers
{
    public class UnderSection22DetailsController : BaseController
    {
        private readonly IUndersection22Service _undersection22Service;

        public UnderSection22DetailsController(IUndersection22Service undersection22Service)
        {
            _undersection22Service = undersection22Service;
        }


        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {

            return View();

        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] Undersection22SearchDto model)
        {
            var result = await _undersection22Service.GetPagedUndersection22(model);
            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Undersection22 undersection22)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    undersection22.CreatedBy = SiteContext.UserId;
                    var result = await _undersection22Service.Create(undersection22);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _undersection22Service.GetAllUndersection22();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(undersection22);

                    }
                }
                else
                {
                    return View(undersection22);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(undersection22);
            }
        }
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _undersection22Service.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Undersection22 undersection22)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    undersection22.ModifiedBy = SiteContext.UserId;
                    var result = await _undersection22Service.Update(id, undersection22);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _undersection22Service.GetAllUndersection22();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(undersection22);

                    }
                }
                catch (Exception ex)
                {

                }
            }
            return View(undersection22);
        }

       

        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var form = await _undersection22Service.Delete(id);
            if (form == false)
            {
                return NotFound();
            }
            var result = await _undersection22Service.GetAllUndersection22();
            ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            return View("Index", result);
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _undersection22Service.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [AuthorizeContext(ViewAction.Download)]
       

        public async Task<IActionResult> Undersection22List()
        {
            var result = await _undersection22Service.GetAllUndersection22();
            List<Undersection22ListDto> data = new List<Undersection22ListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new Undersection22ListDto()
                    {
                        Id = result[i].Id,
                        NotificationNo = result[i].NotificationNo,
                        NotificationDate = Convert.ToDateTime(result[i].NotificationDate).ToString("dd-MMM-yyyy"),
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }
    }
}
