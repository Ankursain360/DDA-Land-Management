using System;
using System.Threading.Tasks;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using System.Web;
using DamagePayee.Models;
using System.Net;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Utility.Helper;
using AutoMapper.Configuration;
using System.Text.Encodings.Web;
using CCA.Util;
using System.Linq;
using Unidecode.NET;

namespace DamagePayee.Controllers
{
    public class DamagePayeeRegistrationController : BaseController

    {

        //private readonly IDataProtector protector;
        static string result = string.Empty;
        private readonly IHostingEnvironment _hostingEnvironment;
        public Microsoft.Extensions.Configuration.IConfiguration _configuration;
        //public DamagePayeeRegistrationController(lmsContext context, IHostingEnvironment en)
        //{
        //    _context = context;
        //    _hostingEnvironment = en;
        //}
        // static string result = string.Empty;

        private readonly IDamagePayeeRegistrationService _damagePayeeRegistrationService;

        public DamagePayeeRegistrationController(Microsoft.Extensions.Configuration.IConfiguration configuration,IDamagePayeeRegistrationService damagePayeeRegistrationService, IHostingEnvironment en)
        {
            _damagePayeeRegistrationService = damagePayeeRegistrationService;
            _configuration = configuration;
            _hostingEnvironment = en;
        }

        public IActionResult Index()
        {
            //var model = _damagePayeeRegistrationService.GetAllPayeeregistration()
            //             .Select(e =>
            //             {
            //                  // Encrypt the ID value and store in EncryptedId property
            //                  e.EncryptedId = protector.Protect(e.Id.ToString());
            //                 return e;
            //             });
            ////return await _damagePayeeRegistrationService.GetAllPayeeregistration();
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


            try
            {

                if (ModelState.IsValid)
                {
                    var AesKey = _configuration.GetSection("EncryptionKey").Value.ToString();
                    payeeregistration.IsVerified = "F";
                    var result = await _damagePayeeRegistrationService.Create(payeeregistration);

                    if (result == true)
                    {
                        EncryptionHelper encryptionHelper = new EncryptionHelper();
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);

                        var list = await _damagePayeeRegistrationService.GetAllPayeeregistration();
                        //At successfull completion send mail and sms
                        string DisplayName = payeeregistration.Name.ToString();
                        string EmailID = payeeregistration.EmailId.ToString();
                        string Id = payeeregistration.Id.ToString().Unidecode();
                        string LoginName = payeeregistration.Name.ToString();


                        string contactno = payeeregistration.MobileNumber.ToString();
                        Uri uri = new Uri("http://localhost:1011/DamagePayeeRegistration");
                        string Action = "Dear " + LoginName + ",  You are succesfully registered with DDA Portal. For verify your email click  below link :-  " + uri;
                        var aburl = Request.GetDisplayUrl().Replace("Create", "RegistrationConfirmed");
                        string url = "https://www.google.com/search?q=yahoo+mail&rlz=1C1CHBF_enIN923IN923&oq=&aqs=chrome.1.69i59i450l5.24765349j0j15&sourceid=chrome&ie=UTF-8#=" + contactno + "&message= " + Action + " Thank you .&priority=11";
                        
                        string link = "<a target=\"_blank\" href=\"" + aburl + "?"+ encryptionHelper.EncryptString(AesKey, "EmailID") + "=" + encryptionHelper.EncryptString(AesKey, EmailID) + "&"+ encryptionHelper.EncryptString(AesKey, "Id") + "=" + encryptionHelper.EncryptString(AesKey, Id) + "\">Click Here</a>";
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
                        //return RedirectToAction("Create", "DamagePayeeRegistration");

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
       [HttpGet]
        public async Task<IActionResult> RegistrationConfirmed()
        {
            EncryptionHelper encryptionHelper=new EncryptionHelper();
            var AesKey = _configuration.GetSection("EncryptionKey").Value.ToString();
            Payeeregistration payeeregistration = new Payeeregistration();
            var EncryptedEmailId = HttpContext.Request.Query[encryptionHelper.EncryptString(AesKey,"EmailID")];
            var EncryptedId = HttpContext.Request.Query[encryptionHelper.EncryptString(AesKey, "Id")];
            var UniqueId = Convert.ToInt32(encryptionHelper.DecryptString(AesKey,Convert.ToString(EncryptedId)).Replace(" ","+"));
            var EmailId = encryptionHelper.DecryptString(AesKey, EncryptedEmailId).Replace(" ", "+");
            var id = Request.QueryString.Value;
            if (id == null)
            {
                id = "0";
            }
            if (ModelState.IsValid)
            {
                payeeregistration.IsVerified = "T";
                var result = await _damagePayeeRegistrationService.FetchSingleResult(UniqueId);
                if (result != null)
                {
                    payeeregistration.Id = UniqueId;
                    var result1 = await _damagePayeeRegistrationService.UpdateVerification(payeeregistration);
                    if (result1 == true)
                        ViewBag.Message = Alert.Show(Messages.RegistrationConfirm, "", AlertType.Success);

                    //var list = await _damagePayeeRegistrationService.GetAllPayeeregistration();
                    // string decryptedId = protector.Unprotect(id);
                    // int decryptedIntId = Convert.ToInt32(decryptedId);

                    //var result2 = _damagePayeeRegistrationService.GetAllPayeeregistration(decryptedIntId);
                    //return View("Index", list);
                    ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                    // var result1 = await _damagepayeeregisterService.GetAllDamagepayeeregisterTemp();
                    return View("RegistrationConfirmed");
                }
                else
                {
                    ViewBag.Message = Alert.Show("User Not Found", "", AlertType.Error);
                    return View("RegistrationConfirmed");
                }


            }

            return View();


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
        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> ExistName(int Id, string Name)
        {
            var result = await _damagePayeeRegistrationService.CheckUniqueName(Id, Name);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Name: {Name} already exist");
            }
        }
        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> Existemail(int Id, string EmailId)
        {
            var result = await _damagePayeeRegistrationService.CheckUniqueemail(Id, EmailId);
            if (result == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email Id : {EmailId} already exist");
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
