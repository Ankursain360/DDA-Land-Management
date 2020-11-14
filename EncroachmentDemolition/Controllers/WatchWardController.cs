using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Libraries.Service.IApplicationService;
using Libraries.Model.Entity;
//using AutoMapper.Configuration;
using Utility.Helper;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.Extensions.Configuration;
using Dto.Search;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace EncroachmentDemolition.Controllers
{
    public class WatchWardController : BaseController
    {
        private readonly IWatchandwardService _watchandwardService;
        public IConfiguration _configuration;
        //string targetPhotoPathLayout = string.Empty;
        //string targetReportfilePathLayout = string.Empty;
        string targetPathLayout = "";
        public WatchWardController(IWatchandwardService watchandwardService, IConfiguration configuration)
        {
            _watchandwardService = watchandwardService;
            _configuration = configuration;
        }

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
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
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

        public async Task<IActionResult> Edit(int id)
        {
          //  Watchandwardphotofiledetails watchandwardphotofiledetails = new Watchandwardphotofiledetails();
            var Data = await _watchandwardService.FetchSingleResult(id);
            //if (Data.Encroachment == 0)
            //    Data.EncroachmentStatus = 0;
            //else
            //    Data.EncroachmentStatus = 1;

            Data.LocalityList = await _watchandwardService.GetAllLocality();
            Data.KhasraList = await _watchandwardService.GetAllKhasra();
            Data.PrimaryListNoList = await _watchandwardService.GetAllPrimaryList();
            //for (int i= 0 ; i< Data.Watchandwardphotofiledetails.Count; i++)
            //{
            //    if (!string.IsNullOrEmpty(Data.Watchandwardphotofiledetails.First().Lattitude) && !string.IsNullOrEmpty(Data.Watchandwardphotofiledetails.First().Longitude))
            //    {
            //        string latitdue = Data.Watchandwardphotofiledetails.First().Lattitude;
            //        string longitude = Data.Watchandwardphotofiledetails.First().Longitude;
            //        ViewBag.LattLongUrlList = $"https://www.google.com/maps/place/{latitdue},{longitude}";
            //        watchandwardphotofiledetails.urlList = "https://www.google.com/maps/place/{latitdue},{longitude}";
            //    }
            //}

            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [HttpPost]
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
                //targetPhotoPathLayout = _configuration.GetSection("FilePaths:WatchAndWard:Photo").Value.ToString();
                //targetReportfilePathLayout = _configuration.GetSection("FilePaths:WatchAndWard:ReportFile").Value.ToString();
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
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
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
            string path23 = "D:\\VedangWorkFromHome\\DDA_LandManagement_Project\\ExtraData\\40BAC009-7963-4F73-AD7A-3F0E52DEC3F3.jpeg";
            var longi = "";
            double? latitdue = null;
            double? longitude = null;
            string url = null;
            if (path != null)
            {
                Bitmap bmp = new Bitmap(path);
                // set Variable Values
                

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
                    Console.WriteLine($"https://www.google.com/maps/place/{latitdue},{longitude}");
                    //ViewState["ImagePath"]
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
            return Json(await _watchandwardService.FetchSingleResultOnPrimaryList(Convert.ToInt32(propertyId)));
        }
    }
}
