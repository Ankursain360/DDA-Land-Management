using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enum;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using LeaseDetails.Filters;
using Utility.Helper;

namespace LeaseDetails.Controllers
{

    public class InterestrateController : BaseController
    {
        private readonly IInterestrateService _InterestrateService;

        public InterestrateController(IInterestrateService InterestrateService)
        {
            _InterestrateService = InterestrateService;
        }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] InterestrateSearchDto model)
        {

            var result = await _InterestrateService.GetPagedInterestrate(model);
            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Interestrate rate = new Interestrate();
            rate.IsActive = 1;
            rate.PropertyTypeList = await _InterestrateService.GetAllPropertyType();
            return View(rate);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Interestrate rate)
        {
            rate.PropertyTypeList = await _InterestrateService.GetAllPropertyType();
            try
            {

                if (ModelState.IsValid)
                {

                    rate.CreatedBy = SiteContext.UserId;
                    var result = await _InterestrateService.Create(rate);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        //return View();
                        var list = await _InterestrateService.GetAllInterestrate();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(rate);

                    }
                }
                else
                {
                    return View(rate);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(rate);
            }
        }
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _InterestrateService.FetchSingleResult(id);
            Data.PropertyTypeList = await _InterestrateService.GetAllPropertyType();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Interestrate rate)
        {
            rate.PropertyTypeList = await _InterestrateService.GetAllPropertyType();
            if (ModelState.IsValid)
            {
                try
                {

                    rate.ModifiedBy = SiteContext.UserId;
                    var result = await _InterestrateService.Update(id, rate);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);

                        var list = await _InterestrateService.GetAllInterestrate();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(rate);

                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(rate);

                }
            }
            return View(rate);
        }

        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _InterestrateService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                var result1 = await _InterestrateService.GetAllInterestrate();
                return View("Index", result1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                var result1 = await _InterestrateService.GetAllInterestrate();
                return View("Index", result1);
            }
        }

        //  [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _InterestrateService.FetchSingleResult(id);
            Data.PropertyTypeList = await _InterestrateService.GetAllPropertyType();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> Download()
        {
            List<Interestrate> result = await _InterestrateService.GetAllInterestrate();
            var memory = ExcelHelper.CreateExcel(result);
            string sFileName = @"Interestrate.xlsx";
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

        }
    }
}
