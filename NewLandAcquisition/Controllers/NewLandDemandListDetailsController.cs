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
using NewLandAcquisition.Filters;
using Core.Enum;
using Utility.Helper;
using Dto.Master;
using Newtonsoft.Json;
using System.Text;

namespace NewLandAcquisition.Controllers
{
    public class NewLandDemandListDetailsController : BaseController
    {
        private readonly INewLandDemandListDetailsService _newLandDemandListDetailsService;
        public IConfiguration _Configuration;
        string ENMDocumentFilePath = "";

        public NewLandDemandListDetailsController(INewLandDemandListDetailsService newLandDemandListDetailsService, IConfiguration configuration)
        {
            _newLandDemandListDetailsService = newLandDemandListDetailsService;
            _Configuration = configuration;
            ENMDocumentFilePath = _Configuration.GetSection("FilePaths:NewLandDemandListDetails:DocumentFIlePath").Value.ToString();
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            ViewBag.VillageList = await _newLandDemandListDetailsService.GetVillageList();
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] NewLandDemandListDetailsSearchDto model)
        {
            var result = await _newLandDemandListDetailsService.GetPagedDMSFileUploadList(model);
            return PartialView("_List", result);
        }
        async Task BindDropDown(Newlanddemandlistdetails newlanddemandlistdetails)
        {
            newlanddemandlistdetails.VillageList = await _newLandDemandListDetailsService.GetVillageList();
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Newlanddemandlistdetails newlanddemandlistdetails = new Newlanddemandlistdetails();
            newlanddemandlistdetails.IsActive = 1;
            await BindDropDown(newlanddemandlistdetails);
            return View(newlanddemandlistdetails);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Create(Newlanddemandlistdetails newlanddemandlistdetails)
        {
            await BindDropDown(newlanddemandlistdetails);
            newlanddemandlistdetails.VillageList = await _newLandDemandListDetailsService.GetVillageList();

            if (ModelState.IsValid)
            {
                FileHelper fileHelper = new FileHelper();
                newlanddemandlistdetails.ENMDocumentName = newlanddemandlistdetails.ENMDocumentIFormFile == null ? newlanddemandlistdetails.ENMDocumentName : fileHelper.SaveFile1(ENMDocumentFilePath, newlanddemandlistdetails.ENMDocumentIFormFile);
                newlanddemandlistdetails.CreatedBy = SiteContext.UserId;
                var result = await _newLandDemandListDetailsService.Create(newlanddemandlistdetails);

                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                    ViewBag.VillageList = await _newLandDemandListDetailsService.GetVillageList();
                    return View("Index");
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    await BindDropDown(newlanddemandlistdetails);
                    return View(newlanddemandlistdetails);

                }
            }
            else
            {
                return View(newlanddemandlistdetails);
            }

        }


        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _newLandDemandListDetailsService.FetchSingleResult(id);
            await BindDropDown(Data);
            Data.KhasraNoList = await _newLandDemandListDetailsService.GetKhasraList(Data.VillageId);
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
        public async Task<IActionResult> Edit(int id, Newlanddemandlistdetails newlanddemandlistdetails)
        {
            await BindDropDown(newlanddemandlistdetails);
            newlanddemandlistdetails.VillageList = await _newLandDemandListDetailsService.GetVillageList();

            if (ModelState.IsValid)
            {
                FileHelper fileHelper = new FileHelper();
                newlanddemandlistdetails.ENMDocumentName = newlanddemandlistdetails.ENMDocumentIFormFile == null ? newlanddemandlistdetails.ENMDocumentName : fileHelper.SaveFile1(ENMDocumentFilePath, newlanddemandlistdetails.ENMDocumentIFormFile);
                newlanddemandlistdetails.ModifiedBy = SiteContext.UserId;
                var result = await _newLandDemandListDetailsService.Update(id, newlanddemandlistdetails);

                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                    ViewBag.VillageList = await _newLandDemandListDetailsService.GetVillageList();
                    return View("Index");
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    await BindDropDown(newlanddemandlistdetails);
                    return View(newlanddemandlistdetails);

                }
            }
            else
            {
                return View(newlanddemandlistdetails);
            }
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _newLandDemandListDetailsService.FetchSingleResult(id);
            await BindDropDown(Data);
            Data.KhasraNoList = await _newLandDemandListDetailsService.GetKhasraList(Data.VillageId);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _newLandDemandListDetailsService.Delete(id, SiteContext.UserId);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            ViewBag.VillageList = await _newLandDemandListDetailsService.GetVillageList();
            return View("Index");
        }
        [HttpGet]
        public async Task<JsonResult> GetKhasraList(int? Id)
        {
            Id = Id ?? 0;
            return Json(await _newLandDemandListDetailsService.GetKhasraList(Convert.ToInt32(Id)));
        }


        [AuthorizeContext(ViewAction.Download)]

        public async Task<IActionResult> DemanddetailsList()
        {
            var result = await _newLandDemandListDetailsService.GetAllDemandlistdetails();
            List<NewLandDemanddetailsListDto> data = new List<NewLandDemanddetailsListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new NewLandDemanddetailsListDto()
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
            Newlanddemandlistdetails Data = await _newLandDemandListDetailsService.FetchSingleResult(Id);
            string filename = ENMDocumentFilePath + Data.ENMDocumentName;
            byte[] FileBytes = System.IO.File.ReadAllBytes(filename);
            return File(FileBytes, file.GetContentType(filename));
        }
    }

}

