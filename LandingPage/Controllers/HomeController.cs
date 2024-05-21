using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LandingPage.Models;
using Libraries.Service.IApplicationService;
using LandingPage.Helper;
using Service.IApplicationService;
using Dto.Master;
using Libraries.Model.Entity;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Security.Cryptography;

namespace LandingPage.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class HomeController : BaseController
    {
        private readonly ISiteContext _siteContext;
        private readonly IUserProfileService _userProfileService;
        private readonly IModuleService _moduleService;
        private readonly IModuleCategoryService _modulecategoryService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IApplicationModificationDetailsService _modificationDetails;


        public HomeController(ISiteContext siteContext,
          IUserProfileService userProfileService, IModuleService moduleService, IModuleCategoryService modulecategoryService, IHttpContextAccessor httpContextAccessor, IApplicationModificationDetailsService modificationDetails)
        {
            _siteContext = siteContext;
            _userProfileService = userProfileService;
            _moduleService = moduleService;
            _modulecategoryService = modulecategoryService;
            _httpContextAccessor = httpContextAccessor;
            _modificationDetails = modificationDetails;
        }
        public void updateDateFun()
        {
            var updatedDate = _modificationDetails.GetApplicationModificationDetails();
            var dt = Convert.ToDateTime(updatedDate).ToString("dd/MMM/yyyy HH:MM:ss tt");
            if (updatedDate != null)
            {
                TempData["updatedDate"] = dt;

            }
            else
            {
                TempData["updatedDate"] = "No Data Available";

            }

        }

        #region changes regarding dynamic categorization of landing page based on module category   Renu added by 7 May 2021

        public async Task<IActionResult> Index()
        {
            var moduleCategoryData = await _moduleService.GetModuleCategory();
            var result = await _moduleService.ModuleFromMenuRoleActionMap(SiteContext.RoleId ?? 0);
            List<RoleWiseModuleMappingDto> data = new List<RoleWiseModuleMappingDto>();
            if (moduleCategoryData != null)
            {
                for (int i = 0; i < moduleCategoryData.Count; i++)
                {
                    data.Add(new RoleWiseModuleMappingDto()
                    {
                        ModuleCategoryId = moduleCategoryData[i].Id,
                        ModuleCategoryName = moduleCategoryData[i].CategoryName,
                        ParentId = 0
                    });
                }
            }
            var mdoulecatlisting = GetModuleListing(data, result);
            updateDateFun();
            //#region session value change for security
            //HttpContext.Session.Clear();
            //if (_httpContextAccessor.HttpContext.Request.Cookies["ASP.NET_SessionId"] != null)
            //{
            //    string any = _httpContextAccessor.HttpContext.Request.Cookies["ASP.NET_SessionId"];
            //    HttpContext.Response.Cookies.Append("ASP.NET_SessionId", string.Empty);
            //    var Value = GenerateHashKey();
            //    HttpContext.Response.Cookies.Append("ASP.NET_SessionId", Value);
            //}
            //if (_httpContextAccessor.HttpContext.Request.Cookies[".AspNetCore.Session"] != null)
            //{
            //    string any = _httpContextAccessor.HttpContext.Request.Cookies[".AspNetCore.Session"];
            //    HttpContext.Response.Cookies.Append(".AspNetCore.Session", string.Empty);
            //    var Value = GenerateHashKey();
            //    HttpContext.Response.Cookies.Append(".AspNetCore.Session", Value);
            //}
            //if (_httpContextAccessor.HttpContext.Request.Cookies["AuthToken"] != null)
            //{
            //    HttpContext.Response.Cookies.Append("AuthToken", string.Empty);
            //    CookieOptions options = new CookieOptions();
            //    options.Expires = DateTime.Now.AddDays(-20);
            //}
            //if (_httpContextAccessor.HttpContext.Request.Cookies["__AntiXsrfToken"] != null)
            //{
            //    HttpContext.Response.Cookies.Append("__AntiXsrfToken", string.Empty);
            //}
            //string newSessionID = _httpContextAccessor.HttpContext.Request.Cookies["ASP.NET_SessionId"];
            //string newCoreSessionID = _httpContextAccessor.HttpContext.Request.Cookies[".AspNetCore.Session"];
            //#endregion
            return View(mdoulecatlisting);
        }
        private string GenerateHashKey()
        {
            StringBuilder myStr = new StringBuilder();
            myStr.Append(Request.Headers["User-Agent"].ToString());
            //myStr.Append(Request.Browser.Platform);
            //myStr.Append(Request.Browser.MajorVersion);
            //myStr.Append(Request.Browser.MinorVersion);
            //myStr.Append(Request.LogonUserIdentity.User.Value);
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] hashdata = sha.ComputeHash(Encoding.UTF8.GetBytes(myStr.ToString()));
            return Convert.ToBase64String(hashdata);
        }

        private IList<RoleWiseModuleMappingDto> GetModuleListing(IList<RoleWiseModuleMappingDto> modulecatList, IList<Menuactionrolemap> result)
        {
            List<RoleWiseModuleMappingDto> vmList = modulecatList.ToList();
            foreach (var item in modulecatList)
            {
                var filteredmodule = result.Where(x => x.Module.ModuleCategoryId == item.ModuleCategoryId)
                                            .OrderBy(x => x.Module.SortBy)
                                            .ToList();
                foreach (var mdoulelist in filteredmodule)
                {
                    var vm = new RoleWiseModuleMappingDto
                    {
                        Id = mdoulelist.ModuleId ?? 0,
                        Url = mdoulelist.Module == null ? "" : mdoulelist.Module.Url,
                        Target = mdoulelist.Module == null ? "" : mdoulelist.Module.Target,
                        Icon = mdoulelist.Module == null ? "" : mdoulelist.Module.Icon,
                        Name = mdoulelist.Module == null ? "" : mdoulelist.Module.Name,
                        Description = mdoulelist.Module == null ? "" : mdoulelist.Module.Description,
                        ParentId = mdoulelist.Module.ModuleCategoryId ?? 0
                    };
                    vmList.Add(vm);
                }
            }
            return vmList;
        }
        #endregion
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Logout()
        {
            
            _httpContextAccessor.HttpContext.Response.Clear();
            //Clear cookies
            var cookies = _httpContextAccessor.HttpContext.Request.Cookies;
            foreach (var cookie in cookies)
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Delete(cookie.Key);
            }             
            //
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

        #region  Session Fixation added by Sachin 27 Aug 21
        //public ActionResult SessionFixation()
        //{
        //    string _sessionIPAdress = string.Empty;
        //    string _sessionBrowserInfo = string.Empty;

        //    if (HttpContext.Session != null)
        //    {

        //        string _encryptedString = HttpContext.Session.GetString("encryptedSession").ToString();
        //        byte[] _encodedAsBytes = System.Convert.FromBase64String(_encryptedString);
        //        string _decryptedString = System.Text.ASCIIEncoding.ASCII.GetString(_encodedAsBytes);
        //        char[] _separator = new char[] { '^' };
        //        if (_decryptedString != string.Empty && _decryptedString != "" && _decryptedString != null)
        //        {
        //            string[] _splitStrings = _decryptedString.Split(_separator);
        //            if (_splitStrings.Count() > 0)
        //            {
        //                if (_splitStrings[1].Count() > 0)
        //                {
        //                    string[] _userBrowserInfo = _splitStrings[2].Split('~');
        //                    if (_userBrowserInfo.Count() > 0)
        //                    {
        //                        _sessionBrowserInfo = _userBrowserInfo[0];
        //                        _sessionIPAdress = _userBrowserInfo[1];
        //                    }
        //                }
        //            }
        //        }
        //        string _currentuseripAddress;
        //        if (string.IsNullOrEmpty(Request.ServerVariables["HTTP_X_FORWARD_FOR"]))
        //        {
        //            _currentuseripAddress = Request.ServerVariables["REMOTE_ADDR"];
        //        }
        //        else
        //        {
        //            _currentuseripAddress = Request.ServerVariables["HTTP_X_FORWARD_FOR"].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
        //        }
        //        System.Net.IPAddress result;
        //        if (!System.Net.IPAddress.TryParse(_currentuseripAddress, out result))
        //        {
        //            result = System.Net.IPAddress.None;
        //        }

        //        string _currentBrowserInfo = Request.Browser.Browser + Request.Browser.Version + Request.UserAgent;

        //        if (_sessionIPAdress != "" && _sessionIPAdress != string.Empty)
        //        {
        //            if (_sessionIPAdress != _currentuseripAddress || _sessionBrowserInfo != _currentBrowserInfo)
        //            {
        //                Session.RemoveAll();
        //                Session.Clear();
        //                Session.Abandon();
        //                Response.Cookies["ASP.Net_Session_Id"].Expires = DateTime.Now.AddSeconds(-30);
        //                Response.Cookies.Add(new HttpCookie("ASP.Net_Session_Id", ""));
        //            }
        //            else
        //            {

        //            }

        //        }
        //        else
        //        {
        //            TempData["SaveResultOTP"] = 1;
        //            TempData["SaveUpdateMessageOTP"] = "Invalid User....Please Try Again";
        //            return (RedirectToAction("Index", "Login", new { UserValid = "" }));
        //        }
        //        return View();
        //    }
        //    else
        //    {
        //        TempData["SaveResultOTP"] = 1;
        //        TempData["SaveUpdateMessageOTP"] = "Invalid User....Please Try Again";
        //        return (RedirectToAction("Index", "Login", new { UserValid = "" }));
        //    }

        //}
        #endregion Session Fixation added by Sachin 27 Aug 21

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
    }
}
