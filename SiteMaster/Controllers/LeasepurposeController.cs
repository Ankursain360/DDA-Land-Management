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
using Dto.Master;
using Core.Enum;
using SiteMaster.Filters;
using Utility.Helper;

namespace SiteMaster.Controllers
{
    public class LeasepurposeController : BaseController
    {
        private readonly ILeasepurposeService _LeasepurposeService;


        public LeasepurposeController(ILeasepurposeService LeasepurposeService)
        {
            _LeasepurposeService = LeasepurposeService;
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            var list = await _LeasepurposeService.GetLeasepurposes();
            return View(list);
        }


        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] LeasepurposeSearchDto model)
        {
            var result = await _LeasepurposeService.GetpagedLeasepurpose(model);
            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Leasepurpose Leasepurpose)
        {
            try
            {


                if (ModelState.IsValid)
                {
                    var result = await _LeasepurposeService.Create(Leasepurpose);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _LeasepurposeService.GetLeasepurposes();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(Leasepurpose);
                    }
                }
                else
                {
                    return View(Leasepurpose);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(Leasepurpose);
            }
        }



        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _LeasepurposeService.FetchSingleResult(id);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Leasepurpose Leasepurpose)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _LeasepurposeService.Update(id, Leasepurpose);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _LeasepurposeService.GetLeasepurposes();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(Leasepurpose);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(Leasepurpose);
                }
            }
            else
            {
                return View(Leasepurpose);
            }
        }
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _LeasepurposeService.Delete(id);
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
            var list = await _LeasepurposeService.GetLeasepurposes();
            return View("Index", list);
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _LeasepurposeService.FetchSingleResult(id);




            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        public async Task<IActionResult> LeasePurposeList()
        {
            var result = await _LeasepurposeService.GetLeasepurposes();
            List<LeasePurposeListDto> data = new List<LeasePurposeListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new LeasePurposeListDto()
                    {
                        Id = result[i].Id,

                        PurposeUse = result[i].PurposeUse,


                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }


    }
}

