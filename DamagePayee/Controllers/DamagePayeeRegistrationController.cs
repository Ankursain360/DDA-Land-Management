using System;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
//using Microsoft.AspNetCore.Identity.UI.Pages.Account.Internal;
using DamagePayee.Models;
using System.Net;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace DamagePayee.Controllers
{
    public class DamagePayeeRegistrationController : BaseController

    {
        //private readonly lmsContext _context;
        static string result = string.Empty;
        private readonly IHostingEnvironment _hostingEnvironment;

        //public DamagePayeeRegistrationController(lmsContext context, IHostingEnvironment en)
        //{
        //    _context = context;
        //    _hostingEnvironment = en;
        //}
        // static string result = string.Empty;

        private readonly IDamagePayeeRegistrationService _damagePayeeRegistrationService;

        public DamagePayeeRegistrationController(IDamagePayeeRegistrationService damagePayeeRegistrationService, IHostingEnvironment en)
        {
            _damagePayeeRegistrationService = damagePayeeRegistrationService;
           
            _hostingEnvironment = en;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DamagePayeeRegistrationSearchDto model)
        {
            var result = await _damagePayeeRegistrationService.GetPagedDamagePayeeRegistration(model);
            return PartialView("_List", result);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Payeeregistration payeeregistration)
        {
            ////At successfull completion send mail and sms
            //string DisplayName = payeeregistration.Name.ToString();
            //string EmailID = payeeregistration.EmailId.ToString();
            //string LoginName = payeeregistration.Name.ToString();
            ////string LoginName = result[0].Name.ToString();
            //// string EmailID = result[0].EmailId.ToString();
            //// string Password = result[0].".ToString();
            //string contactno = payeeregistration.MobileNumber.ToString();
            //Uri uri = new Uri("http://49.50.87.108:8181/Login");
            //string Action = "Dear " + LoginName + ",  You are succesfully registered with DDA Portal. For verify your email click  below link :-  " + uri;

            //string url = "https://www.google.com/search?q=yahoo+mail&rlz=1C1CHBF_enIN923IN923&oq=&aqs=chrome.1.69i59i450l5.24765349j0j15&sourceid=chrome&ie=UTF-8#=" + contactno + "&message= " + Action + " Thank you .&priority=11";
            //string link = "https://meet.google.com/qca-ysgd-nqh";
            //// Using WebRequest
            //WebRequest request = WebRequest.Create(url);
            //WebResponse response = request.GetResponse();
            //string Result = new StreamReader(response.GetResponseStream()).ReadToEnd();
            //// Using WebClien
            //string Res1 = new WebClient().DownloadString(url);
            //string path = Path.Combine(Path.Combine(_hostingEnvironment.WebRootPath, "VirtualDetails"), "MailDetails.html");


            //GenerateMailOTP mail = new GenerateMailOTP();


            //mail.GenerateMailFormatForPassword(DisplayName, EmailID, LoginName, link,  path, Action);

            //ViewData["Msg"] = new Message { Msg = "Dear User,<br/>" + LoginName + " Login Details send on your Registered email and Mobile No, Please check and Login with details", Status = "E", BackPageAction = "ForgetPassword", BackPageController = "Login" };
            //return RedirectToAction("Create", "Login");



           
            try
            {

                if (ModelState.IsValid)
                {

                    payeeregistration.IsVerified = "F";
                     var result = await _damagePayeeRegistrationService.Create(payeeregistration);
                   // var result = await (_damagePayeeRegistrationService.Where(r => r.LoginName == model.LoginName).ToListAsync());
                    //var test = await payeeregistration.N
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        //return View();
                        var list = await _damagePayeeRegistrationService.GetAllPayeeregistration();
                        //At successfull completion send mail and sms
                        string DisplayName = payeeregistration.Name.ToString();
                        string EmailID = payeeregistration.EmailId.ToString();
                        string LoginName = payeeregistration.Name.ToString();
                        //string LoginName = result[0].Name.ToString();
                        // string EmailID = result[0].EmailId.ToString();
                        // string Password = result[0].".ToString();
                        string contactno = payeeregistration.MobileNumber.ToString();
                        Uri uri = new Uri("http://49.50.87.108:8181/Login");
                        string Action = "Dear " + LoginName + ",  You are succesfully registered with DDA Portal. For verify your email click  below link :-  " + uri;

                        string url = "https://www.google.com/search?q=yahoo+mail&rlz=1C1CHBF_enIN923IN923&oq=&aqs=chrome.1.69i59i450l5.24765349j0j15&sourceid=chrome&ie=UTF-8#=" + contactno + "&message= " + Action + " Thank you .&priority=11";
                        string link = "https://meet.google.com/qca-ysgd-nqh";
                        // Using WebRequest
                        WebRequest request = WebRequest.Create(url);
                        WebResponse response = request.GetResponse();
                        string Result = new StreamReader(response.GetResponseStream()).ReadToEnd();
                        // Using WebClien
                        string Res1 = new WebClient().DownloadString(url);
                        string path = Path.Combine(Path.Combine(_hostingEnvironment.WebRootPath, "VirtualDetails"), "MailDetails.html");


                        GenerateMailOTP mail = new GenerateMailOTP();


                        mail.GenerateMailFormatForPassword(DisplayName, EmailID, LoginName, link, path, Action);

                        ViewData["Msg"] = new Message { Msg = "Dear User,<br/>" + LoginName + " Login Details send on your Registered email and Mobile No, Please check and Login with details", Status = "E", BackPageAction = "ForgetPassword", BackPageController = "Login" };
                        return RedirectToAction("Create", "DamagePayeeRegistration");

                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(payeeregistration);

                    }
                }
                else
                {
                    return View(payeeregistration);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(payeeregistration);
            }

            //return View();



            



        }

        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _damagePayeeRegistrationService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, Payeeregistration payeeregistration)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _damagePayeeRegistrationService.Update(id, payeeregistration);
                    if (result == true)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);

                        var list = await _damagePayeeRegistrationService.GetAllPayeeregistration();
                        return View("Index", list);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(payeeregistration);

                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(payeeregistration);

                }
            }
            return View(payeeregistration);
        }
       

        


        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _damagePayeeRegistrationService.Delete(id);
            if (result == true)
            {
                ViewBag.Message = Alert.Show(Messages.DeleteSuccess, "", AlertType.Success);
                var result1 = await _damagePayeeRegistrationService.GetAllPayeeregistration();
                return View("Index", result1);
            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                var result1 = await _damagePayeeRegistrationService.GetAllPayeeregistration();
                return View("Index", result1);
            }
        }


        public async Task<IActionResult> View(int id)
        {
            var Data = await _damagePayeeRegistrationService.FetchSingleResult(id);
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
    }
}
