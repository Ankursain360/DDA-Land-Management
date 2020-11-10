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
            return View(watchandward);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Watchandward watchandward)
        {
            watchandward.LocalityList = await _watchandwardService.GetAllLocality();
            watchandward.KhasraList = await _watchandwardService.GetAllKhasra();

            
            string targetPhotoPathLayout = _configuration.GetSection("FilePaths:WatchAndWard:Photo").Value.ToString();
            string targetReportfilePathLayout = _configuration.GetSection("FilePaths:WatchAndWard:ReportFile").Value.ToString();
               if (ModelState.IsValid)
            {
               
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
                            
                            watchandwardphotofiledetails.Add(new Watchandwardphotofiledetails
                            {
                                WatchAndWardId = watchandward.Id,
                                PhotoFilePath = FilePath
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
                                ReportFilePath= FilePath
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
            var Data = await _watchandwardService.FetchSingleResult(id);
        
            Data.LocalityList = await _watchandwardService.GetAllLocality();
            Data.KhasraList = await _watchandwardService.GetAllKhasra();
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
            string targetPhotoPathLayout = _configuration.GetSection("FilePaths:WatchAndWard:Photo").Value.ToString();
            string targetReportfilePathLayout = _configuration.GetSection("FilePaths:WatchAndWard:ReportFile").Value.ToString();

            if (ModelState.IsValid)
            {
                //targetPhotoPathLayout = _configuration.GetSection("FilePaths:WatchAndWard:Photo").Value.ToString();
                //targetReportfilePathLayout = _configuration.GetSection("FilePaths:WatchAndWard:ReportFile").Value.ToString();

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
                            watchandwardphotofiledetails.Add(new Watchandwardphotofiledetails
                            {
                                WatchAndWardId = watchandward.Id,
                                PhotoFilePath = FilePath
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
        public string GetLattLongDetails(string path)
        {
            string path23 = "D:\\VedangWorkFromHome\\DDA_LandManagement_Project\\ExtraData\\ddaImage.jpg";
            var longi = "";
            using (Bitmap bitmap = new Bitmap(path23, true))
            {
                var longitude = GetCoordinateDouble(bitmap.PropertyItems.Single(p => p.Id == 4));
                var latitude = GetCoordinateDouble(bitmap.PropertyItems.Single(p => p.Id == 2));

                Console.WriteLine($"Longitude: {longitude}");
                Console.WriteLine($"Latitude: {latitude}");

                Console.WriteLine($"https://www.google.com/maps/place/{latitude},{longitude}");

                
               
            }
            return longi;
        }

        private static double GetCoordinateDouble(PropertyItem propItem)
        {
            uint degreesNumerator = BitConverter.ToUInt32(propItem.Value, 0);
            uint degreesDenominator = BitConverter.ToUInt32(propItem.Value, 4);
            double degrees = degreesNumerator / (double)degreesDenominator;


            uint minutesNumerator = BitConverter.ToUInt32(propItem.Value, 8);
            uint minutesDenominator = BitConverter.ToUInt32(propItem.Value, 12);
            double minutes = minutesNumerator / (double)minutesDenominator;

            uint secondsNumerator = BitConverter.ToUInt32(propItem.Value, 16);
            uint secondsDenominator = BitConverter.ToUInt32(propItem.Value, 20);
            double seconds = secondsNumerator / (double)secondsDenominator;

            double coorditate = degrees + (minutes / 60d) + (seconds / 3600d);
            string gpsRef = System.Text.Encoding.ASCII.GetString(new byte[1] { propItem.Value[0] }); //N, S, E, or W  

            if (gpsRef == "S" || gpsRef == "W")
            {
                coorditate = coorditate * -1;
            }
            return coorditate;
        }
    }
}
