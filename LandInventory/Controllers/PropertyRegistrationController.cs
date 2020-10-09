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

namespace SiteMaster.Controllers
{
    public class PropertyRegistrationController : Controller
    {
        //Let suppose user = 1 can Create, user =2 can validate , user =3 can delete , and untill validate 1,2 can only look index
        private readonly IPropertyRegistrationService _propertyregistrationService;
        public IConfiguration _Configuration;
        int UserId = 2;
        string targetPathLayout = "";
        string targetPathGeo = "";
        string targetPathHandedOver = "";
        string targetPathTakenOver = "";
        string targetPathDisposal = "";
        public PropertyRegistrationController(IPropertyRegistrationService propertyregistrationService, IConfiguration configuration)
        {
            _propertyregistrationService = propertyregistrationService;
            _Configuration = configuration;
        }
        //public async Task<IActionResult> Index()
        //{
        //    var result = await _propertyregistrationService.GetAllPropertyregistration(UserId);
        //    return View(result);
        //}
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] PropertyRegisterationSearchDto model)
        {
            var result = await _propertyregistrationService.GetPagedPropertyRegisteration(model,  UserId);
            return PartialView("_List", result);
        }
        async Task BindDropDown(Propertyregistration propertyregistration)
        {
            propertyregistration.ClassificationOfLandList = await _propertyregistrationService.GetClassificationOfLandDropDownList();
           // propertyregistration.ZoneList = await _propertyregistrationService.GetZoneDropDownList();
        //    propertyregistration.LocalityList = await _propertyregistrationService.GetLocalityDropDownList();
            propertyregistration.LandUseList = await _propertyregistrationService.GetLandUseDropDownList();
            propertyregistration.DisposalTypeList = await _propertyregistrationService.GetDisposalTypeDropDownList();
            propertyregistration.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList();
            propertyregistration.HandOverDepartmentList = await _propertyregistrationService.GetDepartmentDropDownList();
          //  propertyregistration.DivisionList = await _propertyregistrationService.GetDivisionDropDownList();
        }
        public async Task<IActionResult> Create()
        {
            Propertyregistration propertyregistration = new Propertyregistration();
            propertyregistration.IsActive = 1;
            await BindDropDown(propertyregistration);
            return View(propertyregistration);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Create(Propertyregistration propertyregistration, IFormFile Assignfile, IFormFile GeoAssignfile, IFormFile TakenOverAssignFile, IFormFile HandedOverAssignFile, IFormFile DisposalTypeAssignFile)
        {
            await BindDropDown(propertyregistration);
            propertyregistration.ZoneList = await _propertyregistrationService.GetZoneDropDownList(propertyregistration.DepartmentId);
            propertyregistration.LocalityList = await _propertyregistrationService.GetLocalityDropDownList(propertyregistration.ZoneId);
            propertyregistration.DivisionList = await _propertyregistrationService.GetDivisionDropDownList(propertyregistration.ZoneId);

            if (ModelState.IsValid)
            {
                propertyregistration.IsValidate = 0;
                propertyregistration.IsDeleted = 1;

                if (propertyregistration.MainLandUseId == 0 )
                {
                    propertyregistration.MainLandUseId = 1;
                }
                if (propertyregistration.DisposalTypeId == 0)
                {
                    propertyregistration.DisposalTypeId = 1;
                }
                if (propertyregistration.Encroached != null)
                {
                   if(propertyregistration.Encroached > propertyregistration.TotalArea)
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
                //if (propertyregistration.Boundary == 1 && propertyregistration.BoundaryRemarks == null)
                //{
                //    ViewBag.Message = Alert.Show("Boundary Remarks Mandatory", "", AlertType.Warning);
                //    return View(propertyregistration);
                //}
                //if (propertyregistration.BuiltUp == 1 && propertyregistration.BuiltUpRemarks == null)
                //{
                //    ViewBag.Message = Alert.Show("Built-Up Remarks Mandatory", "", AlertType.Warning);
                //    return View(propertyregistration);
                //}
                //if (propertyregistration.LitigationStatus == 1 && propertyregistration.LitigationStatusRemarks == null)
                //{
                //    ViewBag.Message = Alert.Show("Litigation Status Remarks Mandatory", "", AlertType.Warning);
                //    return View(propertyregistration);
                //}

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
                    await BindDropDown(propertyregistration);
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
            ViewBag.TakenOverDocView = Data.TakenOverFilePath;
            ViewBag.HandedOverDocView = Data.HandedOverFilePath;
            ViewBag.DisposalTypeDocView = Data.DisposalTypeFilePath;
            ViewBag.IsValidateUser = 2;
            await BindDropDown(Data);

            Data.ZoneList = await _propertyregistrationService.GetZoneDropDownList(Data.DepartmentId);
            Data.LocalityList = await _propertyregistrationService.GetLocalityDropDownList(Data.ZoneId);
            Data.DivisionList = await _propertyregistrationService.GetDivisionDropDownList(Data.ZoneId);

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
        public async Task<IActionResult> Edit(int id, Propertyregistration propertyregistration, IFormFile Assignfile, IFormFile GeoAssignfile, IFormFile TakenOverAssignFile, IFormFile HandedOverAssignFile, IFormFile DisposalTypeAssignFile)
        {
            await BindDropDown(propertyregistration);
            propertyregistration.ZoneList = await _propertyregistrationService.GetZoneDropDownList(propertyregistration.DepartmentId);
            propertyregistration.LocalityList = await _propertyregistrationService.GetLocalityDropDownList(propertyregistration.ZoneId);
            propertyregistration.DivisionList = await _propertyregistrationService.GetDivisionDropDownList(propertyregistration.ZoneId);
            ViewBag.LayoutDocView = propertyregistration.LayoutFilePath;
            ViewBag.GeoDocView = propertyregistration.GeoFilePath;
            ViewBag.TakenOverDocView = propertyregistration.TakenOverFilePath;
            ViewBag.HandedOverDocView = propertyregistration.HandedOverFilePath;
            ViewBag.DisposalTypeDocView = propertyregistration.DisposalTypeFilePath;
            ViewBag.IsValidateUser = 2;
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
                    //return View(propertyregistration);
                    var result1 = await _propertyregistrationService.GetAllPropertyregistration(UserId);
                    return View("Index", result1);

                }

            }
            return View(propertyregistration);
        }

        public async Task<IActionResult> DeleteConfirmed(int id)  // Used to Perform Delete Functionality added by Renu
        {
            int UserId = 3;
            var deleteAuthority = _propertyregistrationService.CheckDeleteAuthority(UserId);
            
            if (UserId == 3)
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
            ViewBag.LayoutDocView = Data.LayoutFilePath;
            ViewBag.GeoDocView = Data.GeoFilePath;
            ViewBag.TakenOverDocView = Data.TakenOverFilePath;
            ViewBag.HandedOverDocView = Data.HandedOverFilePath;
            ViewBag.DisposalTypeDocView = Data.DisposalTypeFilePath;
            await BindDropDown(Data);

            Data.ZoneList = await _propertyregistrationService.GetZoneDropDownList(Data.DepartmentId);
            Data.LocalityList = await _propertyregistrationService.GetLocalityDropDownList(Data.ZoneId);
            Data.DivisionList = await _propertyregistrationService.GetDivisionDropDownList(Data.ZoneId);


            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, Propertyregistration propertyregistration)  
        {
            Deletedproperty model = new Deletedproperty();
            int UserId = 3;
            var deleteAuthority = _propertyregistrationService.CheckDeleteAuthority(UserId);

            if (UserId == 3)
            {
                model.Reason = propertyregistration.Reason;
                var result = await _propertyregistrationService.Delete(id);
                var result2 = await _propertyregistrationService.InsertInDeletedProperty(id, model);
                if (result == true)
                {
                    UserId = 2;
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


        #region Dropdown Dependency calls added  by renu 
        [HttpGet]
        public async Task<JsonResult> GetZoneList(int?departmentId)
        {
            departmentId = departmentId ?? 0;
            return Json(await _propertyregistrationService.GetZoneDropDownList(Convert.ToInt32(departmentId)));
        }

        [HttpGet]
        public async Task<JsonResult> GetLocalityList(int?zoneId)
        {
            zoneId = zoneId ?? 0;
            return Json(await _propertyregistrationService.GetLocalityDropDownList(Convert.ToInt32(zoneId)));
        }

        [HttpGet]
        public async Task<JsonResult> GetDivisionList(int?zoneId)
        {
            zoneId = zoneId ?? 0;
            return Json(await _propertyregistrationService.GetDivisionDropDownList(Convert.ToInt32(zoneId)));
        }
        #endregion
    }

}
