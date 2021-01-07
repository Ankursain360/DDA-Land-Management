using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.ApplicationService;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;

namespace SiteMaster.Controllers
{
    public class ApprovalstatusController : BaseController
    {
        public readonly IApprovalstatusService _approvalstatusService;
        public ApprovalstatusController(IApprovalstatusService approvalstatusService)
       {
            _approvalstatusService = approvalstatusService;
       }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] ApprovalstatusSearchDto model)
        {
           var result = await _approvalstatusService.GetPagedApprovalStatus(model);
            return PartialView("_List", result);
        }
        public async Task<IActionResult> Create()
        {
            Approvalstatus model = new Approvalstatus();
            model.IsActive = 1;          
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Approvalstatus approvalstatus)
        {
            try
            {
                
                if (ModelState.IsValid)
                {
                    var result = await _approvalstatusService.Create(approvalstatus);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _approvalstatusService.GetAllApprovalstatus();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(approvalstatus);
                    }
                }
                else
                {
                    return View(approvalstatus);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(approvalstatus);
            }
        }
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _approvalstatusService.FetchSingleResult(id);
            

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Approvalstatus approvalstatus)
        {        
            if (ModelState.IsValid)
            {
                try
                {

                    var result = await _approvalstatusService.Update(id, approvalstatus);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        return View("Index");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(approvalstatus);

                    }
                }
                catch (DbUpdateConcurrencyException)
                {

                }
            }
            return View(approvalstatus);
        }
        public async Task<IActionResult> View(int id)
        {
            var Data = await _approvalstatusService.FetchSingleResult(id);           

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        //public async Task<IActionResult> Delete(int id)  //Not in use
        //{
        //   if (id == 0)
        //    {
        //        return NotFound();
        //    }

        //    var form = await _approvalstatusService.Delete(id);
        //   if (form == false)
        //    {
        //        return NotFound();
        //    }

        //    ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
        //    return View(form);
        //}

        public async Task<IActionResult> Delete(int id)  
        {

            var result = await _approvalstatusService.Delete(id);
           if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                var result1 = await _approvalstatusService.GetAllApprovalstatus();
                return View("Index", result1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                var result1 = await _approvalstatusService.GetAllApprovalstatus();
                return View("Index", result1);
            }

        }

    }
}
