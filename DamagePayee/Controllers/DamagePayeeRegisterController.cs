using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dto.Search;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Mvc;
using Utility.Helper;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using System.IO;
using Unidecode.NET;
using System.Net;
using DamagePayee.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Extensions;
using DamagePayee.Filters;
using Core.Enum;
namespace DamagePayee.Controllers
{
    public class DamagePayeeRegisterController : BaseController
    {
        private readonly IDamagepayeeregisterService _damagepayeeregisterService;
        private readonly IHostingEnvironment _hostingEnvironment;
        public IConfiguration _configuration;
        public DamagePayeeRegisterController(IDamagepayeeregisterService damagepayeeregisterService, IConfiguration configuration, IHostingEnvironment en)
        {
            _configuration = configuration;
            _damagepayeeregisterService = damagepayeeregisterService;
            _hostingEnvironment = en;
        }


        [AuthorizeContext(ViewAction.View)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DamagepayeeregistertempSearchDto model)
        {
            var result = await _damagepayeeregisterService.GetPagedDamagepayeeregister(model);
            return PartialView("_List", result);
        }
        async Task BindDropDown(Damagepayeeregister damagepayeeregister)
        {
            damagepayeeregister.LocalityList = await _damagepayeeregisterService.GetLocalityList();
            damagepayeeregister.DistrictList = await _damagepayeeregisterService.GetDistrictList();
        }

        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create()
        {
            Damagepayeeregister damagepayeeregister = new Damagepayeeregister();

            await BindDropDown(damagepayeeregister);
            return View(damagepayeeregister);
        }
        [HttpPost]
        [AuthorizeContext(ViewAction.Add)]
        public async Task<IActionResult> Create(Damagepayeeregister damagepayeeregister)
        {
            await BindDropDown(damagepayeeregister);
            string PhotoFilePathLayout = _configuration.GetSection("FilePaths:DamagePayeeFiles:ATSGPADocument").Value.ToString();
            string RecieptDocumentPathLayout = _configuration.GetSection("FilePaths:DamagePayeeFiles:RecieptDocument").Value.ToString();
            string AadharNoDocument = _configuration.GetSection("FilePaths:DamagePayeeFiles:AadharNoDocument").Value.ToString();
            string PanNoDocument = _configuration.GetSection("FilePaths:DamagePayeeFiles:PanNoDocument").Value.ToString();
            string PhotographPersonelDocument = _configuration.GetSection("FilePaths:DamagePayeeFiles:PhotographPersonelDocument").Value.ToString();
            string SignaturePersonelDocument = _configuration.GetSection("FilePaths:DamagePayeeFiles:SignaturePersonelDocument").Value.ToString();

            string PropertyPhotographLayout = _configuration.GetSection("FilePaths:DamagePayeeFiles:PropertyPhotograph").Value.ToString();
            string ShowCauseNoticeDocument = _configuration.GetSection("FilePaths:DamagePayeeFiles:ShowCauseNotice").Value.ToString();
            string FGFormDocument = _configuration.GetSection("FilePaths:DamagePayeeFiles:FGForm").Value.ToString();
            string BillDocument = _configuration.GetSection("FilePaths:DamagePayeeFiles:Bill").Value.ToString();




            if (ModelState.IsValid)
            {
                FileHelper fileHelper = new FileHelper();

                if (damagepayeeregister.PropertyPhoto != null)
                {
                    damagepayeeregister.PropertyPhotoPath = fileHelper.SaveFile(PropertyPhotographLayout, damagepayeeregister.PropertyPhoto);
                }
                if (damagepayeeregister.ShowCauseNotice != null)
                {
                    damagepayeeregister.ShowCauseNoticePath = fileHelper.SaveFile(ShowCauseNoticeDocument, damagepayeeregister.ShowCauseNotice);
                }
                if (damagepayeeregister.Fgform != null)
                {
                    damagepayeeregister.FgformPath = fileHelper.SaveFile(FGFormDocument, damagepayeeregister.Fgform);
                }
                if (damagepayeeregister.DocumentForFile != null)
                {
                    damagepayeeregister.DocumentForFilePath = fileHelper.SaveFile(BillDocument, damagepayeeregister.DocumentForFile);
                }

                var result = await _damagepayeeregisterService.Create(damagepayeeregister);


                if (result)
                {
                    //******* creating damage payee user ******

                    var resultpassword = await _damagepayeeregisterService.CreateUser(damagepayeeregister);
                    if (!resultpassword.Equals("False"))
                    { 
                        //At successfull completion send mail and sms
                    string DisplayName = damagepayeeregister.payeeName[0].ToString();
                    string EmailID = damagepayeeregister.EmailId[0].ToString();
                    string Id = damagepayeeregister.Id.ToString().Unidecode();
                    string LoginName = damagepayeeregister.payeeName[0].ToString();
                    string ContactNo = damagepayeeregister.MobileNo[0].ToString();
                    string Password = resultpassword;
                    string path = Path.Combine(Path.Combine(_hostingEnvironment.WebRootPath, "VirtualDetails"), "UserMailDetails.html");


                    GenerateMailUser mail = new GenerateMailUser();


                    mail.GenerateMailFormatForUserDetails(DisplayName, EmailID, LoginName, path, Password, ContactNo);
                    }

                    //****** code for saving  Damage payee personal info *****

                    if (damagepayeeregister.payeeName != null &&
                      damagepayeeregister.Gender != null &&
                      damagepayeeregister.Address != null &&
                      damagepayeeregister.MobileNo != null)
                    {
                        if (damagepayeeregister.payeeName.Count > 0 &&
                    damagepayeeregister.Gender.Count > 0 &&
                    damagepayeeregister.Address.Count > 0 &&
                    damagepayeeregister.MobileNo.Count > 0)

                        {
                            List<Damagepayeepersonelinfo> damagepayeepersonelinfo = new List<Damagepayeepersonelinfo>();
                            for (int i = 0; i < damagepayeeregister.payeeName.Count; i++)
                            {
                                damagepayeepersonelinfo.Add(new Damagepayeepersonelinfo
                                {
                                    Name = damagepayeeregister.payeeName.Count <= i ? string.Empty : damagepayeeregister.payeeName[i],
                                    FatherName = damagepayeeregister.payeeFatherName.Count <= i ? string.Empty : damagepayeeregister.payeeFatherName[i],
                                    Gender = damagepayeeregister.Gender.Count <= i ? "1" : damagepayeeregister.Gender[i],
                                    Address = damagepayeeregister.Address.Count <= i ? string.Empty : damagepayeeregister.Address[i],
                                    MobileNo = damagepayeeregister.MobileNo.Count <= i ? string.Empty : damagepayeeregister.MobileNo[i],
                                    EmailId = damagepayeeregister.EmailId.Count <= i ? string.Empty : damagepayeeregister.EmailId[i],
                                    DamagePayeeRegisterTempId = damagepayeeregister.Id,
                                    AadharNo = damagepayeeregister.AadharNo.Count <= i ? string.Empty : damagepayeeregister.AadharNo[i],
                                    PanNo = damagepayeeregister.PanNo.Count <= i ? string.Empty : damagepayeeregister.PanNo[i],

                                    AadharNoFilePath = damagepayeeregister.Aadhar != null ?
                                                                damagepayeeregister.Aadhar.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(AadharNoDocument, damagepayeeregister.Aadhar[i]) :
                                                                damagepayeeregister.AadharNoFilePath[i] != null || damagepayeeregister.AadharNoFilePath[i] != "" ?
                                                                damagepayeeregister.AadharNoFilePath[i] : string.Empty,
                                    PanNoFilePath = damagepayeeregister.Pan != null ?
                                                                damagepayeeregister.Pan.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(PanNoDocument, damagepayeeregister.Pan[i]) :
                                                                damagepayeeregister.PanNoFilePath[i] != null || damagepayeeregister.PanNoFilePath[i] != "" ?
                                                                damagepayeeregister.PanNoFilePath[i] : string.Empty,
                                    PhotographPath = damagepayeeregister.Photograph != null ?
                                                                damagepayeeregister.Photograph.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(PhotographPersonelDocument, damagepayeeregister.Photograph[i]) :
                                                                damagepayeeregister.PhotographFilePath[i] != null || damagepayeeregister.PhotographFilePath[i] != "" ?
                                                                damagepayeeregister.PhotographFilePath[i] : string.Empty,
                                    SignaturePath = damagepayeeregister.SignatureFile != null ?
                                                                damagepayeeregister.SignatureFile.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(SignaturePersonelDocument, damagepayeeregister.SignatureFile[i]) :
                                                                damagepayeeregister.SignatureFilePath[i] != null || damagepayeeregister.SignatureFilePath[i] != "" ?
                                                                damagepayeeregister.SignatureFilePath[i] : string.Empty


                                    });
                            }
                            foreach (var item in damagepayeepersonelinfo)
                            {
                                result = await _damagepayeeregisterService.SavePayeePersonalInfo(item);
                            }
                        }
                    }
                

                    //****** code for saving  Allotte Type *****
                    if (damagepayeeregister.Name != null &&
                     damagepayeeregister.FatherName != null &&
                     damagepayeeregister.Date != null)
                    {
                        if (
                         damagepayeeregister.Name.Count > 0 &&
                         damagepayeeregister.FatherName.Count > 0 &&
                         damagepayeeregister.Date.Count > 0
                         )
                        {
                            List<Allottetype> allottetype = new List<Allottetype>();
                            for (int i = 0; i < damagepayeeregister.Name.Count; i++)
                            {
                                allottetype.Add(new Allottetype
                                {
                                    Name = damagepayeeregister.Name.Count <= i ? string.Empty : damagepayeeregister.Name[i],
                                    FatherName = damagepayeeregister.FatherName.Count <= i ? string.Empty : damagepayeeregister.FatherName[i],
                                    Date = damagepayeeregister.Date.Count <= i ? DateTime.Now : damagepayeeregister.Date[i],
                                    DamagePayeeRegisterTempId = damagepayeeregister.Id,
                                    AtsgpadocumentPath = damagepayeeregister.ATSGPA != null ?
                                                                damagepayeeregister.ATSGPA.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(PhotoFilePathLayout, damagepayeeregister.ATSGPA[i]) :
                                                                damagepayeeregister.ATSGPAFilePath[i] != null || damagepayeeregister.ATSGPAFilePath[i] != "" ?
                                                                damagepayeeregister.ATSGPAFilePath[i] : string.Empty


                                });
                            }
                            result = await _damagepayeeregisterService.SaveAllotteType(allottetype);
                        }
                    }

                    //****** code for saving  Damage payment history *****

                    if (damagepayeeregister.PaymntName != null &&
                          damagepayeeregister.RecieptNo != null &&
                          damagepayeeregister.PaymentMode != null &&
                          damagepayeeregister.PaymentDate != null &&
                          damagepayeeregister.Amount != null)
                    {

                        //damagepayeeregister.Reciept != null &&)
                        if (
                             damagepayeeregister.PaymntName.Count > 0 &&
                             damagepayeeregister.RecieptNo.Count > 0 &&
                             damagepayeeregister.PaymentMode.Count > 0 &&
                             damagepayeeregister.PaymentDate.Count > 0 &&
                             damagepayeeregister.Amount.Count > 0
                             )

                        {
                            List<Damagepaymenthistory> damagepaymenthistory = new List<Damagepaymenthistory>();
                            for (int i = 0; i < damagepayeeregister.payeeName.Count; i++)
                            {
                                damagepaymenthistory.Add(new Damagepaymenthistory
                                {
                                    Name = damagepayeeregister.PaymntName.Count <= i ? string.Empty : damagepayeeregister.PaymntName[i],
                                    RecieptNo = damagepayeeregister.RecieptNo.Count <= i ? string.Empty : damagepayeeregister.RecieptNo[i],
                                    PaymentMode = damagepayeeregister.PaymentMode.Count <= i ? string.Empty : damagepayeeregister.PaymentMode[i],
                                    PaymentDate = damagepayeeregister.PaymentDate.Count <= i ? DateTime.Now : damagepayeeregister.PaymentDate[i],
                                    Amount = damagepayeeregister.Amount.Count <= i ? 0 : damagepayeeregister.Amount[i],
                                    RecieptDocumentPath = damagepayeeregister.Reciept != null ?
                                                                damagepayeeregister.Reciept.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(RecieptDocumentPathLayout, damagepayeeregister.Reciept[i]) :
                                                                damagepayeeregister.RecieptFilePath[i] != null || damagepayeeregister.RecieptFilePath[i] != "" ?
                                                                damagepayeeregister.RecieptFilePath[i] : string.Empty,
                                    DamagePayeeRegisterTempId = damagepayeeregister.Id


                                   
                                  
                                    
                                   
                                   

                                     //RecieptDocumentPath = damagepayeeregistertemp.Reciept[i] == null ? "" : fileHelper.SaveFile(RecieptDocumentPathLayout, damagepayeeregistertemp.Reciept[i]),

                                   
                                });
                            }

                            result = await _damagepayeeregisterService.SavePaymentHistory(damagepaymenthistory);

                        }
                    }
                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                    //return View(damagepayeeregistertemp);
                    var result1 = await _damagepayeeregisterService.GetAllDamagepayeeregister();
                    return View("Index", result1);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(damagepayeeregister);
                }

            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(damagepayeeregister);
            }
        }

        public async Task<JsonResult> GetDetailspersonelinfotemp(int? Id)
        {
            Id = Id ?? 0;
            var data = await _damagepayeeregisterService.GetPersonalInfo(Convert.ToInt32(Id));
            //return Json(data.Select(x => new { x.CountOfStructure, DateOfEncroachment = Convert.ToDateTime(x.DateOfEncroachment).ToString("yyyy-MM-dd"), x.Area, x.NameOfStructure, x.ReferenceNoOnLocation, x.Type, x.ConstructionStatus }));
            return Json(data.Select(x => new {
                x.Id,
                x.Name,
                x.FatherName,
                x.Gender,
                x.Address,
                x.MobileNo,
                x.EmailId,
                x.AadharNoFilePath,
                x.PanNoFilePath,
                x.PhotographPath,
                x.SignaturePath,
                x.AadharNo,
                x.PanNo
            }));
        }
        public async Task<JsonResult> GetDetailsAllottetypetemp(int? Id)
        {
            Id = Id ?? 0;
            var data = await _damagepayeeregisterService.GetAllottetype(Convert.ToInt32(Id));
            return Json(data.Select(x => new {
                x.Id,
                x.Name,
                x.FatherName,
                Date = Convert.ToDateTime(x.Date).ToString("yyyy-MM-dd"),
                x.AtsgpadocumentPath
            }));
        }
        public async Task<JsonResult> GetDetailspaymenthistorytemp(int? Id)
        {
            Id = Id ?? 0;
            var data = await _damagepayeeregisterService.GetPaymentHistory(Convert.ToInt32(Id));
            return Json(data.Select(x => new {
                x.Id,
                x.Name,
                x.RecieptNo,
                x.PaymentMode,
                PaymentDate = Convert.ToDateTime(x.PaymentDate).ToString("yyyy-MM-dd"),
                x.Amount,
                x.RecieptDocumentPath
            }));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var Data = await _damagepayeeregisterService.FetchSingleResult(id);
           await BindDropDown(Data);
           
            if (Data == null)
            {
                return NotFound();
            }
            return View(Data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeContext(ViewAction.Edit)]
        public async Task<IActionResult> Edit(int id, Damagepayeeregister damagepayeeregister)
        {

            await BindDropDown(damagepayeeregister);
            string PhotoFilePathLayout = _configuration.GetSection("FilePaths:DamagePayeeFiles:ATSGPADocument").Value.ToString();
            string RecieptDocumentPathLayout = _configuration.GetSection("FilePaths:DamagePayeeFiles:RecieptDocument").Value.ToString();
            string AadharNoDocument = _configuration.GetSection("FilePaths:DamagePayeeFiles:AadharNoDocument").Value.ToString();
            string PanNoDocument = _configuration.GetSection("FilePaths:DamagePayeeFiles:PanNoDocument").Value.ToString();
            string PhotographPersonelDocument = _configuration.GetSection("FilePaths:DamagePayeeFiles:PhotographPersonelDocument").Value.ToString();
            string SignaturePersonelDocument = _configuration.GetSection("FilePaths:DamagePayeeFiles:SignaturePersonelDocument").Value.ToString();

            string PropertyPhotographLayout = _configuration.GetSection("FilePaths:DamagePayeeFiles:PropertyPhotograph").Value.ToString();
            string ShowCauseNoticeDocument = _configuration.GetSection("FilePaths:DamagePayeeFiles:ShowCauseNotice").Value.ToString();
            string FGFormDocument = _configuration.GetSection("FilePaths:DamagePayeeFiles:FGForm").Value.ToString();
            string BillDocument = _configuration.GetSection("FilePaths:DamagePayeeFiles:Bill").Value.ToString();


            if (ModelState.IsValid)

            {
                FileHelper fileHelper = new FileHelper();

                if (damagepayeeregister.PropertyPhoto != null)
                {
                    damagepayeeregister.PropertyPhotoPath = fileHelper.SaveFile(PropertyPhotographLayout, damagepayeeregister.PropertyPhoto);
                }
                if (damagepayeeregister.ShowCauseNotice != null)
                {
                    damagepayeeregister.ShowCauseNoticePath = fileHelper.SaveFile(ShowCauseNoticeDocument, damagepayeeregister.ShowCauseNotice);
                }
                if (damagepayeeregister.Fgform != null)
                {
                    damagepayeeregister.FgformPath = fileHelper.SaveFile(FGFormDocument, damagepayeeregister.Fgform);
                }
                if (damagepayeeregister.DocumentForFile != null)
                {
                    damagepayeeregister.DocumentForFilePath = fileHelper.SaveFile(BillDocument, damagepayeeregister.DocumentForFile);
                }

                var result = await _damagepayeeregisterService.Update(id, damagepayeeregister);
                if (result)
                {
                    //  FileHelper fileHelper = new FileHelper();

                    //****** code for saving  Damage payee personal info *****

                    if (damagepayeeregister.payeeName != null &&
                      damagepayeeregister.Gender != null &&
                      damagepayeeregister.Address != null &&
                      damagepayeeregister.MobileNo != null)
                    {
                        if (damagepayeeregister.payeeName.Count > 0 &&
                    damagepayeeregister.Gender.Count > 0 &&
                    damagepayeeregister.Address.Count > 0 &&
                    damagepayeeregister.MobileNo.Count > 0)

                        {
                            List<Damagepayeepersonelinfo> damagepayeepersonelinfo = new List<Damagepayeepersonelinfo>();
                            result = await _damagepayeeregisterService.DeletePayeePersonalInfo(id);
                            for (int i = 0; i < damagepayeeregister.payeeName.Count; i++)
                            {
                                damagepayeepersonelinfo.Add(new Damagepayeepersonelinfo
                                {
                                    Name = damagepayeeregister.payeeName.Count <= i ? string.Empty : damagepayeeregister.payeeName[i],
                                    FatherName = damagepayeeregister.payeeFatherName.Count <= i ? string.Empty : damagepayeeregister.payeeFatherName[i],
                                    Gender = damagepayeeregister.Gender.Count <= i ? "1" : damagepayeeregister.Gender[i],
                                    Address = damagepayeeregister.Address.Count <= i ? string.Empty : damagepayeeregister.Address[i],
                                    MobileNo = damagepayeeregister.MobileNo.Count <= i ? string.Empty : damagepayeeregister.MobileNo[i],
                                    EmailId = damagepayeeregister.EmailId.Count <= i ? string.Empty : damagepayeeregister.EmailId[i],
                                    DamagePayeeRegisterTempId = damagepayeeregister.Id,
                                    AadharNo = damagepayeeregister.AadharNo.Count <= i ? string.Empty : damagepayeeregister.AadharNo[i],
                                    PanNo = damagepayeeregister.PanNo.Count <= i ? string.Empty : damagepayeeregister.PanNo[i],

                                    AadharNoFilePath = damagepayeeregister.Aadhar != null ?
                                                                damagepayeeregister.Aadhar.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(AadharNoDocument, damagepayeeregister.Aadhar[i]) :
                                                                damagepayeeregister.AadharNoFilePath[i] != null || damagepayeeregister.AadharNoFilePath[i] != "" ?
                                                                damagepayeeregister.AadharNoFilePath[i] : string.Empty,
                                    PanNoFilePath = damagepayeeregister.Pan != null ?
                                                                damagepayeeregister.Pan.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(PanNoDocument, damagepayeeregister.Pan[i]) :
                                                                damagepayeeregister.PanNoFilePath[i] != null || damagepayeeregister.PanNoFilePath[i] != "" ?
                                                                damagepayeeregister.PanNoFilePath[i] : string.Empty,
                                    PhotographPath = damagepayeeregister.Photograph != null ?
                                                                damagepayeeregister.Photograph.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(PhotographPersonelDocument, damagepayeeregister.Photograph[i]) :
                                                                damagepayeeregister.PhotographFilePath[i] != null || damagepayeeregister.PhotographFilePath[i] != "" ?
                                                                damagepayeeregister.PhotographFilePath[i] : string.Empty,
                                    SignaturePath = damagepayeeregister.SignatureFile != null ?
                                                                damagepayeeregister.SignatureFile.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(SignaturePersonelDocument, damagepayeeregister.SignatureFile[i]) :
                                                                damagepayeeregister.SignatureFilePath[i] != null || damagepayeeregister.SignatureFilePath[i] != "" ?
                                                                damagepayeeregister.SignatureFilePath[i] : string.Empty

                                     });

                              
                            }
                            foreach (var item in damagepayeepersonelinfo)
                            {
                                result = await _damagepayeeregisterService.SavePayeePersonalInfo(item);
                            }
                        }
                    }

                    //****** code for saving  Allotte Type *****
                    if (damagepayeeregister.Name != null &&
                     damagepayeeregister.FatherName != null &&
                     damagepayeeregister.Date != null)
                    {
                        if (
                         damagepayeeregister.Name.Count > 0 &&
                         damagepayeeregister.FatherName.Count > 0 &&
                         damagepayeeregister.Date.Count > 0
                         )
                        {
                           
                            List<Allottetype> allottetype = new List<Allottetype>();
                            result = await _damagepayeeregisterService.DeleteAllotteType(id);
                            for (int i = 0; i < damagepayeeregister.Name.Count; i++)
                            {
                                allottetype.Add(new Allottetype
                                {
                                    Name = damagepayeeregister.Name.Count <= i ? string.Empty : damagepayeeregister.Name[i],
                                    FatherName = damagepayeeregister.FatherName.Count <= i ? string.Empty : damagepayeeregister.FatherName[i],
                                    Date = damagepayeeregister.Date.Count <= i ? DateTime.Now : damagepayeeregister.Date[i],
                                    DamagePayeeRegisterTempId = damagepayeeregister.Id,
                                    AtsgpadocumentPath = damagepayeeregister.ATSGPA != null ?
                                                                damagepayeeregister.ATSGPA.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(PhotoFilePathLayout, damagepayeeregister.ATSGPA[i]) :
                                                                damagepayeeregister.ATSGPAFilePath[i] != null || damagepayeeregister.ATSGPAFilePath[i] != "" ?
                                                                damagepayeeregister.ATSGPAFilePath[i] : string.Empty

                                });
                            }
                            result = await _damagepayeeregisterService.SaveAllotteType(allottetype);
                        }
                    }

                    //****** code for saving  Damage payment history *****

                    if (damagepayeeregister.PaymntName != null &&
                          damagepayeeregister.RecieptNo != null &&
                          damagepayeeregister.PaymentMode != null &&
                          damagepayeeregister.PaymentDate != null &&
                          damagepayeeregister.Amount != null)
                    {

                        //damagepayeeregister.Reciept != null &&)
                        if (
                             damagepayeeregister.PaymntName.Count > 0 &&
                             damagepayeeregister.RecieptNo.Count > 0 &&
                             damagepayeeregister.PaymentMode.Count > 0 &&
                             damagepayeeregister.PaymentDate.Count > 0 &&
                             damagepayeeregister.Amount.Count > 0
                             )

                        {
                            List<Damagepaymenthistory> damagepaymenthistory = new List<Damagepaymenthistory>();
                            result = await _damagepayeeregisterService.DeletePaymentHistory(id);
                            for (int i = 0; i < damagepayeeregister.payeeName.Count; i++)
                            {
                                damagepaymenthistory.Add(new Damagepaymenthistory
                                {
                                    Name = damagepayeeregister.PaymntName.Count <= i ? string.Empty : damagepayeeregister.PaymntName[i],
                                    RecieptNo = damagepayeeregister.RecieptNo.Count <= i ? string.Empty : damagepayeeregister.RecieptNo[i],
                                    PaymentMode = damagepayeeregister.PaymentMode.Count <= i ? string.Empty : damagepayeeregister.PaymentMode[i],
                                    PaymentDate = damagepayeeregister.PaymentDate.Count <= i ? DateTime.Now : damagepayeeregister.PaymentDate[i],
                                    Amount = damagepayeeregister.Amount.Count <= i ? 0 : damagepayeeregister.Amount[i],
                                    RecieptDocumentPath = damagepayeeregister.Reciept != null ?
                                                                damagepayeeregister.Reciept.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(RecieptDocumentPathLayout, damagepayeeregister.Reciept[i]) :
                                                                damagepayeeregister.RecieptFilePath[i] != null || damagepayeeregister.RecieptFilePath[i] != "" ?
                                                                damagepayeeregister.RecieptFilePath[i] : string.Empty,
                                    DamagePayeeRegisterTempId = damagepayeeregister.Id

                  //  RecieptDocumentPath = damagepayeeregistertemp.Reciept[i] == null ? "" : fileHelper.SaveFile(RecieptDocumentPathLayout, damagepayeeregistertemp.Reciept[i]),

                                   
                                });
                            }

                            result = await _damagepayeeregisterService.SavePaymentHistory(damagepaymenthistory);

                        }
                    }
                    if (result)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var result1 = await _damagepayeeregisterService.GetAllDamagepayeeregister();
                        return View("Index", result1);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(damagepayeeregister);
                    }
                
            }

            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(damagepayeeregister);
            }

        } 

          
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(damagepayeeregister);
            }
           
        }
        //******************  download files **************************
        public async Task<IActionResult> DownloadPropertyPhoto(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregister Data = await _damagepayeeregisterService.FetchSingleResult(Id);
            string filename = Data.PropertyPhotoPath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadShowCauseNotice(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregister Data = await _damagepayeeregisterService.FetchSingleResult(Id);
            string filename = Data.ShowCauseNoticePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadFgform(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregister Data = await _damagepayeeregisterService.FetchSingleResult(Id);
            string filename = Data.FgformPath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadBillfile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregister Data = await _damagepayeeregisterService.FetchSingleResult(Id);
            string filename = Data.DocumentForFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }


        //**********************  download repeater files****************************
        public async Task<FileResult> ViewPersonelInfoAadharFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfo Data = await _damagepayeeregisterService.GetPersonelInfoFilePath(Id);
            string path = Data.AadharNoFilePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<FileResult> ViewPersonelInfoPanFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfo Data = await _damagepayeeregisterService.GetPersonelInfoFilePath(Id);
            string path = Data.PanNoFilePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<FileResult> ViewPersonelInfoPhotoFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfo Data = await _damagepayeeregisterService.GetPersonelInfoFilePath(Id);
            string path = Data.PhotographPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<FileResult> ViewPersonelInfoSignautreFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfo Data = await _damagepayeeregisterService.GetPersonelInfoFilePath(Id);
            string path = Data.SignaturePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }

        
        public async Task<FileResult> ViewATSFile(int Id)
        {
            FileHelper file = new FileHelper();
            Allottetype Data = await _damagepayeeregisterService.GetATSFilePath(Id);
            string path = Data.AtsgpadocumentPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }

        public async Task<FileResult> ViewReceiptFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepaymenthistory Data = await _damagepayeeregisterService.GetReceiptFilePath(Id);
            string path = Data.RecieptDocumentPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }

    }
}
