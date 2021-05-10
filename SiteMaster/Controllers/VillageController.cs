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
    public class VillageController : BaseController
    {
        private readonly IVillageService _villageService;
        public VillageController(IVillageService villageService)
        {
            _villageService = villageService;
        }

        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
           
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] VillageSearchDto model)
        {
            var result = await _villageService.GetPagedVillage(model);
            return PartialView("_List", result);
        }


        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Village village = new Village();
            village.IsActive = 1;
            village.DepartmentList = await _villageService.GetAllDepartment();
           
            village.ZoneList = await _villageService.GetAllZone(Convert.ToInt32(village.DepartmentId));
            village.DivisionList = await _villageService.GetAllDivisionList(Convert.ToInt32(village.ZoneId));
            return View(village);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Village village)
        {
            village.DepartmentList = await _villageService.GetAllDepartment();

            village.ZoneList = await _villageService.GetAllZone(Convert.ToInt32(village.DepartmentId));
            village.DivisionList = await _villageService.GetAllDivisionList(Convert.ToInt32(village.ZoneId));

            try
            {
                village.DepartmentList = await _villageService.GetAllDepartment();
                if (ModelState.IsValid)
                {
                    var result = await _villageService.Create(village);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _villageService.GetAllVillage();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(village);
                    }
                }
                else
                {
                    return View(village);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(village);
            }
        }
        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> Exist(int Id, string Name)
        {
            var result = await _villageService.CheckUniqueName(Id, Name);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Village: {Name} already exist");
            }
        }

        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _villageService.FetchSingleResult(id);
            Data.DepartmentList = await _villageService.GetAllDepartment();
            Data.ZoneList = await _villageService.GetAllZone(Convert.ToInt32(Data.DepartmentId));
            Data.DivisionList = await _villageService.GetAllDivisionList(Convert.ToInt32(Data.ZoneId));
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Village village)
        {
            village.DepartmentList = await _villageService.GetAllDepartment();
            village.ZoneList = await _villageService.GetAllZone(Convert.ToInt32(village.DepartmentId));
            village.DivisionList = await _villageService.GetAllDivisionList(Convert.ToInt32(village.ZoneId));

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _villageService.Update(id, village);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _villageService.GetAllVillage();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(village);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(village);
                }
            }
            else
            {
                return View(village);
            }
        }



        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)  // Used to Perform Delete Functionality added by Praveen
        {
            try
            {

                var result = await _villageService.Delete(id);
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
            var list = await _villageService.GetAllVillage();
            return View("Index", list);
        }



        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _villageService.FetchSingleResult(id);
          
            Data.DepartmentList = await _villageService.GetAllDepartment();
            Data.ZoneList = await _villageService.GetAllZone(Convert.ToInt32(Data.DepartmentId));
            Data.DivisionList = await _villageService.GetAllDivisionList(Convert.ToInt32(Data.ZoneId));
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
            return Json(await _villageService.GetAllZone(Convert.ToInt32(DepartmentId)));
        }

        [HttpGet]
        public async Task<JsonResult> GetDivisionList(int? ZoneId)
        {
            ZoneId = ZoneId ?? 0;
            return Json(await _villageService.GetAllDivisionList(Convert.ToInt32(ZoneId)));
        }


       

        [AuthorizeContext(ViewAction.Download)]

        public async Task<IActionResult> VillageList()
        {
            var result = await _villageService.GetAllVillage();
            List<VillageListDto> data = new List<VillageListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new VillageListDto()
                    {
                        Id = result[i].Id,
                        ZoneName = result[i].Zone == null ? "" : result[i].Zone.Name,
                        VillageName = result[i].Name,

                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }



    }
}