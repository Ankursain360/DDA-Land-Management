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
using NewLandAcquisition.Filters;
using Core.Enum;
using Dto.Master;
using Utility.Helper;
using System.Text;
using Microsoft.Extensions.Configuration;

using Microsoft.AspNetCore.Http;

using System.IO;


namespace NewLandAcquisition.Controllers
{
    
   public class NewlandPossesionDetailsController : BaseController
    {
        public IConfiguration _configuration;
        private readonly INewlandpossessiondetailsService _Possessiondetailservice;
        string DocumentFilePath = "";
        public object JsonRequestBehavior { get; private set; }
        public NewlandPossesionDetailsController(INewlandpossessiondetailsService possessiondetailsService, IConfiguration configuration)
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
        public async Task<PartialViewResult> List([FromBody] NewlandpossesiondetailsSearchDto model)
        {
            var result = await _Possessiondetailservice.GetPagedNoPossessiondetails(model);

            return PartialView("_List", result);
        }



        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Newlandpossessiondetails newlandpossessiondetails = new Newlandpossessiondetails();
            newlandpossessiondetails.IsActive = 1;

            newlandpossessiondetails.KhasraList = await _Possessiondetailservice.BindKhasra(newlandpossessiondetails.VillageId);
            newlandpossessiondetails.VillageList = await _Possessiondetailservice.GetAllVillage();
            newlandpossessiondetails.PossKhasraList = await _Possessiondetailservice.GetAllPossKhasra();
            newlandpossessiondetails.us17List = await _Possessiondetailservice.GetAllus17();
            newlandpossessiondetails.us4List = await _Possessiondetailservice.GetAllus4();
            newlandpossessiondetails.us6List = await _Possessiondetailservice.GetAllus6();
            return View(newlandpossessiondetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Newlandpossessiondetails newlandpossessiondetails)
        {
            try
            {
                bool IsValidpdf = CheckMimeType(newlandpossessiondetails);

                newlandpossessiondetails.KhasraList = await _Possessiondetailservice.BindKhasra(newlandpossessiondetails.VillageId);
                newlandpossessiondetails.VillageList = await _Possessiondetailservice.GetAllVillage();
                newlandpossessiondetails.PossKhasraList = await _Possessiondetailservice.GetAllPossKhasra();
                newlandpossessiondetails.us17List = await _Possessiondetailservice.GetAllus17();
                newlandpossessiondetails.us4List = await _Possessiondetailservice.GetAllus4();
                newlandpossessiondetails.us6List = await _Possessiondetailservice.GetAllus6();

                if (ModelState.IsValid)
                {
                    if (IsValidpdf == true)
                    {

                        FileHelper fileHelper = new FileHelper();
                        newlandpossessiondetails.DocumentName = newlandpossessiondetails.DocumentIFormFile == null ? newlandpossessiondetails.DocumentName : fileHelper.SaveFile1(DocumentFilePath, newlandpossessiondetails.DocumentIFormFile);
                        StringBuilder str = new StringBuilder();
                        if (newlandpossessiondetails.IsVacant == true)
                        {
                            str.Append("Vacant");
                        }
                        if (newlandpossessiondetails.IsBuiltup == true)
                        {
                            if (str.Length != 0)
                                str.Append("|");
                            str.Append("Built Up");
                        }
                        newlandpossessiondetails.PossType = str.ToString();
                        var result = await _Possessiondetailservice.Create(newlandpossessiondetails);

                        if (result == true)
                        {
                            ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                            var list = await _Possessiondetailservice.GetAllPossessiondetails();
                            return View("Index", list);
                        }
                        else
                        {
                            ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                            return View(newlandpossessiondetails);
                        }
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                        return View(newlandpossessiondetails);

                    }
                }
                else
                {
                    return View(newlandpossessiondetails);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(newlandpossessiondetails);
            }
        }


        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _Possessiondetailservice.FetchSingleResult(id);



            Data.KhasraList = await _Possessiondetailservice.BindKhasra(Data.VillageId);
            Data.VillageList = await _Possessiondetailservice.GetAllVillage();
            Data.PossKhasraList = await _Possessiondetailservice.GetAllPossKhasra();
            Data.us17List = await _Possessiondetailservice.GetAllus17();
            Data.us4List = await _Possessiondetailservice.GetAllus4();
            Data.us6List = await _Possessiondetailservice.GetAllus6();
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
        public async Task<IActionResult> Edit(int id, Newlandpossessiondetails newlandpossessiondetails)
        {
            bool IsValidpdf = CheckMimeType(newlandpossessiondetails);
            newlandpossessiondetails.KhasraList = await _Possessiondetailservice.BindKhasra(newlandpossessiondetails.VillageId);
            newlandpossessiondetails.VillageList = await _Possessiondetailservice.GetAllVillage();
            newlandpossessiondetails.PossKhasraList = await _Possessiondetailservice.GetAllPossKhasra();
            newlandpossessiondetails.us17List = await _Possessiondetailservice.GetAllus17();
            newlandpossessiondetails.us4List = await _Possessiondetailservice.GetAllus4();
            newlandpossessiondetails.us6List = await _Possessiondetailservice.GetAllus6();
            if (ModelState.IsValid)
            {
                if (IsValidpdf == true)
                {
                    try
                    {
                        StringBuilder str = new StringBuilder();
                        if (newlandpossessiondetails.IsVacant == true)
                        {
                            str.Append("Vacant");
                        }
                        if (newlandpossessiondetails.IsBuiltup == true)
                        {
                            if (str.Length != 0)
                                str.Append("|");
                            str.Append("Built Up");
                        }
                        FileHelper fileHelper = new FileHelper();
                        newlandpossessiondetails.DocumentName = newlandpossessiondetails.DocumentIFormFile == null ? newlandpossessiondetails.DocumentName : fileHelper.SaveFile1(DocumentFilePath, newlandpossessiondetails.DocumentIFormFile);
                        newlandpossessiondetails.PossType = str.ToString();
                        var result = await _Possessiondetailservice.Update(id, newlandpossessiondetails);
                        if (result == true)
                        {
                            ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                            var list = await _Possessiondetailservice.GetAllPossessiondetails();
                            return View("Index", list);
                            // return View("Index");
                        }
                        else
                        {
                            ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                            return View(newlandpossessiondetails);
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(newlandpossessiondetails);
                    }
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                    return View(newlandpossessiondetails);

                }
            }
            else
            {
                return View(newlandpossessiondetails);
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
            Data.PossKhasraList = await _Possessiondetailservice.GetAllPossKhasra();
            Data.us17List = await _Possessiondetailservice.GetAllus17();
            Data.us4List = await _Possessiondetailservice.GetAllus4();
            Data.us6List = await _Possessiondetailservice.GetAllus6();
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
        public async Task<IActionResult> NewLandPossessionDetailsList([FromBody] NewlandpossesiondetailsSearchDto model)
        {
            var result = await _Possessiondetailservice.GetAllPossessiondetailsList(model);
            List<NewLandPossessionDetailsListDto> data = new List<NewLandPossessionDetailsListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new NewLandPossessionDetailsListDto()
                    {
                        Id = result[i].Id,
                        VillageName = result[i].Village == null ? "" : result[i].Village.Name.ToString(),
                        KhasraNo = result[i].Khasra == null ? "" : result[i].Khasra.Name.ToString(),
                        PossessionTake = result[i].PossessionTake,
                        TypeofPossession = result[i].PossType,                      
                        IsActive = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive"
                    }); 
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            TempData["file"] = memory;
            return Ok();

        }
        [HttpGet]
        [AuthorizeContext(ViewAction.Download)]
        public virtual ActionResult download()
        {
            byte[] data = TempData["file"] as byte[];
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<IActionResult> ViewUploadedDocument(int Id)
        {
            FileHelper file = new FileHelper();
            Newlandpossessiondetails Data = await _Possessiondetailservice.FetchSingleResult(Id);
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


        public bool CheckMimeType(Newlandpossessiondetails newlandpossessiondetails)
        {
            bool Flag = true;
            string fullpath = string.Empty;
            //   string fullpath = string.Empty;
            string extension = string.Empty;
            DocumentFilePath = _configuration.GetSection("FilePaths:Possesion:DocumentFIlePath").Value.ToString();
            IFormFile files = newlandpossessiondetails.DocumentIFormFile;
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
                     
                    }

                }
            }

            return Flag;
        }



    }
}
