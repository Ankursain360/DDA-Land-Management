using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcquiredLandInformationManagement.Filters;
using Core.Enum;
using Dto.Master;
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

    public class TehsilController : BaseController
    {
        private readonly ITehsilService _tehsilService;

        public TehsilController(ITehsilService tehsilService)
        {
            _tehsilService = tehsilService;
        }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
          
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] TehsilSearchDto model)
        {
            var result = await _tehsilService.GetPagedTehsil(model);
            return PartialView("_List", result);
        }

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Tehsil tehsil = new Tehsil();
            tehsil.IsActive = 1;           
            return View(tehsil);
        }


        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Tehsil tehsil)
        {
            try
            {
            
                if (ModelState.IsValid)
                {


                    var result = await _tehsilService.Create(tehsil);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _tehsilService.GetAllTehsil();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(tehsil);

                    }
                }
                else
                {
                    return View(tehsil);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(tehsil);
            }
        }
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {

            var Data = await _tehsilService.FetchSingleResult(id);
           
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Tehsil tehsil)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var result = await _tehsilService.Update(id, tehsil);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _tehsilService.GetAllTehsil();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(tehsil);

                    }
                }
                catch (Exception ex)
                {

                }
            }
            return View(tehsil);
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> Exist(int Id, string Name)
        {
            var result = await _tehsilService.CheckUniqueName(Id, Name);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Page: {Name} already exist");
            }
        }

        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var form = await _tehsilService.Delete(id);
            if (form == false)
            {
                return NotFound();
            }
            var result = await _tehsilService.GetAllTehsil();
            ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            return View("Index", result);
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _tehsilService.FetchSingleResult(id);
           
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [AuthorizeContext(ViewAction.Download)]
      
        public async Task<IActionResult> TehsilList()
        {
            var result = await _tehsilService.GetAllTehsil();
            List<TehsilListDto> data = new List<TehsilListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new TehsilListDto()
                    {
                        Id = result[i].Id,
                        TehsilName = result[i].Name,
                       
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }
    }
}
