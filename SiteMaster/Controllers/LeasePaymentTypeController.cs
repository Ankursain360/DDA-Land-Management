using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using System;
using System.Threading.Tasks;
using SiteMaster.Filters;
using Core.Enum;
using System.Collections.Generic;
using Utility.Helper;
using Dto.Master;

namespace SiteMaster.Controllers
{
    public class LeasePaymentTypeController : BaseController
    {
        private readonly ILeasePaymentTypeService _LeasePaymentTypeService;
        public LeasePaymentTypeController(ILeasePaymentTypeService LeasePaymentTypeService)
        {
            _LeasePaymentTypeService = LeasePaymentTypeService;
        }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
           // var list = _LeasePaymentTypeService.GetAllLeasepaymenttype();
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] LeasePaymentTypeSearchDto model)
        {
            var result = await _LeasePaymentTypeService.GetPagedLeasepaymenttype(model);
            return PartialView("_List", result);
        }


        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Leasepaymenttype Leasepaymenttype = new Leasepaymenttype();
            Leasepaymenttype.IsActive = 1;
            Leasepaymenttype.CreatedBy = SiteContext.UserId;

            return View(Leasepaymenttype);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Leasepaymenttype Leasepaymenttype)
        {
            try
            {

                if (ModelState.IsValid)

                {
                    Leasepaymenttype.CreatedBy = SiteContext.UserId;
                    var result = await _LeasePaymentTypeService.Create(Leasepaymenttype);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        return View("Index");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(Leasepaymenttype);
                    }
                }
                else
                {
                    return View(Leasepaymenttype);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(Leasepaymenttype);
            }
        }


        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            Leasepaymenttype Leasepaymenttype = new Leasepaymenttype();


            var Data = await _LeasePaymentTypeService.FetchSingleResult(id);


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Leasepaymenttype Leasepaymenttype)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Leasepaymenttype.ModifiedBy = SiteContext.UserId;

                    var result = await _LeasePaymentTypeService.Update(id, Leasepaymenttype);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        //var list = await _PropertyTypeService.GetAllPropertyType();
                        return View("Index");
                        //return RedirectToAction("Index", "Leasepaymenttype");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(Leasepaymenttype);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(Leasepaymenttype);
                }
            }
            else
            {
                return View(Leasepaymenttype);
            }
        }



        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {

                var result = await _LeasePaymentTypeService.Delete(id);
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
            //var list = await _PropertyTypeService.GetAllPropertyType();
            return View("Index");
            //return RedirectToAction("Index", "Leasepaymenttype");
        }



        // [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _LeasePaymentTypeService.FetchSingleResult(id);


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


      


        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> LeasepaymenttypeList()
        {
            var result = await _LeasePaymentTypeService.GetAll();
            List<LeasepaymenttypeDto> data = new List<LeasepaymenttypeDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new LeasepaymenttypeDto()
                    {
                        Id = result[i].Id,
                        LeasePaymentType = result[i].Name,

                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    });
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }


    }
}
