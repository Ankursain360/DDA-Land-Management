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
using Microsoft.Extensions.Configuration;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Utility.Helper;
using Microsoft.AspNetCore.Http;

using System.IO;

namespace AcquiredLandInformationManagement.Controllers
{
   
   public class LDOlandController : BaseController
    {
        private readonly ILdolandService _ldolandService;
        public IConfiguration _configuration;
        string GOINotificationDocumentFilePath = "";
        string OrderDocumentFilePath = "";
        string PossessionDocumentFilePath = "";
        public object JsonRequestBehavior { get; private set; }


        public LDOlandController(ILdolandService ldolandService, IConfiguration configuration)
        {
            _ldolandService = ldolandService;
            _configuration = configuration;
            GOINotificationDocumentFilePath = _configuration.GetSection("FilePaths:LDOLands:GOINotificationDocumentFIlePath").Value.ToString();
            OrderDocumentFilePath = _configuration.GetSection("FilePaths:LDOLands:OrderDocumentFIlePath").Value.ToString();
            PossessionDocumentFilePath = _configuration.GetSection("FilePaths:LDOLands:PossessionDocumentFIlePath").Value.ToString();

        }

        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        { 
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] LdolandSearchDto model)
        {
            var result = await _ldolandService.GetPagedLdoland(model);
            return PartialView("_List", result);
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Ldoland ldoland = new Ldoland();
            ldoland.IsActive = 1;

            //ldoland.LandNotificationList = await _ldolandService.GetAllLandNotification();
            ldoland.OtherlandnotificationList = await _ldolandService.GetAllOtherLandNotification();
            return View(ldoland);
        }


        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Ldoland ldoland)
        {
            bool IsValidpdf = CheckMimeType(ldoland);
            try
            {
                ldoland.OtherlandnotificationList = await _ldolandService.GetAllOtherLandNotification();
            
                if (ModelState.IsValid)
                {
                    if (IsValidpdf == true)
                    {
                        FileHelper fileHelper = new FileHelper();
                        ldoland.GOINotificationDocumentName = ldoland.GOINotificationDocumentIFormFile == null ? ldoland.GOINotificationDocumentName : fileHelper.SaveFile1(GOINotificationDocumentFilePath, ldoland.GOINotificationDocumentIFormFile);
                        ldoland.OrderDocumentName = ldoland.OrderDocumentIFormFile == null ? ldoland.OrderDocumentName : fileHelper.SaveFile1(OrderDocumentFilePath, ldoland.OrderDocumentIFormFile);
                        ldoland.PossessionDocumentName = ldoland.PossessionDocumentIFormFile == null ? ldoland.PossessionDocumentName : fileHelper.SaveFile1(PossessionDocumentFilePath, ldoland.PossessionDocumentIFormFile);


                        ldoland.IsActive = 1;
                        ldoland.CreatedBy = SiteContext.UserId;
                        var result = await _ldolandService.Create(ldoland);

                        if (result == true)
                        {
                            ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                            var list = await _ldolandService.GetAllLdoland();
                            return View("Index", list);
                        }
                        else
                        {
                            ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                            return View(ldoland);

                        }
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                        return View(ldoland);
                    }
                }
                else
                {
                    return View(ldoland);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(ldoland);
            }
        }
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {

            var Data = await _ldolandService.FetchSingleResult(id);
            Data.OtherlandnotificationList = await _ldolandService.GetAllOtherLandNotification();
      
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Ldoland ldoland)
        {
            bool IsValidpdf = CheckMimeType(ldoland);

            ldoland.OtherlandnotificationList = await _ldolandService.GetAllOtherLandNotification();
            if (ModelState.IsValid)
            {
                if (IsValidpdf == true)
                {
                    try
                    {
                        FileHelper fileHelper = new FileHelper();
                        ldoland.GOINotificationDocumentName = ldoland.GOINotificationDocumentIFormFile == null ? ldoland.GOINotificationDocumentName : fileHelper.SaveFile1(GOINotificationDocumentFilePath, ldoland.GOINotificationDocumentIFormFile);
                        ldoland.OrderDocumentName = ldoland.OrderDocumentIFormFile == null ? ldoland.OrderDocumentName : fileHelper.SaveFile1(OrderDocumentFilePath, ldoland.OrderDocumentIFormFile);
                        ldoland.PossessionDocumentName = ldoland.PossessionDocumentIFormFile == null ? ldoland.PossessionDocumentName : fileHelper.SaveFile1(PossessionDocumentFilePath, ldoland.PossessionDocumentIFormFile);

                        ldoland.ModifiedBy = SiteContext.UserId;
                        var result = await _ldolandService.Update(id, ldoland);
                        if (result == true)
                        {
                            ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                            var list = await _ldolandService.GetAllLdoland();
                            return View("Index", list);
                        }
                        else
                        {
                            ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                            return View(ldoland);

                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                    return View(ldoland);

                }
            }
            return View(ldoland);
        }

      

        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)  
        {
            if (id == 0)
            {
                return NotFound();
            }

            var form = await _ldolandService.Delete(id);
            if (form == false)
            {
                return NotFound();
            }
            var result = await _ldolandService.GetAllLdoland();
            ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            return View("Index", result);
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _ldolandService.FetchSingleResult(id);
           
            Data.OtherlandnotificationList = await _ldolandService.GetAllOtherLandNotification();

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

      
      
        [AuthorizeContext(ViewAction.Download)]

        public async Task<IActionResult> LdolandList([FromBody] LdolandSearchDto model)
        {
            var result = await _ldolandService.GetAllLdolandList(model);
            List<LdolandListDto> data = new List<LdolandListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new LdolandListDto()
                    {
                        Id = result[i].Id,
                        NotificationNo = result[i].OtherLandNotification == null ? "" : result[i].OtherLandNotification.NotificationNumber,
                        NotificationDate = Convert.ToDateTime(result[i].NotificationDate).ToString("dd-MMM-yyyy"),
                        SerialNo = result[i].SerialNumber == null ? ' ' : result[i].SerialNumber,
                        PropertySiteNo =result[i].PropertySiteNo,
                        LocationNameVillage = result[i].Location,
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }
            //var memory = ExcelHelper.CreateExcel(data);
            //TempData["file"] = memory;
            //return Ok();
            var memory = ExcelHelper.CreateExcel(data);
            HttpContext.Session.Set("file", memory);
            return Ok();
        }

        [HttpGet]
        [AuthorizeContext(ViewAction.Download)]
        public virtual ActionResult download()
        {
            //byte[] data = TempData["file"] as byte[];
            //return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            byte[] data = HttpContext.Session.Get("file") as byte[];
            HttpContext.Session.Remove("file");
            // var dem = Decompress(data);
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
        public async Task<IActionResult> ViewGOINotificationDocument(int Id)
        {
            FileHelper file = new FileHelper();
            Ldoland Data = await _ldolandService.FetchSingleResult(Id);
            string filename = GOINotificationDocumentFilePath + Data.GOINotificationDocumentName;
            byte[] FileBytes = System.IO.File.ReadAllBytes(filename);
            return File(FileBytes, file.GetContentType(filename));
        }
        public async Task<IActionResult> ViewOrderDocument(int Id)
        {
            FileHelper file = new FileHelper();
            Ldoland Data = await _ldolandService.FetchSingleResult(Id);
            string filename = OrderDocumentFilePath + Data.OrderDocumentName;
            byte[] FileBytes = System.IO.File.ReadAllBytes(filename);
            return File(FileBytes, file.GetContentType(filename));
        }
        public async Task<IActionResult> ViewPossessionDocument(int Id)
        {
            FileHelper file = new FileHelper();
            Ldoland Data = await _ldolandService.FetchSingleResult(Id);
            string filename = PossessionDocumentFilePath + Data.PossessionDocumentName;
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
            GOINotificationDocumentFilePath = _configuration.GetSection("FilePaths:LDOLands:GOINotificationDocumentFIlePath").Value.ToString();
            IFormFile files = Request.Form.Files["file"];
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                GOINotificationDocumentFilePath = _configuration.GetSection("FilePaths:LDOLands:GOINotificationDocumentFIlePath").Value.ToString();
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
                                fullpath = _configuration.GetSection("FilePaths:LDOLands:GOINotificationDocumentFIlePath").Value.ToString();
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



        public bool CheckMimeType(Ldoland ldoland)
        {
            bool Flag = true;
            string fullpath = string.Empty;          
            string extension = string.Empty;
            GOINotificationDocumentFilePath = _configuration.GetSection("FilePaths:LDOLands:GOINotificationDocumentFIlePath").Value.ToString();
            IFormFile files = ldoland.GOINotificationDocumentIFormFile;
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                GOINotificationDocumentFilePath = _configuration.GetSection("FilePaths:LDOLands:GOINotificationDocumentFIlePath").Value.ToString();
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
                                fullpath = _configuration.GetSection("FilePaths:LDOLands:GOINotificationDocumentFIlePath").Value.ToString();
                                FileInfo doc = new FileInfo(fullpath);
                                if (doc.Exists)
                                {
                                    doc.Delete();
                                }
                            }
                            catch (iTextSharp.text.exceptions.InvalidPdfException)
                            {
                                Flag = false;
                            }

                        }
                        else
                        {
                            Flag = false;
                        }
                    }
                    catch (OutOfMemoryException ex)
                    {
                        Flag = false;

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
                      
                    }

                }
            }

            return Flag;
        }




    }




}
