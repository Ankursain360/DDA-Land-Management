﻿using System;
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
using Microsoft.Extensions.Configuration;
using Utility.Helper;
using Core.Enum;
using SiteMaster.Filters;
using Dto.Master;

namespace SiteMaster.Controllers
{
    public class DemolitionprogrammasterController : BaseController
    {
        private readonly IDemolitionprogrammasterService _demolitionprogrammasterService;
        public IConfiguration _configuration;

        public DemolitionprogrammasterController(IDemolitionprogrammasterService demolitionprogrammasterService, IConfiguration configuration)
        {
            _demolitionprogrammasterService = demolitionprogrammasterService;
            _configuration = configuration;
        }



        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {

            return View();
        }


        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DemolitionprogrammasterSearchDto model)
        {
            var result = await _demolitionprogrammasterService.GetPagedDemolitionprogrammaster(model);

            return PartialView("_List", result);
        }

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Demolitionprogram demolitionprogrammaster = new Demolitionprogram();
            demolitionprogrammaster.IsActive = 1;
            return View(demolitionprogrammaster);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Demolitionprogram demolitionprogrammaster)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = await _demolitionprogrammasterService.Create(demolitionprogrammaster);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _demolitionprogrammasterService.GetDemolitionprogrammaster();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(demolitionprogrammaster);
                    }
                }
                else
                {
                    return View(demolitionprogrammaster);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(demolitionprogrammaster);
            }
        }
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _demolitionprogrammasterService.FetchSingleResult(id);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Demolitionprogram demolitionprogrammaster)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _demolitionprogrammasterService.Update(id, demolitionprogrammaster);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _demolitionprogrammasterService.GetDemolitionprogrammaster();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(demolitionprogrammaster);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(demolitionprogrammaster);
                }
            }
            else
            {
                return View(demolitionprogrammaster);
            }
        }


        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _demolitionprogrammasterService.Delete(id);
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
            var list = await _demolitionprogrammasterService.GetDemolitionprogrammaster();
            return View("Index", list);
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _demolitionprogrammasterService.FetchSingleResult(id);


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


       
        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> DemolitionProgramList()
        {
            var result = await _demolitionprogrammasterService.GetDemolitionprogrammaster();
            List<DemolitionProgramListDto> data = new List<DemolitionProgramListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new DemolitionProgramListDto()
                    {
                        Id = result[i].Id,
                        Items = result[i].Items,
                        ItemsType = result[i].ItemsType,
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    });
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }


    }
}
