using Dto.Master;
using Microsoft.AspNetCore.Mvc;
using Service.IApplicationService;
using AcquiredLandInformationManagement.Helper;
using AcquiredLandInformationManagement.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Libraries.Service.IApplicationService;
using System;
using Microsoft.Extensions.Configuration;
using NPOI.HPSF;
using System.IO;
using System.Collections.Generic;
using Libraries.Service.Common;
using Utility.Helper;
using Core.Enum;
using AcquiredLandInformationManagement.Filters;

namespace AcquiredLandInformationManagement.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ISiteContext _siteContext;
        private readonly IUserProfileService _userProfileService;
        private readonly IApplicationModificationDetailsService _modificationDetails;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(ISiteContext siteContext,
           IUserProfileService userProfileService, IHttpContextAccessor httpContextAccessor, IApplicationModificationDetailsService modificationDetails, IConfiguration configuration)
        {
            _siteContext = siteContext;
            _userProfileService = userProfileService;
            _httpContextAccessor = httpContextAccessor;
            _modificationDetails = modificationDetails;
            _configuration = configuration;
        }
        public void updateDateFun()
        {
            var updatedDate = _modificationDetails.GetApplicationModificationDetails();
            var dt = Convert.ToDateTime(updatedDate).ToString("dd/MMM/yyyy HH:MM:ss tt");
            if (updatedDate != null)
            {
                HttpContext.Session.SetString("LastUpdatedDate", dt);
                //TempData["updatedDate"] = dt;
               
            }
            else
            {
                TempData["updatedDate"] = "No Data Available";
               
            }
            
        }
        public async Task<IActionResult> Index()
        {
            UserProfileDto user = await _userProfileService.GetUserById(_siteContext.UserId);

            updateDateFun();
            
            return View(user);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Logout()
        {
            _httpContextAccessor.HttpContext.Response.Clear();
            //Clear cookies
            var cookies = _httpContextAccessor.HttpContext.Request.Cookies;
            foreach (var cookie in cookies)
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Delete(cookie.Key);
            }
            return SignOut("Cookies", "oidc");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult UnAuthorized()
        {
            return View();
        }

        public IActionResult ExceptionLog()
        {
            return View();
        }
        public IActionResult copyRight()
        {
            updateDateFun();
            return View();
        }
        public IActionResult PrivacyPolicy() 
        {
            updateDateFun();
            return View(); 
        }
        public IActionResult HyperlinkPolicy()
        {
            updateDateFun();
            return View();
        }
        public IActionResult WebInformationManager()
        {
            updateDateFun();
            return View();
        }
        public IActionResult TermsandConditions()
        {
            updateDateFun();
            return View();
        } 

        public IActionResult Help()
        {
            updateDateFun();
            return View();
        }
        public IActionResult Contactus()
        {
            updateDateFun();
            return View();
        }
        public IActionResult AboutUs()
        {
            updateDateFun();
            return View();
        }
        public IActionResult Disclaimer()
        {
            updateDateFun();
            return View();
        }
        public IActionResult ContingencyManagementPlan()
        {
            updateDateFun();
            return View();
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

        [AuthorizeContext(ViewAction.View)]
        public IActionResult Usermanual()
        {
            //string path = _configuration.GetSection("FilePaths:Docs:UsermanualPath").Value.ToString();
            //string filename = "Acquiredlandinformation.pdf";
            //var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + @"\docs", filename);
            //var memory = new MemoryStream();
            //using (var stream = new FileStream(path, FileMode.Open))
            //{
            //    stream.CopyToAsync(memory);
            //}
            //memory.Position = 0;
            //return File(memory, GetContentType(path), Path.GetFileName(path));

            FileHelper file = new FileHelper();
            string FilePath = _configuration.GetSection("FilePaths:Docs:UsermanualPath").Value.ToString();
            string path = FilePath;
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, file.GetContentType(path));
        }
    }
}
