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
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace SiteMaster.Controllers
{
    public class PropertyRegistrationController : Controller
    {
        //Let suppose user = 1 can Create, user =2 can validate , user =3 can delete , and untill validate 1,2 can only look index
        private readonly IPropertyRegistrationService _propertyregistrationService;
        private readonly IHostingEnvironment _hostingEnvironment;
        int UserId = 2;
        public PropertyRegistrationController(IPropertyRegistrationService propertyregistrationService, IHostingEnvironment en)
        {
            _propertyregistrationService = propertyregistrationService;
            _hostingEnvironment = en;
        }
        public async Task<IActionResult> Index()
        {
            int UserId = 2;
            var result = await _propertyregistrationService.GetAllPropertyregistration(UserId);
            return View(result);
        }

        async Task BindDropDown(Propertyregistration propertyregistration)
        {
            propertyregistration.ClassificationOfLandList = await _propertyregistrationService.GetClassificationOfLandDropDownList();
            propertyregistration.ZoneList = await _propertyregistrationService.GetZoneDropDownList();
            propertyregistration.LocalityList = await _propertyregistrationService.GetLocalityDropDownList();
            propertyregistration.LandUseList = await _propertyregistrationService.GetLandUseDropDownList();
            propertyregistration.DisposalTypeList = await _propertyregistrationService.GetDisposalTypeDropDownList();
            propertyregistration.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList();
            propertyregistration.DivisionList = await _propertyregistrationService.GetDivisionDropDownList();
        }
        public async Task<IActionResult> Create()
        {
            Propertyregistration propertyregistration = new Propertyregistration();
            await BindDropDown(propertyregistration);
            return View(propertyregistration);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Propertyregistration propertyregistration, IFormFile Assignfile, IFormFile GeoAssignfile, IFormFile TakenOverAssignFile, IFormFile HandedOverAssignFile, IFormFile DisposalTypeAssignFile)
        {
            await BindDropDown(propertyregistration);
            if (ModelState.IsValid)
            {
                propertyregistration.IsValidate = 0;
                propertyregistration.IsDelated = 1;
                if (propertyregistration.Boundary == 1 && propertyregistration.BoundaryRemarks == null)
                {
                    ViewBag.Message = Alert.Show("Boundary Remarks Mandatory", "", AlertType.Warning);
                    return View(propertyregistration);
                }
                if (propertyregistration.BuiltUp == 1 && propertyregistration.BuiltUpRemarks == null)
                {
                    ViewBag.Message = Alert.Show("Built-Up Remarks Mandatory", "", AlertType.Warning);
                    return View(propertyregistration);
                }
                if (propertyregistration.LitigationStatus == 1 && propertyregistration.LitigationStatusRemarks == null)
                {
                    ViewBag.Message = Alert.Show("Litigation Status Remarks Mandatory", "", AlertType.Warning);
                    return View(propertyregistration);
                }

                #region File Upload  Added by Renu 16 Sep 2020
                /* For Layout Plan File Upload*/
                if (propertyregistration.LayoutPlan == 1)
                {
                    if (Assignfile is null && propertyregistration.LayoutFilePath is null)
                    {
                        ViewBag.Message = Alert.Show("Layout Plan Document is Required", "", AlertType.Warning);
                        return View(propertyregistration);
                    }

                }
                string FileName = "";
                string DocumentPath = "";
                string filePath = "";
                propertyregistration.FileData = Assignfile;
                if (propertyregistration.FileData != null)
                {
                    DocumentPath = @"D:\VedangWorkFromHome\DDA_LandManagement_Project\GitHubFolder\Documents\PropertyRegisteration\LayoutDocs";
                    if (!Directory.Exists(DocumentPath))
                    {
                        // Try to create the directory.
                        DirectoryInfo di = Directory.CreateDirectory(DocumentPath);
                    }
                    FileName = Guid.NewGuid().ToString() + "_" + propertyregistration.FileData.FileName;
                    filePath = Path.Combine(DocumentPath, FileName);
                    propertyregistration.FileData.CopyTo(new FileStream(filePath, FileMode.Create));
                    propertyregistration.LayoutFilePath = filePath;
                }

                /* For GeoReferncing File Upload*/
                if (propertyregistration.GeoReferencing == 1)
                {
                    if (GeoAssignfile is null && propertyregistration.GeoFilePath is null)
                    {
                        ViewBag.Message = Alert.Show("Geo Referencing  Document is Required", "", AlertType.Warning);
                        return View(propertyregistration);
                    }

                }
                string GeoFileName = "";
                string GeoDocumentPath = "";
                string GeofilePath = "";
                propertyregistration.GeoFileData = GeoAssignfile;
                if (propertyregistration.GeoFileData != null)
                {
                    GeoDocumentPath = @"D:\VedangWorkFromHome\DDA_LandManagement_Project\GitHubFolder\Documents\PropertyRegisteration\GeoReferencingDocs";
                    if (!Directory.Exists(GeoDocumentPath))
                    {
                        // Try to create the directory.
                        DirectoryInfo di = Directory.CreateDirectory(GeoDocumentPath);
                    }
                    GeoFileName = Guid.NewGuid().ToString() + "_" + propertyregistration.GeoFileData.FileName;
                    GeofilePath = Path.Combine(GeoDocumentPath, GeoFileName);
                    propertyregistration.GeoFileData.CopyTo(new FileStream(GeofilePath, FileMode.Create));
                    propertyregistration.GeoFilePath = GeofilePath;
                }

                /* For TakenOver File Upload*/
                if (propertyregistration.GeoReferencing == 1)
                {
                    if (GeoAssignfile is null && propertyregistration.GeoFilePath is null)
                    {
                        ViewBag.Message = Alert.Show("Geo Referencing  Document is Required", "", AlertType.Warning);
                        return View(propertyregistration);
                    }

                }
                string TakenOverFileName = "";
                string TakenOverDocumentPath = "";
                string TakenOverfilePath = "";
                propertyregistration.GeoFileData = GeoAssignfile;
                if (propertyregistration.GeoFileData != null)
                {
                    TakenOverDocumentPath = @"D:\VedangWorkFromHome\DDA_LandManagement_Project\GitHubFolder\Documents\PropertyRegisteration\TakenOverDocs";
                    if (!Directory.Exists(TakenOverDocumentPath))
                    {
                        // Try to create the directory.
                        DirectoryInfo di = Directory.CreateDirectory(TakenOverDocumentPath);
                    }
                    TakenOverFileName = Guid.NewGuid().ToString() + "_" + propertyregistration.GeoFileData.FileName;
                    TakenOverfilePath = Path.Combine(TakenOverDocumentPath, TakenOverFileName);
                    propertyregistration.TakenOverFileData.CopyTo(new FileStream(TakenOverfilePath, FileMode.Create));
                    propertyregistration.TakenOverFilePath = TakenOverfilePath;
                }

                /* For Handed Over File Upload*/
                if (propertyregistration.GeoReferencing == 1)
                {
                    if (GeoAssignfile is null && propertyregistration.GeoFilePath is null)
                    {
                        ViewBag.Message = Alert.Show("Geo Referencing  Document is Required", "", AlertType.Warning);
                        return View(propertyregistration);
                    }

                }
                string HandedOverFileName = "";
                string HandedOverDocumentPath = "";
                string HandedOverfilePath = "";
                propertyregistration.GeoFileData = GeoAssignfile;
                if (propertyregistration.GeoFileData != null)
                {
                    HandedOverDocumentPath = @"D:\VedangWorkFromHome\DDA_LandManagement_Project\GitHubFolder\Documents\PropertyRegisteration\GeoReferencingDocs";
                    if (!Directory.Exists(HandedOverDocumentPath))
                    {
                        // Try to create the directory.
                        DirectoryInfo di = Directory.CreateDirectory(HandedOverDocumentPath);
                    }
                    HandedOverFileName = Guid.NewGuid().ToString() + "_" + propertyregistration.GeoFileData.FileName;
                    HandedOverfilePath = Path.Combine(HandedOverDocumentPath, HandedOverFileName);
                    propertyregistration.HandedOverFileData.CopyTo(new FileStream(HandedOverfilePath, FileMode.Create));
                    propertyregistration.HandedOverFilePath = HandedOverfilePath;
                }

                /* For Disposal Type File Upload*/
                if (propertyregistration.GeoReferencing == 1)
                {
                    if (GeoAssignfile is null && propertyregistration.GeoFilePath is null)
                    {
                        ViewBag.Message = Alert.Show("Geo Referencing  Document is Required", "", AlertType.Warning);
                        return View(propertyregistration);
                    }

                }
                string DisposalTypeFileName = "";
                string DisposalTypeDocumentPath = "";
                string DisposalTypefilePath = "";
                propertyregistration.GeoFileData = GeoAssignfile;
                if (propertyregistration.GeoFileData != null)
                {
                    DisposalTypeDocumentPath = @"D:\VedangWorkFromHome\DDA_LandManagement_Project\GitHubFolder\Documents\PropertyRegisteration\GeoReferencingDocs";
                    if (!Directory.Exists(DisposalTypeDocumentPath))
                    {
                        // Try to create the directory.
                        DirectoryInfo di = Directory.CreateDirectory(DisposalTypeDocumentPath);
                    }
                    DisposalTypeFileName = Guid.NewGuid().ToString() + "_" + propertyregistration.GeoFileData.FileName;
                    DisposalTypefilePath = Path.Combine(DisposalTypeDocumentPath, DisposalTypeFileName);
                    propertyregistration.GeoFileData.CopyTo(new FileStream(DisposalTypefilePath, FileMode.Create));
                    propertyregistration.GeoFilePath = DisposalTypefilePath;
                }
                #endregion

                var result = await _propertyregistrationService.Create(propertyregistration);

                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                    var result1 = await _propertyregistrationService.GetAllPropertyregistration(UserId);
                    return View("Index", result1);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(propertyregistration);

                }
            }
            else
            {
                return View(propertyregistration);
            }

        }

        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _propertyregistrationService.FetchSingleResult(id);
            ViewBag.LayoutDocView = Data.LayoutFilePath;
            ViewBag.GeoDocView = Data.GeoFilePath;
            ViewBag.IsValidateUser = 2;
            await BindDropDown(Data);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Propertyregistration propertyregistration, IFormFile Assignfile, IFormFile GeoAssignfile, IFormFile TakenOverAssignFile, IFormFile HandedOverAssignFile, IFormFile DisposalTypeAssignFile)
        {
            await BindDropDown(propertyregistration);
            if (ModelState.IsValid)
            {
                if (propertyregistration.IsValidateData == true)
                {
                    propertyregistration.IsValidate = 1;
                }
                else
                {
                    propertyregistration.IsValidate = 0;
                }
                propertyregistration.IsDelated = 1;
                if (propertyregistration.Boundary == 1 && propertyregistration.BoundaryRemarks == null)
                {
                    ViewBag.Message = Alert.Show("Boundary Remarks Mandatory", "", AlertType.Warning);
                    return View(propertyregistration);
                }
                if (propertyregistration.BuiltUp == 1 && propertyregistration.BuiltUpRemarks == null)
                {
                    ViewBag.Message = Alert.Show("Built-Up Remarks Mandatory", "", AlertType.Warning);
                    return View(propertyregistration);
                }
                if (propertyregistration.LitigationStatus == 1 && propertyregistration.LitigationStatusRemarks == null)
                {
                    ViewBag.Message = Alert.Show("Litigation Status Remarks Mandatory", "", AlertType.Warning);
                    return View(propertyregistration);
                }

                #region File Upload  Added by Renu 16 Sep 2020
                /* For Layout Plan File Upload*/
                if (propertyregistration.LayoutPlan == 1)
                {
                    if (Assignfile is null && propertyregistration.LayoutFilePath is null)
                    {
                        ViewBag.Message = Alert.Show("Layout Plan Document is Required", "", AlertType.Warning);
                        return View(propertyregistration);
                    }

                }
                string FileName = "";
                string DocumentPath = "";
                string filePath = "";
                propertyregistration.FileData = Assignfile;
                if (propertyregistration.FileData != null)
                {
                    DocumentPath = @"D:\VedangWorkFromHome\DDA_LandManagement_Project\GitHubFolder\Documents";
                    if (!Directory.Exists(DocumentPath))
                    {
                        // Try to create the directory.
                        DirectoryInfo di = Directory.CreateDirectory(DocumentPath);
                    }
                    FileName = Guid.NewGuid().ToString() + "_" + propertyregistration.FileData.FileName;
                    filePath = Path.Combine(DocumentPath, FileName);
                    propertyregistration.FileData.CopyTo(new FileStream(filePath, FileMode.Create));
                    propertyregistration.LayoutFilePath = filePath;
                }

                /* For GeoReferncing File Upload*/
                if (propertyregistration.GeoReferencing == 1)
                {
                    if (GeoAssignfile is null && propertyregistration.GeoFilePath is null)
                    {
                        ViewBag.Message = Alert.Show("Geo Referencing  Document is Required", "", AlertType.Warning);
                        return View(propertyregistration);
                    }

                }
                string GeoFileName = "";
                string GeoDocumentPath = "";
                string GeofilePath = "";
                propertyregistration.GeoFileData = GeoAssignfile;
                if (propertyregistration.GeoFileData != null)
                {
                    GeoDocumentPath = @"D:\VedangWorkFromHome\DDA_LandManagement_Project\GitHubFolder\Documents\GeoReferencingDocs";
                    if (!Directory.Exists(GeoDocumentPath))
                    {
                        // Try to create the directory.
                        DirectoryInfo di = Directory.CreateDirectory(GeoDocumentPath);
                    }
                    GeoFileName = Guid.NewGuid().ToString() + "_" + propertyregistration.GeoFileData.FileName;
                    GeofilePath = Path.Combine(GeoDocumentPath, GeoFileName);
                    propertyregistration.GeoFileData.CopyTo(new FileStream(GeofilePath, FileMode.Create));
                    propertyregistration.GeoFilePath = GeofilePath;
                }
                #endregion


                var result = await _propertyregistrationService.Update(id, propertyregistration);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                    var result1 = await _propertyregistrationService.GetAllPropertyregistration(UserId);
                    return View("Index", result1);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(propertyregistration);

                }

            }
            return View(propertyregistration);
        }

        public async Task<IActionResult> DeleteConfirmed(int id)  // Used to Perform Delete Functionality added by Renu
        {
            int UserId = 3;
            var deleteAuthority = _propertyregistrationService.CheckDeleteAuthority(UserId);

            if(UserId == 3)
            {
                var result = await _propertyregistrationService.Delete(id);
                if (result == true)
                {
                    ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                    var result1 = await _propertyregistrationService.GetAllPropertyregistration(UserId);
                    return View("Index", result1);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    var result1 = await _propertyregistrationService.GetAllPropertyregistration(UserId);
                    return View("Index", result1);
                }
            }
            else
            {
                ViewBag.Message = Alert.Show("You are not Authorized to Delete Record", "", AlertType.Warning);
                var result1 = await _propertyregistrationService.GetAllPropertyregistration(UserId);
                return View("Index", result1);
            }
            

        }

        public async Task<IActionResult> View(int id)
        {
            var Data = await _propertyregistrationService.FetchSingleResult(id);
            await BindDropDown(Data);
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
    }

}
