﻿using Core.Enum;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using SiteMaster.Filters;
using System;
using System.Threading.Tasks;

using System.Collections.Generic;

using Utility.Helper;
using Dto.Master;

namespace SiteMaster.Controllers
{
    public class yearcaseController : BaseController
    {
        private readonly ICaseyearService _caseService;

        public yearcaseController(ICaseyearService caseService)
        {
            _caseService = caseService;
        }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }
        public async Task<PartialViewResult> List([FromBody] CaseyearSearchDto model)
        {
            var result = await _caseService.GetPagedcaseyear(model);
            return PartialView("_List", result);
        }

       


        [AuthorizeContext(ViewAction.Add)]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Caseyear caseyear)
        {
            try
            {

                if (ModelState.IsValid)
                {


                    var result = await _caseService.Create(caseyear);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                       

                        var list = await _caseService.GetAll();
                        return View("Index", list);

                       

                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(caseyear);

                    }
                }
                else
                {
                    return View(caseyear);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(caseyear);
            }
        }






        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _caseService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Edit)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Caseyear caseyear)
        {
            if (ModelState.IsValid)
            {
                try
                {



                    var result = await _caseService.Update(id, caseyear);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);


                        var list = await _caseService.GetAll();
                        return View("Index", list);

                      

                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(caseyear);

                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(caseyear);

                }
            }
            return View(caseyear);
        }



        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _caseService.FetchSingleResult(id);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _caseService.Delete(id);
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

            var list = await _caseService.GetAll();
            return View("Index", list);

           

        }

       

        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> CaseYearList()
        {
            var result = await _caseService.GetAll();
            List<CaseYearListDto> data = new List<CaseYearListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new CaseYearListDto()
                    {
                        Id = result[i].Id,
                        CaseYear = result[i].Name,

                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    });
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

    }
}
