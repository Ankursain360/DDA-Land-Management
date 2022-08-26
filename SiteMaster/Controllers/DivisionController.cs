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
using Dto.Search;
using SiteMaster.Filters;
using Core.Enum;
using Dto.Master;
using Utility.Helper;


namespace SiteMaster.Controllers
{
    public class DivisionController : BaseController
    {
        private readonly IDivisionService _divisionService;


        public DivisionController(IDivisionService divisionService)
        {
            _divisionService = divisionService;
        }



        [AuthorizeContext(ViewAction.View)]
        public  IActionResult Index()
        {
           
            return View();

        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DivisionSearchDto model)
        {
            var result = await _divisionService.GetPagedDivision(model);
            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Division model = new Division();
            model.IsActive = 1;
            model.DepartmentList = await _divisionService.GetAllDepartment();
            model.ZoneList = await _divisionService.GetAllZone(model.DepartmentId);
            return View(model);
        }

      


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Division division)
        {
            try
            {
                division.DepartmentList = await _divisionService.GetAllDepartment();
                division.ZoneList = await _divisionService.GetAllZone(division.DepartmentId);


                if (ModelState.IsValid)
                {
                  
                    var result = await _divisionService.Create(division);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _divisionService.GetAllDivision();
                        return View("Index", list);
                       
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(division);

                    }
                }
                else
                {
                    return View(division);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(division);
            }
        }

        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _divisionService.FetchSingleResult(id);
            Data.DepartmentList = await _divisionService.GetAllDepartment();
            Data.ZoneList = await _divisionService.GetAllZone(Data.DepartmentId);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Division division)
        {
            division.DepartmentList = await _divisionService.GetAllDepartment();
            division.ZoneList = await _divisionService.GetAllZone(division.DepartmentId);
            if (ModelState.IsValid)
            {
                var result = await _divisionService.Update(id, division);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                    var list = await _divisionService.GetAllDivision();
                    return View("Index", list);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(division);
                }
            }
            else
            {
                return View(division);
            }
          
        }


        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> Exist(int Id, string Name)
        {
            var result = await _divisionService.CheckUniqueName(Id, Name);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Division: {Name} already exist");
            }
        }

        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)  
        {
            try
            {

                var result = await _divisionService.Delete(id);
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
            var list = await _divisionService.GetAllDivision();
            return View("Index", list);
        }


        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _divisionService.FetchSingleResult(id);
            Data.DepartmentList = await _divisionService.GetAllDepartment();
            Data.ZoneList = await _divisionService.GetAllZone(Data.DepartmentId);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }




        [HttpGet]
        public async Task<JsonResult> GetZoneList(int? DepartmentId)
        {
            DepartmentId = DepartmentId ?? 0;
            return Json(await _divisionService.GetAllZone(Convert.ToInt32(DepartmentId)));
        }

        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> DivisionList()
        {
            var result = await _divisionService.GetAllDivision();
            List<DivisionListDto> data = new List<DivisionListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new DivisionListDto()
                    {
                        Id = result[i].Id,
                        Department = result[i].Department == null ? "" : result[i].Department.Name,
                        Zone = result[i].Zone==null ?"" : result[i].Zone.Name,                      
                        DivisionCode = result[i].Code,
                        DivisionName= result[i].Name,
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    });
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

      


    }
}
