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
using Microsoft.Extensions.Configuration;
using Utility.Helper;
using Core.Enum;
using SiteMaster.Filters;
using Dto.Master;

namespace SiteMaster.Controllers
{
    public class DemolitiondocumentController : BaseController
    {
        private readonly IDemolitiondocumentService _demolitiondocumentService;
        public IConfiguration _configuration;
        string targetPhotoPathLayout = string.Empty;
        string targetReportfilePathLayout = string.Empty;

        public DemolitiondocumentController(IDemolitiondocumentService demolitiondocumentService, IConfiguration configuration)
        {
            _demolitiondocumentService = demolitiondocumentService;
            _configuration = configuration;
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {

            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DemolitiondocumentSearchDto model)
        {
            var result = await _demolitiondocumentService.GetPagedDemolitiondocument(model);

            return PartialView("_List", result);
        }
              
        
        
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Demolitiondocument demolitiondocument = new Demolitiondocument();
            demolitiondocument.IsActive = 1;
            return View(demolitiondocument);
        }
         [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Demolitiondocument demolitiondocument)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = await _demolitiondocumentService.Create(demolitiondocument);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _demolitiondocumentService.GetDemolitiondocument();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(demolitiondocument);
                    }
                }
                else
                {
                    return View(demolitiondocument);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(demolitiondocument);
            }
        }
         [AuthorizeContext(ViewAction.Edit)]
          public async Task<IActionResult> Edit(int id)
        {
            var Data = await _demolitiondocumentService.FetchSingleResult(id);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
         [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Demolitiondocument demolitiondocument)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _demolitiondocumentService.Update(id, demolitiondocument);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _demolitiondocumentService.GetDemolitiondocument();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(demolitiondocument);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(demolitiondocument);
                }
            }
            else
            {
                return View(demolitiondocument);
            }
        }
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _demolitiondocumentService.Delete(id);
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
            var list = await _demolitiondocumentService.GetDemolitiondocument();
            return View("Index", list);
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _demolitiondocumentService.FetchSingleResult(id);


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
      

        public async Task<IActionResult> DemolitionDocumentList()
        {
            var result = await _demolitiondocumentService.GetDemolitiondocument();
            List<DemolitionDocumentList> data = new List<DemolitionDocumentList>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new DemolitionDocumentList()
                    {
                        Id = result[i].Id,
                        DocumentName = result[i].DocumentName,
                        IsMandatory = result[i].IsMandatory,

                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    });
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }






    }
}
