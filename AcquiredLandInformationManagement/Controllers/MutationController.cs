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
  

    public class MutationController : BaseController
    {
        public object JsonRequestBehavior { get; private set; }
        private readonly IMutationService _mutationService;
        private readonly IKhasraService _khasraService;
        public IConfiguration _Configuration;
        string UploadFilePath = "";
        string targetPathGeo = "";
        string DocumentFilePath = "";
        public MutationController(IMutationService mutationService, IConfiguration configuration, IKhasraService khasraService)
        {
            _mutationService = mutationService;
            _Configuration = configuration;
            _khasraService = khasraService;
            DocumentFilePath = _Configuration.GetSection("FilePaths:Mutation:DocumentFIlePath").Value.ToString();
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> Index()
        {
            ViewBag.VillageList = await _mutationService.GetVillageList();
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DemandListDetailsSearchDto model)
        {
            var result = await _mutationService.GetPagedDMSFileUploadList(model);
            return PartialView("_List", result);
        }
        async Task BindDropDown(Mutation mutation)
        {
            mutation.VillageList = await _mutationService.GetVillageList();
        }
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Mutation mutation = new Mutation();
            mutation.IsActive = 1;
            await BindDropDown(mutation);
            return View(mutation);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Create(Mutation mutation)
        {
            bool IsValidpdf = CheckMimeType(mutation);
            await BindDropDown(mutation);
            mutation.VillageList = await _mutationService.GetVillageList();

            if (ModelState.IsValid)
            {
                if (IsValidpdf == true)
                {
                    FileHelper fileHelper = new FileHelper();
                    mutation.DocumentName = mutation.DocumentIFormFile == null ? mutation.DocumentName : fileHelper.SaveFile1(DocumentFilePath, mutation.DocumentIFormFile);
                    mutation.CreatedBy = SiteContext.UserId;
                    var result = await _mutationService.Create(mutation);

                    if (result)
                    {
                        //****** code for saving  Mutation Particulars *****
                        if (mutation.Name[0] == null && mutation.FatherName[0] == null && mutation.Address[0] == null && mutation.Share[0] == null)
                        {
                        }
                        else
                        {
                            List<Mutationparticulars> mutationparticulars = new List<Mutationparticulars>();
                            for (int i = 0; i < mutation.Name.Count; i++)
                            {
                                mutationparticulars.Add(new Mutationparticulars
                                {
                                    Name = mutation.Name.Count <= i ? string.Empty : mutation.Name[i],
                                    FatherName = mutation.FatherName.Count <= i ? string.Empty : mutation.FatherName[i],
                                    Share = mutation.Address.Count <= i ? string.Empty : mutation.Share[i],
                                    Address = mutation.Share.Count <= i ? string.Empty : mutation.Address[i],
                                    CreatedBy = SiteContext.UserId,
                                    MutationId = mutation.Id
                                });
                            }
                            result = await _mutationService.SaveMutationParticulars(mutationparticulars);
                        }

                    }
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        ViewBag.VillageList = await _mutationService.GetVillageList();
                        return View("Index");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        await BindDropDown(mutation);
                        return View(mutation);

                    }
                }
                else
                {

                    ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                    return View(mutation);
                }
            }
            else
            {
                return View(mutation);
            }

        }


        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _mutationService.FetchSingleResult(id);
            await BindDropDown(Data);
            Data.KhasraNoList = await _mutationService.GetKhasraList(Data.AcquiredVillageId);
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
        public async Task<IActionResult> Edit(int id, Mutation mutation)
        {
            bool IsValidpdf = CheckMimeType(mutation);
            await BindDropDown(mutation);
            mutation.VillageList = await _mutationService.GetVillageList();

            if (ModelState.IsValid)
            {
                if (IsValidpdf == true)
                {

                    FileHelper fileHelper = new FileHelper();
                    mutation.DocumentName = mutation.DocumentIFormFile == null ? mutation.DocumentName : fileHelper.SaveFile1(DocumentFilePath, mutation.DocumentIFormFile);
                    mutation.ModifiedBy = SiteContext.UserId;
                    var result = await _mutationService.Update(id, mutation);

                    if (result)
                    {
                        //****** code for saving  Mutation Particulars *****
                        if (mutation.Name[0] == null && mutation.FatherName[0] == null && mutation.Address[0] == null && mutation.Share[0] == null)
                        {
                        }
                        else
                        {
                            List<Mutationparticulars> mutationparticulars = new List<Mutationparticulars>();
                            for (int i = 0; i < mutation.Name.Count; i++)
                            {
                                mutationparticulars.Add(new Mutationparticulars
                                {
                                    Name = mutation.Name.Count <= i ? string.Empty : mutation.Name[i],
                                    FatherName = mutation.FatherName.Count <= i ? string.Empty : mutation.FatherName[i],
                                    Share = mutation.Address.Count <= i ? string.Empty : mutation.Share[i],
                                    Address = mutation.Share.Count <= i ? string.Empty : mutation.Address[i],
                                    CreatedBy = SiteContext.UserId,
                                    MutationId = mutation.Id
                                });
                            }

                            result = await _mutationService.DeleteMutationParticulars(mutation.Id);
                            result = await _mutationService.SaveMutationParticulars(mutationparticulars);
                        }

                    }
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        ViewBag.VillageList = await _mutationService.GetVillageList();
                        return View("Index");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        await BindDropDown(mutation);
                        return View(mutation);

                    }
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                    return View(mutation);
                }
            }
            else
            {
                return View(mutation);
            }
        }
        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _mutationService.FetchSingleResult(id);
            await BindDropDown(Data);
            Data.KhasraNoList = await _mutationService.GetKhasraList(Data.AcquiredVillageId);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mutationService.Delete(id, SiteContext.UserId);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            ViewBag.VillageList = await _mutationService.GetVillageList();
            return View("Index");
        }
        [HttpGet]
        public async Task<JsonResult> GetKhasraList(int? Id)
        {
            Id = Id ?? 0;
            return Json(await _mutationService.GetKhasraList(Convert.ToInt32(Id)));
        }

        public async Task<JsonResult> GetDetailsMutationParticulars(int? Id)
        {
            Id = Id ?? 0;
            var data = await _mutationService.GetMutationParticulars(Convert.ToInt32(Id));
            return Json(data.Select(x => new
            {
                x.Id,
                x.Name,
                x.FatherName,
                x.Address,
                x.Share
            }));
        }

        public async Task<PartialViewResult> KhasraView(int id)
        {
            var Data = await _khasraService.FetchSingleResult(id);
            if (Data != null)
            {
                Data.LandCategoryList = await _khasraService.GetAllLandCategory();
                Data.VillageList = await _khasraService.GetAllVillageList();
            }
            return PartialView("_KhasraView", Data);
        }
        [AuthorizeContext(ViewAction.Download)]

        public async Task<IActionResult> MutationList()
        {
            var result = await _mutationService.GetAllMutation();
            List<MutationListDto> data = new List<MutationListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new MutationListDto()
                    {
                        Id = result[i].Id,
                        Village = result[i].AcquiredVillage == null ? "" : result[i].AcquiredVillage.Name,
                        KhasraNo = result[i].Khasra == null ? "" : result[i].Khasra.Name,
                        MutationOwnerLessee = result[i].MutationOwnerLessee,
                        MutationNo = result[i].MutationNo,
                        MutationFees = result[i].MutationFees.ToString(),
                      
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
            Mutation Data = await _mutationService.FetchSingleResult(Id);
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
            DocumentFilePath = _Configuration.GetSection("FilePaths:Mutation:DocumentFIlePath").Value.ToString();
            IFormFile files = Request.Form.Files["file"];
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                DocumentFilePath = _Configuration.GetSection("FilePaths:Mutation:DocumentFIlePath").Value.ToString();
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
                                fullpath = _Configuration.GetSection("FilePaths:Mutation:DocumentFIlePath").Value.ToString();
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



        public bool CheckMimeType(Mutation mutation)
        {
            bool Flag = true;
            string fullpath = string.Empty;
            //   string fullpath = string.Empty;
            string extension = string.Empty;
            DocumentFilePath = _Configuration.GetSection("FilePaths:Mutation:DocumentFIlePath").Value.ToString();
            IFormFile files = mutation.DocumentIFormFile;
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                DocumentFilePath = _Configuration.GetSection("FilePaths:Mutation:DocumentFIlePath").Value.ToString();
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
                                fullpath = _Configuration.GetSection("FilePaths:Mutation:DocumentFIlePath").Value.ToString();
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
