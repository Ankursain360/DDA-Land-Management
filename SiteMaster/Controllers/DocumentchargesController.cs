

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
using SiteMaster.Filters;
using Utility.Helper;
using Dto.Master;

namespace SiteMaster.Controllers
{

    public class DocumentchargesController : BaseController
    {
        private readonly IDocumentchargesServices _documentchargesService;

        public DocumentchargesController(IDocumentchargesServices documentchargesService)
        {
            _documentchargesService = documentchargesService;
        }
        [AuthorizeContext(ViewAction.View)]
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
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Documentcharges charge = new Documentcharges();
            charge.IsActive = 1;
            //charge.PropertyTypeList = await _documentchargesService.GetAllPropertyType();
            charge.LeasePurposeList = await _documentchargesService.GetAllLeasepurpose();
            charge.LeaseSubPurposeList = await _documentchargesService.GetAllLeaseSubpurpose(charge.LeasePurposesTypeId);
            return View(charge);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
          [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Documentcharges charge)
        {
            //charge.PropertyTypeList = await _documentchargesService.GetAllPropertyType();
            charge.LeasePurposeList = await _documentchargesService.GetAllLeasepurpose();
            charge.LeaseSubPurposeList = await _documentchargesService.GetAllLeaseSubpurpose(charge.LeasePurposesTypeId);
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
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _documentchargesService.FetchSingleResult(id);
            //Data.PropertyTypeList = await _documentchargesService.GetAllPropertyType();
            Data.LeasePurposeList = await _documentchargesService.GetAllLeasepurpose();
            Data.LeaseSubPurposeList = await _documentchargesService.GetAllLeaseSubpurpose(Data.LeasePurposesTypeId);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Documentcharges charge)
        {
            //charge.PropertyTypeList = await _documentchargesService.GetAllPropertyType();
            charge.LeasePurposeList = await _documentchargesService.GetAllLeasepurpose();
            charge.LeaseSubPurposeList = await _documentchargesService.GetAllLeaseSubpurpose(charge.LeasePurposesTypeId);
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

        [AuthorizeContext(ViewAction.Delete)]
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

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _documentchargesService.FetchSingleResult(id);
            //Data.PropertyTypeList = await _documentchargesService.GetAllPropertyType();
            Data.LeasePurposeList = await _documentchargesService.GetAllLeasepurpose();
            Data.LeaseSubPurposeList = await _documentchargesService.GetAllLeaseSubpurpose(Data.LeasePurposesTypeId);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> Download()
        {
            List<Documentcharges> result = await _documentchargesService.GetAllDocumentcharges();
            var memory = ExcelHelper.CreateExcel(result);
            string sFileName = @"Documentcharges.xlsx";
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

        }
        [HttpGet]
        public async Task<JsonResult> GetAllLeaseSubpurpose(int? purposeUseId)
        {
            purposeUseId = purposeUseId ?? 0;
            return Json(await _documentchargesService.GetAllLeaseSubpurpose(Convert.ToInt32(purposeUseId)));
        }

        public async Task<IActionResult> DocumentChargesList()
        {
            var result = await _documentchargesService.GetAllDocumentchargesList();
            List<DocumentChargesListDto> data = new List<DocumentChargesListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new DocumentChargesListDto()
                    {
                        Id = result[i].Id,
                        LeasePurpose = result[i].LeasePurposesType == null ? "" : result[i].LeasePurposesType.PurposeUse.ToString(),
                        LeaseSubPurpose = result[i].LeaseSubPurpose == null ? "" : result[i].LeaseSubPurpose.SubPurposeUse.ToString(),
                        DocumentCharges = result[i].DocumentCharge.ToString(),
                        FromDate = result[i].FromDate.ToString("dd/MM/yyyy"),
                        ToDate = result[i].ToDate.ToString("dd/MM/yyyy"),


                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }




    }
}
