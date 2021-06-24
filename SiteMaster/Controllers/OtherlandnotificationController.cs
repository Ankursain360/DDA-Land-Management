using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using SiteMaster.Models;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Dto.Search;
using Dto.Master;
using SiteMaster.Filters;
using Core.Enum;
using Utility.Helper;

namespace SiteMaster.Controllers
{
    public class OtherlandnotificationController : BaseController
    {
        private readonly IOtherlandnotificationService _otherlandnotificationService;

        public OtherlandnotificationController(IOtherlandnotificationService otherlandnotificationService)
        {
            _otherlandnotificationService = otherlandnotificationService;
        }

        [AuthorizeContext(ViewAction.View)]
    
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] OtherlandnotificationSearchDto model)
        {
            var result = await _otherlandnotificationService.GetPagedOtherlandnotification(model);
            return PartialView("_List", result);
        }

       [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Otherlandnotification otherlandnotification = new Otherlandnotification();
            otherlandnotification.IsActive = 1;
          
            return View(otherlandnotification);
        }

        [HttpPost]
       [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Otherlandnotification otherlandnotification)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    otherlandnotification.CreatedBy = SiteContext.UserId;
                    var result = await _otherlandnotificationService.Create(otherlandnotification);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                      
                        return View("Index");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(otherlandnotification);

                    }
                }
                else
                {
                    return View(otherlandnotification);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(otherlandnotification);
            }
        }



        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _otherlandnotificationService.FetchSingleResult(id);
           
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Otherlandnotification otherlandnotification)
        {
            
            if (ModelState.IsValid)
            {
                try
                {

                    otherlandnotification.ModifiedBy = SiteContext.UserId;
                    var result = await _otherlandnotificationService.Update(id, otherlandnotification);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                      
                        return View("Index");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(otherlandnotification);

                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(otherlandnotification);
                }
            }
            return View(otherlandnotification);
        }



        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> DeleteConfirmed(int id)  // Used to Perform Delete Functionality 
        {
            var result = await _otherlandnotificationService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
          
            return View("Index");

        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _otherlandnotificationService.FetchSingleResult(id);
         
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }





        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> OtherlandnotificationList()
        {
            var result = await _otherlandnotificationService.GetOtherlandnotification();
            List<OtherlandnotificationListDto> data = new List<OtherlandnotificationListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new OtherlandnotificationListDto()
                    {
                        Id = result[i].Id,
                        LandType = result[i].LandType == null ? "" : result[i].LandType,
                        NotificationNumber = result[i].NotificationNumber == null ? "" : result[i].NotificationNumber,
                     
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    });
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }





    }
}
