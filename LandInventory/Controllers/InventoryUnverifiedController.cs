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
using LandInventory.Models;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Dto.Search;
using LandInventory.Filters;
using Core.Enum;
using Utility.Helper;
using Dto.Master;

namespace LandInventory.Controllers
{
    public class InventoryUnverifiedController : BaseController
    {
        private readonly IPropertyRegistrationService _propertyregistrationService;
        public IConfiguration _Configuration;
        string targetPathLayout = "";
        string targetPathGeo = "";
        string targetPathHandedOver = "";
        string targetPathTakenOver = "";
        string targetPathDisposal = "";
        string targetPathHandedOverCopyOfOrder = "";
        string targetPathATR = "";
        public object JsonRequestBehavior { get; private set; }

        public InventoryUnverifiedController(IPropertyRegistrationService propertyregistrationService, IConfiguration configuration)
        {
            _propertyregistrationService = propertyregistrationService;
            _Configuration = configuration;
        }

        async Task BindDropDown(Propertyregistration propertyregistration)
        {
            propertyregistration.ClassificationOfLandList = await _propertyregistrationService.GetClassificationOfLandDropDownListReport();
            propertyregistration.LandUseList = await _propertyregistrationService.GetLandUseDropDownList();
            propertyregistration.DisposalTypeList = await _propertyregistrationService.GetDisposalTypeDropDownList();
            propertyregistration.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList();
            propertyregistration.TakenOverDepartmentList = await _propertyregistrationService.GetTakenDepartmentDropDownList();
            propertyregistration.HandOverDepartmentList = await _propertyregistrationService.GetHandedDepartmentDropDownList();

        }


        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            var Msg = TempData["Message"] as string;
            if (Msg != null)
                ViewBag.Message = Msg;
            Propertyregistration propertyregistration = new Propertyregistration();
            await BindDropDown(propertyregistration);
            return View(propertyregistration);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetDetails([FromBody] InvnentoryUnverifiedVerifiedSearchDto model)
        {
            var result = await _propertyregistrationService.GetInventoryUnverifiedVerified(model, SiteContext.UserId,SiteContext.RoleId);

            ViewBag.IsUserCanEdit = SiteContext.UserId;
            if (result != null)
            {
                return PartialView("_Index", result);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return PartialView();
            }
        }

        #region Dropdown Dependency calls added  by renu 
        [HttpGet]
        public async Task<JsonResult> GetZoneList(int? departmentId)
        {
            departmentId = departmentId ?? 0;
            return Json(await _propertyregistrationService.GetZoneDropDownList(Convert.ToInt32(departmentId)));
        }

        [HttpGet]
        public async Task<JsonResult> GetLocalityList(int? zoneId)
        {
            zoneId = zoneId ?? 0;
            return Json(await _propertyregistrationService.GetLocalityDropDownList(Convert.ToInt32(zoneId)));
        }

        [HttpGet]
        public async Task<JsonResult> GetDivisionList(int? zoneId)
        {
            zoneId = zoneId ?? 0;
            return Json(await _propertyregistrationService.GetDivisionDropDownList(Convert.ToInt32(zoneId)));
        }
        #endregion

        #region Property Inventory changes
        [AuthorizeContext(ViewAction.Verify)]
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _propertyregistrationService.FetchSingleResult(id);
            ViewBag.LayoutDocView = Data.LayoutFilePath;
            ViewBag.GeoDocView = Data.GeoFilePath;
            ViewBag.TakenOverDocView = Data.TakenOverFilePath;
            ViewBag.HandedOverDocView = Data.HandedOverFilePath;
            ViewBag.DisposalTypeDocView = Data.DisposalTypeFilePath;
            ViewBag.EncroachAtrDocView = Data.EncroachAtrfilepath;
            ViewBag.HandedOverCopyofOrderView = Data.HandedOverCopyofOrderFilepath;
            ViewBag.IsValidateUser = SiteContext.UserId;
            await BindDropDown(Data);

            Data.ZoneList = await _propertyregistrationService.GetZoneDropDownList(Data.DepartmentId);
            Data.LocalityList = await _propertyregistrationService.GetLocalityDropDownList(Data.ZoneId);
            Data.DivisionList = await _propertyregistrationService.GetDivisionDropDownList(Data.ZoneId);
            Data.HandedOverZoneList = await _propertyregistrationService.GetZoneDropDownList(Data.DepartmentId);
            Data.HandedOverDivisionList = await _propertyregistrationService.GetDivisionDropDownList(Data.ZoneId);
            Data.TakenOverZoneList = await _propertyregistrationService.GetZoneDropDownList(Data.DepartmentId);
            Data.TakenOverDivisionList = await _propertyregistrationService.GetDivisionDropDownList(Data.ZoneId);

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [DisableRequestSizeLimit]
        [AuthorizeContext(ViewAction.Verify)]
        public async Task<IActionResult> Edit(int id, Propertyregistration propertyregistration, IFormFile Assignfile, IFormFile GeoAssignfile, IFormFile TakenOverAssignFile, IFormFile HandedOverAssignFile, IFormFile DisposalTypeAssignFile)
        {
            bool IsValidpdf = CheckMimeType(propertyregistration);
            await BindDropDown(propertyregistration);
            propertyregistration.ZoneList = await _propertyregistrationService.GetZoneDropDownList(propertyregistration.DepartmentId);
            propertyregistration.LocalityList = await _propertyregistrationService.GetLocalityDropDownList(propertyregistration.ZoneId);
            propertyregistration.DivisionList = await _propertyregistrationService.GetDivisionDropDownList(propertyregistration.ZoneId);
            propertyregistration.HandedOverZoneList = await _propertyregistrationService.GetZoneDropDownList(propertyregistration.DepartmentId);
            propertyregistration.HandedOverDivisionList = await _propertyregistrationService.GetDivisionDropDownList(propertyregistration.ZoneId);
            propertyregistration.TakenOverZoneList = await _propertyregistrationService.GetZoneDropDownList(propertyregistration.DepartmentId);
            propertyregistration.TakenOverDivisionList = await _propertyregistrationService.GetDivisionDropDownList(propertyregistration.ZoneId);
            ViewBag.LayoutDocView = propertyregistration.LayoutFilePath;
            ViewBag.GeoDocView = propertyregistration.GeoFilePath;
            ViewBag.TakenOverDocView = propertyregistration.TakenOverFilePath;
            ViewBag.HandedOverDocView = propertyregistration.HandedOverFilePath;
            ViewBag.DisposalTypeDocView = propertyregistration.DisposalTypeFilePath;
            ViewBag.IsValidateUser = 2;
            if (ModelState.IsValid)
            {
                if (IsValidpdf == true)
                {

                    if (propertyregistration.IsValidateData == true)
                    {
                        propertyregistration.IsValidate = 1;
                    }
                    else
                    {
                        propertyregistration.IsValidate = 0;
                    }
                    if (propertyregistration.MainLandUseId == 0)
                    {
                        propertyregistration.MainLandUseId = 1;
                    }
                    if (propertyregistration.DisposalTypeId == 0)
                    {
                        propertyregistration.DisposalTypeId = 1;
                    }
                    if (propertyregistration.Encroached != null)
                    {
                        if (propertyregistration.Encroached > propertyregistration.TotalArea)
                        {
                            ViewBag.Message = Alert.Show("Encroached Value Must Not be Greater than Total Area", "", AlertType.Warning);
                            return View(propertyregistration);
                        }
                    }
                    if (propertyregistration.BuiltUpEncraochmentArea != null)
                    {
                        if (propertyregistration.BuiltUpEncraochmentArea > propertyregistration.TotalArea)
                        {
                            ViewBag.Message = Alert.Show("Built Up Encraochment Area Value Must Not be Greater than Total Area", "", AlertType.Warning);
                            return View(propertyregistration);
                        }
                    }
                    if (propertyregistration.Vacant != null)
                    {
                        if (propertyregistration.Vacant > propertyregistration.TotalArea)
                        {
                            ViewBag.Message = Alert.Show("Vacant Value Must Not be Greater than Total Area", "", AlertType.Warning);
                            return View(propertyregistration);
                        }
                    }
                    #region File Upload  Added by Renu 16 Sep 2020
                    /* For Layout Plan File Upload*/
                    string FileName = "";
                    string filePath = "";
                    propertyregistration.FileData = Assignfile;
                    targetPathLayout = _Configuration.GetSection("FilePaths:PropertyRegistration:LayoutDocs").Value.ToString();
                    if (propertyregistration.FileData != null)
                    {
                        if (!Directory.Exists(targetPathLayout))
                        {
                            // Try to create the directory.
                            DirectoryInfo di = Directory.CreateDirectory(targetPathLayout);
                        }
                        FileName = Guid.NewGuid().ToString() + "_" + propertyregistration.FileData.FileName;
                        filePath = Path.Combine(targetPathLayout, FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            propertyregistration.FileData.CopyTo(stream);
                        }
                        propertyregistration.LayoutFilePath = filePath;
                    }

                    /* For GeoReferncing File Upload*/
                    string GeoFileName = "";
                    string GeofilePath = "";
                    propertyregistration.GeoFileData = GeoAssignfile;
                    targetPathGeo = _Configuration.GetSection("FilePaths:PropertyRegistration:GeoReferencingDocs").Value.ToString();
                    if (propertyregistration.GeoFileData != null)
                    {
                        if (!Directory.Exists(targetPathGeo))
                        {
                            // Try to create the directory.
                            DirectoryInfo di = Directory.CreateDirectory(targetPathGeo);
                        }
                        GeoFileName = Guid.NewGuid().ToString() + "_" + propertyregistration.GeoFileData.FileName;
                        GeofilePath = Path.Combine(targetPathGeo, GeoFileName);
                        using (var stream = new FileStream(GeofilePath, FileMode.Create))
                        {
                            propertyregistration.GeoFileData.CopyTo(stream);
                        }
                        propertyregistration.GeoFilePath = GeofilePath;
                    }

                    /* For Taken Over File Upload*/
                    string TakenOverFileName = "";
                    string TakenOverfilePath = "";
                    propertyregistration.TakenOverFileData = TakenOverAssignFile;
                    targetPathTakenOver = _Configuration.GetSection("FilePaths:PropertyRegistration:TakenOverDocs").Value.ToString();
                    if (propertyregistration.TakenOverFileData != null)
                    {
                        if (!Directory.Exists(targetPathTakenOver))
                        {
                            // Try to create the directory.
                            DirectoryInfo di = Directory.CreateDirectory(targetPathTakenOver);
                        }
                        TakenOverFileName = Guid.NewGuid().ToString() + "_" + propertyregistration.TakenOverFileData.FileName;
                        TakenOverfilePath = Path.Combine(targetPathTakenOver, TakenOverFileName);
                        using (var stream = new FileStream(TakenOverfilePath, FileMode.Create))
                        {
                            propertyregistration.TakenOverFileData.CopyTo(stream);
                        }
                        propertyregistration.TakenOverFilePath = TakenOverfilePath;
                    }

                    /* For Handed Over File Upload*/
                    string HandedOverFileName = "";
                    string HandedOverfilePath = "";
                    propertyregistration.HandedOverFileData = HandedOverAssignFile;
                    targetPathHandedOver = _Configuration.GetSection("FilePaths:PropertyRegistration:HandedOverDocs").Value.ToString();
                    if (propertyregistration.HandedOverFileData != null)
                    {
                        if (!Directory.Exists(targetPathHandedOver))
                        {
                            // Try to create the directory.
                            DirectoryInfo di = Directory.CreateDirectory(targetPathHandedOver);
                        }
                        HandedOverFileName = Guid.NewGuid().ToString() + "_" + propertyregistration.HandedOverFileData.FileName;
                        HandedOverfilePath = Path.Combine(targetPathHandedOver, HandedOverFileName);
                        using (var stream = new FileStream(HandedOverfilePath, FileMode.Create))
                        {
                            propertyregistration.HandedOverFileData.CopyTo(stream);
                        }
                        propertyregistration.HandedOverFilePath = HandedOverfilePath;
                    }

                    /* For Disposal Type File Upload*/
                    string DisposalTypeFileName = "";
                    string DisposalTypefilePath = "";
                    propertyregistration.DisposalTypeFileData = DisposalTypeAssignFile;
                    targetPathDisposal = _Configuration.GetSection("FilePaths:PropertyRegistration:DisposalTypeDocs").Value.ToString();
                    if (propertyregistration.DisposalTypeFileData != null)
                    {
                        if (!Directory.Exists(targetPathDisposal))
                        {
                            // Try to create the directory.
                            DirectoryInfo di = Directory.CreateDirectory(targetPathDisposal);
                        }
                        DisposalTypeFileName = Guid.NewGuid().ToString() + "_" + propertyregistration.DisposalTypeFileData.FileName;
                        DisposalTypefilePath = Path.Combine(targetPathDisposal, DisposalTypeFileName);
                        using (var stream = new FileStream(DisposalTypefilePath, FileMode.Create))
                        {
                            propertyregistration.DisposalTypeFileData.CopyTo(stream);
                        }
                        propertyregistration.DisposalTypeFilePath = DisposalTypefilePath;
                    }
                    /* For Handed Over Copy of Order*/
                    string HandedOverCopyOrderFileName = "";
                    string HandedOverCopyOrderfilePath = "";
                    targetPathHandedOverCopyOfOrder = _Configuration.GetSection("FilePaths:PropertyRegistration:HandedOverCopyofOrderDocs").Value.ToString();
                    if (propertyregistration.HandedOverCopyofOrderDoc != null)
                    {
                        if (!Directory.Exists(targetPathHandedOverCopyOfOrder))
                        {
                            // Try to create the directory.
                            DirectoryInfo di = Directory.CreateDirectory(targetPathHandedOverCopyOfOrder);
                        }
                        HandedOverCopyOrderFileName = Guid.NewGuid().ToString() + "_" + propertyregistration.HandedOverCopyofOrderDoc.FileName;
                        HandedOverCopyOrderfilePath = Path.Combine(targetPathHandedOverCopyOfOrder, HandedOverCopyOrderFileName);
                        using (var stream = new FileStream(HandedOverCopyOrderfilePath, FileMode.Create))
                        {
                            propertyregistration.HandedOverCopyofOrderDoc.CopyTo(stream);
                        }
                        propertyregistration.HandedOverCopyofOrderFilepath = HandedOverCopyOrderfilePath;
                    }

                    string ATRFileName = "";
                    string ATRfilePath = "";
                    targetPathATR = _Configuration.GetSection("FilePaths:PropertyRegistration:EcroachedATRDocs").Value.ToString();
                    if (propertyregistration.EncroachAtrDoc != null)
                    {
                        if (!Directory.Exists(targetPathATR))
                        {
                            // Try to create the directory.
                            DirectoryInfo di = Directory.CreateDirectory(targetPathATR);
                        }
                        ATRFileName = Guid.NewGuid().ToString() + "_" + propertyregistration.EncroachAtrDoc.FileName;
                        ATRfilePath = Path.Combine(targetPathATR, ATRFileName);
                        using (var stream = new FileStream(ATRfilePath, FileMode.Create))
                        {
                            propertyregistration.EncroachAtrDoc.CopyTo(stream);
                        }
                        propertyregistration.EncroachAtrfilepath = ATRfilePath;
                    }
                    #endregion


                    var result = await _propertyregistrationService.Update(id, propertyregistration);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        TempData["Message"] = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);

                        return RedirectToAction("Create");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);

                        ViewBag.Items = await _propertyregistrationService.GetClassificationOfLandDropDownList();
                        ViewBag.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList();
                        await BindDropDown(propertyregistration);
                        return View("Create");

                    }

                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "Invalid Pdf", AlertType.Warning);
                    ViewBag.Items = await _propertyregistrationService.GetClassificationOfLandDropDownList();
                    ViewBag.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList();
                    await BindDropDown(propertyregistration);
                    return View(propertyregistration);
                }
            }
            return View(propertyregistration);
        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _propertyregistrationService.FetchSingleResult(id);
            ViewBag.LayoutDocView = Data.LayoutFilePath;
            ViewBag.GeoDocView = Data.GeoFilePath;
            ViewBag.TakenOverDocView = Data.TakenOverFilePath;
            ViewBag.HandedOverDocView = Data.HandedOverFilePath;
            ViewBag.DisposalTypeDocView = Data.DisposalTypeFilePath;
            ViewBag.EncroachAtrDocView = Data.EncroachAtrfilepath;
            ViewBag.HandedOverCopyofOrderView = Data.HandedOverCopyofOrderFilepath;
            await BindDropDown(Data);

            Data.ZoneList = await _propertyregistrationService.GetZoneDropDownList(Data.DepartmentId);
            Data.LocalityList = await _propertyregistrationService.GetLocalityDropDownList(Data.ZoneId);
            Data.DivisionList = await _propertyregistrationService.GetDivisionDropDownList(Data.ZoneId);
            Data.HandedOverZoneList = await _propertyregistrationService.GetZoneDropDownList(Data.DepartmentId);
            Data.HandedOverDivisionList = await _propertyregistrationService.GetDivisionDropDownList(Data.ZoneId);
            Data.TakenOverZoneList = await _propertyregistrationService.GetZoneDropDownList(Data.DepartmentId);
            Data.TakenOverDivisionList = await _propertyregistrationService.GetDivisionDropDownList(Data.ZoneId);


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id, Propertyregistration propertyregistration)
        {
            Deletedproperty model = new Deletedproperty();
            model.Reason = propertyregistration.Reason;
            model.DeletedBy = SiteContext.UserId;
            var result = await _propertyregistrationService.Delete(id);
            var result2 = await _propertyregistrationService.InsertInDeletedProperty(id, model);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                ViewBag.Items = await _propertyregistrationService.GetClassificationOfLandDropDownList();
                ViewBag.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList();
                Propertyregistration propertyregistration1 = new Propertyregistration();
                await BindDropDown(propertyregistration1);
                return RedirectToAction("Create", propertyregistration1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                ViewBag.Items = await _propertyregistrationService.GetClassificationOfLandDropDownList();
                ViewBag.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList();
                Propertyregistration propertyregistration1 = new Propertyregistration();
                await BindDropDown(propertyregistration1);
                return RedirectToAction("Create", propertyregistration1);
            }

        }
        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var Data = await _propertyregistrationService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


        public async Task<IActionResult> Download(int Id)
        {
            string filename = _propertyregistrationService.GetFile(Id);
            var memory = new MemoryStream();
            using (var stream = new FileStream(filename, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> TakenOverDownload(int Id)
        {
            string filename = _propertyregistrationService.GetTakenOverFile(Id);
            var memory = new MemoryStream();
            using (var stream = new FileStream(filename, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> HandedOverDownload(int Id)
        {
            string filename = _propertyregistrationService.GetHandedOverFile(Id);
            var memory = new MemoryStream();
            using (var stream = new FileStream(filename, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DisposalTypeDownload(int Id)
        {
            string filename = _propertyregistrationService.GetDisposalFile(Id);
            var memory = new MemoryStream();
            using (var stream = new FileStream(filename, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> EncroachAtrDownload(int Id)
        {
            string filename = _propertyregistrationService.GetEncroachAtr(Id);
            var memory = new MemoryStream();
            using (var stream = new FileStream(filename, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(filename), Path.GetFileName(filename));
        }

        public async Task<IActionResult> HandedOverCopyofOrderDownload(int Id)
        {
            string filename = _propertyregistrationService.GetHandedOverCopyofOrderFile(Id);
            var memory = new MemoryStream();
            using (var stream = new FileStream(filename, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(filename), Path.GetFileName(filename));
        }

        #region Document Download added By Renu 16 Sep 2020
        public async Task<IActionResult> GeoDownload(int Id)
        {
            string filename = _propertyregistrationService.GetGeoFile(Id);
            var memory = new MemoryStream();
            using (var stream = new FileStream(filename, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(filename), Path.GetFileName(filename));
        }


        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
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

        #endregion


        #endregion



        public async Task<IActionResult> UnverifiedPropertyList()
        {
            var result = await _propertyregistrationService.GetUnverifiedList(SiteContext.UserId);
            List<LandInventoryUnverifiedListDto> data = new List<LandInventoryUnverifiedListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new LandInventoryUnverifiedListDto()
                    {
                        Id = result[i].Id,
                        InventoriedIn = result[i].InventoriedInId.ToString() == "1" ? "VLMS" : "Used",
                        PlannedUnplannedLand = result[i].PlannedUnplannedLand,

                        ClassificationofLand = result[i].ClassificationOfLand == null ? " " : result[i].ClassificationOfLand.Name,
                        Department = result[i].Department == null ? " " : result[i].Department.Name,
                        Zone = result[i].Zone == null ? " " : result[i].Zone.Name,
                        Division = result[i].Division == null ? " " : result[i].Division.Name,
                        PrimaryListNo = result[i].PrimaryListNo,
                        KhasraNo = result[i].KhasraNo,

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
            targetPathLayout = _Configuration.GetSection("FilePaths:PropertyRegistration:LayoutDocs").Value.ToString();
            IFormFile files = Request.Form.Files["file"];
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                targetPathLayout = _Configuration.GetSection("FilePaths:PropertyRegistration:LayoutDocs").Value.ToString();
                string FilePath = Path.Combine(targetPathLayout, FileName);
                if (files.Length > 0)
                {
                    if (!Directory.Exists(targetPathLayout))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(targetPathLayout);// Try to create the directory.
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
                                fullpath = _Configuration.GetSection("FilePaths:PropertyRegistration:LayoutDocs").Value.ToString();
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




        public bool CheckMimeType(Propertyregistration propertyregistration)
        {
            bool Flag = true;
            string fullpath = string.Empty;
            string extension = string.Empty;
            targetPathLayout = _Configuration.GetSection("FilePaths:PropertyRegistration:LayoutDocs").Value.ToString();
            IFormFile files = propertyregistration.FileData;
            if (files != null)
            {
                extension = System.IO.Path.GetExtension(files.FileName);
                string FileName = Guid.NewGuid().ToString() + "_" + files.FileName;
                targetPathLayout = _Configuration.GetSection("FilePaths:PropertyRegistration:LayoutDocs").Value.ToString();
                string FilePath = Path.Combine(targetPathLayout, FileName);
                if (files.Length > 0)
                {
                    if (!Directory.Exists(targetPathLayout))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(targetPathLayout);// Try to create the directory.
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
                                fullpath = _Configuration.GetSection("FilePaths:PropertyRegistration:LayoutDocs").Value.ToString();
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
