using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Dto.Search;
using AcquiredLandInformationManagement.Filters;
using Core.Enum;
using Utility.Helper;
using Dto.Master;
using System.Text;
using Microsoft.Extensions.Configuration;




using Microsoft.AspNetCore.Http;

using System.IO;


namespace AcquiredLandInformationManagement.Controllers
{
    public class PossessionDetailController : Controller
    {
        public IConfiguration _configuration;
        private readonly IPossessiondetailsService _Possessiondetailservice;
        string DocumentFilePath = "";

        public object JsonRequestBehavior { get; private set; }

        public PossessionDetailController(IPossessiondetailsService possessiondetailsService, IConfiguration configuration)
        {
            _Possessiondetailservice = possessiondetailsService;
            _configuration = configuration;
            DocumentFilePath = _configuration.GetSection("FilePaths:Possesion:DocumentFIlePath").Value.ToString();
        }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {

            return View();
        }


        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] PossessiondetailsSearchDto model)
        {
            var result = await _Possessiondetailservice.GetPagedNoPossessiondetails(model);

            return PartialView("_List", result);
        }



        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Possessiondetails undersection4plot = new Possessiondetails();
            undersection4plot.IsActive = 1;
         
            undersection4plot.KhasraList = await _Possessiondetailservice.BindKhasra(undersection4plot.VillageId);
            undersection4plot.VillageList = await _Possessiondetailservice.GetAllVillage();

            return View(undersection4plot);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Possessiondetails undersection4plot)
        {
            bool IsValidpdf = CheckMimeType(undersection4plot);
            try
            {
             
                undersection4plot.KhasraList = await _Possessiondetailservice.BindKhasra(undersection4plot.VillageId);
                undersection4plot.VillageList = await _Possessiondetailservice.GetAllVillage();
                StringBuilder str = new StringBuilder();
                if(undersection4plot.IsVacant == true)
                {
                   str.Append("Vacant");
                }
                if (undersection4plot.IsBuiltup == true)
                {
                    if(str.Length != 0)
                        str.Append("|");
                    str.Append("Built Up");
                }

                if (ModelState.IsValid)
                {
                    if (IsValidpdf == true)
                    {
                        FileHelper fileHelper = new FileHelper();
                        undersection4plot.DocumentName = undersection4plot.DocumentIFormFile == null ? undersection4plot.DocumentName : fileHelper.SaveFile1(DocumentFilePath, undersection4plot.DocumentIFormFile);
                        undersection4plot.PossType = str.ToString();
                        var result = await _Possessiondetailservice.Create(undersection4plot);

                        if (result == true)
                        {
                            ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                            var list = await _Possessiondetailservice.GetAllPossessiondetails();
                            return View("Index", list);
                        }
                        else
                        {
                            ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                            return View(undersection4plot);
                        }
                    }
                    else
                    {

                        ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                        return View(undersection4plot);

                    }
                }
                else
                {
                    return View(undersection4plot);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(undersection4plot);
            }
        }


        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _Possessiondetailservice.FetchSingleResult(id);         
            Data.KhasraList = await _Possessiondetailservice.BindKhasra(Data.VillageId);
            Data.VillageList = await _Possessiondetailservice.GetAllVillage();

            if (Data.PossType != "")
            {
                string[] multiTo = Data.PossType.Split('|');
                foreach (string Multi in multiTo)
                {
                    if (Multi == "Vacant")
                        Data.IsVacant = true;
                    if (Multi == "Built Up")
                        Data.IsBuiltup = true;
                }
            }
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Possessiondetails undersection4plot)
        {
            bool IsValidpdf = CheckMimeType(undersection4plot);

            undersection4plot.KhasraList = await _Possessiondetailservice.BindKhasra(undersection4plot.VillageId);
            undersection4plot.VillageList = await _Possessiondetailservice.GetAllVillage();
            StringBuilder str = new StringBuilder();
            if (undersection4plot.IsVacant == true)
            {
                str.Append("Vacant");
            }
            if (undersection4plot.IsBuiltup == true)
            {
                if (str.Length != 0)
                    str.Append("|");
                str.Append("Built Up");
            }

            if (ModelState.IsValid)
            {
                if (IsValidpdf == true)
                { 
                    try
                    {
                        FileHelper fileHelper = new FileHelper();
                        undersection4plot.DocumentName = undersection4plot.DocumentIFormFile == null ? undersection4plot.DocumentName : fileHelper.SaveFile1(DocumentFilePath, undersection4plot.DocumentIFormFile);
                        undersection4plot.PossType = str.ToString();
                        var result = await _Possessiondetailservice.Update(id, undersection4plot);
                        if (result == true)
                        {
                            ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                            var list = await _Possessiondetailservice.GetAllPossessiondetails();
                            return View("Index", list);
                        }
                        else
                        {
                            ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                            return View(undersection4plot);
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(undersection4plot);
                    }
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                return View(undersection4plot);

            }
            }
            else
            {
                return View(undersection4plot);
            }
        }
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _Possessiondetailservice.Delete(id);
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
            var list = await _Possessiondetailservice.GetAllPossessiondetails();
            return View("Index", list);
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _Possessiondetailservice.FetchSingleResult(id);
         
            Data.KhasraList = await _Possessiondetailservice.BindKhasra(Data.VillageId);
            Data.VillageList = await _Possessiondetailservice.GetAllVillage();
            if (Data.PossType != "")
            {
                string[] multiTo = Data.PossType.Split('|');
                foreach (string Multi in multiTo)
                {
                    if (Multi == "Vacant")
                        Data.IsVacant = true;
                    if (Multi == "Built Up")
                        Data.IsBuiltup = true;
                }
            }

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


        [HttpGet]
        public async Task<JsonResult> GetKhasraList(int? villageId)
        {
            villageId = villageId ?? 0;
            return Json(await _Possessiondetailservice.BindKhasra(Convert.ToInt32(villageId)));
        }




        [HttpGet]
        public async Task<JsonResult> GetAreaList(int? khasraid)
        {
            khasraid = khasraid ?? 0;

            return Json(await _Possessiondetailservice.FetchSingleKhasraResult(Convert.ToInt32(khasraid)));
        }

        [AuthorizeContext(ViewAction.Download)]
        public async Task<IActionResult> PossessiondetailsList()
        {
            var result = await _Possessiondetailservice.GetAllPossessiondetails();
            List<PossessiondetailsListDto> data = new List<PossessiondetailsListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new PossessiondetailsListDto()
                    {
                        Id = result[i].Id,
                        VillageName = result[i].Village == null ? "" : result[i].Village.Name,
                        KhasraNo = result[i].Khasra == null ? "" : result[i].Khasra.Name,
                        Date = Convert.ToDateTime(result[i].PossDate).ToString("dd-MMM-yyyy"),
                        PlotNo = result[i].PlotNo,
                       
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }
        public async Task<IActionResult> ViewUploadedDocument(int Id)
        {
            FileHelper file = new FileHelper();
            Possessiondetails Data = await _Possessiondetailservice.FetchSingleResult(Id);
            string filename = DocumentFilePath + Data.DocumentName;
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
            DocumentFilePath = _configuration.GetSection("FilePaths:Possesion:DocumentFIlePath").Value.ToString();
            IFormFile files = Request.Form.Files["file"];
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                DocumentFilePath = _configuration.GetSection("FilePaths:Possesion:DocumentFIlePath").Value.ToString();
                string FilePath = Path.Combine(DocumentFilePath, FileName);
                if (files.Length > 0)
                {
                    if (!Directory.Exists(DocumentFilePath))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(DocumentFilePath);// Try to create the directory.
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
                                fullpath = _configuration.GetSection("FilePaths:Possesion:DocumentFIlePath").Value.ToString();
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


        public bool CheckMimeType(Possessiondetails possessiondetails)
        {
            bool Flag = true;
            string fullpath = string.Empty;           
            string extension = string.Empty;
            DocumentFilePath = _configuration.GetSection("FilePaths:Possesion:DocumentFIlePath").Value.ToString();
            IFormFile files = possessiondetails.DocumentIFormFile;
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                DocumentFilePath = _configuration.GetSection("FilePaths:Possesion:DocumentFIlePath").Value.ToString();
                string FilePath = Path.Combine(DocumentFilePath, FileName);
                if (files.Length > 0)
                {
                    if (!Directory.Exists(DocumentFilePath))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(DocumentFilePath);// Try to create the directory.
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
                                fullpath = _configuration.GetSection("FilePaths:Possesion:DocumentFIlePath").Value.ToString();
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
                        // Image.FromFile will throw this if file is invalid.  
                    }

                }
            }

            return Flag;
        }

    }
}
