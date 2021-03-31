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
using Microsoft.Extensions.Configuration;
using Utility.Helper;
using Microsoft.AspNetCore.Http;
using System.IO;
using LeaseDetails.Filters;
using Core.Enum;
using System.Drawing;
using System.Drawing.Imaging;

namespace LeaseDetails.Controllers
{
    public class HearingdetailController : BaseController
    {
        private readonly IHearingdetailsService _hearingdetailsService;
        public IConfiguration _configuration;
          private readonly IRequestforproceedingService _requestforproceedingService;


        public HearingdetailController(IHearingdetailsService hearingdetailsService, IConfiguration configuration, IRequestforproceedingService requestforproceedingService)
        {
            _configuration = configuration;
            _hearingdetailsService = hearingdetailsService;
            _requestforproceedingService = requestforproceedingService;
        }

        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] LeaseHearingDetailsSearchDto model)
        {
            var result = await _hearingdetailsService.GetPagedRequestLetterDetails(model);
            return PartialView("_List", result);
        }
        //public IActionResult Index()
        //{

        //    return View();
        //}
        //[HttpPost]
        //public async Task<PartialViewResult> ListReq([FromBody] RequestForProceedingSearchDto model)
        //{
        //    var result = await _requestforproceedingService.GetPagedRequestForProceeding(model);

        //    return PartialView("_ListReq", result);
        //}

        //[HttpPost]
        //public async Task<PartialViewResult> List([FromBody] HearingdetailsSeachDto model)
        //{

        //    var result = await _hearingdetailsService.GetPagedHearingDetails(model);
        //    return PartialView("_List", result);
        //}

        //   [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(int id)
        {
            //if (id == 0)
            //{
                Hearingdetails groundrent = new Hearingdetails();
                groundrent.IsActive = 1;
                groundrent.CreatedBy = SiteContext.UserId;
                // groundrent.PropertyTypeList = await _groundRentService.GetAllPropertyTypeList();
                return View(groundrent);

            //}
            //else
            //{ var Data = await _hearingdetailsService.FetchSingleResultReq(id);

            //    if (Data == null)
            //    {
            //        return NotFound();
                    
            //    }
            //    return View(Data);

            //}

            

        }
        [HttpPost]
        public async Task<IActionResult> Create(Hearingdetails hearingdetails)
        {
            try
            {
                //string targetPhotoPathLayout = _configuration.GetSection("FilePaths:Hearingdetail:Photo").Value.ToString();
                //if (ModelState.IsValid)
                //{
                hearingdetails.CreatedBy = SiteContext.UserId;
                    hearingdetails.ReqProcId = 18;
                    hearingdetails.NoticeGenId = 3;
                    hearingdetails.EvidanceDocId = 1;
                    hearingdetails.Remark = " ";
                    hearingdetails.IsActive = 1;
                    hearingdetails.CreatedBy = 1;
                    var result = await _hearingdetailsService.Create(hearingdetails);
                /*     for multiple files       */
                //if (result)
                //{
                //    FileHelper fileHelper = new FileHelper();
                //    ///for photo file:
                //    if (hearingdetails.Photo != null && hearingdetails.Photo.Count > 0)
                //    {
                //        List<Watchandwardphotofiledetails> watchandwardphotofiledetails = new List<Watchandwardphotofiledetails>();
                //        for (int i = 0; i < hearingdetails.Photo.Count; i++)
                //        {
                //            string FilePath = fileHelper.SaveFile(targetPhotoPathLayout, hearingdetails.Photo[i]);
                //            GetLattLongDetails(FilePath, hearingdetails.Latitude, hearingdetails.Longitude);
                //            var LattitudeValue = TempData["LattitudeValue"] as string;
                //            hearingdetails.Latitude = LattitudeValue;
                //            var LongitudeValue = TempData["LongitudeValue"] as string;
                //            hearingdetails.Longitude = LongitudeValue;
                //            var lattlongurlvalue = TempData["url"] as string;
                //            watchandwardphotofiledetails.Add(new Watchandwardphotofiledetails
                //            {
                //                WatchAndWardId = hearingdetails.Id,
                //                PhotoFilePath = FilePath,
                //                Lattitude = hearingdetails.Latitude,
                //                Longitude = hearingdetails.Longitude,
                //                LattLongUrl = lattlongurlvalue

                //            });
                //        }
                //           foreach (var item in watchandwardphotofiledetails)
                //        {
                //            result = await _hearingdetailsService.SaveHearingphotofiledetails(item);
                //        }
                //    } }

                    /* Multiple file upload ends here        */
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        return RedirectToAction("Index", "Hearingdetail");
                        //return View();
                        //var list = await _possesionplanService.GetAllPossesionplan();
                        //return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(hearingdetails);

                    }
                //}
                //else
                //{
                //    return View(hearingdetails);
                //}
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(hearingdetails);
            }
       }
        public async Task<IActionResult> DownloadPhotoFile(int Id)
        {
            FileHelper file = new FileHelper();
            Hearingdetailsphotofiledetails Data = await _hearingdetailsService.GetHphotofiledetails(Id);
            string filename = Data.PhotoFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
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
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _hearingdetailsService.FetchSingleResult(id);
           
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        //  [ValidateAntiForgeryToken]
        //  [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Hearingdetails rate)
        {
           
            if (ModelState.IsValid)
            {
                try
                {
                    rate.ReqProcId = 18;
                    rate.NoticeGenId = 3;
                    rate.EvidanceDocId = 1;
                    rate.ModifiedBy = SiteContext.UserId;
                    var result = await _hearingdetailsService.Update(id, rate);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);

                        //var list = await _hearingdetailsService.GetAllInterestrate();
                        //return View("Index", list);
                        return RedirectToAction("Create", "Hearingdetail");
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(rate);

                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(rate);

                }
            }
            return View(rate);
        }
        public IActionResult ViewLetter()
        {
            return View();
        }
        public async Task<FileResult> ViewDocument(int Id)
        {
            FileHelper file = new FileHelper();
            Hearingdetailsphotofiledetails Data = await _hearingdetailsService.GetHphotofiledetails(Id);
            string path = Data.PhotoFilePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }



    }
}
