using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Dto.Search;
using AcquiredLandInformationManagement.Filters;
using Core.Enum;
using Utility.Helper;
using Dto.Master;
using Newtonsoft.Json;
using System.Text;

namespace AcquiredLandInformationManagement.Controllers
{
    public class DemandListDetailsController : BaseController
    {
        private readonly IDemandListDetailsService _demandListDetailsService;
        public IConfiguration _Configuration;
        string ENMDocumentFilePath = "";

        public DemandListDetailsController(IDemandListDetailsService demandListDetailsService, IConfiguration configuration)
        {
            _demandListDetailsService = demandListDetailsService;
            _Configuration = configuration;
            ENMDocumentFilePath = _Configuration.GetSection("FilePaths:DemandListDetails:DocumentFIlePath").Value.ToString();
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            ViewBag.VillageList = await _demandListDetailsService.GetVillageList();
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DemandListDetailsSearchDto model)
        {
            var result = await _demandListDetailsService.GetPagedDMSFileUploadList(model);
            return PartialView("_List", result);
        }
        async Task BindDropDown(Demandlistdetails demandlistdetails)
        {
            demandlistdetails.VillageList = await _demandListDetailsService.GetVillageList();
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Demandlistdetails demandlistdetails = new Demandlistdetails();
            demandlistdetails.IsActive = 1;
            await BindDropDown(demandlistdetails);
            return View(demandlistdetails);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Create(Demandlistdetails demandlistdetails)
        {
            await BindDropDown(demandlistdetails);
            demandlistdetails.VillageList = await _demandListDetailsService.GetVillageList();

            if (ModelState.IsValid)
            {
                FileHelper fileHelper = new FileHelper();
                demandlistdetails.ENMDocumentName = demandlistdetails.ENMDocumentIFormFile == null ? demandlistdetails.ENMDocumentName : fileHelper.SaveFile1(ENMDocumentFilePath, demandlistdetails.ENMDocumentIFormFile);
                demandlistdetails.CreatedBy = SiteContext.UserId;
                var result = await _demandListDetailsService.Create(demandlistdetails);

                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                    ViewBag.VillageList = await _demandListDetailsService.GetVillageList();
                    return View("Index");
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    await BindDropDown(demandlistdetails);
                    return View(demandlistdetails);

                }
            }
            else
            {
                return View(demandlistdetails);
            }

        }


        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _demandListDetailsService.FetchSingleResult(id);
            await BindDropDown(Data);
            Data.KhasraNoList = await _demandListDetailsService.GetKhasraList(Data.VillageId);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Edit)]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Edit(int id, Demandlistdetails demandlistdetails)
        {
            await BindDropDown(demandlistdetails);
            demandlistdetails.VillageList = await _demandListDetailsService.GetVillageList();

            if (ModelState.IsValid)
            {
                FileHelper fileHelper = new FileHelper();
                demandlistdetails.ENMDocumentName = demandlistdetails.ENMDocumentIFormFile == null ? demandlistdetails.ENMDocumentName : fileHelper.SaveFile1(ENMDocumentFilePath, demandlistdetails.ENMDocumentIFormFile);
                demandlistdetails.ModifiedBy = SiteContext.UserId;
                var result = await _demandListDetailsService.Update(id, demandlistdetails);

                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                    ViewBag.VillageList = await _demandListDetailsService.GetVillageList();
                    return View("Index");
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    await BindDropDown(demandlistdetails);
                    return View(demandlistdetails);

                }
            }
            else
            {
                return View(demandlistdetails);
            }
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _demandListDetailsService.FetchSingleResult(id);
            await BindDropDown(Data);
            Data.KhasraNoList = await _demandListDetailsService.GetKhasraList(Data.VillageId);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _demandListDetailsService.Delete(id, SiteContext.UserId);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            ViewBag.VillageList = await _demandListDetailsService.GetVillageList();
            return View("Index");
        }
        [HttpGet]
        public async Task<JsonResult> GetKhasraList(int? Id)
        {
            Id = Id ?? 0;
            return Json(await _demandListDetailsService.GetKhasraList(Convert.ToInt32(Id)));
        }


        [AuthorizeContext(ViewAction.Download)]

        public async Task<IActionResult> DemanddetailsList()
        {
            var result = await _demandListDetailsService.GetAllDemandlistdetails();
            List<DemanddetailsListDto> data = new List<DemanddetailsListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new DemanddetailsListDto()
                    {
          
                        Id = result[i].Id,
                        DemandListNo = result[i].DemandListNo,
                        Village = result[i].Village == null ? "" : result[i].Village.Name,
                        KhasraNo = result[i].KhasraNo == null ? "" : result[i].KhasraNo.Name,
                        ENMSrNo = result[i].Enmsno.ToString(),
                        TotalAmount = result[i].TotalAmount.ToString(),
                       
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }

        public async Task<IActionResult> ViewENMDocument(int Id)
        {
            FileHelper file = new FileHelper();
            Demandlistdetails Data = await _demandListDetailsService.FetchSingleResult(Id);
            string filename = ENMDocumentFilePath + Data.ENMDocumentName;
            byte[] FileBytes = System.IO.File.ReadAllBytes(filename);
            return File(FileBytes, file.GetContentType(filename));
        }
    }

}
