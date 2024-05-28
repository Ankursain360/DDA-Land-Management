using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Dto.Search;
using Core.Enum;
using Dto.Master;
using Utility.Helper;

using GIS.Filters;
namespace SiteMaster.Controllers
{
    public class KhasraMasterController : Controller
    {
        private readonly IKhasraService _khasraService;


        public KhasraMasterController(IKhasraService khasraService)
        {
            _khasraService = khasraService;
        }
        //[AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            var list = await _khasraService.GetAllKhasra();
            return View(list);
        }
       // [HttpPost]
        public async Task<PartialViewResult> List([FromBody] KhasraMasterSearchDto model)
        {
            var result = await _khasraService.GetPagedKhasra(model);
            return PartialView("_List", result);
        }
       // [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        
        {
            Khasra khasra = new Khasra();
            khasra.IsActive = 1;
            khasra.LandCategoryList = await _khasraService.GetAllLandCategory();
           
            khasra.VillageList = await _khasraService.GetAllVillageList();
            return View(khasra);
        }


        [HttpPost]
       // [ValidateAntiForgeryToken]
       // [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Khasra khasra)
        {
            try
            {
                khasra.LandCategoryList = await _khasraService.GetAllLandCategory();

                khasra.VillageList = await _khasraService.GetAllVillageList();

                if (ModelState.IsValid)
                {
                    var result = await _khasraService.Create(khasra);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _khasraService.GetAllKhasra();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(khasra);
                    }
                }
                else
                {
                    return View(khasra);    
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(khasra);
            }
        }



       // [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _khasraService.FetchSingleResult(id);
            Data.LandCategoryList = await _khasraService.GetAllLandCategory();
          
            Data.VillageList = await _khasraService.GetAllVillageList();

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
       // [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Khasra khasra)

        {
            khasra.VillageList = await _khasraService.GetAllVillageList();
            khasra.LandCategoryList = await _khasraService.GetAllLandCategory();
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _khasraService.Update(id, khasra);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _khasraService.GetAllKhasra();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(khasra);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(khasra);
                }
            }
            else
            {
                return View(khasra);
            }
        }
       // [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _khasraService.Delete(id);
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
            var list = await _khasraService.GetAllKhasra();
            return View("Index", list);
        }
       // [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _khasraService.FetchSingleResult(id);
           
            Data.LandCategoryList = await _khasraService.GetAllLandCategory();
            Data.VillageList = await _khasraService.GetAllVillageList();


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


       // [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> KhasraMasterList()
        {
            var result = await _khasraService.GetAllKhasra();
            List<KhasraMasterListDto> data = new List<KhasraMasterListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new KhasraMasterListDto()
                    {
                        Id = result[i].Id,
                        Village = result[i].Acquiredlandvillage == null ? "" : result[i].Acquiredlandvillage.Name,
                        RectNo = result[i].RectNo,
                        LandCategory = result[i].LandCategory == null ? "" : result[i].LandCategory.Name,
                        KhasraNo = result[i].Name,
                        Area = result[i].Bigha.ToString()
                                  + '-' + result[i].Biswa.ToString()
                                  + '-' + result[i].Biswanshi.ToString(),

                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }

    }
}
