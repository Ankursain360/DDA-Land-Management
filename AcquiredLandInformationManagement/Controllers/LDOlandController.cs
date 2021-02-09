using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcquiredLandInformationManagement.Filters;
using Core.Enum;
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
   
   public class LDOlandController : BaseController
    {
        private readonly ILdolandService _ldolandService;

        public LDOlandController(ILdolandService ldolandService)
        {
            _ldolandService = ldolandService;
        }

        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        { 
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] LdolandSearchDto model)
        {
            var result = await _ldolandService.GetPagedLdoland(model);
            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Ldoland ldoland = new Ldoland();
            ldoland.IsActive = 1;

            ldoland.LandNotificationList = await _ldolandService.GetAllLandNotification();
            ldoland.SerialnumberList = await _ldolandService.GetAllSerialnumber();
            return View(ldoland);
        }


        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Ldoland ldoland)
        {
            try
            {
                ldoland.LandNotificationList = await _ldolandService.GetAllLandNotification();
                ldoland.SerialnumberList = await _ldolandService.GetAllSerialnumber();
                if (ModelState.IsValid)
                {

                    ldoland.IsActive = 1;
                    ldoland.CreatedBy = SiteContext.UserId;
                    var result = await _ldolandService.Create(ldoland);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _ldolandService.GetAllLdoland();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(ldoland);

                    }
                }
                else
                {
                    return View(ldoland);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(ldoland);
            }
        }
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {

            var Data = await _ldolandService.FetchSingleResult(id);
            Data.LandNotificationList = await _ldolandService.GetAllLandNotification();
            Data.SerialnumberList = await _ldolandService.GetAllSerialnumber();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Ldoland ldoland)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ldoland.ModifiedBy = SiteContext.UserId;
                    var result = await _ldolandService.Update(id, ldoland);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _ldolandService.GetAllLdoland();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(ldoland);

                    }
                }
                catch (Exception ex)
                {

                }
            }
            return View(ldoland);
        }

      

        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)  
        {
            if (id == 0)
            {
                return NotFound();
            }

            var form = await _ldolandService.Delete(id);
            if (form == false)
            {
                return NotFound();
            }
            var result = await _ldolandService.GetAllLdoland();
            ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            return View("Index", result);
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _ldolandService.FetchSingleResult(id);
           
            Data.LandNotificationList = await _ldolandService.GetAllLandNotification();
            Data.SerialnumberList = await _ldolandService.GetAllSerialnumber();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> Download()
        {
            List<Ldoland> result = await _ldolandService.GetAllLdoland();
            var memory = ExcelHelper.CreateExcel(result);
            string sFileName = @"Ldoland.xlsx";
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

        }
    }
}
