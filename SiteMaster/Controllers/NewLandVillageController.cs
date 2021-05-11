using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Dto.Search;
using SiteMaster.Filters;
using Core.Enum;
using System.Data;
using Newtonsoft.Json;
using Utility.Helper;
using System.Collections.Generic;
using Dto.Master;
namespace SiteMaster.Controllers
{
    public class NewLandVillageController : BaseController
    {
        private readonly INewlandvillageService _newlandvillageService;
       
        public NewLandVillageController(INewlandvillageService newlandvillageService)
        {
            _newlandvillageService = newlandvillageService;
        }

      
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] NewlandvillageSearchDto model)
        {
            var result = await _newlandvillageService.GetPagedNewlandvillage(model);

            return PartialView("_List", result);
        }


        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Newlandvillage newlandvillage = new Newlandvillage();
            newlandvillage.IsActive = 1;
            newlandvillage.DistrictList = await _newlandvillageService.GetAllDistrict();
            newlandvillage.TehsilList = await _newlandvillageService.GetAllTehsil();
            newlandvillage.ZoneList = await _newlandvillageService.GetAllZone();
            return View(newlandvillage);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Newlandvillage newlandvillage)
        {
            try
            {
                newlandvillage.DistrictList = await _newlandvillageService.GetAllDistrict();
                newlandvillage.TehsilList = await _newlandvillageService.GetAllTehsil();
                newlandvillage.ZoneList = await _newlandvillageService.GetAllZone();

                if (ModelState.IsValid)
                {
                    newlandvillage.CreatedBy = SiteContext.UserId;
                    var result = await _newlandvillageService.Create(newlandvillage);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _newlandvillageService.GetNewlandvillage();
                        return View("Index");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(newlandvillage);
                    }
                }
                else
                {
                    return View(newlandvillage);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(newlandvillage);
            }
        }



        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _newlandvillageService.FetchSingleResult(id);
            Data.DistrictList = await _newlandvillageService.GetAllDistrict();
            Data.TehsilList = await _newlandvillageService.GetAllTehsil();
            Data.ZoneList = await _newlandvillageService.GetAllZone();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Newlandvillage newlandvillage)
        {

            newlandvillage.DistrictList = await _newlandvillageService.GetAllDistrict();
            newlandvillage.TehsilList = await _newlandvillageService.GetAllTehsil();
            newlandvillage.ZoneList = await _newlandvillageService.GetAllZone();

            if (ModelState.IsValid)
            {
                try
                {
                    newlandvillage.ModifiedBy = SiteContext.UserId;
                    var result = await _newlandvillageService.Update(id, newlandvillage);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _newlandvillageService.GetNewlandvillage();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(newlandvillage);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(newlandvillage);
                }
            }
            else
            {
                return View(newlandvillage);
            }
        }


        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _newlandvillageService.Delete(id);
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
            var list = await _newlandvillageService.GetNewlandvillage();
            return View("Index", list);
        }


        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _newlandvillageService.FetchSingleResult(id);
            Data.DistrictList = await _newlandvillageService.GetAllDistrict();
            Data.TehsilList = await _newlandvillageService.GetAllTehsil();
            Data.ZoneList = await _newlandvillageService.GetAllZone();

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }




        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> NewLandVillageList()
        {
            var result = await _newlandvillageService.GetNewlandvillage();
            List<NewLandVillageListDto> data = new List<NewLandVillageListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new NewLandVillageListDto()
                    {
                        Id = result[i].Id,
                        VillageName=result[i].Name,
                        DistrictName = result[i].District == null ? "" : result[i].District.Name.ToString(),
                        TehsilName = result[i].Tehsil == null ? "" : result[i].Tehsil.Name.ToString(),
                        ZoneName = result[i].Zone == null ? "" : result[i].Zone.Name.ToString(),                      
                        Acquired=result[i].Acquired,
                        VillageType=result[i].VillageType,                        
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

    }
}
