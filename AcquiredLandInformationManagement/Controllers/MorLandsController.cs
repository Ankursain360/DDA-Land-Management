using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcquiredLandInformationManagement.Filters;
using Core.Enum;
using Dto.Master;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Utility.Helper;
using Microsoft.AspNetCore.Http;

using System.IO;

namespace AcquiredLandInformationManagement.Controllers
{
    public class MorLandsController : BaseController
    {
        private readonly IMorlandService _morlandService;
        public IConfiguration _configuration;
        string GOINotificationDocumentFilePath = "";
        public object JsonRequestBehavior { get; private set; }



        public MorLandsController(IMorlandService morlandService, IConfiguration configuration)
        {
            _morlandService = morlandService;
            _configuration = configuration;
            GOINotificationDocumentFilePath = _configuration.GetSection("FilePaths:MorLands:GOINotificationDocumentFIlePath").Value.ToString();
        }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] MorLandsSearchDto model)
        {
            var result = await _morlandService.GetPagedMorland(model);
            return PartialView("_List", result);
        }
        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> Exist(int Id, string Name)
        {
            var result = await _morlandService.CheckUniqueName(Id, Name);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Location/Village Name : {Name} already exist");
            }
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()

        {
            Morland morland = new Morland();
            morland.IsActive = 1;
            morland.LandNotificationList = await _morlandService.GetAllLandNotification();

           
            return View(morland);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Morland morland)
        {
            try
            {
                morland.LandNotificationList = await _morlandService.GetAllLandNotification();

           

                if (ModelState.IsValid)
                {
                    FileHelper fileHelper = new FileHelper();
                    morland.GOINotificationDocumentName = morland.GOINotificationDocumentIFormFile == null ? morland.GOINotificationDocumentName : fileHelper.SaveFile1(GOINotificationDocumentFilePath, morland.GOINotificationDocumentIFormFile);
                    morland.CreatedBy = SiteContext.UserId;
                    var result = await _morlandService.Create(morland);

                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _morlandService.GetAllMorland();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(morland);
                    }
                }
                else
                {
                    return View(morland);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(morland);
            }
        }



        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _morlandService.FetchSingleResult(id);
            Data.LandNotificationList = await _morlandService.GetAllLandNotification();

         

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Morland morland)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    FileHelper fileHelper = new FileHelper();
                    morland.GOINotificationDocumentName = morland.GOINotificationDocumentIFormFile == null ? morland.GOINotificationDocumentName : fileHelper.SaveFile1(GOINotificationDocumentFilePath, morland.GOINotificationDocumentIFormFile);
                    morland.ModifiedBy = SiteContext.UserId;
                    var result = await _morlandService.Update(id, morland);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _morlandService.GetAllMorland();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(morland);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(morland);
                }
            }
            else
            {
                return View(morland);
            }
        }
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _morlandService.Delete(id);
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
            var list = await _morlandService.GetAllMorland();
            return View("Index", list);
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _morlandService.FetchSingleResult(id);

            Data.LandNotificationList = await _morlandService.GetAllLandNotification();
          
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [AuthorizeContext(ViewAction.Download)]

        public async Task<IActionResult> MorlandList()
        {
            var result = await _morlandService.GetAllMorland();
            List<MorlandListDto> data = new List<MorlandListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new MorlandListDto()
                    {
                        Id = result[i].Id,
                        NotificationNo = result[i].LandNotification == null ? "" : result[i].LandNotification.NotificationNumber,
                        PropertySiteNo = result[i].PropertySiteNo,
                        LocationNameVillage = result[i].Name,
                        SiteDescription = result[i].SiteDescription,
                        Area = result[i].Area.ToString(),
                        StatusOfLand = result[i].StatusOfLand,
                       
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }


        public async Task<IActionResult> ViewGOINotificationDocument(int Id)
        {
            FileHelper file = new FileHelper();
            Morland Data = await _morlandService.FetchSingleResult(Id);
            string filename = GOINotificationDocumentFilePath + Data.GOINotificationDocumentName;
            byte[] FileBytes = System.IO.File.ReadAllBytes(filename);
            return File(FileBytes, file.GetContentType(filename));
        }


        [HttpPost]
        public JsonResult CheckFile()
        {
            bool IsImg = true;
            string fullpath = string.Empty;
            //   string fullpath = string.Empty;
            string extension = string.Empty;
            GOINotificationDocumentFilePath = _configuration.GetSection("FilePaths:MorLands:GOINotificationDocumentFIlePath").Value.ToString();
            IFormFile files = Request.Form.Files["file"];
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                GOINotificationDocumentFilePath = _configuration.GetSection("FilePaths:MorLands:GOINotificationDocumentFIlePath").Value.ToString();
                string FilePath = Path.Combine(GOINotificationDocumentFilePath, FileName);
                if (files.Length > 0)
                {
                    if (!Directory.Exists(GOINotificationDocumentFilePath))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(GOINotificationDocumentFilePath);// Try to create the directory.
                    }
                    try
                    {
                        if (extension.ToLower() == ".pdf")
                        {
                            try
                            {
                                using (var stream = new FileStream(FilePath, FileMode.Create))
                                {
                                    files.CopyTo(stream);

                                }

                                iTextSharp.text.pdf.PdfReader oPdfReader = new iTextSharp.text.pdf.PdfReader(FilePath);
                                oPdfReader.Close();
                                fullpath = _configuration.GetSection("FilePaths:MorLands:GOINotificationDocumentFIlePath").Value.ToString();

                                FileInfo doc = new FileInfo(fullpath);
                                if (doc.Exists)
                                {
                                    doc.Delete();
                                }
                            }
                            catch (iTextSharp.text.exceptions.InvalidPdfException)
                            {
                                IsImg = false;
                            }

                        }
                    }
                    catch (OutOfMemoryException ex)
                    {
                        IsImg = false;

                        if (System.IO.File.Exists(fullpath))
                        {
                            try
                            {
                                System.IO.File.Delete(fullpath);
                            }
                            catch (Exception exs)
                            {
                            }
                        }
                        // Image.FromFile will throw this if file is invalid.  
                    }

                }
            }

            return Json(IsImg, JsonRequestBehavior);
        }



    }
}
