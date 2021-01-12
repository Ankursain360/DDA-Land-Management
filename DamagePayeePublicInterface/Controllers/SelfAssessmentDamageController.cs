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

namespace DamagePayeePublicInterface.Controllers
{
    public class SelfAssessmentDamageController : BaseController
    {
        private readonly ISelfAssessmentDamageService _selfAssessmentDamageService;
        public IConfiguration _configuration;
        public SelfAssessmentDamageController(ISelfAssessmentDamageService selfAssessmentDamageService, IConfiguration configuration)
        {
            _configuration = configuration;
            _selfAssessmentDamageService = selfAssessmentDamageService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<PartialViewResult> List([FromBody] DamagepayeeregistertempSearchDto model)
        {
            var result = await _selfAssessmentDamageService.GetPagedDamagepayeeregister(model);
            return PartialView("_List", result);
        }
        async Task BindDropDown(Damagepayeeregister damagepayeeregistertemp)
        {
            damagepayeeregistertemp.LocalityList = await _selfAssessmentDamageService.GetLocalityList();
            damagepayeeregistertemp.DistrictList = await _selfAssessmentDamageService.GetDistrictList();
        }
        public async Task<IActionResult> Create()
        {
            Damagepayeeregister damagepayeeregistertemp = new Damagepayeeregister();
            var Data = await _selfAssessmentDamageService.FetchSelfAssessmentUserId(SiteContext.UserId);
            var value = await _selfAssessmentDamageService.GetRebateValue();
            if (value == null)
                ViewBag.RebateValue = 0;
            else
                ViewBag.RebateValue = Math.Round((value.RebatePercentage), 2);
            if (Data != null)
            {
                ViewBag.MainDamagePayeeId = Data.Id;
                await BindDropDown(Data);
                return View(Data);
            }
            else
            {
                ViewBag.MainDamagePayeeId = 0;
                await BindDropDown(damagepayeeregistertemp);
                return View(damagepayeeregistertemp);
            }

        }
        [HttpPost]

        public async Task<IActionResult> Create(Damagepayeeregister damagepayeeregistertemp)
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
                damagepayeeregistertemp.UserId = SiteContext.UserId;
                if (damagepayeeregistertemp.Id == 0)
                {
                    damagepayeeregistertemp.CreatedBy = SiteContext.UserId;
                    var result = await _selfAssessmentDamageService.Create(damagepayeeregistertemp);
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
                                List<Damagepayeepersonelinfo> damagepayeepersonelinfotemp = new List<Damagepayeepersonelinfo>();
                                for (int i = 0; i < damagepayeeregistertemp.payeeName.Count; i++)
                                {
                                    damagepayeepersonelinfotemp.Add(new Damagepayeepersonelinfo
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
                                    result = await _selfAssessmentDamageService.SavePayeePersonalInfoTemp(item);
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
                                List<Allottetype> allottetypetemp = new List<Allottetype>();
                                for (int i = 0; i < damagepayeeregistertemp.Name.Count; i++)
                                {
                                    allottetypetemp.Add(new Allottetype
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
                                result = await _selfAssessmentDamageService.SaveAllotteTypeTemp(allottetypetemp);
                            }
                        }

                        //****** code for saving  Damage payment history *****

                        if (damagepayeeregistertemp.PaymntName != null &&
                              damagepayeeregistertemp.RecieptNo != null &&
                              damagepayeeregistertemp.PaymentMode != null &&
                              damagepayeeregistertemp.PaymentDate != null &&
                              damagepayeeregistertemp.Amount != null)
                        {
                            if (
                                 damagepayeeregistertemp.PaymntName.Count > 0 &&
                                 damagepayeeregistertemp.RecieptNo.Count > 0 &&
                                 damagepayeeregistertemp.PaymentMode.Count > 0 &&
                                 damagepayeeregistertemp.PaymentDate.Count > 0 &&
                                 damagepayeeregistertemp.Amount.Count > 0
                                 )

                            {
                                List<Damagepaymenthistory> damagepaymenthistorytemp = new List<Damagepaymenthistory>();
                                for (int i = 0; i < damagepayeeregistertemp.payeeName.Count; i++)
                                {
                                    damagepaymenthistorytemp.Add(new Damagepaymenthistory
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
                                    });
                                }

                                result = await _selfAssessmentDamageService.SavePaymentHistoryTemp(damagepaymenthistorytemp);

                            }
                        }
                        ViewBag.MainDamagePayeeId = damagepayeeregistertemp.Id;
                        ViewBag.Message = Alert.Show(Messages.AddRecordSuccess, "", AlertType.Success);
                        if (damagepayeeregistertemp.IsMutaionYes != 0)
                            return View(damagepayeeregistertemp);
                        else
                            return RedirectToAction("Index", "SubstitutionMutationDetails", new { id = damagepayeeregistertemp.Id });
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(damagepayeeregistertemp);
                    }
                }
                else
                {
                    damagepayeeregistertemp.ModifiedBy = SiteContext.UserId;
                    var result = await _selfAssessmentDamageService.Update(damagepayeeregistertemp);
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
                                List<Damagepayeepersonelinfo> damagepayeepersonelinfotemp = new List<Damagepayeepersonelinfo>();
                                result = await _selfAssessmentDamageService.DeletePayeePersonalInfoTemp(damagepayeeregistertemp.Id);
                                for (int i = 0; i < damagepayeeregistertemp.payeeName.Count; i++)
                                {
                                    damagepayeepersonelinfotemp.Add(new Damagepayeepersonelinfo
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
                                                                damagepayeeregistertemp.SignatureFilePath[i] : string.Empty,
                                    });
                                }
                                foreach (var item in damagepayeepersonelinfotemp)
                                {
                                    result = await _selfAssessmentDamageService.SavePayeePersonalInfoTemp(item);
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
                                List<Allottetype> allottetypetemp = new List<Allottetype>();
                                result = await _selfAssessmentDamageService.DeleteAllotteTypeTemp(damagepayeeregistertemp.Id);
                                for (int i = 0; i < damagepayeeregistertemp.Name.Count; i++)
                                {
                                    allottetypetemp.Add(new Allottetype
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
                                result = await _selfAssessmentDamageService.SaveAllotteTypeTemp(allottetypetemp);
                            }
                        }

                        //****** code for saving  Damage payment history *****

                        if (damagepayeeregistertemp.PaymntName != null &&
                              damagepayeeregistertemp.RecieptNo != null &&
                              damagepayeeregistertemp.PaymentMode != null &&
                              damagepayeeregistertemp.PaymentDate != null &&
                              damagepayeeregistertemp.Amount != null)
                        {
                            if (
                                 damagepayeeregistertemp.PaymntName.Count > 0 &&
                                 damagepayeeregistertemp.RecieptNo.Count > 0 &&
                                 damagepayeeregistertemp.PaymentMode.Count > 0 &&
                                 damagepayeeregistertemp.PaymentDate.Count > 0 &&
                                 damagepayeeregistertemp.Amount.Count > 0
                                 )

                            {
                                List<Damagepaymenthistory> damagepaymenthistorytemp = new List<Damagepaymenthistory>();
                                result = await _selfAssessmentDamageService.DeletePaymentHistoryTemp(damagepayeeregistertemp.Id);

                                for (int i = 0; i < damagepayeeregistertemp.PaymntName.Count; i++)
                                {
                                    damagepaymenthistorytemp.Add(new Damagepaymenthistory
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
                                    });
                                }

                                result = await _selfAssessmentDamageService.SavePaymentHistoryTemp(damagepaymenthistorytemp);

                            }
                        }
                        var paymentLink = "https://online.dda.org.in/onlinepmt/Forms/landspmt.aspx?FileNo=" + damagepayeeregistertemp.FileNo + "&Locality=" + damagepayeeregistertemp.LocalityId + "&Amount=" + damagepayeeregistertemp.TotalValueWithInterest + "&Interest=" + damagepayeeregistertemp.InterestDueAmountCompund;
                        ViewBag.MainDamagePayeeId = damagepayeeregistertemp.Id;
                        ViewBag.Message = Alert.Show(Messages.UpdateRecordSuccess, "", AlertType.Success);
                        if (damagepayeeregistertemp.IsMutaionYes == 1)
                            return View(damagepayeeregistertemp);
                        else if (damagepayeeregistertemp.IsMutaionYes == 2)
                            return Redirect(paymentLink);
                        else
                            return RedirectToAction("Index", "SubstitutionMutationDetails", new { id = damagepayeeregistertemp.Id });
                    }
                    else
                    {
                        ViewBag.Message = Alert.Show(Messages.Error, "", AlertType.Warning);
                        return View(damagepayeeregistertemp);
                    }
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
            var data = await _selfAssessmentDamageService.GetPersonalInfoTemp(Convert.ToInt32(Id));
            return Json(data.Select(x => new
            {
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
            var data = await _selfAssessmentDamageService.GetAllottetypeTemp(Convert.ToInt32(Id));
            return Json(data.Select(x => new
            {
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
            var data = await _selfAssessmentDamageService.GetPaymentHistoryTemp(Convert.ToInt32(Id));
            return Json(data.Select(x => new
            {
                x.Id,
                x.Name,
                x.RecieptNo,
                x.PaymentMode,
                PaymentDate = Convert.ToDateTime(x.PaymentDate).ToString("yyyy-MM-dd"),
                x.Amount,
                x.RecieptDocumentPath
            }));
        }

        public async Task<FileResult> ViewPersonelInfoAadharFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfo Data = await _selfAssessmentDamageService.GetPersonelInfoFilePath(Id);
            string path = Data.AadharNoFilePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<FileResult> ViewPersonelInfoPanFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfo Data = await _selfAssessmentDamageService.GetPersonelInfoFilePath(Id);
            string path = Data.PanNoFilePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<FileResult> ViewPersonelInfoPhotoFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfo Data = await _selfAssessmentDamageService.GetPersonelInfoFilePath(Id);
            string path = Data.PhotographPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<FileResult> ViewPersonelInfoSignautreFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeepersonelinfo Data = await _selfAssessmentDamageService.GetPersonelInfoFilePath(Id);
            string path = Data.SignaturePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<FileResult> ViewATSGPAFile(int Id)
        {
            FileHelper file = new FileHelper();
            Allottetype Data = await _selfAssessmentDamageService.GetAllotteTypeSingleResult(Id);
            string path = Data.AtsgpadocumentPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<FileResult> ViewRecieptFile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepaymenthistory Data = await _selfAssessmentDamageService.GetPaymentHistorySingleResult(Id);
            string path = Data.RecieptDocumentPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }

        //******************  download files **************************
        public async Task<IActionResult> DownloadPropertyPhoto(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregister Data = await _selfAssessmentDamageService.FetchSingleResult(Id);
            string path = Data.PropertyPhotoPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<IActionResult> DownloadShowCauseNotice(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregister Data = await _selfAssessmentDamageService.FetchSingleResult(Id);
            string path = Data.ShowCauseNoticePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<IActionResult> DownloadFgform(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregister Data = await _selfAssessmentDamageService.FetchSingleResult(Id);
            string path = Data.FgformPath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }
        public async Task<IActionResult> DownloadBillfile(int Id)
        {
            FileHelper file = new FileHelper();
            Damagepayeeregister Data = await _selfAssessmentDamageService.FetchSingleResult(Id);
            string path = Data.DocumentForFilePath;
            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, file.GetContentType(path));
        }


    }
}
