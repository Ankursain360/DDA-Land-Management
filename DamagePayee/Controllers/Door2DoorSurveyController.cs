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
using DamagePayee.Models;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.AspNetCore.Authorization;
using Dto.Search;
using DamagePayee.Filters;
using Core.Enum;
using Utility.Helper;
using Dto.Master;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace DamagePayee.Controllers
{
    public class Door2DoorSurveyController : BaseController
    {
        public object JsonRequestBehavior { get; private set; }

        private readonly IDoortodoorsurveyService _doortodoorsurveyService;
        public IConfiguration _configuration;
        string documentPhotoPathLayout = string.Empty;
        string propertyPhotoPathLayout = string.Empty;

        public Door2DoorSurveyController(IDoortodoorsurveyService doortodoorsurveyService, IConfiguration configuration)
        {
            _doortodoorsurveyService = doortodoorsurveyService;
            _configuration = configuration;
            documentPhotoPathLayout = _configuration.GetSection("FilePaths:Door2DoorSurvey:SurveyIdentityDocumentPath").Value.ToString();
            propertyPhotoPathLayout = _configuration.GetSection("FilePaths:Door2DoorSurvey:SurveyPropertyDocumentPath").Value.ToString();

        }
        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DoortodoorsurveySearchDto model)
        {
            try
            {
                var result = await _doortodoorsurveyService.GetPagedDoortodoorsurvey(model);

                return PartialView("_List", result);
            }
            catch (Exception Ex)
            {

                return PartialView("_List", Ex);
            }
        }

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Doortodoorsurvey doortodoorsurvey = new Doortodoorsurvey();

            doortodoorsurvey.PresentuseList = await _doortodoorsurveyService.GetAllPresentuse();
            doortodoorsurvey.GetAreaunitList = await _doortodoorsurveyService.GetAllAreaunit();
            doortodoorsurvey.GetFloorList = await _doortodoorsurveyService.GetAllFloor();
            return View(doortodoorsurvey);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Doortodoorsurvey doortodoorsurvey)
        {
            bool IsValidpdf = CheckMimeType(doortodoorsurvey);
            bool IsValidpdf1 = CheckMimeType1(doortodoorsurvey);
            try
            {
                doortodoorsurvey.PresentuseList = await _doortodoorsurveyService.GetAllPresentuse();
                doortodoorsurvey.GetAreaunitList = await _doortodoorsurveyService.GetAllAreaunit();
                doortodoorsurvey.GetFloorList = await _doortodoorsurveyService.GetAllFloor();
                if (ModelState.IsValid)
                {
                    if (IsValidpdf == true)
                    {
                        if (IsValidpdf1 == true)
                        {
                            doortodoorsurvey.CreatedBy = SiteContext.UserId;
                            var result = await _doortodoorsurveyService.Create(doortodoorsurvey);

                            if (result == true)
                            {
                                FileHelper fileHelper = new FileHelper();
                                ///for Identity Proof file:
                                if (doortodoorsurvey.DocumentPhoto != null && doortodoorsurvey.DocumentPhoto.Count > 0)
                                {
                                    List<Doortodoorsurveyidentityproof> doortodoorsurveyidentityproofs = new List<Doortodoorsurveyidentityproof>();
                                    for (int i = 0; i < doortodoorsurvey.DocumentPhoto.Count; i++)
                                    {
                                        string filename = fileHelper.SaveFile1(documentPhotoPathLayout, doortodoorsurvey.DocumentPhoto[i]);
                                        doortodoorsurveyidentityproofs.Add(new Doortodoorsurveyidentityproof
                                        {
                                            DoorToDoorSurveyId = doortodoorsurvey.Id,
                                            OccupantIdentityPrrofFilePath = filename,
                                            CreatedBy = SiteContext.UserId

                                        });
                                    }
                                    foreach (var item in doortodoorsurveyidentityproofs)
                                    {
                                        result = await _doortodoorsurveyService.SaveDoorToDoorSurveyIdentityProofs(item);
                                    }
                                }
                                ///for Property Proof file:
                                if (doortodoorsurvey.PropertyPhoto != null && doortodoorsurvey.PropertyPhoto.Count > 0)
                                {
                                    List<Doortodoorsurveypropertyproof> doortodoorsurveypropertyproofs = new List<Doortodoorsurveypropertyproof>();
                                    for (int i = 0; i < doortodoorsurvey.PropertyPhoto.Count; i++)
                                    {
                                        string filename = fileHelper.SaveFile1(propertyPhotoPathLayout, doortodoorsurvey.PropertyPhoto[i]);
                                        doortodoorsurveypropertyproofs.Add(new Doortodoorsurveypropertyproof
                                        {
                                            DoorToDoorSurveyId = doortodoorsurvey.Id,
                                            PropertyFilePath = filename,
                                            CreatedBy = SiteContext.UserId

                                        });
                                    }
                                    foreach (var item in doortodoorsurveypropertyproofs)
                                    {
                                        result = await _doortodoorsurveyService.SaveDoorToDoorSurveyPropertyProofs(item);
                                    }
                                }

                                ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                                var list = await _doortodoorsurveyService.GetDoortodoorsurvey();
                                return View("Index", list);
                            }
                            else
                            {
                                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                                return View(doortodoorsurvey);
                            }
                        }
                        else
                        {
                            ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                            return View(doortodoorsurvey);
                        }
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                        return View(doortodoorsurvey);
                    }
                }
                else
                {
                    return View(doortodoorsurvey);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(doortodoorsurvey);
            }
        }

        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _doortodoorsurveyService.FetchSingleResult(id);
            Data.PresentuseList = await _doortodoorsurveyService.GetAllPresentuse();
            Data.GetAreaunitList = await _doortodoorsurveyService.GetAllAreaunit();
            Data.GetFloorList = await _doortodoorsurveyService.GetAllFloor();

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Doortodoorsurvey doortodoorsurvey)
        {
            bool IsValidpdf = CheckMimeType(doortodoorsurvey);
            bool IsValidpdf1 = CheckMimeType1(doortodoorsurvey);
            doortodoorsurvey.PresentuseList = await _doortodoorsurveyService.GetAllPresentuse();
            doortodoorsurvey.GetAreaunitList = await _doortodoorsurveyService.GetAllAreaunit();
            doortodoorsurvey.GetFloorList = await _doortodoorsurveyService.GetAllFloor();
            try
            {
                doortodoorsurvey.PresentuseList = await _doortodoorsurveyService.GetAllPresentuse();
                doortodoorsurvey.GetAreaunitList = await _doortodoorsurveyService.GetAllAreaunit();
                doortodoorsurvey.GetFloorList = await _doortodoorsurveyService.GetAllFloor();

                if (ModelState.IsValid)
                {
                    if (IsValidpdf == true)
                    {
                        if (IsValidpdf1 == true)
                        {
                            doortodoorsurvey.ModifiedBy = SiteContext.UserId;
                            var result = await _doortodoorsurveyService.Update(id, doortodoorsurvey);

                            if (result == true)
                            {
                                FileHelper fileHelper = new FileHelper();
                                ///for Identity Proof file:
                                if (doortodoorsurvey.DocumentPhoto != null && doortodoorsurvey.DocumentPhoto.Count > 0)
                                {
                                    List<Doortodoorsurveyidentityproof> doortodoorsurveyidentityproofs = new List<Doortodoorsurveyidentityproof>();
                                    result = await _doortodoorsurveyService.DeleteDoorToDoorSurveyIdentityProofs(doortodoorsurvey.Id);
                                    for (int i = 0; i < doortodoorsurvey.DocumentPhoto.Count; i++)
                                    {
                                        string filename = fileHelper.SaveFile1(documentPhotoPathLayout, doortodoorsurvey.DocumentPhoto[i]);
                                        doortodoorsurveyidentityproofs.Add(new Doortodoorsurveyidentityproof
                                        {
                                            DoorToDoorSurveyId = doortodoorsurvey.Id,
                                            OccupantIdentityPrrofFilePath = filename,
                                            CreatedBy = SiteContext.UserId

                                        });
                                    }
                                    foreach (var item in doortodoorsurveyidentityproofs)
                                    {
                                        result = await _doortodoorsurveyService.SaveDoorToDoorSurveyIdentityProofs(item);
                                    }
                                }
                                ///for Property Proof file:
                                if (doortodoorsurvey.PropertyPhoto != null && doortodoorsurvey.PropertyPhoto.Count > 0)
                                {
                                    List<Doortodoorsurveypropertyproof> doortodoorsurveypropertyproofs = new List<Doortodoorsurveypropertyproof>();
                                    result = await _doortodoorsurveyService.DeleteDoorToDoorSurveyPropertyProofs(doortodoorsurvey.Id);
                                    for (int i = 0; i < doortodoorsurvey.PropertyPhoto.Count; i++)
                                    {
                                        string filename = fileHelper.SaveFile1(propertyPhotoPathLayout, doortodoorsurvey.PropertyPhoto[i]);
                                        doortodoorsurveypropertyproofs.Add(new Doortodoorsurveypropertyproof
                                        {
                                            DoorToDoorSurveyId = doortodoorsurvey.Id,
                                            PropertyFilePath = filename,
                                            CreatedBy = SiteContext.UserId

                                        });
                                    }
                                    foreach (var item in doortodoorsurveypropertyproofs)
                                    {
                                        result = await _doortodoorsurveyService.SaveDoorToDoorSurveyPropertyProofs(item);
                                    }
                                }


                                ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                                var list = await _doortodoorsurveyService.GetDoortodoorsurvey();
                                return View("Index", list);
                            }
                            else
                            {
                                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                                return View(doortodoorsurvey);
                            }
                        }
                        else
                        {
                            ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                            return View(doortodoorsurvey);
                        }
                    }
                    else
                    {


                        ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                        return View(doortodoorsurvey);

                    }

                }
                else
                {
                    return View(doortodoorsurvey);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(doortodoorsurvey);
            }
        }

        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var result = await _doortodoorsurveyService.Delete(id);
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
            var list = await _doortodoorsurveyService.GetDoortodoorsurvey();
            return View("Index", list);
        }



        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _doortodoorsurveyService.FetchSingleResult(id);
            Data.PresentuseList = await _doortodoorsurveyService.GetAllPresentuse();
            Data.GetAreaunitList = await _doortodoorsurveyService.GetAllAreaunit();
            Data.GetFloorList = await _doortodoorsurveyService.GetAllFloor();

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


        public async Task<FileResult> ViewIdentityProof(int Id)
        {
            try
            {
                FileHelper file = new FileHelper();
                var Data = await _doortodoorsurveyService.FetchSingleResultDoor2DoorSurveyIdentity(Id);
                string targetPhotoPathLayout = documentPhotoPathLayout + Data.OccupantIdentityPrrofFilePath;
                byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
                return File(FileBytes, file.GetContentType(targetPhotoPathLayout));
            }
            catch (Exception ex)
            {

                FileHelper file = new FileHelper();
                var Data = await _doortodoorsurveyService.FetchSingleResult(Id);
                string targetPhotoPathLayout = Data.OccupantIdentityPrrofFilePath;
                byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
                return File(FileBytes, file.GetContentType(targetPhotoPathLayout));

            }
        }



        public async Task<FileResult> ViewPropertyFile(int Id)
        {
            try
            {
                FileHelper file = new FileHelper();
                var Data = await _doortodoorsurveyService.FetchSingleResultDoor2DoorSurveyProperty(Id);
                string targetPhotoPathLayout = propertyPhotoPathLayout + Data.PropertyFilePath;
                byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
                return File(FileBytes, file.GetContentType(targetPhotoPathLayout));
            }
            catch (Exception ex)
            {

                FileHelper file = new FileHelper();
                var Data = await _doortodoorsurveyService.FetchSingleResult(Id);
                string targetPhotoPathLayout = Data.PropertyFilePath;
                byte[] FileBytes = System.IO.File.ReadAllBytes(targetPhotoPathLayout);
                return File(FileBytes, file.GetContentType(targetPhotoPathLayout));

            }
        }



        public async Task<IActionResult> DoorToDoorSurveyList([FromBody] DoortodoorsurveySearchDto model)
        {
            var result = await _doortodoorsurveyService.GetDoortodoorsurveyList(model);
            List<DoorToDoorSurveyListDto> data = new List<DoorToDoorSurveyListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new DoorToDoorSurveyListDto()
                    {

                        Id = result[i].Id,
                        LocationAddressofProperty = result[i].PropertyAddress,
                        // MunicipalNumberifany = result[i].MuncipalNo,
                        lattitude = result[i].GeoReferencingLattitude,
                        longitude = result[i].Longitude,
                        presentUse = result[i].PresentUseNavigation.Name,
                        ApproxAreaoftheProperty = result[i].ApproxPropertyArea.ToString(),
                        //AreaUnit = result[i].AreaUnit.ToString(),
                        AreaUnit = result[i].AreaUnitNavigation == null ? "" : result[i].AreaUnitNavigation.Name,
                        NumberofFloors = result[i].NumberOfFloorsNavigation.Name,
                        FileNo = result[i].FileNo,
                        CA_NumberOfElectricityConnection = result[i].CaelectricityNo,
                        k_NumberOfWaterConnection = result[i].KwaterNo,
                        HouseTaxNumberIssueBy_MCD = result[i].PropertyHouseTaxNo,
                        NameOfOccupant = result[i].OccupantName,
                        email = result[i].Email,
                        Mobile = result[i].MobileNo,
                        AadharNumberOfOccupant = result[i].OccupantAadharNo,
                        VoterIdNumber = result[i].VoterIdNo,
                        DemagePaidInThePast = result[i].DamagePaidPast,
                        CreatedDate = result[i].CreatedDate.ToString("dd-MM-yyyy hh:mm:ss tt"),
                        Status = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                        remarks = result[i].Remarks,
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            TempData["file"] = memory;
            return Ok();

        }
        [HttpGet]
        public virtual ActionResult download()
        {
            byte[] data = TempData["file"] as byte[];
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }



        [HttpPost]
        public JsonResult CheckFile()
        {
            bool IsImg = true;
            string fullpath = string.Empty;
            //   string fullpath = string.Empty;
            string extension = string.Empty;
            documentPhotoPathLayout = _configuration.GetSection("FilePaths:Door2DoorSurvey:SurveyIdentityDocumentPath").Value.ToString();
            IFormFile files = Request.Form.Files["file"];
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                documentPhotoPathLayout = _configuration.GetSection("FilePaths:Door2DoorSurvey:SurveyIdentityDocumentPath").Value.ToString();
                string FilePath = Path.Combine(documentPhotoPathLayout, FileName);
                if (files.Length > 0)
                {
                    if (!Directory.Exists(documentPhotoPathLayout))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(documentPhotoPathLayout);// Try to create the directory.
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
                                fullpath = _configuration.GetSection("FilePaths:Door2DoorSurvey:SurveyIdentityDocumentPath").Value.ToString();
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

        public bool CheckMimeType(Doortodoorsurvey doortodoorsurvey)
        {
            bool Flag = true;
            string fullpath = string.Empty;       
            string extension = string.Empty;
            documentPhotoPathLayout = _configuration.GetSection("FilePaths:Door2DoorSurvey:SurveyIdentityDocumentPath").Value.ToString();
            IFormFile files = doortodoorsurvey.DocumentPhoto1;
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                documentPhotoPathLayout = _configuration.GetSection("FilePaths:Door2DoorSurvey:SurveyIdentityDocumentPath").Value.ToString();
                string FilePath = Path.Combine(documentPhotoPathLayout, FileName);
                if (files.Length > 0)
                {
                    if (!Directory.Exists(documentPhotoPathLayout))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(documentPhotoPathLayout);// Try to create the directory.
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
                                fullpath = _configuration.GetSection("FilePaths:Door2DoorSurvey:SurveyIdentityDocumentPath").Value.ToString();
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

        public bool CheckMimeType1(Doortodoorsurvey doortodoorsurvey)
        {
            bool Flag = true;
            string fullpath = string.Empty;
            string extension = string.Empty;
            documentPhotoPathLayout = _configuration.GetSection("FilePaths:Door2DoorSurvey:SurveyPropertyDocumentPath").Value.ToString();
            IFormFile files = doortodoorsurvey.PropertyPhoto1;
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                documentPhotoPathLayout = _configuration.GetSection("FilePaths:Door2DoorSurvey:SurveyPropertyDocumentPath").Value.ToString();
                string FilePath = Path.Combine(documentPhotoPathLayout, FileName);
                if (files.Length > 0)
                {
                    if (!Directory.Exists(documentPhotoPathLayout))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(documentPhotoPathLayout);// Try to create the directory.
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
                                fullpath = _configuration.GetSection("FilePaths:Door2DoorSurvey:SurveyPropertyDocumentPath").Value.ToString();
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
