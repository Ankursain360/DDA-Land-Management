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
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Dto.Search;
using SiteMaster.Filters;
using Core.Enum;
using Utility.Helper;
using Dto.Master;
namespace SiteMaster.Controllers
{
    public class DistrictController : BaseController
    {
        private readonly IDistrictService _districtService;

        public DistrictController(IDistrictService districtService)
        {
            _districtService = districtService;
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            var result = await _districtService.GetAllDistrict();
            return View(result);
        }
        public async Task<PartialViewResult> List([FromBody] DistrictSearchDto model)
        {
            var result = await _districtService.GetPagedDistrict(model);
            return PartialView("_List", result);
        }

        [AuthorizeContext(ViewAction.Add)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(District district)
        {
            try
            {

                if (ModelState.IsValid)
                {


                    var result = await _districtService.Create(district);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                       
                        var list = await _districtService.GetAllDistrict();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(district);

                    }
                }
                else
                {
                    return View(district);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(district);
            }
        }

        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _districtService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, District district)
        {
            if (ModelState.IsValid)
            {
                var result = await _districtService.Update(id, district);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);

                    var list = await _districtService.GetAllDistrict();
                    return View("Index", list);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(district);
                }
            }
            return View(district);
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> Exist(int Id, string Name)
        {
            var result = await _districtService.CheckUniqueName(Id, Name);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Department: {Name} already exist");
            }
        }


        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _districtService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        
        public async Task<IActionResult> DeleteConfirmed(int id)  // Used to Perform Delete Functionality added by Renu
        {
            var result = await _districtService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            return RedirectToAction("Index", "District");
        }


        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)  // Used to Perform Delete Functionality added by Praveen
        {
            try
            {
                var result = await _districtService.Delete(id);
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
            var list = await _districtService.GetAllDistrict();
            return View("Index", list);
        }
        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> Download()
        {
            List<District> result = await _districtService.GetAllDistrict();
            var memory = ExcelHelper.CreateExcel(result);
            string sFileName = @"District.xlsx";            
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

        }
        
        [AuthorizeContext(ViewAction.Download)]        
        public async Task<IActionResult> DownloadIndex()
        {
            var result = await  _districtService.GetAllDistrict();
            List<DistrictListDTO> data = new List<DistrictListDTO>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new DistrictListDTO()
                    {
                        Id = result[i].Id,
                        Code = result[i].Code,
                        Name=result[i].Name,
                        IsActive = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    });
                }
            }

            var memory = ExcelHelper.CreateExcel(data);         
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }






    }
}
