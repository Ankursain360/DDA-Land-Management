

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

    public class DocumentchargesController : BaseController
    {
        private readonly IDocumentchargesServices _documentchargesService;

        public DocumentchargesController(IDocumentchargesServices documentchargesService)
        {
            _documentchargesService = documentchargesService;
        }
        //  [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DocumentchargesSearchDto model)
        {

            var result = await _documentchargesService.GetPagedDocumentcharges(model);
            return PartialView("_List", result);
        }
        //   [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Documentcharges charge = new Documentcharges();
            charge.IsActive = 1;
            charge.PropertyTypeList = await _documentchargesService.GetAllPropertyType();
            return View(charge);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //  [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Documentcharges charge)
        {
            charge.PropertyTypeList = await _documentchargesService.GetAllPropertyType();
            try
            {

                if (ModelState.IsValid)
                {
                   
                    charge.CreatedBy = SiteContext.UserId;
                    var result = await _documentchargesService.Create(charge);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        //return View();
                        var list = await _documentchargesService.GetAllDocumentcharges();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(charge);

                    }
                }
                else
                {
                    return View(charge);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(charge);
            }
        }
        // [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _documentchargesService.FetchSingleResult(id);
            Data.PropertyTypeList = await _documentchargesService.GetAllPropertyType();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        //  [ValidateAntiForgeryToken]
        //  [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Documentcharges charge)
        {
            charge.PropertyTypeList = await _documentchargesService.GetAllPropertyType();
            if (ModelState.IsValid)
            {
                try
                {

                    charge.ModifiedBy = SiteContext.UserId;
                    var result = await _documentchargesService.Update(id, charge);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);

                        var list = await _documentchargesService.GetAllDocumentcharges();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(charge);

                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(charge);

                }
            }
            return View(charge);
        }

        //  [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _documentchargesService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                var result1 = await _documentchargesService.GetAllDocumentcharges();
                return View("Index", result1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                var result1 = await _documentchargesService.GetAllDocumentcharges();
                return View("Index", result1);
            }
        }

        //  [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _documentchargesService.FetchSingleResult(id);
            Data.PropertyTypeList = await _documentchargesService.GetAllPropertyType();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        //  [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> Download()
        {
            List<Documentcharges> result = await _documentchargesService.GetAllDocumentcharges();
            var memory = ExcelHelper.CreateExcel(result);
            string sFileName = @"Documentcharges.xlsx";
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

        }
    }
}
