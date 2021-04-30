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
using AcquiredLandInformationManagement.Models;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.AspNetCore.Authorization;
using Dto.Search;
using AcquiredLandInformationManagement.Filters;
using Core.Enum;
using Utility.Helper;
using Dto.Master;

namespace AcquiredLandInformationManagement.Controllers
{
    public class NazulController : Controller
    {

        private readonly INazulService _nazulService;

        public NazulController(INazulService nazulService)
        {
            _nazulService = nazulService;
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            var result = await _nazulService.GetAllNazul();
            return View(result);
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] NazulSearchDto model)
        {
            var result = await _nazulService.GetPagedNazul(model);
            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Nazul nazul = new Nazul();
            nazul.IsActive = 1;
           

            nazul.VillageList = await _nazulService.GetAllVillageList();
            return View(nazul);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Nazul nazul)
        {
            try
            {
              

                nazul.VillageList = await _nazulService.GetAllVillageList();

                if (ModelState.IsValid)
                {
                    var result = await _nazulService.Create(nazul);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _nazulService.GetAllNazul();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(nazul);
                    }
                }
                else
                {
                    return View(nazul);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(nazul);
            }
        }
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _nazulService.FetchSingleResult(id);
            Data.VillageList = await _nazulService.GetAllVillageList();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Nazul nazul)
        {
            nazul.VillageList = await _nazulService.GetAllVillageList();
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _nazulService.Update(id, nazul);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var result1 = await _nazulService.GetAllNazul();
                        return View("Index", result1);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(nazul);

                    }
                }
                catch (Exception ex)
                {

                }
            }
            return View(nazul);
        }
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> DeleteConfirmed(int id)  
        {

            var result = await _nazulService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                var result1 = await _nazulService.GetAllNazul();
                return View("Index", result1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                var result1 = await _nazulService.GetAllNazul();
                return View("Index", result1);
            }

        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _nazulService.FetchSingleResult(id);
            Data.VillageList = await _nazulService.GetAllVillageList();

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [AuthorizeContext(ViewAction.Download)]

        public async Task<IActionResult> NazulList()
        {
            var result = await _nazulService.GetAllNazul();
            List<NazulListDto> data = new List<NazulListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new NazulListDto()
                    {
                        Id = result[i].Id,
                        Village = result[i].Village == null ? "" : result[i].Village.Name,
                        JaraiSakni = result[i].JaraiSakani,
                        Language = result[i].Language,
                        DateOfConsolidation = Convert.ToDateTime(result[i].YearOfConsolidation).ToString("dd-MMM-yyyy"),
                        DateOfJamabandi = Convert.ToDateTime(result[i].YearOfJamabandi).ToString("dd-MMM-yyyy"),
                        LastMutationNo = result[i].LastMutationNo,
                       
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }
    }

}
