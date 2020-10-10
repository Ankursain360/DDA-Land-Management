using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.Identity.UI.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
//using BotDetect.Web.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace DocumentManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        //private readonly lmsContext _context;
        //static string Result = string.Empty;
        //private readonly IHostingEnvironment _hostingEnvironment;

        //public LoginController(lmsContext context, IHostingEnvironment en)
        //{
        //    _context = context;
        //    _hostingEnvironment = en;
        //}
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }

        //[HttpPost]
        //[CaptchaValidationActionFilter("CaptchaCode", "ExampleCaptcha", "Wrong Captcha!")]
        //public async Task<IActionResult> ForgetPassword(TblMasterUserInfo model)
        //{
        //    MvcCaptcha mvcCaptcha = new MvcCaptcha("ExampleCaptcha");
        //    string userInput = HttpContext.Request.Form["CaptchaCode"];
        //    string validatingInstanceId = HttpContext.Request.Form[mvcCaptcha.CurrentInstanceId];

        //    if (mvcCaptcha.Validate(userInput))
        //    {
        //        MvcCaptcha.ResetCaptcha("ExampleCaptcha");
        //        if (ModelState.IsValid)
        //        {
        //            var result = await (_context.TblMasterUserInfo.Where(r => r.LoginName == model.LoginName).ToListAsync());

        //            if (result.Count() == 0)
        //            {
        //                ViewData["Msg"] = new Message { Msg = "Dear User,<br/> Username is Not Exists. Please try again with correct login details.", Status = "E", BackPageAction = "ForgetPassword", BackPageController = "Login" };
        //                return RedirectToAction("ForgetPassword", "Login");
        //            }
        //            else
        //            {


        //                //At successfull completion send mail and sms
        //                string DisplayName = result[0].DisplayName.ToString();
        //                string LoginName = result[0].LoginName.ToString();
        //                string EmailID = result[0].Emailid.ToString();
        //                string Password = result[0].Password.ToString();
        //                string contactno = result[0].ContactNo.ToString();
        //                Uri uri = new Uri("http://49.50.87.108:8181/Login");
        //                string Action = "Dear User,  Your user name  is  " + LoginName + " and Password is " + Password + " for login into DDA Portal click  here to login:-  " + uri ;
        //                //string url = "http://sms.justclicksky.com/pushsms.php?username=GNIDA&api_password=242f38iadg4voobux&sender=VCSSMS&to=" + contactno + "&message=Dear User, Thank you . Your user name  is  " + LoginName + " and Password is " + Password + " for login into DDA Portal click  here to login:-  http://49.50.87.108:8181/Login   .&priority=11";
        //                string url = "http://sms.justclicksky.com/pushsms.php?username=GNIDA&api_password=242f38iadg4voobux&sender=VCSSMS&to=" + contactno + "&message= "+  Action  + " Thank you .&priority=11";
        //                // Using WebRequest
        //                WebRequest request = WebRequest.Create(url);
        //                WebResponse response = request.GetResponse();
        //                string Result = new StreamReader(response.GetResponseStream()).ReadToEnd();
        //                // Using WebClien
        //                string Res1 = new WebClient().DownloadString(url);
        //                string path = Path.Combine(Path.Combine(_hostingEnvironment.WebRootPath, "VirtualDetails"), "MailDetails.html");


        //                GenerateMailOTP mail = new GenerateMailOTP();


        //                mail.GenerateMailFormatForPassword(DisplayName, EmailID, LoginName, Password, path,Action);

        //                ViewData["Msg"] = new Message { Msg = "Dear User,<br/>" + LoginName + " Login Details send on your Registered email and Mobile No, Please check and Login with details", Status = "E", BackPageAction = "ForgetPassword", BackPageController = "Login" };
        //                return RedirectToAction("Create", "Login");
        //            }
        //        }
        //        else
        //        {

        //            //  ModelState.AddModelError("Model", "Incorrect!");
        //            ViewData["Msg"] = new Models.Message { Msg = "Dear User,<br/> Invalid Data  !", Status = "E", BackPageAction = "ForgetPassword", BackPageController = "Login" };

        //            // return RedirectToAction("ForgetPassword", "Login");
        //        }
        //    }
        //    else
        //    {
               
        //        ViewData["Msg"] = new Models.Message { Msg = "Dear User,<br/> Invalid CAPTCHA  !", Status = "E", BackPageAction = "ForgetPassword", BackPageController = "Login" };

        //        // return RedirectToAction("ForgetPassword", "Login");
        //    }
        //    return View();
        //}


    }
}
