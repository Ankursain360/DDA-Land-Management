using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Libraries.Service.IApplicationService;
using Libraries.Model.Entity;
using Utility.Helper;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.Extensions.Configuration;
using Dto.Search;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using EncroachmentDemolition.Filters;
using Core.Enum;
using Dto.Master;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using EncroachmentDemolition.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EncroachmentDemolition.Controllers
{
    public class WatchWardController : BaseController
    {
        private readonly IWatchandwardService _watchandwardService;
        private readonly IPropertyRegistrationService _propertyregistrationService;
        private readonly IWorkflowTemplateService _workflowtemplateService;
        private readonly IApprovalProccessService _approvalproccessService;

        public IConfiguration _configuration;
        string targetPathLayout = "";
        public WatchWardController(IWatchandwardService watchandwardService, IApprovalProccessService approvalproccessService,
            IWorkflowTemplateService workflowtemplateService, IConfiguration configuration, 
            IPropertyRegistrationService propertyregistrationService)
        {
            _workflowtemplateService = workflowtemplateService;
            _propertyregistrationService = propertyregistrationService;
            _watchandwardService = watchandwardService;
            _configuration = configuration;
            _approvalproccessService = approvalproccessService;
        }


        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] WatchandwardSearchDto model)
        {
            var result = await _watchandwardService.GetPagedWatchandward(model);
            return PartialView("_List", result);
        }


        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Watchandward watchandward = new Watchandward();
            watchandward.IsActive = 1;
            watchandward.LocalityList = await _watchandwardService.GetAllLocality();
            watchandward.KhasraList = await _watchandwardService.GetAllKhasra();
            watchandward.PrimaryListNoList = await _watchandwardService.GetAllPrimaryList();
            return View(watchandward);
        }


        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Watchandward watchandward)
        {
            watchandward.LocalityList = await _watchandwardService.GetAllLocality();
            watchandward.KhasraList = await _watchandwardService.GetAllKhasra();
            watchandward.PrimaryListNoList = await _watchandwardService.GetAllPrimaryList();

            string targetPhotoPathLayout = _configuration.GetSection("FilePaths:WatchAndWard:Photo").Value.ToString();
            string targetReportfilePathLayout = _configuration.GetSection("FilePaths:WatchAndWard:ReportFile").Value.ToString();
            if (ModelState.IsValid)
            {
                if (watchandward.Encroachment == 0)
                    watchandward.Encroachment = 0;
                else if (watchandward.Encroachment == 1)
                    watchandward.Encroachment = 1;
                var result = await _watchandwardService.Create(watchandward);

                if (result)
                {
                    FileHelper fileHelper = new FileHelper();
                    ///for photo file:
                    if (watchandward.Photo != null && watchandward.Photo.Count > 0)
                    {
                        List<Watchandwardphotofiledetails> watchandwardphotofiledetails = new List<Watchandwardphotofiledetails>();
                        for (int i = 0; i < watchandward.Photo.Count; i++)
                        {
                            string FilePath = fileHelper.SaveFile(targetPhotoPathLayout, watchandward.Photo[i]);
                            GetLattLongDetails(FilePath, watchandward.Latitude, watchandward.Longitude);
                            var LattitudeValue = TempData["LattitudeValue"] as string;
                            watchandward.Latitude = LattitudeValue;
                            var LongitudeValue = TempData["LongitudeValue"] as string;
                            watchandward.Longitude = LongitudeValue;
                            var lattlongurlvalue = TempData["url"] as string;
                            watchandwardphotofiledetails.Add(new Watchandwardphotofiledetails
                            {
                                WatchAndWardId = watchandward.Id,
                                PhotoFilePath = FilePath,
                                Lattitude = watchandward.Latitude,
                                Longitude = watchandward.Longitude,
                                LattLongUrl = lattlongurlvalue

                            });
                        }
                        foreach (var item in watchandwardphotofiledetails)
                        {
                            result = await _watchandwardService.SaveWatchandwardphotofiledetails(item);
                        }
                    }
                    //for report file:
                    if (watchandward.ReportFile != null && watchandward.ReportFile.Count > 0)
                    {
                        List<Watchandwardreportfiledetails> watchandwardreportfiledetails = new List<Watchandwardreportfiledetails>();
                        for (int i = 0; i < watchandward.Photo.Count; i++)
                        {
                            string FilePath = fileHelper.SaveFile(targetReportfilePathLayout, watchandward.Photo[i]);
                            watchandwardreportfiledetails.Add(new Watchandwardreportfiledetails
                            {
                                WatchAndWardId = watchandward.Id,
                                ReportFilePath = FilePath
                            });
                        }
                        foreach (var item in watchandwardreportfiledetails)
                        {
                            result = await _watchandwardService.SaveWatchandwardreportfiledetails(item);
                        }
                    }

                    if (result)
                    {
                        //#region Approval Proccess At 1st level start Added by Renu 26 Nov 2020
                        //var DataFlow = await dataAsync();
                        //for (int i = 0; i < DataFlow.Count; i++)
                        //{
                        //    if (!DataFlow[i].parameterSkip)
                        //    {
                        //        watchandward.ApprovedStatus = 0;
                        //        watchandward.PendingAt = Convert.ToInt32(DataFlow[i].parameterName);
                        //        watchandward.ModifiedBy = SiteContext.UserId;
                        //        result = await _watchandwardService.UpdateBeforeApproval(watchandward.Id, watchandward);  //Update Table details 
                        //        if (result)
                        //        {
                        //            Approvalproccess approvalproccess = new Approvalproccess();
                        //            approvalproccess.ModuleId = Convert.ToInt32(_configuration.GetSection("approvalModuleId").Value);
                        //            approvalproccess.ProccessID = Convert.ToInt32(_configuration.GetSection("workflowPreccessId").Value);
                        //            approvalproccess.ServiceId = watchandward.Id;
                        //            approvalproccess.SendFrom = SiteContext.UserId;
                        //            approvalproccess.SendTo = Convert.ToInt32(DataFlow[i].parameterName);
                        //            approvalproccess.PendingStatus = 1;   //1
                        //            approvalproccess.Status = null;   //1
                        //            approvalproccess.Remarks = "Record Added and Send for Approval";///May be Uncomment
                        //            result = await _approvalproccessService.Create(approvalproccess, SiteContext.UserId); //Create a row in approvalproccess Table
                        //        }

                        //        break;
                        //    }
                        //}

                        //#endregion 

                        ViewBag.Message = Alert.Show(Messages.AddAndApprovalRecordSuccess, "", AlertType.Success);
                        var result1 = await _watchandwardService.GetAllWatchandward();
                        return View("Index", result1);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(watchandward);
                    }
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(watchandward);
                }
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(watchandward);
            }
        }


        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            
            var Data = await _watchandwardService.FetchSingleResult(id);
          

            Data.LocalityList = await _watchandwardService.GetAllLocality();
            Data.KhasraList = await _watchandwardService.GetAllKhasra();
            Data.PrimaryListNoList = await _watchandwardService.GetAllPrimaryList();
          

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [HttpPost]

        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Watchandward watchandward)
        {
            var Data = await _watchandwardService.FetchSingleResult(id);
            Data.LocalityList = await _watchandwardService.GetAllLocality();
            Data.KhasraList = await _watchandwardService.GetAllKhasra();
            Data.PrimaryListNoList = await _watchandwardService.GetAllPrimaryList();
            string targetPhotoPathLayout = _configuration.GetSection("FilePaths:WatchAndWard:Photo").Value.ToString();
            string targetReportfilePathLayout = _configuration.GetSection("FilePaths:WatchAndWard:ReportFile").Value.ToString();

            if (ModelState.IsValid)
            {
                 if (watchandward.Encroachment == 0)
                    watchandward.Encroachment = 0;
                else if (watchandward.Encroachment == 1)
                    watchandward.Encroachment = 1;
                var result = await _watchandwardService.Update(id, watchandward);
                if (result)
                {
                    FileHelper fileHelper = new FileHelper();


                    //for photo file:

                    if (watchandward.Photo != null && watchandward.Photo.Count > 0)
                    {
                        List<Watchandwardphotofiledetails> watchandwardphotofiledetails = new List<Watchandwardphotofiledetails>();
                        result = await _watchandwardService.DeleteWatchandwardphotofiledetails(id);
                        for (int i = 0; i < watchandward.Photo.Count; i++)
                        {
                            string FilePath = fileHelper.SaveFile(targetPhotoPathLayout, watchandward.Photo[i]);
                            GetLattLongDetails(FilePath, watchandward.Latitude, watchandward.Longitude);
                            var LattitudeValue = TempData["LattitudeValue"] as string;
                            watchandward.Latitude = LattitudeValue;
                            var LongitudeValue = TempData["LongitudeValue"] as string;
                            watchandward.Longitude = LongitudeValue;
                            var lattlongurlvalue = TempData["url"] as string;
                            watchandwardphotofiledetails.Add(new Watchandwardphotofiledetails
                            {
                                WatchAndWardId = watchandward.Id,
                                PhotoFilePath = FilePath,
                                Lattitude = watchandward.Latitude,
                                Longitude = watchandward.Longitude,
                                LattLongUrl = lattlongurlvalue

                            });
                        }
                        foreach (var item in watchandwardphotofiledetails)
                        {
                            result = await _watchandwardService.SaveWatchandwardphotofiledetails(item);
                        }
                    }

                    //for report file:

                    if (watchandward.ReportFile != null && watchandward.ReportFile.Count > 0)
                    {
                        List<Watchandwardreportfiledetails> watchandwardreportfiledetails = new List<Watchandwardreportfiledetails>();
                        result = await _watchandwardService.DeleteWatchandwardreportfiledetails(id);
                        for (int i = 0; i < watchandward.Photo.Count; i++)
                        {
                            string FilePath = fileHelper.SaveFile(targetReportfilePathLayout, watchandward.Photo[i]);
                            watchandwardreportfiledetails.Add(new Watchandwardreportfiledetails
                            {
                                WatchAndWardId = watchandward.Id,
                                ReportFilePath = FilePath
                            });
                        }
                        foreach (var item in watchandwardreportfiledetails)
                        {
                            result = await _watchandwardService.SaveWatchandwardreportfiledetails(item);
                        }
                    }

                    if (result)
                    {
                      
                        ViewBag.Message = Alert.Show(Messages.UpdateAndApprovalRecordSuccess, "", AlertType.Success);
                        var result1 = await _watchandwardService.GetAllWatchandward();

                        return View("Index", result1);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(watchandward);
                    }

                }
                else
                {
                    return View(watchandward);
                }
            }
            else
            {
                return View(watchandward);
            }

        }

        [AuthorizeContext(ViewAction.View)]
        public async Task<IActionResult> View(int id)
        {
            var Data = await _watchandwardService.FetchSingleResult(id);
            Data.LocalityList = await _watchandwardService.GetAllLocality();
            Data.KhasraList = await _watchandwardService.GetAllKhasra();
            Data.PrimaryListNoList = await _watchandwardService.GetAllPrimaryList();
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }


        [AuthorizeContext(ViewAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _watchandwardService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
            }
            var result1 = await _watchandwardService.GetAllWatchandward();
            return View("Index", result1);
        }


        //***to download photo file ***
        public async Task<IActionResult> DownloadPhotoFile(int Id)
        {
            FileHelper file = new FileHelper();
            Watchandwardphotofiledetails Data = await _watchandwardService.GetWatchandwardphotofiledetails(Id);
            string filename = Data.PhotoFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        //***to download report file ***
        public async Task<IActionResult> DownloadReportFile(int Id)
        {
            FileHelper file = new FileHelper();
            Watchandwardreportfiledetails Data = await _watchandwardService.GetWatchandwardreportfiledetails(Id);
            string filename = Data.ReportFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public IActionResult WatchWardApproval()
        {
            return View();
        }

        public IActionResult WatchWardApprovalCreate()
        {
            return View();
        }

        [HttpGet]
        public void GetLattLongDetails(string path, string Latt, string Long)
        {
            double? latitdue = null;
            double? longitude = null;
            string url = null;
            if (path != null)
            {
                Bitmap bmp = new Bitmap(path);
                foreach (PropertyItem propItem in bmp.PropertyItems)
                {
                    switch (propItem.Type)
                    {
                        case 5:
                            if (propItem.Id == 2) // Latitude Array
                            {
                                latitdue = GetLatitudeAndLongitude(propItem);
                            }
                            if (propItem.Id == 4) //Longitude Array
                            {
                                longitude = GetLatitudeAndLongitude(propItem);
                            }
                            break;
                    }
                }

                if (!string.IsNullOrEmpty(latitdue.ToString()) && !string.IsNullOrEmpty(longitude.ToString()))
                {
                    url = $"https://www.google.com/maps/place/{latitdue},{longitude}";
                }
                else
                {
                    ViewBag.Message = Alert.Show("Uploaded Image does not contain any geo location.please enter your request location in below textbox", "", AlertType.Error);

                }
                bmp.Dispose();
            }
            TempData["LattitudeValue"] = latitdue.ToString();
            TempData["LongitudeValue"] = longitude.ToString();
            TempData["url"] = url;
        }

        private static double? GetLatitudeAndLongitude(PropertyItem propItem)
        {
            try
            {
                uint degreesNumerator = BitConverter.ToUInt32(propItem.Value, 0);
                uint degreesDenominator = BitConverter.ToUInt32(propItem.Value, 4);
                uint minutesNumerator = BitConverter.ToUInt32(propItem.Value, 8);
                uint minutesDenominator = BitConverter.ToUInt32(propItem.Value, 12);
                uint secondsNumerator = BitConverter.ToUInt32(propItem.Value, 16);
                uint secondsDenominator = BitConverter.ToUInt32(propItem.Value, 20);
                return (Convert.ToDouble(degreesNumerator) / Convert.ToDouble(degreesDenominator)) + (Convert.ToDouble(Convert.ToDouble(minutesNumerator) / Convert.ToDouble(minutesDenominator)) / 60) +
                       (Convert.ToDouble((Convert.ToDouble(secondsNumerator) / Convert.ToDouble(secondsDenominator)) / 3600));
            }
            catch (Exception)
            {

                return null;
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetOtherDetails(int? propertyId)
        {
            propertyId = propertyId ?? 0;
            var data = await _watchandwardService.FetchSingleResultOnPrimaryList(Convert.ToInt32(propertyId));
            return Json(data);
        }

        [HttpGet]
        public async Task<JsonResult> GetLocalityList()
        {
            return Json(await _watchandwardService.GetAllLocality());
        }

        public async Task<FileResult> ViewDocument(int Id)
        {
            FileHelper file = new FileHelper();
            Watchandwardphotofiledetails Data = await _watchandwardService.GetWatchandwardphotofiledetails(Id);
            string path = Data.PhotoFilePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }

        #region View Details of Property Inventory

        async Task BindDropDown(Propertyregistration propertyregistration)
        {
            propertyregistration.ClassificationOfLandList = await _propertyregistrationService.GetClassificationOfLandDropDownList();
            propertyregistration.LandUseList = await _propertyregistrationService.GetLandUseDropDownList();
            propertyregistration.DisposalTypeList = await _propertyregistrationService.GetDisposalTypeDropDownList();
            propertyregistration.DepartmentList = await _propertyregistrationService.GetDepartmentDropDownList();
            propertyregistration.TakenOverDepartmentList = await _propertyregistrationService.GetTakenDepartmentDropDownList();
            propertyregistration.HandOverDepartmentList = await _propertyregistrationService.GetHandedDepartmentDropDownList();

        }
        public async Task<PartialViewResult> InventoryView(int id)
        {
            var Data = await _propertyregistrationService.FetchSingleResult(id);
            if (Data != null)
            {
                ViewBag.LayoutDocView = Data.LayoutFilePath;
                ViewBag.GeoDocView = Data.GeoFilePath;
                ViewBag.TakenOverDocView = Data.TakenOverFilePath;
                ViewBag.HandedOverDocView = Data.HandedOverFilePath;
                ViewBag.DisposalTypeDocView = Data.DisposalTypeFilePath;
                await BindDropDown(Data);

                Data.ZoneList = await _propertyregistrationService.GetZoneDropDownList(Data.DepartmentId);
                Data.LocalityList = await _propertyregistrationService.GetLocalityDropDownList(Data.ZoneId);
                Data.DivisionList = await _propertyregistrationService.GetDivisionDropDownList(Data.ZoneId);
                Data.HandedOverZoneList = await _propertyregistrationService.GetZoneDropDownList(Data.DepartmentId);
                Data.HandedOverDivisionList = await _propertyregistrationService.GetDivisionDropDownList(Data.ZoneId);
                Data.TakenOverZoneList = await _propertyregistrationService.GetZoneDropDownList(Data.DepartmentId);
                Data.TakenOverDivisionList = await _propertyregistrationService.GetDivisionDropDownList(Data.ZoneId);
            }
            return PartialView("_InventoryView", Data);
        }

        #endregion

        #region Fetch workflow data for approval prrocess Added by Renu 26 Nov 2020
        private async Task<List<TemplateStructure>> dataAsync()
        {
            var Data = await _workflowtemplateService.FetchSingleResult(Convert.ToInt32(_configuration.GetSection("workflowPreccessId").Value));
            var template = Data.Template;
            List<TemplateStructure> ObjList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TemplateStructure>>(template);
            return ObjList;
        }
        #endregion





        public async Task<IActionResult> WatchWardList()
        {
            var result = await _watchandwardService.GetAllWatchandward();
            List<WatchWardListDto> data = new List<WatchWardListDto>();
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    data.Add(new WatchWardListDto()
                    {
                        Id = result[i].Id,  
                        Date=result[i].Date.ToString()==null ? "": result[i].Date.ToString(),
                        
                        Loaclity = result[i].PrimaryListNoNavigation == null ? "" : result[i].PrimaryListNoNavigation.Locality == null ? "" : result[i].PrimaryListNoNavigation.Locality.Name,
                        KhasraNo = result[i].Khasra == null ? "" : result[i].Khasra.Name.ToString(),
                        PrimaryListNo = result[i].PrimaryListNo.ToString(),
                        StatusOnGround= result[i].StatusOnGround.ToString(),
                        IsActive = result[i].IsActive.ToString() == "1" ? "Active" : "Inactive",
                    }); ;
                }
            }

            var memory = ExcelHelper.CreateExcel(data);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

    }
}
