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
    public class JudgementstatusController : BaseController
    {
        private readonly IJudgementstatusService _JudgementstatusService;
        public JudgementstatusController(IJudgementstatusService JudgementstatusService)
        {
            _JudgementstatusService = JudgementstatusService;
        }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            var list = _JudgementstatusService.GetAllJudgementstatus();
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] JudgementstatusSearchDto model)
        {
            var result = await _JudgementstatusService.GetPagedJudgementstatus(model);
            return PartialView("_List", result);
        }


        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Judgementstatus Judgementstatus = new Judgementstatus();
            Judgementstatus.IsActive = 1;
            Judgementstatus.CreatedBy = SiteContext.UserId;

            return View(Judgementstatus);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Judgementstatus Judgementstatus)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = await _JudgementstatusService.Create(Judgementstatus);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        //var list = await _PropertyTypeService.GetAllPropertyType();
                        return View("Index");
                        //return RedirectToAction("Index", "Judgementstatus");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(Judgementstatus);
                    }
                }
                else
                {
                    return View(Judgementstatus);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(Judgementstatus);
            }
        }

        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            Judgementstatus Judgementstatus = new Judgementstatus();


            var Data = await _JudgementstatusService.FetchSingleResult(id);


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Judgementstatus Judgementstatus)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Judgementstatus.ModifiedBy = SiteContext.UserId;

                    var result = await _JudgementstatusService.Update(id, Judgementstatus);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        //var list = await _PropertyTypeService.GetAllPropertyType();
                        return View("Index");
                       // return RedirectToAction("Index", "Judgementstatus");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(Judgementstatus);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(Judgementstatus);
                }
            }
            else
            {
                return View(Judgementstatus);
            }
        }



        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {

                var result = await _JudgementstatusService.Delete(id);
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
            //return RedirectToAction("Index", "Judgementstatus");
        }



        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _JudgementstatusService.FetchSingleResult(id);


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


      
      

        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> JudgementstatusList()
        {
            var result = await _JudgementstatusService.GetAll();
            List<JudgementstatusListDto> data = new List<JudgementstatusListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new JudgementstatusListDto()
                    {
                        Id = result[i].Id,
                        JudgementStatus = result[i].Status,

                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    });
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }



    }
}
