using Core.Enum;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NewLandAcquisition.Filters;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.Helper;
using Dto.Master;



using Microsoft.AspNetCore.Http;

using System.IO;


namespace NewLandAcquisition.Controllers
{
    public class NewlandNotificationController : BaseController
    {
        public IConfiguration _configuration;
        public readonly INewlandnotificationService _newlandnotificationService;
        string NewlandNotificationFilePath = string.Empty;
        public object JsonRequestBehavior { get; private set; }


        public NewlandNotificationController(INewlandnotificationService newlandnotificationService, IConfiguration configuration)
        {
            _newlandnotificationService = newlandnotificationService;
            _configuration = configuration;
             NewlandNotificationFilePath = _configuration.GetSection("FilePaths:NewlandNotificationMasterFiles:NewlandNotificationFilePath").Value.ToString();


        }
        // [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] NewlandnotificationSearchDto model)
        {
            var result = await _newlandnotificationService.GetPagedNewlandnotificationdetails(model);

            return PartialView("_List", result);
        }
         [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Newlandnotification newlandnotification = new Newlandnotification();
            newlandnotification.notificationtypeList = await _newlandnotificationService.GetAllNotificationType();
            return View(newlandnotification);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Newlandnotification newlandnotification)
        {
            try
            {
                bool IsValidpdf = CheckMimeType(newlandnotification);
                newlandnotification.notificationtypeList = await _newlandnotificationService.GetAllNotificationType();
                string NewlandNotificationFilePath = _configuration.GetSection("FilePaths:NewlandNotificationMasterFiles:NewlandNotificationFilePath").Value.ToString();

                if (ModelState.IsValid)
                {
                    if (IsValidpdf == true)
                    { 

                        FileHelper fileHelper = new FileHelper();

                    if (newlandnotification.NewlandNotificationFile != null)
                    {
                        newlandnotification.GazetteNotificationFilePath = fileHelper.SaveFile1(NewlandNotificationFilePath, newlandnotification.NewlandNotificationFile);

                    }

                    newlandnotification.CreatedBy = SiteContext.UserId;
                    var result = await _newlandnotificationService.Create(newlandnotification);
                    if (result)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        var list = await _newlandnotificationService.GetAllNewlandNotification();

                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(newlandnotification);
                    }
                }
                else 
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                    return View(newlandnotification);
                }
                }
                else
                {
                    return View(newlandnotification);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(newlandnotification);
            }
        }


       


        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _newlandnotificationService.FetchSingleResult1(id);          
            Data.notificationtypeList = await _newlandnotificationService.GetAllNotificationType();

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        


       [HttpPost]
       
        public async Task<IActionResult> Edit(int id, Newlandnotification newlandnotification)
        {
            newlandnotification.notificationtypeList = await _newlandnotificationService.GetAllNotificationType();
            bool IsValidpdf = CheckMimeType(newlandnotification);
            if (ModelState.IsValid)
            {
                if (IsValidpdf == true)
                {
                    newlandnotification.ModifiedBy = SiteContext.UserId;
                    var result = await _newlandnotificationService.Update(id, newlandnotification);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var list = await _newlandnotificationService.GetAllNewlandNotification();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(newlandnotification);

                    }
                }
                else
                {

                    ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                    return View(newlandnotification);

                }
            }
            return View(newlandnotification);
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _newlandnotificationService.FetchSingleResult1(id);
            Data.notificationtypeList = await _newlandnotificationService.GetAllNotificationType();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        


        public async Task<IActionResult> ViewDocumenbtsdG(int Id)
        {
            FileHelper fileHelper = new FileHelper();         
            Newlandnotification Data = await _newlandnotificationService.NewLandNotificationFile(Id);
            string filename = Data.GazetteNotificationFilePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(filename);
            return File(FileBytes, fileHelper.GetContentType(filename));
        }


        public async Task<IActionResult> NewLandNotificationList()
        {
            var result = await _newlandnotificationService.GetAllNewlandNotification();
            List<NewLandNotificationListDto> data = new List<NewLandNotificationListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new NewLandNotificationListDto()
                    {
                        Id = result[i].Id,
                        NotificationNo = result[i].NotificationNo,
                        notificationDate = result[i].Date.ToString(),
                        Remarks = result[i].Remarks,                     

                        IsActive = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }
            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }



        [HttpPost]
        public JsonResult CheckFile()
        {
            bool IsImg = true;
            string fullpath = string.Empty;
            //   string fullpath = string.Empty;
            string extension = string.Empty;
            NewlandNotificationFilePath = _configuration.GetSection("FilePaths:NewlandNotificationMasterFiles:NewlandNotificationFilePath").Value.ToString();
            IFormFile files = Request.Form.Files["file"];
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                NewlandNotificationFilePath = _configuration.GetSection("FilePaths:NewlandNotificationMasterFiles:NewlandNotificationFilePath").Value.ToString();
                string FilePath = Path.Combine(NewlandNotificationFilePath, FileName);
                if (files.Length > 0)
                {
                    if (!Directory.Exists(NewlandNotificationFilePath))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(NewlandNotificationFilePath);// Try to create the directory.
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
                                fullpath = _configuration.GetSection("FilePaths:NewlandNotificationMasterFiles:NewlandNotificationFilePath").Value.ToString();

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



      
        public bool CheckMimeType(Newlandnotification data)
        {
            bool Flag = true;
            string fullpath = string.Empty;
            //   string fullpath = string.Empty;
            string extension = string.Empty;
            NewlandNotificationFilePath = _configuration.GetSection("FilePaths:NewlandNotificationMasterFiles:NewlandNotificationFilePath").Value.ToString();
            IFormFile files = data.NewlandNotificationFile;
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                NewlandNotificationFilePath = _configuration.GetSection("FilePaths:NewlandNotificationMasterFiles:NewlandNotificationFilePath").Value.ToString();
                string FilePath = Path.Combine(NewlandNotificationFilePath, FileName);
                if (files.Length > 0)
                {
                    if (!Directory.Exists(NewlandNotificationFilePath))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(NewlandNotificationFilePath);// Try to create the directory.
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
                                fullpath = _configuration.GetSection("FilePaths:NewlandNotificationMasterFiles:NewlandNotificationFilePath").Value.ToString();

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
