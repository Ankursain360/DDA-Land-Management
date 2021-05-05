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
using LeaseDetails.Models;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.AspNetCore.Authorization;
using Dto.Search;
using Microsoft.Extensions.Configuration;
using Utility.Helper;
using Microsoft.AspNetCore.Http;
using System.IO;
using LeaseDetails.Filters;
using Core.Enum;
using Dto.Master;

namespace LeaseDetails.Controllers
{
    public class PossesionplanController : BaseController
    {
        private readonly IPossesionplanService _possesionplanService;
        public IConfiguration _configuration;
        public PossesionplanController(IPossesionplanService possesionplanService, IConfiguration configuration)
        {
            _configuration = configuration;
            _possesionplanService = possesionplanService;
        }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> BindLeaseApplicationDetails(int? appId)
        {
            appId = appId ?? 0;
            var leaseappId =0;
            var result = await _possesionplanService.BindAllotmentDetails(Convert.ToInt32(appId));
            if (result != null)
            {
                 leaseappId = result[0].ApplicationId;
              
            }
            return Json(await _possesionplanService.BindLeaseApplicationDetails(Convert.ToInt32(leaseappId)));

        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] PossesionplanSearchDto model)
        {

            var result = await _possesionplanService.GetPagedPossesionPlan(model);
            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Possesionplan rate = new Possesionplan();
            rate.IsActive = 1;
            rate.AllotmententryList = await _possesionplanService.GetAllAllotmententry();
            rate.LeaseApplicationList = await _possesionplanService.GetAllLeaseApplication();
            return View(rate);
           // return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Possesionplan rate)
        {
            rate.AllotmententryList = await _possesionplanService.GetAllAllotmententry();
            rate.LeaseApplicationList = await _possesionplanService.GetAllLeaseApplication();
          string PossesionplanFilePath = _configuration.GetSection("FilePaths:Possesionplan:PossesionplanFilePath").Value.ToString();

            try
            {

                if (ModelState.IsValid)
                {
                    FileHelper fileHelper = new FileHelper();
                    if (rate.StayFile != null)
                    {
                        rate.SitePlanFilePath = fileHelper.SaveFile(PossesionplanFilePath, rate.StayFile);
                    }
                    rate.CreatedBy = SiteContext.UserId;
                    var result = await _possesionplanService.Create(rate);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        return RedirectToAction("Index", "Possesionplan");
                        //return View();
                        //var list = await _possesionplanService.GetAllPossesionplan();
                        //return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(rate);

                    }
                }
                else
                {
                    return View(rate);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(rate);
            }
        }
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _possesionplanService.FetchSingleResult(id);
            Data.AllotmententryList = await _possesionplanService.GetAllAllotmententry();
            Data.LeaseApplicationList = await _possesionplanService.GetAllLeaseApplication();
            ViewBag.ExistStayFile = Data.SitePlanFilePath;
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Possesionplan rate, IFormFile AssignSIfile)
        {
            rate.AllotmententryList = await _possesionplanService.GetAllAllotmententry();
            rate.LeaseApplicationList = await _possesionplanService.GetAllLeaseApplication();
            ViewBag.ExistStayFile = rate.SitePlanFilePath ;

            if (ModelState.IsValid)
            {
                /*.......For Stay Interim File .......*/
                string FileNameS = "";
                string filePathS = "";
                rate.StayFile = AssignSIfile;
                string PossesionplanFilePath = _configuration.GetSection("FilePaths:Possesionplan:PossesionplanFilePath").Value.ToString();



                if (rate.StayFile != null)
                {
                    if (!Directory.Exists(PossesionplanFilePath))
                    {
                        // Try to create the directory.
                        DirectoryInfo dij = Directory.CreateDirectory(PossesionplanFilePath);
                    }
                    FileNameS = Guid.NewGuid().ToString() + "_" + rate.StayFile.FileName;
                    filePathS = Path.Combine(PossesionplanFilePath, FileNameS);
                    using (var stream = new FileStream(filePathS, FileMode.Create))
                    {
                        rate.StayFile.CopyTo(stream);
                    }
                    rate.SitePlanFilePath = filePathS;
                }
                try
                {

                    rate.ModifiedBy = SiteContext.UserId;
                    var result = await _possesionplanService.Update(id, rate);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        return RedirectToAction("Index", "Possesionplan");
                        //var list = await _possesionplanService.GetAllPossesionplan();
                        //return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(rate);

                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(rate);

                }
            }
            return View(rate);
        }

        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _possesionplanService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                return RedirectToAction("Index", "Possesionplan");
                //var result1 = await _possesionplanService.GetAllPossesionplan();
                //return View("Index", result1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                var result1 = await _possesionplanService.GetAllPossesionplan();
                return View("Index", result1);
            }
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _possesionplanService.FetchSingleResult(id);
            Data.AllotmententryList = await _possesionplanService.GetAllAllotmententry();
            Data.LeaseApplicationList = await _possesionplanService.GetAllLeaseApplication();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> DownloadExcel()
        {
            List<Possesionplan> result = await _possesionplanService.GetAllPossesionplan();
            var memory = ExcelHelper.CreateExcel(result);
            string sFileName = @"PossesionPlan.xlsx";
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }
        public async Task<IActionResult> Download(int Id)
        {
            string filename = _possesionplanService.GetDownload(Id);
            var memory = new MemoryStream();
            using (var stream = new FileStream(filename, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(filename), Path.GetFileName(filename));
        }



        public async Task<IActionResult> PossesionPlanList()
        {
            var result = await _possesionplanService.GetAllPossesionplan();
            List<PossesionListDto> data = new List<PossesionListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new PossesionListDto()
                    {
                        Id = result[i].Id,
                        AllotmentNo = result[i].Allotment == null ? "" : result[i].Allotment.Name,
                        DiffrenceInArea= result[i].DiffernceArea.ToString(),
                        PossesionTakenName= result[i].PossessionTakenName.ToString(),
                        PossesionHandoverName= result[i].PossesionHandOverName.ToString(),
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    });
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }
    }
}

