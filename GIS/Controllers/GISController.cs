using Dto.Master;
using GIS.Helper;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.Record.CF;
using NPOI.SS.Formula.Functions;
using Service.IApplicationService;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using OpenCvSharp;
using Dto.GIS;
using Microsoft.Extensions.Configuration;
using Accord.Imaging.Filters;
using Accord.Imaging;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.MachineLearning.VectorMachines;
using Accord.Statistics.Kernels;
using Org.BouncyCastle.Asn1.Tsp;
using Accord.MachineLearning;
using Accord.Math;
using Accord.IO;
using NPOI.POIFS.Crypt.Dsig;
using Utility.Helper;
using Microsoft.AspNetCore.Http;
using Point = OpenCvSharp.Point;
using AForge.Imaging.Filters;
using Notification.Constants;
using Notification.OptionEnums;
using Notification;
using Microsoft.AspNetCore.Hosting;
using System.Text;
using System.Xml.Linq;

namespace GIS.Controllers
{
    public class GISController : BaseController
    {
        private readonly IGISService _GISService;
        private readonly ISiteContext _siteContext;
        private readonly IUserProfileService _userProfileService;
        public IConfiguration _Configuration;
        private readonly IHostingEnvironment _hostingEnvironment;
        public GISController(IGISService GISService, IUserProfileService userProfileService, ISiteContext siteContext, IConfiguration configuration, IHostingEnvironment en)
        {
            _siteContext = siteContext;
            _userProfileService = userProfileService;
            _GISService = GISService;
            _Configuration = configuration;
            _hostingEnvironment = en;
        }
        public async Task<IActionResult> Index()
        {
            //GISDtoProfile gis = new GISDtoProfile();
            ViewBag.ZoneList = await _GISService.GetZoneList();
            //return View(gis);

            UserProfileDto user = await _userProfileService.GetUserById(_siteContext.UserId);
            return View(user);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<JsonResult> GetVillageList(int? ZoneId)
        {
            return Json(await _GISService.GetVillageList(ZoneId ?? 0));
        }
        public async Task<JsonResult> GetPlotList(int? VillageId)
        {
            return Json(await _GISService.GetPlotList(VillageId ?? 0));
        }
        public async Task<JsonResult> GetZoneDetails(int ZoneId)
        {
            return Json(await _GISService.GetZoneDetails(ZoneId));
        }
        public async Task<JsonResult> GetVillageDetails(int VillageId)
        {
            return Json(await _GISService.GetVillageDetails(VillageId));
        }
        public async Task<JsonResult> GetZoneList()
        {
            return Json(await _GISService.GetZoneList());
        }
        public async Task<JsonResult> GetAbadiDetails(int VillageId)
        {
            return Json(await _GISService.GetAbadiDetails(VillageId));
        }
        public async Task<JsonResult> GetBurjiDetails(int VillageId)
        {
            return Json(await _GISService.GetBurjiDetails(VillageId));
        }
        public async Task<JsonResult> GetCleanDetails(int VillageId)
        {
            return Json(await _GISService.GetCleanDetails(VillageId));
        }
        public async Task<JsonResult> GetCleantextDetails(int VillageId)
        {
            return Json(await _GISService.GetCleantextDetails(VillageId));
        }
        public async Task<JsonResult> GetDimDetails(int VillageId)
        {
            return Json(await _GISService.GetDimDetails(VillageId));
        }
        public async Task<JsonResult> GetEncroachmentDetails(int VillageId)
        {
            return Json(await _GISService.GetEncroachmentDetails(VillageId));
        }
        public async Task<JsonResult> GetGoshaDetails(int VillageId)
        {
            return Json(await _GISService.GetGoshaDetails(VillageId));
        }
        public async Task<JsonResult> GetGridDetails(int VillageId)
        {
            return Json(await _GISService.GetGridDetails(VillageId));
        }
        public async Task<JsonResult> GetNalaDetails(int VillageId)
        {
            return Json(await _GISService.GetNalaDetails(VillageId));
        }
        public async Task<JsonResult> GetTextDetails(int VillageId)
        {
            return Json(await _GISService.GetTextDetails(VillageId));
        }
        public async Task<JsonResult> GetTriJunctionDetails(int VillageId)
        {
            return Json(await _GISService.GetTriJunctionDetails(VillageId));
        }
        public async Task<JsonResult> GetInitiallyStateDetails()
        {
            return Json(await _GISService.GetInitiallyStateDetails());
        }
        public async Task<JsonResult> GetDashedDetails(int VillageId)
        {
            return Json(await _GISService.GetDashedDetails(VillageId));
        }
        public async Task<JsonResult> GetCloseDetails(int VillageId)
        {
            return Json(await _GISService.GetCloseDetails(VillageId));
        }
        public async Task<JsonResult> GetCloseTextDetails(int VillageId)
        {
            return Json(await _GISService.GetCloseTextDetails(VillageId));
        }
        public async Task<JsonResult> GetDimTextDetails(int VillageId)
        {
            return Json(await _GISService.GetDimTextDetails(VillageId));
        }
        public async Task<JsonResult> GetFieldBounDetails(int VillageId)
        {
            return Json(await _GISService.GetFieldBounDetails(VillageId));
        }
        public async Task<JsonResult> GetKillaDetails(int VillageId)
        {
            return Json(await _GISService.GetKillaDetails(VillageId));
        }
        public async Task<JsonResult> GetKhasraNoDetails(int VillageId)
        {
            return Json(await _GISService.GetKhasraNoDetails(VillageId));
        }
        public async Task<JsonResult> GetKhasraLineDetails(int VillageId)
        {
            return Json(await _GISService.GetKhasraLineDetails(VillageId));
        }
        public async Task<JsonResult> GetKhasraBoundaryDetails(int VillageId)
        {
            return Json(await _GISService.GetKhasraBoundaryDetails(VillageId));
        }
        public async Task<JsonResult> GetKachaPakaLineDetails(int VillageId)
        {
            return Json(await _GISService.GetKachaPakaLineDetails(VillageId));
        }
        public async Task<JsonResult> GetInnerDetails(int VillageId)
        {
            return Json(await _GISService.GetInnerDetails(VillageId));
        }
        public async Task<JsonResult> GetNaliDetails(int VillageId)
        {
            return Json(await _GISService.GetNaliDetails(VillageId));
        }
        public async Task<JsonResult> GetRailwayLineDetails(int VillageId)
        {
            return Json(await _GISService.GetRailwayLineDetails(VillageId));
        }
        public async Task<JsonResult> GetSahedaDetails(int VillageId)
        {
            return Json(await _GISService.GetSahedaDetails(VillageId));
        }
        public async Task<JsonResult> GetRoadDetails(int VillageId)
        {
            return Json(await _GISService.GetRoadDetails(VillageId));
        }
        public async Task<JsonResult> GetZeroDetails(int VillageId)
        {
            return Json(await _GISService.GetZeroDetails(VillageId));
        }
        public async Task<JsonResult> GetVillageTextDetails(int VillageId)
        {
            return Json(await _GISService.GetVillageTextDetails(VillageId));
        }
        public async Task<JsonResult> GetVillageBoundaryDetails(int VillageId)
        {
            return Json(await _GISService.GetVillageBoundaryDetails(VillageId));
        }

        [HttpPost]
        public async Task<JsonResult> AutoComplete(string prefix)
        {
            return Json(await _GISService.GetVillageAutoCompleteDetails(prefix));
        }


        public async Task<JsonResult> GetInfrastructureDetails(int VillageId)
        {
            return Json(await _GISService.GetInfrastructureDetails(VillageId));
        }

        public async Task<JsonResult> GetGisDataLayersDetails(int VillageId)
        {
            var data = await _GISService.GetGisDataLayersDetails(VillageId);
            List<gisDataTemp> temp = new List<gisDataTemp>();

            if (data != null)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    temp.Add(new gisDataTemp
                    {
                        Id = data[i].Id,
                        VillageId = data[i].VillageId,
                        GisLayerId = data[i].GisLayerId,
                        Xcoordinate = data[i].Xcoordinate,
                        Ycoordinate = data[i].Ycoordinate,
                        Polygon = data[i].Polygon,
                        Label = data[i].Label,
                        LabelXcoordinate = data[i].LabelXcoordinate,
                        LabelYcoordinate = data[i].LabelYcoordinate,
                        Name = data[i].GisLayer.Name,
                        Code = data[i].GisLayer.Code,
                        FillColor = data[i].GisLayer.FillColor,
                        StrokeColor = data[i].GisLayer.StrokeColor,
                        Type = data[i].GisLayer.Type,
                        CheckedStatus = data[i].GisLayer.CheckedStatus

                    });
                }
            }
            var result = Json(temp);
            return result;
        }

        public async Task<JsonResult> GetKhasraBasisOtherDetails(int VillageId, string KhasraNo, string RectNo)
        {
            var data = await _GISService.GetKhasraBasisOtherDetails(VillageId, KhasraNo, RectNo);
            return Json(data);
        }
        public async Task<JsonResult> GetKhasraBasisOtherDetailsForCourtCases(int VillageId, string KhasraNo, string RectNo)
        {
            var data = await _GISService.GetKhasraBasisOtherDetailsForCourtCases(VillageId, KhasraNo, RectNo);
            return Json(data);
        }
        public async Task<JsonResult> GetKhasraList(int? VillageId)
        {
            return Json(await _GISService.GetKhasraList(VillageId ?? 0));
        }
        public async Task<JsonResult> GetKhasraNoPolygon(int? gisDataId)
        {
            return Json(await _GISService.GetKhasraNoPolygon(gisDataId ?? 0));
        }

        //Testing With Open Street map
        public async Task<IActionResult> OSM()
        {
            return View();
        }
        //

        //update khasrano
        public async Task<JsonResult> UpdatekhasraNo(int khasraid, string KhasraNo)
        {
            var data = await _GISService.UpdatekhasraNo(khasraid, KhasraNo, _siteContext.UserId);
            return Json(data);
        }
        //
        #region AI based Encroachment
        //public async Task<IActionResult> AIChangeDetection()
        //{
        //    string path1 = @"D:\Vedang\DDA\AI Based Change Detection\Change-detection-in-multitemporal-satellite-images-master\images\Dubai\2022Image_page-0001.jpg";
        //    string path2 = @"D:\Vedang\DDA\AI Based Change Detection\Change-detection-in-multitemporal-satellite-images-master\images\Dubai\14-2022Image_page-0001.jpg";
        //    string savepath = @"D:\Vedang\DDA\AI Based Change Detection\Change-detection-in-multitemporal-satellite-images-master\images\Dubai\";
        //    // Load the two time images
        //    Bitmap image1 = new Bitmap(path1);
        //    Bitmap image2 = new Bitmap(path2);

        //    // Convert the images to grayscale
        //    Bitmap gray1 = Grayscale(image1);
        //    Bitmap gray2 = Grayscale(image2);

        //    // Subtract the two images to create a difference image
        //    Bitmap difference =Difference(gray1, gray2);
        //    difference.Save(savepath + Guid.NewGuid().ToString() + ".jpg");


        //    // Threshold the difference image to create a binary mask
        //    int threshold= 30;
        //    Bitmap mask = Threshold(difference, threshold);
        //    mask.Save(savepath + Guid.NewGuid().ToString() + "masked_.jpg");

        //    // Create a Graphics object to draw on the original image
        //    Graphics g = Graphics.FromImage(image1);

        //    // Find the bounding rectangle of the changes in the binary mask
        //    Rectangle bounds = FindBounds(mask);

        //    // Draw a red rectangle around the changes in the original image
        //    Pen pen = new Pen(Color.Red, 3);
        //    g.DrawRectangle(pen, bounds);

        //    image1.Save(savepath + Guid.NewGuid().ToString() + "colored_.jpg");
        //    // Find the location of the maximum intensity in the difference image
        //    //int max_x = 0;
        //    //int max_y = 0;
        //    //int max_intensity = 0;
        //    //for (int x = 0; x < difference.Width; x++)
        //    //{
        //    //    for (int y = 0; y < difference.Height; y++)
        //    //    {
        //    //        Color pixel = difference.GetPixel(x, y);
        //    //        int intensity = (int)(0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B);
        //    //        if (intensity > max_intensity)
        //    //        {
        //    //            max_x = x;
        //    //            max_y = y;
        //    //            max_intensity = intensity;
        //    //        }
        //    //    }
        //    //}

        //    // Calculate the wavelength based on the location of the maximum intensity
        //    //  double wavelength = CalculateWavelength(max_x, max_y);

        //    // Display the calculated wavelength
        //    //Console.WriteLine("Wavelength = " + wavelength.ToString());
        //    return View();
        //}

        // Grayscale function


        public async Task<IActionResult> AIChangeDetection()
        {
            ViewBag.ZoneList = await _GISService.GetZoneList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Process(ChangeDetectionDto dto)
        {

            string firstPhotoPath = string.Empty;
            string secondPhotoPath = string.Empty;
            var UploadFilePath = _Configuration.GetSection("FilePaths:InputImages:FirstPhotoPath").Value.ToString();
            var ChangedPhotoPath = _Configuration.GetSection("FilePaths:OutPutImages:ChangedImagePath").Value.ToString();
            if (dto.FirstPhoto != null)
            {
                if (!Directory.Exists(UploadFilePath))
                {
                    DirectoryInfo di = Directory.CreateDirectory(UploadFilePath);// Try to create the directory.
                }
                string FileName = Guid.NewGuid().ToString() + "_" + dto.FirstPhoto.FileName;
                firstPhotoPath = Path.Combine(UploadFilePath, FileName);
                using (var stream = new FileStream(firstPhotoPath, FileMode.Create))
                {
                    dto.FirstPhoto.CopyTo(stream);
                }
            }
            else
            {
                ViewBag.Message = Alert.Show("Please select First Image", "Alert", AlertType.Warning);
            }

            if (dto.SecondPhoto != null)
            {
                if (!Directory.Exists(UploadFilePath))
                {
                    DirectoryInfo di = Directory.CreateDirectory(UploadFilePath);// Try to create the directory.
                }
                string FileName = Guid.NewGuid().ToString() + "_" + dto.SecondPhoto.FileName;
                secondPhotoPath = Path.Combine(UploadFilePath, FileName);
                using (var stream = new FileStream(secondPhotoPath, FileMode.Create))
                {
                    dto.SecondPhoto.CopyTo(stream);
                }
            }
            else
            {
                ViewBag.Message = Alert.Show("Please select Second Image", "Alert", AlertType.Warning);
            }


            Mat img1 = Cv2.ImRead(firstPhotoPath);
            Mat img2 = Cv2.ImRead(secondPhotoPath);
            string savepath = ChangedPhotoPath;
            if (!Directory.Exists(savepath))
            {
                DirectoryInfo di = Directory.CreateDirectory(savepath);// Try to create the directory.
            }
            // Convert the images to grayscale
            Mat gray1 = new Mat();
            Mat gray2 = new Mat();
            Cv2.CvtColor(img1, gray1, ColorConversionCodes.BGR2GRAY);
            Cv2.CvtColor(img2, gray2, ColorConversionCodes.BGR2GRAY);

            // Calculate the absolute difference between the two grayscale images
            Mat delta = new Mat();
            Cv2.Absdiff(gray1, gray2, delta);

            // Apply threshold to the delta image to highlight the differences
            double threshold = 30;
            Mat thresholded = new Mat();
            Cv2.Threshold(delta, thresholded, threshold, 255, ThresholdTypes.Binary);

            // Apply morphological operations to remove noise and fill gaps
            Mat kernel = Cv2.GetStructuringElement(MorphShapes.Ellipse, new OpenCvSharp.Size(3, 3));
            Cv2.MorphologyEx(thresholded, thresholded, MorphTypes.Open, kernel);
            Cv2.MorphologyEx(thresholded, thresholded, MorphTypes.Close, kernel);
            string changeimg = savepath + Guid.NewGuid().ToString() + "delta_image.png";
            // Save the delta image
            Cv2.ImWrite(changeimg, thresholded);
            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(changeimg);
            dto.ChangedImage = string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(fileBytes));
            return View("AIChangeDetection", dto);
        }
        [HttpPost]
        public async Task<IActionResult> Process1(ChangeDetectionDto dto)
        {
            ViewBag.ZoneList = await _GISService.GetZoneList();
            try
            {
                string firstPhotoPath = string.Empty;
                string secondPhotoPath = string.Empty;
                var UploadFilePath = _Configuration.GetSection("FilePaths:InputImages:FirstPhotoPath").Value.ToString();
                var ChangedPhotoPath = _Configuration.GetSection("FilePaths:OutPutImages:ChangedImagePath").Value.ToString();
                string image1Path, image2Path, outputImagePath = string.Empty;

                if (dto.FirstPhoto != null)
                {
                    if (!Directory.Exists(UploadFilePath))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(UploadFilePath);// Try to create the directory.
                    }
                    string FileName = Guid.NewGuid().ToString() + "_" + dto.FirstPhoto.FileName;
                    dto.FirstPhotoPath = FileName;
                    firstPhotoPath = Path.Combine(UploadFilePath, FileName);
                    using (var stream = new FileStream(firstPhotoPath, FileMode.Create))
                    {
                        dto.FirstPhoto.CopyTo(stream);
                    }
                }
                else
                {
                    ViewBag.Message = Alert.Show("Please select First Image", "Alert", AlertType.Warning);
                }

                if (dto.SecondPhoto != null)
                {
                    if (!Directory.Exists(UploadFilePath))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(UploadFilePath);// Try to create the directory.
                    }
                    string FileName = Guid.NewGuid().ToString() + "_" + dto.SecondPhoto.FileName;
                    dto.SecondPhotoPath = FileName;
                    secondPhotoPath = Path.Combine(UploadFilePath, FileName);
                    using (var stream = new FileStream(secondPhotoPath, FileMode.Create))
                    {
                        dto.SecondPhoto.CopyTo(stream);
                    }
                }
                else
                {
                    ViewBag.Message = Alert.Show("Please select Second Image", "Alert", AlertType.Warning);
                }
                image1Path = firstPhotoPath;
                image2Path = secondPhotoPath;
                string savepath = ChangedPhotoPath;
                // Load the images
                Mat img1 = Cv2.ImRead(image1Path);
                Mat img2 = Cv2.ImRead(image2Path);

                // Convert images to grayscale
                Mat gray1 = new Mat();
                Mat gray2 = new Mat();
                Cv2.CvtColor(img1, gray1, ColorConversionCodes.BGR2GRAY);
                Cv2.CvtColor(img2, gray2, ColorConversionCodes.BGR2GRAY);

                // Get image resolutions
                OpenCvSharp.Size resolution1 = img1.Size();
                OpenCvSharp.Size resolution2 = img2.Size();

                dto.FirstImageResoultion = resolution1.ToString();
                dto.SecondImageResoultion = resolution2.ToString();
                // Compute absolute difference between the two images
                Mat diff = new Mat();
                Cv2.Absdiff(gray1, gray2, diff);

                // Apply a threshold to highlight differences
              
                Cv2.Threshold(diff, diff, 30, 255, ThresholdTypes.Binary);

                // Find contours of differences
                Point[][] contours;
                HierarchyIndex[] hierarchy;
                Cv2.FindContours(diff, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);

                // Draw contours on the original images
                Mat result = img1.Clone();
                Cv2.DrawContours(result, contours, -1, Scalar.Red, 2); // Highlight differences in red

                // Calculate similarity percentage
                int totalPixels = resolution1.Width * resolution1.Height;
                int differentPixels = Cv2.CountNonZero(diff);
                double similarityPercentage = ((totalPixels - differentPixels) / (double)totalPixels) * 100;
                dto.Similarity = similarityPercentage.ToString();

                // Save the result
                //Cv2.ImWrite(outputImagePath, result);
                Cv2.Resize(result, result, new OpenCvSharp.Size(800, 500));
                dto.ChangedImagePath = Guid.NewGuid().ToString() + "diff.jpg";
                string changeimg = savepath + dto.ChangedImagePath;

                Cv2.ImWrite(changeimg, result);
                // Cv2.ImShow("diff.jpg", result);
                // Cv2.WaitKey();
                byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(changeimg);
                dto.ChangedImage = string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(fileBytes));
                var results = true;
                dto.CreatedBy = _siteContext.UserId;
                //insert data
                results = await _GISService.InsertchangeDetectiondata(dto);
                // 
                if (results)
                {
                    var sendMailResult = false;
                    MailSMSHelper mailG = new MailSMSHelper();
                    string path = Path.Combine(Path.Combine(_hostingEnvironment.WebRootPath, "EmailTemp"), "ChangeDetection.html");
                    var senderUser = await _userProfileService.GetUserById(SiteContext.UserId);

                    string body = string.Empty;
                    using (StreamReader reader = new StreamReader(path))
                    {
                        body = reader.ReadToEnd();
                    }

                    body = body.Replace("{User}", senderUser.User.Name);
                    body = body.Replace("{Emaild}", senderUser.User.Email);

                    #region Common Mail Genration
                    SentMailGenerationDto maildto = new SentMailGenerationDto();
                    maildto.strMailSubject = "Detected Changes in Satellite Images";
                    maildto.strMailCC = ""; maildto.strMailBCC = ""; maildto.strAttachPath = changeimg;
                    maildto.strBodyMsg = body;
                    maildto.defaultPswd = (_Configuration.GetSection("EmailConfiguration:defaultPswd").Value).ToString();
                    maildto.fromMail = (_Configuration.GetSection("EmailConfiguration:fromMail").Value).ToString();
                    maildto.fromMailPwd = (_Configuration.GetSection("EmailConfiguration:fromMailPwd").Value).ToString();
                    maildto.mailHost = (_Configuration.GetSection("EmailConfiguration:mailHost").Value).ToString();
                    maildto.port = Convert.ToInt32(_Configuration.GetSection("EmailConfiguration:port").Value);

                    maildto.strMailTo = senderUser.User.Email;
                    sendMailResult = mailG.SendMailWithAttachment(maildto);
                    #endregion
                    ViewBag.Message = Alert.Show("Image Successfully Processed and Result shared thorugh Email", "Alert", AlertType.Success);
                }
                else
                {
                    ViewBag.Message = Alert.Show("Unable to process the images", "Alert", AlertType.Error);
                }

            }
            catch (Exception ex)
            {

                ViewBag.Message = Alert.Show(ex.Message.ToString(), "Alert", AlertType.Error);
            }
            return View("AIChangeDetection", dto);
        }
        public string PopulateBodyApprovalMailDetails(ApprovalMailBodyDto element)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(element.path))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{ApplicationName}", element.ApplicationName);
            body = body.Replace("{AppRefNo}", element.AppRefNo);
            body = body.Replace("{SubmitDate}", element.SubmitDate);
            body = body.Replace("{SenderName}", element.SenderName);
            body = body.Replace("{Remarks}", element.Remarks);
            body = body.Replace("{Status}", element.Status);
            body = body.Replace("{Link}", element.Link);


            return body;
        }

        private static Bitmap Grayscales(Bitmap bitmap)
        {
            Bitmap grayscale = new Bitmap(bitmap.Width, bitmap.Height);
            for (int x = 0; x < grayscale.Width; x++)
            {
                for (int y = 0; y < grayscale.Height; y++)
                {
                    Color pixel = bitmap.GetPixel(x, y);
                    int gray = (int)(0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B);
                    grayscale.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
                }
            }
            return grayscale;
        }

        // Difference function
        private static Bitmap Difference(Bitmap bitmap1, Bitmap bitmap2)
        {
            Bitmap difference = new Bitmap(bitmap1.Width, bitmap1.Height);
            for (int x = 0; x < difference.Width; x++)
            {
                for (int y = 0; y < difference.Height; y++)
                {
                    Color pixel1 = bitmap1.GetPixel(x, y);
                    Color pixel2 = bitmap2.GetPixel(x, y);
                    int r = Math.Abs(pixel1.R - pixel2.R);
                    int g = Math.Abs(pixel1.G - pixel2.G);
                    int b = Math.Abs(pixel1.B - pixel2.B);
                    int gray = (int)(0.299 * r + 0.587 * g + 0.114 * b);
                    difference.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
                }
            }
            return difference;
        }

        // Calculate wavelength function
        //private static double CalculateWavelength(int x, int y)
        //{
        //    // TODO: Implement wavelength calculation based on the location of the maximum intensity
        //}
        //  Note that the CalculateWavelength function is not implemented in this code.You will need to write this function based on your specific requirements and the characteristics of your time images.

        // Threshold function
        private static Bitmap Threshold(Bitmap bitmap, int threshold)
        {
            Bitmap thresholded = new Bitmap(bitmap.Width, bitmap.Height);
            for (int x = 0; x < thresholded.Width; x++)
            {
                for (int y = 0; y < thresholded.Height; y++)
                {
                    Color pixel = bitmap.GetPixel(x, y);
                    int intensity = (int)(0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B);
                    if (intensity > threshold)
                    {
                        thresholded.SetPixel(x, y, Color.White);
                    }
                    else
                    {
                        thresholded.SetPixel(x, y, Color.Black);
                    }
                }
            }
            return thresholded;
        }


        // FindBounds function
        private static Rectangle FindBounds(Bitmap bitmap)
        {
            // Find the minimum and maximum coordinates of the nonzero pixels
            int minX = bitmap.Width;
            int minY = bitmap.Height;
            int maxX = 0;
            int maxY = 0;
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color pixel = bitmap.GetPixel(x, y);
                    if (pixel.R > 0)
                    {
                        minX = Math.Min(minX, x);
                        minY = Math.Min(minY, y);
                        maxX = Math.Max(maxX, x);
                        maxY = Math.Max(maxY, y);
                    }
                }
            }

            // Create a rectangle based on the minimum and maximum coordinates
            Rectangle bounds = new Rectangle(minX, minY, maxX - minX + 1, maxY - minY + 1);
            return bounds;
        }





        //public void test()
        //{
        //    try
        //    {
        //        // Load the satellite images dataset and labels
        //        var bitmap1 = new Bitmap(image1.OpenReadStream());
        //        var bitmap2 = new Bitmap(image2.OpenReadStream());
        //        var bitmapLabels = new Bitmap(labels.OpenReadStream());

        //        // Convert the images to grayscale
        //        var gray1 = Grayscale.CommonAlgorithms.BT709.Apply(bitmap1);


        //        var gray2 = Grayscale.CommonAlgorithms.BT709.Apply(bitmap2);

        //        // Compute the difference between the two images
        //        var diff = new Difference(gray1, gray2).Apply();

        //        // Extract features from the difference image
        //        var hog = new HistogramsOfOrientedGradients()
        //        {
        //            CellSize = new Size(16, 16),
        //            BlockSize = new Size(2, 2),
        //            BlockStride = new Size(1, 1),
        //            GradientSize = 1,
        //            UseSignedOrientation = false
        //        };
        //        var features = hog.ProcessImage(diff);

        //        // Convert the labels to a 1D array
        //        var labelsArray = new int[bitmapLabels.Width * bitmapLabels.Height];
        //        for (var y = 0; y < bitmapLabels.Height; y++)
        //        {
        //            for (var x = 0; x < bitmapLabels.Width; x++)
        //            {
        //                var color = bitmapLabels.GetPixel(x, y);
        //                labelsArray[y * bitmapLabels.Width + x] = color.R == 0 ? 0 : 1;
        //            }
        //        }

        //        // Split the dataset into training and testing sets
        //        var splitter = new Splitter(0.8);
        //        var split = splitter.Split(features, labelsArray);

        //        // Create a kernel support vector machine
        //        var kernel = new Linear();
        //        var svm = new KernelSupportVectorMachine(kernel, 1);

        //        // Train the SVM on the training data
        //        var teacher = new SequentialMinimalOptimization(svm, split.TrainingInputs, split.TrainingOutputs);
        //        teacher.Run();

        //        // Predict the labels for the testing data
        //        var predicted = new int[split.TestingInputs.Length];
        //        for (var i = 0; i < split.TestingInputs.Length; i++)
        //        {
        //            predicted[i] = svm.Compute(split.TestingInputs[i]) > 0 ? 1 : 0;
        //        }

        //        // Compute the accuracy of the classification
        //        var accuracy = new Accuracy(split.TestingOutputs, predicted).Accuracy;

        //        // Return the accuracy
        //        return Ok(accuracy);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Return a bad request with the error message
        //        return BadRequest(ex.Message);
        //    }
        //}

        #endregion


        public async Task<JsonResult> GetGCPList(int? VillageId)
        {
            return Json(await _GISService.GetGCPList(VillageId ?? 0));
        }

        public async Task<IActionResult> GetKhasraNoForExport(int? villageId)
        {
            var data = await _GISService.GetKhasraListforExport(villageId ?? 0);
            var memory = ExcelHelper.CreateExcel(data);
            HttpContext.Session.Set("exportfile", memory);
            return Json("Success");

        }
        [HttpGet]
        public virtual IActionResult download()
        {
            byte[] data = HttpContext.Session.Get("exportfile") as byte[];
            HttpContext.Session.Remove("exportfile");
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "KhasraDetails.xlsx");

        }
    }
}
