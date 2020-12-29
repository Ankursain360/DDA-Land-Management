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

namespace DamagePayee.Controllers
{
    public class DamagePayeeRegisterController : BaseController
    {
        private readonly IDamagepayeeregisterService _damagepayeeregisterService;
        public IConfiguration _configuration;
        public DamagePayeeRegisterController(IDamagepayeeregisterService damagepayeeregisterService, IConfiguration configuration)
        {
            _configuration = configuration;
            _damagepayeeregisterService = damagepayeeregisterService;
        }
        public IActionResult Index()
        {
            return View();
        }

       

        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DamagepayeeregistertempSearchDto model)
        {
            var result = await _damagepayeeregisterService.GetPagedDamagepayeeregistertemp(model);
            return PartialView("_List", result);
        }
        async Task BindDropDown(Damagepayeeregistertemp damagepayeeregistertemp)
        {
            damagepayeeregistertemp.LocalityList = await _damagepayeeregisterService.GetLocalityList();
            damagepayeeregistertemp.DistrictList = await _damagepayeeregisterService.GetDistrictList();
        }
        public async Task<IActionResult> Create()
        {
            Damagepayeeregistertemp damagepayeeregistertemp = new Damagepayeeregistertemp();

            await BindDropDown(damagepayeeregistertemp);
            return View(damagepayeeregistertemp);
        }
        [HttpPost]

        public async Task<IActionResult> Create(Damagepayeeregistertemp damagepayeeregistertemp)
        {
            await BindDropDown(damagepayeeregistertemp);
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

                if (damagepayeeregistertemp.PropertyPhoto != null)
                {
                    damagepayeeregistertemp.PropertyPhotoPath = fileHelper.SaveFile(PropertyPhotographLayout, damagepayeeregistertemp.PropertyPhoto);
                }
                if (damagepayeeregistertemp.ShowCauseNotice != null)
                {
                    damagepayeeregistertemp.ShowCauseNoticePath = fileHelper.SaveFile(ShowCauseNoticeDocument, damagepayeeregistertemp.ShowCauseNotice);
                }
                if (damagepayeeregistertemp.Fgform != null)
                {
                    damagepayeeregistertemp.FgformPath = fileHelper.SaveFile(FGFormDocument, damagepayeeregistertemp.Fgform);
                }
                if (damagepayeeregistertemp.DocumentForFile != null)
                {
                    damagepayeeregistertemp.DocumentForFilePath = fileHelper.SaveFile(BillDocument, damagepayeeregistertemp.DocumentForFile);
                }

                var result = await _damagepayeeregisterService.Create(damagepayeeregistertemp);
                if (result)
                {
                   

                    //****** code for saving  Damage payee personal info *****

                    if (damagepayeeregistertemp.payeeName != null &&
                      damagepayeeregistertemp.Gender != null &&
                      damagepayeeregistertemp.Address != null &&
                      damagepayeeregistertemp.MobileNo != null)
                    {
                        if (damagepayeeregistertemp.payeeName.Count > 0 &&
                    damagepayeeregistertemp.Gender.Count > 0 &&
                    damagepayeeregistertemp.Address.Count > 0 &&
                    damagepayeeregistertemp.MobileNo.Count > 0)

                        {
                            List<Damagepayeepersonelinfotemp> damagepayeepersonelinfotemp = new List<Damagepayeepersonelinfotemp>();
                            for (int i = 0; i < damagepayeeregistertemp.payeeName.Count; i++)
                            {
                                damagepayeepersonelinfotemp.Add(new Damagepayeepersonelinfotemp
                                {
                                    Name = damagepayeeregistertemp.payeeName.Count <= i ? string.Empty : damagepayeeregistertemp.payeeName[i],
                                    FatherName = damagepayeeregistertemp.payeeFatherName.Count <= i ? string.Empty : damagepayeeregistertemp.payeeFatherName[i],
                                    Gender = damagepayeeregistertemp.Gender.Count <= i ? "1" : damagepayeeregistertemp.Gender[i],
                                    Address = damagepayeeregistertemp.Address.Count <= i ? string.Empty : damagepayeeregistertemp.Address[i],
                                    MobileNo = damagepayeeregistertemp.MobileNo.Count <= i ? string.Empty : damagepayeeregistertemp.MobileNo[i],
                                    EmailId = damagepayeeregistertemp.EmailId.Count <= i ? string.Empty : damagepayeeregistertemp.EmailId[i],
                                    DamagePayeeRegisterTempId = damagepayeeregistertemp.Id,
                                    AadharNo = damagepayeeregistertemp.AadharNo.Count <= i ? string.Empty : damagepayeeregistertemp.AadharNo[i],
                                    PanNo = damagepayeeregistertemp.PanNo.Count <= i ? string.Empty : damagepayeeregistertemp.PanNo[i],

                                    AadharNoFilePath = damagepayeeregistertemp.Aadhar != null ?
                                                                damagepayeeregistertemp.Aadhar.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(AadharNoDocument, damagepayeeregistertemp.Aadhar[i]) :
                                                                damagepayeeregistertemp.AadharNoFilePath[i] != null || damagepayeeregistertemp.AadharNoFilePath[i] != "" ?
                                                                damagepayeeregistertemp.AadharNoFilePath[i] : string.Empty,
                                    PanNoFilePath = damagepayeeregistertemp.Pan != null ?
                                                                damagepayeeregistertemp.Pan.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(PanNoDocument, damagepayeeregistertemp.Pan[i]) :
                                                                damagepayeeregistertemp.PanNoFilePath[i] != null || damagepayeeregistertemp.PanNoFilePath[i] != "" ?
                                                                damagepayeeregistertemp.PanNoFilePath[i] : string.Empty,
                                    PhotographPath = damagepayeeregistertemp.Photograph != null ?
                                                                damagepayeeregistertemp.Photograph.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(PhotographPersonelDocument, damagepayeeregistertemp.Photograph[i]) :
                                                                damagepayeeregistertemp.PhotographFilePath[i] != null || damagepayeeregistertemp.PhotographFilePath[i] != "" ?
                                                                damagepayeeregistertemp.PhotographFilePath[i] : string.Empty,
                                    SignaturePath = damagepayeeregistertemp.SignatureFile != null ?
                                                                damagepayeeregistertemp.SignatureFile.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(SignaturePersonelDocument, damagepayeeregistertemp.SignatureFile[i]) :
                                                                damagepayeeregistertemp.SignatureFilePath[i] != null || damagepayeeregistertemp.SignatureFilePath[i] != "" ?
                                                                damagepayeeregistertemp.SignatureFilePath[i] : string.Empty


                                    });
                            }
                            foreach (var item in damagepayeepersonelinfotemp)
                            {
                                result = await _damagepayeeregisterService.SavePayeePersonalInfoTemp(item);
                            }
                        }
                    }

                    //****** code for saving  Allotte Type *****
                    if (damagepayeeregistertemp.Name != null &&
                     damagepayeeregistertemp.FatherName != null &&
                     damagepayeeregistertemp.Date != null)
                    {
                        if (
                         damagepayeeregistertemp.Name.Count > 0 &&
                         damagepayeeregistertemp.FatherName.Count > 0 &&
                         damagepayeeregistertemp.Date.Count > 0
                         )
                        {
                            List<Allottetypetemp> allottetypetemp = new List<Allottetypetemp>();
                            for (int i = 0; i < damagepayeeregistertemp.Name.Count; i++)
                            {
                                allottetypetemp.Add(new Allottetypetemp
                                {
                                    Name = damagepayeeregistertemp.Name.Count <= i ? string.Empty : damagepayeeregistertemp.Name[i],
                                    FatherName = damagepayeeregistertemp.FatherName.Count <= i ? string.Empty : damagepayeeregistertemp.FatherName[i],
                                    Date = damagepayeeregistertemp.Date.Count <= i ? DateTime.Now : damagepayeeregistertemp.Date[i],
                                    DamagePayeeRegisterTempId = damagepayeeregistertemp.Id,
                                    AtsgpadocumentPath = damagepayeeregistertemp.ATSGPA != null ?
                                                                damagepayeeregistertemp.ATSGPA.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(PhotoFilePathLayout, damagepayeeregistertemp.ATSGPA[i]) :
                                                                damagepayeeregistertemp.ATSGPAFilePath[i] != null || damagepayeeregistertemp.ATSGPAFilePath[i] != "" ?
                                                                damagepayeeregistertemp.ATSGPAFilePath[i] : string.Empty


                                });
                            }
                            result = await _damagepayeeregisterService.SaveAllotteTypeTemp(allottetypetemp);
                        }
                    }

                    //****** code for saving  Damage payment history *****

                    if (damagepayeeregistertemp.PaymntName != null &&
                          damagepayeeregistertemp.RecieptNo != null &&
                          damagepayeeregistertemp.PaymentMode != null &&
                          damagepayeeregistertemp.PaymentDate != null &&
                          damagepayeeregistertemp.Amount != null)
                    {

                        //damagepayeeregister.Reciept != null &&)
                        if (
                             damagepayeeregistertemp.PaymntName.Count > 0 &&
                             damagepayeeregistertemp.RecieptNo.Count > 0 &&
                             damagepayeeregistertemp.PaymentMode.Count > 0 &&
                             damagepayeeregistertemp.PaymentDate.Count > 0 &&
                             damagepayeeregistertemp.Amount.Count > 0
                             )

                        {
                            List<Damagepaymenthistorytemp> damagepaymenthistorytemp = new List<Damagepaymenthistorytemp>();
                            for (int i = 0; i < damagepayeeregistertemp.payeeName.Count; i++)
                            {
                                damagepaymenthistorytemp.Add(new Damagepaymenthistorytemp
                                {
                                    Name = damagepayeeregistertemp.PaymntName.Count <= i ? string.Empty : damagepayeeregistertemp.PaymntName[i],
                                    RecieptNo = damagepayeeregistertemp.RecieptNo.Count <= i ? string.Empty : damagepayeeregistertemp.RecieptNo[i],
                                    PaymentMode = damagepayeeregistertemp.PaymentMode.Count <= i ? string.Empty : damagepayeeregistertemp.PaymentMode[i],
                                    PaymentDate = damagepayeeregistertemp.PaymentDate.Count <= i ? DateTime.Now : damagepayeeregistertemp.PaymentDate[i],
                                    Amount = damagepayeeregistertemp.Amount.Count <= i ? 0 : damagepayeeregistertemp.Amount[i],
                                    RecieptDocumentPath = damagepayeeregistertemp.Reciept != null ?
                                                                damagepayeeregistertemp.Reciept.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(RecieptDocumentPathLayout, damagepayeeregistertemp.Reciept[i]) :
                                                                damagepayeeregistertemp.RecieptFilePath[i] != null || damagepayeeregistertemp.RecieptFilePath[i] != "" ?
                                                                damagepayeeregistertemp.RecieptFilePath[i] : string.Empty,
                                    DamagePayeeRegisterTempId = damagepayeeregistertemp.Id


                                   
                                  
                                    
                                   
                                   

                                     //RecieptDocumentPath = damagepayeeregistertemp.Reciept[i] == null ? "" : fileHelper.SaveFile(RecieptDocumentPathLayout, damagepayeeregistertemp.Reciept[i]),

                                   
                                });
                            }

                            result = await _damagepayeeregisterService.SavePaymentHistoryTemp(damagepaymenthistorytemp);

                        }
                    }
                    ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                    //return View(damagepayeeregistertemp);
                    var result1 = await _damagepayeeregisterService.GetAllDamagepayeeregisterTemp();
                    return View("Index", result1);
                }
                else
                {
                    ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                    return View(damagepayeeregistertemp);
                }

            }
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(damagepayeeregistertemp);
            }
        }

        public async Task<JsonResult> GetDetailspersonelinfotemp(int? Id)
        {
            Id = Id ?? 0;
            var data = await _damagepayeeregisterService.GetPersonalInfoTemp(Convert.ToInt32(Id));
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
            var data = await _damagepayeeregisterService.GetAllottetypeTemp(Convert.ToInt32(Id));
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
            var data = await _damagepayeeregisterService.GetPaymentHistoryTemp(Convert.ToInt32(Id));
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

        public async Task<IActionResult> Edit(int id, Damagepayeeregistertemp damagepayeeregistertemp)
        {

            await BindDropDown(damagepayeeregistertemp);
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

                if (damagepayeeregistertemp.PropertyPhoto != null)
                {
                    damagepayeeregistertemp.PropertyPhotoPath = fileHelper.SaveFile(PropertyPhotographLayout, damagepayeeregistertemp.PropertyPhoto);
                }
                if (damagepayeeregistertemp.ShowCauseNotice != null)
                {
                    damagepayeeregistertemp.ShowCauseNoticePath = fileHelper.SaveFile(ShowCauseNoticeDocument, damagepayeeregistertemp.ShowCauseNotice);
                }
                if (damagepayeeregistertemp.Fgform != null)
                {
                    damagepayeeregistertemp.FgformPath = fileHelper.SaveFile(FGFormDocument, damagepayeeregistertemp.Fgform);
                }
                if (damagepayeeregistertemp.DocumentForFile != null)
                {
                    damagepayeeregistertemp.DocumentForFilePath = fileHelper.SaveFile(BillDocument, damagepayeeregistertemp.DocumentForFile);
                }

                var result = await _damagepayeeregisterService.Update(id, damagepayeeregistertemp);
                if (result)
                {
                    //  FileHelper fileHelper = new FileHelper();

                    //****** code for saving  Damage payee personal info *****

                    if (damagepayeeregistertemp.payeeName != null &&
                      damagepayeeregistertemp.Gender != null &&
                      damagepayeeregistertemp.Address != null &&
                      damagepayeeregistertemp.MobileNo != null)
                    {
                        if (damagepayeeregistertemp.payeeName.Count > 0 &&
                    damagepayeeregistertemp.Gender.Count > 0 &&
                    damagepayeeregistertemp.Address.Count > 0 &&
                    damagepayeeregistertemp.MobileNo.Count > 0)

                        {
                            List<Damagepayeepersonelinfotemp> damagepayeepersonelinfotemp = new List<Damagepayeepersonelinfotemp>();
                            result = await _damagepayeeregisterService.DeletePayeePersonalInfoTemp(id);
                            for (int i = 0; i < damagepayeeregistertemp.payeeName.Count; i++)
                            {
                                damagepayeepersonelinfotemp.Add(new Damagepayeepersonelinfotemp
                                {
                                    Name = damagepayeeregistertemp.payeeName.Count <= i ? string.Empty : damagepayeeregistertemp.payeeName[i],
                                    FatherName = damagepayeeregistertemp.payeeFatherName.Count <= i ? string.Empty : damagepayeeregistertemp.payeeFatherName[i],
                                    Gender = damagepayeeregistertemp.Gender.Count <= i ? "1" : damagepayeeregistertemp.Gender[i],
                                    Address = damagepayeeregistertemp.Address.Count <= i ? string.Empty : damagepayeeregistertemp.Address[i],
                                    MobileNo = damagepayeeregistertemp.MobileNo.Count <= i ? string.Empty : damagepayeeregistertemp.MobileNo[i],
                                    EmailId = damagepayeeregistertemp.EmailId.Count <= i ? string.Empty : damagepayeeregistertemp.EmailId[i],
                                    DamagePayeeRegisterTempId = damagepayeeregistertemp.Id,
                                    AadharNo = damagepayeeregistertemp.AadharNo.Count <= i ? string.Empty : damagepayeeregistertemp.AadharNo[i],
                                    PanNo = damagepayeeregistertemp.PanNo.Count <= i ? string.Empty : damagepayeeregistertemp.PanNo[i],

                                    AadharNoFilePath = damagepayeeregistertemp.Aadhar != null ?
                                                                damagepayeeregistertemp.Aadhar.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(AadharNoDocument, damagepayeeregistertemp.Aadhar[i]) :
                                                                damagepayeeregistertemp.AadharNoFilePath[i] != null || damagepayeeregistertemp.AadharNoFilePath[i] != "" ?
                                                                damagepayeeregistertemp.AadharNoFilePath[i] : string.Empty,
                                    PanNoFilePath = damagepayeeregistertemp.Pan != null ?
                                                                damagepayeeregistertemp.Pan.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(PanNoDocument, damagepayeeregistertemp.Pan[i]) :
                                                                damagepayeeregistertemp.PanNoFilePath[i] != null || damagepayeeregistertemp.PanNoFilePath[i] != "" ?
                                                                damagepayeeregistertemp.PanNoFilePath[i] : string.Empty,
                                    PhotographPath = damagepayeeregistertemp.Photograph != null ?
                                                                damagepayeeregistertemp.Photograph.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(PhotographPersonelDocument, damagepayeeregistertemp.Photograph[i]) :
                                                                damagepayeeregistertemp.PhotographFilePath[i] != null || damagepayeeregistertemp.PhotographFilePath[i] != "" ?
                                                                damagepayeeregistertemp.PhotographFilePath[i] : string.Empty,
                                    SignaturePath = damagepayeeregistertemp.SignatureFile != null ?
                                                                damagepayeeregistertemp.SignatureFile.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(SignaturePersonelDocument, damagepayeeregistertemp.SignatureFile[i]) :
                                                                damagepayeeregistertemp.SignatureFilePath[i] != null || damagepayeeregistertemp.SignatureFilePath[i] != "" ?
                                                                damagepayeeregistertemp.SignatureFilePath[i] : string.Empty

                                     });

                              
                            }
                            foreach (var item in damagepayeepersonelinfotemp)
                            {
                                result = await _damagepayeeregisterService.SavePayeePersonalInfoTemp(item);
                            }
                        }
                    }

                    //****** code for saving  Allotte Type *****
                    if (damagepayeeregistertemp.Name != null &&
                     damagepayeeregistertemp.FatherName != null &&
                     damagepayeeregistertemp.Date != null)
                    {
                        if (
                         damagepayeeregistertemp.Name.Count > 0 &&
                         damagepayeeregistertemp.FatherName.Count > 0 &&
                         damagepayeeregistertemp.Date.Count > 0
                         )
                        {
                           
                            List<Allottetypetemp> allottetypetemp = new List<Allottetypetemp>();
                            result = await _damagepayeeregisterService.DeleteAllotteTypeTemp(id);
                            for (int i = 0; i < damagepayeeregistertemp.Name.Count; i++)
                            {
                                allottetypetemp.Add(new Allottetypetemp
                                {
                                    Name = damagepayeeregistertemp.Name.Count <= i ? string.Empty : damagepayeeregistertemp.Name[i],
                                    FatherName = damagepayeeregistertemp.FatherName.Count <= i ? string.Empty : damagepayeeregistertemp.FatherName[i],
                                    Date = damagepayeeregistertemp.Date.Count <= i ? DateTime.Now : damagepayeeregistertemp.Date[i],
                                    DamagePayeeRegisterTempId = damagepayeeregistertemp.Id,
                                    AtsgpadocumentPath = damagepayeeregistertemp.ATSGPA != null ?
                                                                damagepayeeregistertemp.ATSGPA.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(PhotoFilePathLayout, damagepayeeregistertemp.ATSGPA[i]) :
                                                                damagepayeeregistertemp.ATSGPAFilePath[i] != null || damagepayeeregistertemp.ATSGPAFilePath[i] != "" ?
                                                                damagepayeeregistertemp.ATSGPAFilePath[i] : string.Empty

                                });
                            }
                            result = await _damagepayeeregisterService.SaveAllotteTypeTemp(allottetypetemp);
                        }
                    }

                    //****** code for saving  Damage payment history *****

                    if (damagepayeeregistertemp.PaymntName != null &&
                          damagepayeeregistertemp.RecieptNo != null &&
                          damagepayeeregistertemp.PaymentMode != null &&
                          damagepayeeregistertemp.PaymentDate != null &&
                          damagepayeeregistertemp.Amount != null)
                    {

                        //damagepayeeregister.Reciept != null &&)
                        if (
                             damagepayeeregistertemp.PaymntName.Count > 0 &&
                             damagepayeeregistertemp.RecieptNo.Count > 0 &&
                             damagepayeeregistertemp.PaymentMode.Count > 0 &&
                             damagepayeeregistertemp.PaymentDate.Count > 0 &&
                             damagepayeeregistertemp.Amount.Count > 0
                             )

                        {
                            List<Damagepaymenthistorytemp> damagepaymenthistorytemp = new List<Damagepaymenthistorytemp>();
                            result = await _damagepayeeregisterService.DeletePaymentHistoryTemp(id);
                            for (int i = 0; i < damagepayeeregistertemp.payeeName.Count; i++)
                            {
                                damagepaymenthistorytemp.Add(new Damagepaymenthistorytemp
                                {
                                    Name = damagepayeeregistertemp.PaymntName.Count <= i ? string.Empty : damagepayeeregistertemp.PaymntName[i],
                                    RecieptNo = damagepayeeregistertemp.RecieptNo.Count <= i ? string.Empty : damagepayeeregistertemp.RecieptNo[i],
                                    PaymentMode = damagepayeeregistertemp.PaymentMode.Count <= i ? string.Empty : damagepayeeregistertemp.PaymentMode[i],
                                    PaymentDate = damagepayeeregistertemp.PaymentDate.Count <= i ? DateTime.Now : damagepayeeregistertemp.PaymentDate[i],
                                    Amount = damagepayeeregistertemp.Amount.Count <= i ? 0 : damagepayeeregistertemp.Amount[i],
                                    RecieptDocumentPath = damagepayeeregistertemp.Reciept != null ?
                                                                damagepayeeregistertemp.Reciept.Count <= i ? string.Empty :
                                                                fileHelper.SaveFile(RecieptDocumentPathLayout, damagepayeeregistertemp.Reciept[i]) :
                                                                damagepayeeregistertemp.RecieptFilePath[i] != null || damagepayeeregistertemp.RecieptFilePath[i] != "" ?
                                                                damagepayeeregistertemp.RecieptFilePath[i] : string.Empty,
                                    DamagePayeeRegisterTempId = damagepayeeregistertemp.Id

                  //  RecieptDocumentPath = damagepayeeregistertemp.Reciept[i] == null ? "" : fileHelper.SaveFile(RecieptDocumentPathLayout, damagepayeeregistertemp.Reciept[i]),

                                   
                                });
                            }

                            result = await _damagepayeeregisterService.SavePaymentHistoryTemp(damagepaymenthistorytemp);

                        }
                    }
                    if (result)
                    {
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        var result1 = await _damagepayeeregisterService.GetAllDamagepayeeregisterTemp();
                        return View("Index", result1);
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(damagepayeeregistertemp);
                    }
                
            }

            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(damagepayeeregistertemp);
            }

        } 

          
            else
            {
                ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                return View(damagepayeeregistertemp);
            }
           
        }
        //******************  download files **************************
        public async Task<IActionResult> DownloadPropertyPhoto(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregistertemp Data = await _damagepayeeregisterService.FetchSingleResult(Id);
            string filename = Data.PropertyPhotoPath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadShowCauseNotice(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregistertemp Data = await _damagepayeeregisterService.FetchSingleResult(Id);
            string filename = Data.ShowCauseNoticePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadFgform(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregistertemp Data = await _damagepayeeregisterService.FetchSingleResult(Id);
            string filename = Data.FgformPath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }
        public async Task<IActionResult> DownloadBillfile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregistertemp Data = await _damagepayeeregisterService.FetchSingleResult(Id);
            string filename = Data.DocumentForFilePath;
            return File(file.GetMemory(filename), file.GetContentType(filename), Path.GetFileName(filename));
        }


        //**********************  download repeater files****************************
        public async Task<FileResult> ViewPersonelInfoAadharFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfotemp Data = await _damagepayeeregisterService.GetPersonelInfoFilePath(Id);
            string path = Data.AadharNoFilePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<FileResult> ViewPersonelInfoPanFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfotemp Data = await _damagepayeeregisterService.GetPersonelInfoFilePath(Id);
            string path = Data.PanNoFilePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<FileResult> ViewPersonelInfoPhotoFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfotemp Data = await _damagepayeeregisterService.GetPersonelInfoFilePath(Id);
            string path = Data.PhotographPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<FileResult> ViewPersonelInfoSignautreFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfotemp Data = await _damagepayeeregisterService.GetPersonelInfoFilePath(Id);
            string path = Data.SignaturePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }

        
        public async Task<FileResult> ViewATSFile(int Id)
        {
            FileHelper file = new FileHelper();
            Allottetypetemp Data = await _damagepayeeregisterService.GetATSFilePath(Id);
            string path = Data.AtsgpadocumentPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }

        public async Task<FileResult> ViewReceiptFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepaymenthistorytemp Data = await _damagepayeeregisterService.GetReceiptFilePath(Id);
            string path = Data.RecieptDocumentPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
    }
}
