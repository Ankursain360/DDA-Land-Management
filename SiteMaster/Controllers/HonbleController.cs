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
    public class HonbleController : BaseController
    {
        private readonly IHonbleService _HonbleService;
        public HonbleController(IHonbleService HonbleService)
        {
            _HonbleService = HonbleService;
        }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            var list = _HonbleService.GetAllHonble();
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] HonbleSearchDto model)
        {
            var result = await _HonbleService.GetPagedHonble(model);
            return PartialView("_List", result);
        }


        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Honble Honble = new Honble();
            Honble.IsActive = 1;
            Honble.CreatedBy = SiteContext.UserId;

            return View(Honble);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Honble Honble)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = await _HonbleService.Create(Honble);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        //var list = await _PropertyTypeService.GetAllPropertyType();
                      return View("Index");
                        //return RedirectToAction("Index", "Honble");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(Honble);
                    }
                }
                else
                {
                    return View(Honble);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(Honble);
            }
        }
        //[AcceptVerbs("Get", "Post")]
        //[AllowAnonymous]
        //public async Task<IActionResult> Exist(int Id, string Name)
        //{
        //    var result = await _PropertyTypeService.CheckUniqueName(Id, Name);
        //    if (result == false)
        //    {
        //        return Json(true);
        //    }
        //    else
        //    {
        //        return Json($"Village: {Name} already exist");
        //    }
        //}

          [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            Honble Honble = new Honble();


            var Data = await _HonbleService.FetchSingleResult(id);


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Honble Honble)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Honble.ModifiedBy = SiteContext.UserId;

                    var result = await _HonbleService.Update(id, Honble);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        //var list = await _PropertyTypeService.GetAllPropertyType();
                  return View("Index");
                        //return RedirectToAction("Index", "Honble");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(Honble);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(Honble);
                }
            }
            else
            {
                return View(Honble);
            }
        }



        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {

                var result = await _HonbleService.Delete(id);
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
            //return RedirectToAction("Index", "Honble");
        }



        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _HonbleService.FetchSingleResult(id);


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> Download()
        {
            List<Honble> result = await _HonbleService.GetAll();
            var memory = ExcelHelper.CreateExcel(result);
            string sFileName = @"Honble.xlsx";
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

        }

        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> HonourableList()
        {
            var result = await _HonbleService.GetAll();
            List<HonourableListDto> data = new List<HonourableListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new HonourableListDto()
                    {
                        Id = result[i].Id,
                        HonourableName = result[i].HonbleName,

                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    });
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }



    }
}
